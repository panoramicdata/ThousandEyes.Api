using Refit;

namespace ThousandEyes.Api.Infrastructure;

/// <summary>
/// The raw facts of a failed HTTP call, from which an exception is built
/// </summary>
internal sealed record ErrorSource(
	int StatusCode,
	string? ReasonPhrase,
	string? Content,
	HttpRequestMessage Request,
	ApiException? Inner = null,
	int? RetryAfterSeconds = null)
{
	public static ErrorSource FromApiException(ApiException apiException, HttpRequestMessage request) => new(
		(int)apiException.StatusCode,
		apiException.ReasonPhrase,
		apiException.Content,
		request,
		apiException,
		ExtractRetryAfterSeconds(apiException));

	private static int? ExtractRetryAfterSeconds(ApiException apiException)
	{
		if (apiException.Headers is null || !apiException.Headers.TryGetValues("Retry-After", out var values))
		{
			return null;
		}

		return int.TryParse(values.FirstOrDefault(), out var seconds) ? seconds : null;
	}
}
