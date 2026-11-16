using ThousandEyes.Api.Exceptions;

namespace ThousandEyes.Api.Infrastructure;

/// <summary>
/// Maps a failed HTTP call to the matching ThousandEyesApiException
/// </summary>
internal static class ThousandEyesExceptionFactory
{
	public static ThousandEyesApiException Create(ErrorSource source)
	{
		var parsed = ErrorContentParser.Parse(source.Content);
		var code = source.StatusCode;
		var message = parsed.Message ?? source.ReasonPhrase ?? $"API request failed with status {code}";
		var url = source.Request.RequestUri?.ToString();
		var method = source.Request.Method.Method;
		var inner = source.Inner;

		return code switch
		{
			400 => new ThousandEyesBadRequestException(message, parsed.ValidationErrors, code, parsed.Code, parsed.Details, url, method, inner),
			401 => new ThousandEyesAuthenticationException($"Authentication failed: {message}", code, parsed.Code, parsed.Details, url, method, inner),
			403 => new ThousandEyesAuthorizationException($"Authorization failed: {message}", code, parsed.Code, parsed.Details, url, method, inner),
			404 => CreateNotFound($"Resource not found: {message}", code, parsed, url, method, inner),
			429 => new ThousandEyesRateLimitException($"Rate limit exceeded: {message}", source.RetryAfterSeconds),
			>= 500 => new ThousandEyesServerException($"Server error: {message}", code, parsed.Code, parsed.Details, url, method, inner),
			_ => new ThousandEyesApiException($"API error: {message}", code, parsed.Code, parsed.Details, url, method, inner)
		};
	}

	private static ThousandEyesNotFoundException CreateNotFound(
		string message,
		int code,
		ParsedError parsed,
		string? url,
		string method,
		Exception? inner) => new(
			message,
			ExtractResourceType(url),
			ExtractResourceId(url),
			new ThousandEyesApiErrorContext
			{
				StatusCode = code,
				ErrorCode = parsed.Code,
				Details = parsed.Details,
				RequestUrl = url,
				RequestMethod = method,
				InnerException = inner
			});
	private static string? ExtractResourceType(string? url) =>
		SegmentAfterVersion(url, 1)?.TrimEnd('/');

	private static object? ExtractResourceId(string? url)
	{
		var idSegment = SegmentAfterVersion(url, 2)?.TrimEnd('/');
		if (idSegment is null)
		{
			return null;
		}

		return int.TryParse(idSegment, out var intId) ? intId : idSegment;
	}

	private static string? SegmentAfterVersion(string? url, int offset)
	{
		if (string.IsNullOrEmpty(url))
		{
			return null;
		}

		var segments = new Uri(url).Segments;
		var version = Array.FindIndex(segments, s => s.Equals("v7/", StringComparison.OrdinalIgnoreCase));
		return version >= 0 && version + offset < segments.Length ? segments[version + offset] : null;
	}
}
