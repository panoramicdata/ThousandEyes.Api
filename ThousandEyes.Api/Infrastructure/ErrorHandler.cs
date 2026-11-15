using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Refit;
using ThousandEyes.Api.Exceptions;

namespace ThousandEyes.Api.Infrastructure;

/// <summary>
/// HTTP message handler that converts error responses to ThousandEyesApiExceptions
/// </summary>
internal sealed class ErrorHandler(ILogger? logger) : DelegatingHandler
{
	private readonly ILogger _logger = logger ?? NullLogger.Instance;

	/// <summary>
	/// Processes HTTP requests and converts any error responses to ThousandEyesApiExceptions
	/// </summary>
	/// <param name="request">The HTTP request message</param>
	/// <param name="cancellationToken">Cancellation token</param>
	/// <returns>The HTTP response message</returns>
	protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
	{
		try
		{
			var response = await base.SendAsync(request, cancellationToken);
			if (response.IsSuccessStatusCode)
			{
				return response;
			}

			var content = await response.Content.ReadAsStringAsync(cancellationToken);

			_logger.LogError("API request failed: {StatusCode} {ReasonPhrase} - {Content}",
				(int)response.StatusCode, response.ReasonPhrase, content);

			throw ThousandEyesExceptionFactory.Create(new ErrorSource(
				(int)response.StatusCode,
				response.ReasonPhrase ?? string.Empty,
				content,
				request));
		}
		catch (ApiException apiException)
		{
			_logger.LogError(apiException, "API exception occurred: {StatusCode} {ReasonPhrase}",
				apiException.StatusCode, apiException.ReasonPhrase);

			throw ThousandEyesExceptionFactory.Create(ErrorSource.FromApiException(apiException, request));
		}
		catch (Exception ex) when (ex is not ThousandEyesApiException)
		{
			_logger.LogError(ex, "Unexpected error occurred during HTTP request to {RequestUri}", request.RequestUri);
			throw;
		}
	}
}
