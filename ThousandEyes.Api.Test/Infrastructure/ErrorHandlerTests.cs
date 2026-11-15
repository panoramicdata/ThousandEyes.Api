using System.Net;
using AwesomeAssertions;
using Refit;
using ThousandEyes.Api.Exceptions;
using ThousandEyes.Api.Infrastructure;

namespace ThousandEyes.Api.Test.Infrastructure;

public class ErrorHandlerTests
{
	private static async Task<T> CaptureAsync<T>(StubHandler inner, string url = "https://api.thousandeyes.com/v7/users/123")
		where T : ThousandEyesApiException
	{
		using var handler = new ErrorHandler(null);
		var act = () => handler.GetAsync(inner, url);
		return (await act.Should().ThrowAsync<T>()).Which;
	}

	[Theory]
	[InlineData(400, typeof(ThousandEyesBadRequestException), "bad")]
	[InlineData(401, typeof(ThousandEyesAuthenticationException), "Authentication failed: bad")]
	[InlineData(403, typeof(ThousandEyesAuthorizationException), "Authorization failed: bad")]
	[InlineData(404, typeof(ThousandEyesNotFoundException), "Resource not found: bad")]
	[InlineData(429, typeof(ThousandEyesRateLimitException), "Rate limit exceeded: bad")]
	[InlineData(500, typeof(ThousandEyesServerException), "Server error: bad")]
	[InlineData(503, typeof(ThousandEyesServerException), "Server error: bad")]
	[InlineData(418, typeof(ThousandEyesApiException), "API error: bad")]
	public async Task ErrorStatus_IsMappedToTheMatchingException(int status, Type expected, string message)
	{
		var exception = await CaptureAsync<ThousandEyesApiException>(
			StubHandler.Always((HttpStatusCode)status, """{"message":"bad","error":"E1"}"""));

		_ = exception.GetType().Should().Be(expected);
		_ = exception.Message.Should().Be(message);
		if (status != 429)
		{
			_ = exception.RequestMethod.Should().Be("GET");
			_ = exception.RequestUrl.Should().Be("https://api.thousandeyes.com/v7/users/123");
			_ = exception.StatusCode.Should().Be(status);
			_ = exception.ErrorCode.Should().Be("E1");
		}
	}

	[Fact]
	public async Task BadRequest_ExposesValidationErrorsAndDetails()
	{
		var exception = await CaptureAsync<ThousandEyesBadRequestException>(
			StubHandler.Always(HttpStatusCode.BadRequest, """{"errors":["a",1,"b"],"count":2,"ratio":1.5,"flag":true,"off":false,"none":null,"nested":{"x":1}}"""));

		_ = exception.ValidationErrors.Should().Equal("a", "b");
		_ = exception.Details.Should().ContainKey("count").WhoseValue.Should().Be(2);
		_ = exception.Details!["ratio"].Should().Be(1.5);
		_ = exception.Details["flag"].Should().Be(true);
		_ = exception.Details["off"].Should().Be(false);
		_ = exception.Details["none"].Should().BeNull();
		_ = exception.Details["nested"].Should().Be("""{"x":1}""");
	}

	[Theory]
	[InlineData("""{"errors":"not-an-array"}""")]
	[InlineData("""{"message":42}""")]
	public async Task UnexpectedShapes_DoNotThrowWhileParsing(string body)
	{
		var exception = await CaptureAsync<ThousandEyesBadRequestException>(StubHandler.Always(HttpStatusCode.BadRequest, body));

		_ = exception.Message.Should().NotBeNull();
	}

	[Theory]
	[InlineData("not json")]
	[InlineData("[1,2]")]
	public async Task NonObjectBody_IsKeptAsRawContent(string body)
	{
		var exception = await CaptureAsync<ThousandEyesApiException>(StubHandler.Always(HttpStatusCode.BadRequest, body));

		_ = exception.Details.Should().ContainKey("rawContent").WhoseValue.Should().Be(body);
	}

