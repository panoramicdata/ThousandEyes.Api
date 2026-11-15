using System.Net;
using AwesomeAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using ThousandEyes.Api.Infrastructure;

namespace ThousandEyes.Api.Test.Infrastructure;

public class RetryAndLoggingHandlerTests
{
	private static RetryHandler Retry(int attempts, bool exponential = false, ILogger? logger = null) =>
		new(attempts, TimeSpan.Zero, exponential, TimeSpan.FromMilliseconds(5), logger);

	[Fact]
	public async Task Retry_RecoversAfterRetryableStatuses()
	{
		var inner = new StubHandler((_, call) => StubHandler.Response(call < 3 ? HttpStatusCode.ServiceUnavailable : HttpStatusCode.OK));

		using var response = await Retry(3).GetAsync(inner);

		_ = response.StatusCode.Should().Be(HttpStatusCode.OK);
		_ = inner.Calls.Should().Be(3);
	}

	[Fact]
	public async Task Retry_GivesUpWithAFailureResponseAfterTheLastAttempt()
	{
		var inner = StubHandler.Always(HttpStatusCode.BadGateway);

		using var response = await Retry(2).GetAsync(inner);

		_ = response.StatusCode.Should().Be(HttpStatusCode.BadGateway);
		_ = response.ReasonPhrase.Should().Be("Failed after 3 attempts");
		_ = inner.Calls.Should().Be(3);
	}

	[Fact]
	public async Task Retry_DoesNotRetryNonRetryableStatuses()
	{
		var inner = StubHandler.Always(HttpStatusCode.BadRequest);

		using var response = await Retry(3).GetAsync(inner);

		_ = response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
		_ = inner.Calls.Should().Be(1);
	}

	[Fact]
	public async Task Retry_RetriesRetryableExceptions()
	{
		var inner = new StubHandler((_, call) => call == 1 ? throw new HttpRequestException("flaky") : StubHandler.Response(HttpStatusCode.OK));

		using var response = await Retry(2, exponential: true).GetAsync(inner);

		_ = response.StatusCode.Should().Be(HttpStatusCode.OK);
		_ = inner.Calls.Should().Be(2);
	}

	[Fact]
	public async Task Retry_RethrowsTheLastExceptionWhenAttemptsAreExhausted()
	{
		var inner = new StubHandler((_, _) => throw new HttpRequestException("down"));

		var act = () => Retry(1, exponential: true).GetAsync(inner);

		_ = await act.Should().ThrowAsync<HttpRequestException>();
		_ = inner.Calls.Should().Be(2);
	}

	[Fact]
	public async Task Retry_DoesNotRetryOtherExceptions()
	{
		var inner = new StubHandler((_, _) => throw new InvalidOperationException("logic"));

		var act = () => Retry(3).GetAsync(inner);

		_ = await act.Should().ThrowAsync<InvalidOperationException>();
		_ = inner.Calls.Should().Be(1);
	}

	[Fact]
	public async Task Retry_LogsEachStageWhenALoggerIsSupplied()
	{
		var logger = new Mock<ILogger>();
		var inner = new StubHandler((_, call) => call == 1 ? throw new HttpRequestException("x") : StubHandler.Response(HttpStatusCode.OK));

		using var response = await Retry(2, logger: logger.Object).GetAsync(inner);

		_ = LoggedLevels(logger).Should().Equal(LogLevel.Warning, LogLevel.Information);
	}

	[Theory]
	[InlineData(true, true, HttpStatusCode.OK, new[] { LogLevel.Information, LogLevel.Information })]
	[InlineData(true, true, HttpStatusCode.NotFound, new[] { LogLevel.Information, LogLevel.Warning, LogLevel.Debug })]
	[InlineData(false, false, HttpStatusCode.OK, new LogLevel[0])]
	public async Task Logging_ReportsRequestsAndResponsesAccordingToTheOptions(bool requests, bool responses, HttpStatusCode status, LogLevel[] expected)
	{
		var logger = new Mock<ILogger>();
		using var handler = new LoggingHandler(logger.Object, requests, responses);

		using var response = await handler.GetAsync(StubHandler.Always(status));

		_ = LoggedLevels(logger).Should().Equal(expected);
	}

	[Fact]
	public async Task Logging_ReportsFailuresAndRethrows()
	{
		var logger = new Mock<ILogger>();
		using var handler = new LoggingHandler(logger.Object, logRequests: false, logResponses: true);
		var inner = new StubHandler((_, _) => throw new HttpRequestException("down"));

		var act = () => handler.GetAsync(inner);

		_ = await act.Should().ThrowAsync<HttpRequestException>();
		_ = LoggedLevels(logger).Should().Equal(LogLevel.Error);
	}

	[Fact]
	public async Task Logging_AddsRequestHeadersAtDebugWhenThereIsContent()
	{
		var logger = new Mock<ILogger>();
		using var handler = new LoggingHandler(logger.Object, logRequests: true, logResponses: false);
		handler.InnerHandler = StubHandler.Always(HttpStatusCode.OK);
		using var invoker = new HttpMessageInvoker(handler, disposeHandler: false);
		using var request = new HttpRequestMessage(HttpMethod.Post, "https://api.thousandeyes.com/v7/users")
		{
			Content = new StringContent("{}")
		};

		using var response = await invoker.SendAsync(request, TestContext.Current.CancellationToken);

		_ = LoggedLevels(logger).Should().Equal(LogLevel.Information, LogLevel.Debug);
	}

	private static List<LogLevel> LoggedLevels(Mock<ILogger> logger) =>
		[.. logger.Invocations.Where(i => i.Method.Name == nameof(ILogger.Log)).Select(i => (LogLevel)i.Arguments[0]!)];
}
