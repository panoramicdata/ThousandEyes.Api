using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using System.Net;
using System.Net.Sockets;

namespace ThousandEyes.Api.Infrastructure;

/// <summary>
/// HTTP message handler that implements retry logic with exponential backoff
/// </summary>
internal sealed class RetryHandler(
	int maxRetryAttempts,
	TimeSpan retryDelay,
	bool useExponentialBackoff,
	TimeSpan maxRetryDelay,
	ILogger? logger) : DelegatingHandler
{
	private readonly int _maxRetryAttempts = maxRetryAttempts;
	private readonly TimeSpan _retryDelay = retryDelay;
	private readonly bool _useExponentialBackoff = useExponentialBackoff;
	private readonly TimeSpan _maxRetryDelay = maxRetryDelay;
	private readonly ILogger _logger = logger ?? NullLogger.Instance;

	private static readonly HttpStatusCode[] RetryableStatusCodes =
	[
		HttpStatusCode.RequestTimeout,
		HttpStatusCode.TooManyRequests,
		HttpStatusCode.InternalServerError,
		HttpStatusCode.BadGateway,
		HttpStatusCode.ServiceUnavailable,
		HttpStatusCode.GatewayTimeout
	];

	protected override async Task<HttpResponseMessage> SendAsync(
		HttpRequestMessage request,
		CancellationToken cancellationToken)
	{
		Exception? lastException = null;

		for (var attempt = 0; attempt <= _maxRetryAttempts; attempt++)
		{
			try
			{
				var response = await base.SendAsync(request, cancellationToken).ConfigureAwait(false);
				var result = HandleResponse(response, request, attempt);
				if (result is not null)
				{
					return result;
				}
			}
			catch (Exception ex) when (IsRetryableException(ex))
			{
				lastException = ex;
				LogFailedAttempt(ex, request, attempt);
				if (attempt == _maxRetryAttempts)
				{
					throw;
				}
			}

			if (attempt < _maxRetryAttempts)
			{
				await Task.Delay(CalculateDelay(attempt), cancellationToken).ConfigureAwait(false);
			}
		}

		throw lastException ?? new HttpRequestException($"Request failed after {_maxRetryAttempts + 1} attempts");
	}

	private HttpResponseMessage? HandleResponse(HttpResponseMessage response, HttpRequestMessage request, int attempt)
	{
		if (response.IsSuccessStatusCode || !IsRetryableStatusCode(response.StatusCode))
		{
			if (attempt > 0)
			{
				_logger.LogInformation(
					"HTTP request succeeded on attempt {Attempt} for {Method} {Uri}",
					attempt + 1,
					request.Method,
					request.RequestUri);
			}

			return response;
		}

		response.Dispose();

		if (attempt == _maxRetryAttempts)
		{
			_logger.LogWarning(
				"HTTP request failed after {MaxAttempts} attempts for {Method} {Uri} with status {StatusCode}",
				_maxRetryAttempts + 1,
				request.Method,
				request.RequestUri,
				response.StatusCode);

			return new HttpResponseMessage(response.StatusCode)
			{
				RequestMessage = request,
				ReasonPhrase = $"Failed after {_maxRetryAttempts + 1} attempts"
			};
		}

		_logger.LogWarning(
			"HTTP request failed on attempt {Attempt}, retrying in {Delay}ms for {Method} {Uri} (Status: {StatusCode})",
			attempt + 1,
			CalculateDelay(attempt).TotalMilliseconds,
			request.Method,
			request.RequestUri,
			response.StatusCode);

		return null;
	}

	private void LogFailedAttempt(Exception ex, HttpRequestMessage request, int attempt)
	{
		if (attempt == _maxRetryAttempts)
		{
			_logger.LogError(ex,
				"HTTP request failed after {MaxAttempts} attempts for {Method} {Uri}",
				_maxRetryAttempts + 1,
				request.Method,
				request.RequestUri);
			return;
		}

		_logger.LogWarning(ex,
			"HTTP request failed on attempt {Attempt}, retrying in {Delay}ms for {Method} {Uri}",
			attempt + 1,
			CalculateDelay(attempt).TotalMilliseconds,
			request.Method,
			request.RequestUri);
	}

	private TimeSpan CalculateDelay(int attemptNumber)
	{
		if (!_useExponentialBackoff)
		{
			return _retryDelay;
		}

		var exponentialDelay = TimeSpan.FromMilliseconds(_retryDelay.TotalMilliseconds * Math.Pow(2, attemptNumber));
		return exponentialDelay > _maxRetryDelay ? _maxRetryDelay : exponentialDelay;
	}

	private static bool IsRetryableStatusCode(HttpStatusCode statusCode)
		=> RetryableStatusCodes.Contains(statusCode);

	private static bool IsRetryableException(Exception ex)
		=> ex is HttpRequestException or TaskCanceledException or SocketException;
}