	[Fact]
	public async Task EmptyBody_FallsBackToReasonPhrase()
	{
		var exception = await CaptureAsync<ThousandEyesApiException>(
			new StubHandler((_, _) => StubHandler.Response(HttpStatusCode.BadRequest, reason: "Nope")));

		_ = exception.Message.Should().Be("Nope");
		_ = exception.Details.Should().BeNull();
	}

	[Theory]
	[InlineData("https://api.thousandeyes.com/v7/users/123", "users", 123)]
	[InlineData("https://api.thousandeyes.com/v7/tests/abc/", "tests", "abc")]
	public async Task NotFound_ReportsResourceTypeAndId(string url, string type, object id)
	{
		var exception = await CaptureAsync<ThousandEyesNotFoundException>(StubHandler.Always(HttpStatusCode.NotFound), url);

		_ = exception.ResourceType.Should().Be(type);
		_ = exception.ResourceId.Should().Be(id);
	}

	[Theory]
	[InlineData("https://api.thousandeyes.com/other/users/123")]
	[InlineData("https://api.thousandeyes.com/v7/")]
	[InlineData("https://api.thousandeyes.com/v7/users")]
	public async Task NotFound_WithoutIdentifiableResource_LeavesIdEmpty(string url)
	{
		var exception = await CaptureAsync<ThousandEyesNotFoundException>(StubHandler.Always(HttpStatusCode.NotFound), url);

		_ = exception.ResourceId.Should().BeNull();
	}

	[Fact]
	public async Task Success_IsReturnedUntouched()
	{
		using var handler = new ErrorHandler(null);

		using var response = await handler.GetAsync(StubHandler.Always(HttpStatusCode.OK, "{}"));

		_ = response.StatusCode.Should().Be(HttpStatusCode.OK);
	}

	[Fact]
	public async Task OtherExceptions_PassThroughUnwrapped()
	{
		using var handler = new ErrorHandler(null);
		var inner = new StubHandler((_, _) => throw new HttpRequestException("boom"));

		var act = () => handler.GetAsync(inner);

		_ = (await act.Should().ThrowAsync<HttpRequestException>()).Which.Message.Should().Be("boom");
	}

	[Theory]
	[InlineData(HttpStatusCode.TooManyRequests, "7", typeof(ThousandEyesRateLimitException))]
	[InlineData(HttpStatusCode.TooManyRequests, "soon", typeof(ThousandEyesRateLimitException))]
	[InlineData(HttpStatusCode.NotFound, null, typeof(ThousandEyesNotFoundException))]
	public async Task RefitApiException_IsConvertedKeepingTheCause(HttpStatusCode status, string? retryAfter, Type expected)
	{
		var apiException = await CreateApiExceptionAsync(status, retryAfter);
		var inner = new StubHandler((_, _) => throw apiException);

		var exception = await CaptureAsync<ThousandEyesApiException>(inner);

		_ = exception.GetType().Should().Be(expected);
		if (exception is not ThousandEyesRateLimitException)
		{
			_ = exception.InnerException.Should().BeSameAs(apiException);
		}

		if (exception is ThousandEyesRateLimitException rateLimit)
		{
			_ = rateLimit.RetryAfterSeconds.Should().Be(int.TryParse(retryAfter, out var seconds) ? seconds : null);
		}
	}

	private static async Task<ApiException> CreateApiExceptionAsync(HttpStatusCode status, string? retryAfter)
	{
		using var request = new HttpRequestMessage(HttpMethod.Get, "https://api.thousandeyes.com/v7/users/9");
		using var response = StubHandler.Response(status, """{"message":"m"}""");
		if (retryAfter is not null)
		{
			response.Headers.TryAddWithoutValidation("Retry-After", retryAfter);
		}

		return await ApiException.Create(request, HttpMethod.Get, response, new RefitSettings());
	}
}
