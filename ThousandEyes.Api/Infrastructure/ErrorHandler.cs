using ThousandEyes.Api.Exceptions;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Refit;
using System.Text.Json;

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
		HttpResponseMessage? response = null;
		try
		{
			response = await base.SendAsync(request, cancellationToken);

			// Check if the response indicates an error
			if (!response.IsSuccessStatusCode)
			{
				// Read the content before creating the exception
				var content = await response.Content.ReadAsStringAsync(cancellationToken);

				_logger.LogError("API request failed: {StatusCode} {ReasonPhrase} - {Content}",
					(int)response.StatusCode, response.ReasonPhrase, content);

				// Create and throw the appropriate ThousandEyesApiException
				var exception = CreateThousandEyesApiException(
					(int)response.StatusCode,
					response.ReasonPhrase ?? string.Empty,
					content,
					request);

				throw exception;
			}

			return response;
		}
		catch (ApiException apiException)
		{
			// This catch block handles ApiExceptions that might be thrown by Refit
			// (though we're trying to prevent them by checking status codes above)
			_logger.LogError(apiException, "API exception occurred: {StatusCode} {ReasonPhrase}",
				apiException.StatusCode, apiException.ReasonPhrase);

			var thousandEyesException = ConvertApiExceptionToThousandEyesApiException(apiException, request);
			throw thousandEyesException;
		}
		catch (ThousandEyesApiException)
		{
			// Re-throw ThousandEyesApiExceptions without wrapping
			throw;
		}
		catch (Exception ex)
		{
			_logger.LogError(ex, "Unexpected error occurred during HTTP request to {RequestUri}", request.RequestUri);
			throw;
		}
	}

	/// <summary>
	/// Creates the appropriate ThousandEyesApiException from HTTP response data
	/// </summary>
	/// <param name="statusCode">The HTTP status code</param>
	/// <param name="reasonPhrase">The HTTP reason phrase</param>
	/// <param name="content">The response content</param>
	/// <param name="request">The original HTTP request</param>
	/// <returns>The appropriate ThousandEyesApiException</returns>
	private static ThousandEyesApiException CreateThousandEyesApiException(
		int statusCode,
		string reasonPhrase,
		string content,
		HttpRequestMessage request)
	{
		var requestUrl = request.RequestUri?.ToString();
		var requestMethod = request.Method.Method;

		// Try to parse error details from response content
		Dictionary<string, object?>? details = null;
		IReadOnlyList<string>? validationErrors = null;
		string? errorMessage = null;
		string? errorCode = null;

		if (!string.IsNullOrEmpty(content))
		{
			try
			{
				var jsonDoc = JsonDocument.Parse(content);
				details = ExtractErrorDetails(jsonDoc.RootElement);

				// Extract the error message from JSON response (preferred over ReasonPhrase)
				if (jsonDoc.RootElement.TryGetProperty("message", out var messageElement))
				{
					errorMessage = messageElement.GetString();
				}

				// Extract error code if present
				if (jsonDoc.RootElement.TryGetProperty("error", out var errorElement))
				{
					errorCode = errorElement.GetString();
				}

				// Extract validation errors if present
				if (jsonDoc.RootElement.TryGetProperty("errors", out var errorsElement))
				{
					var errorsList = new List<string>();
					if (errorsElement.ValueKind == JsonValueKind.Array)
					{
						foreach (var error in errorsElement.EnumerateArray())
						{
							if (error.ValueKind == JsonValueKind.String)
							{
								errorsList.Add(error.GetString() ?? string.Empty);
							}
						}
					}

					validationErrors = errorsList.AsReadOnly();
				}
			}
			catch (JsonException)
			{
				// If we can't parse the JSON, just use the raw content
				details = new Dictionary<string, object?> { ["rawContent"] = content };
			}
		}

		// Use the API error message if available, otherwise fall back to ReasonPhrase
		var message = errorMessage ?? reasonPhrase ?? $"API request failed with status {statusCode}";

		// Map status codes to specific exception types
		return statusCode switch
		{
			400 => new ThousandEyesBadRequestException(
				message: message,
				validationErrors: validationErrors,
				statusCode: statusCode,
				errorCode: errorCode,
				details: details,
				requestUrl: requestUrl,
				requestMethod: requestMethod,
				innerException: null),

			401 => new ThousandEyesAuthenticationException(
				message: $"Authentication failed: {message}",
				statusCode: statusCode,
				errorCode: errorCode,
				details: details,
				requestUrl: requestUrl,
				requestMethod: requestMethod,
				innerException: null),

			403 => new ThousandEyesAuthorizationException(
				message: $"Authorization failed: {message}",
				statusCode: statusCode,
				errorCode: errorCode,
				details: details,
				requestUrl: requestUrl,
				requestMethod: requestMethod,
				innerException: null),

			404 => new ThousandEyesNotFoundException(
				message: $"Resource not found: {message}",
				resourceType: ExtractResourceTypeFromUrl(requestUrl),
				resourceId: ExtractResourceIdFromUrl(requestUrl),
				statusCode: statusCode,
				errorCode: errorCode,
				details: details,
				requestUrl: requestUrl,
				requestMethod: requestMethod,
				innerException: null),

			429 => new ThousandEyesRateLimitException(
				message: $"Rate limit exceeded: {message}",
				retryAfterSeconds: null), // Could extract from headers if available

			>= 500 => new ThousandEyesServerException(
				message: $"Server error: {message}",
				statusCode: statusCode,
				errorCode: errorCode,
				details: details,
				requestUrl: requestUrl,
				requestMethod: requestMethod,
				innerException: null),

			_ => new ThousandEyesApiException(
				message: $"API error: {message}",
				statusCode: statusCode,
				errorCode: errorCode,
				details: details,
				requestUrl: requestUrl,
				requestMethod: requestMethod,
				innerException: null)
		};
	}

	/// <summary>
	/// Converts a Refit ApiException to the appropriate ThousandEyesApiException type
	/// </summary>
	/// <param name="apiException">The Refit API exception</param>
	/// <param name="request">The original HTTP request</param>
	/// <returns>The appropriate ThousandEyesApiException</returns>
	private static ThousandEyesApiException ConvertApiExceptionToThousandEyesApiException(ApiException apiException, HttpRequestMessage request)
	{
		var statusCode = (int)apiException.StatusCode;
		var requestUrl = request.RequestUri?.ToString();
		var requestMethod = request.Method.Method;

		// Try to parse error details from response content
		Dictionary<string, object?>? details = null;
		IReadOnlyList<string>? validationErrors = null;
		string? errorMessage = null;
		string? errorCode = null;

		if (!string.IsNullOrEmpty(apiException.Content))
		{
			try
			{
				var jsonDoc = JsonDocument.Parse(apiException.Content);
				details = ExtractErrorDetails(jsonDoc.RootElement);

				// Extract the error message from JSON response (preferred over ReasonPhrase)
				if (jsonDoc.RootElement.TryGetProperty("message", out var messageElement))
				{
					errorMessage = messageElement.GetString();
				}

				// Extract error code if present
				if (jsonDoc.RootElement.TryGetProperty("error", out var errorElement))
				{
					errorCode = errorElement.GetString();
				}

				// Extract validation errors if present
				if (jsonDoc.RootElement.TryGetProperty("errors", out var errorsElement))
				{
					var errorsList = new List<string>();
					if (errorsElement.ValueKind == JsonValueKind.Array)
					{
						foreach (var error in errorsElement.EnumerateArray())
						{
							if (error.ValueKind == JsonValueKind.String)
							{
								errorsList.Add(error.GetString() ?? string.Empty);
							}
						}
					}

					validationErrors = errorsList.AsReadOnly();
				}
			}
			catch (JsonException)
			{
				// If we can't parse the JSON, just use the raw content
				details = new Dictionary<string, object?> { ["rawContent"] = apiException.Content };
			}
		}

		// Use the API error message if available, otherwise fall back to ReasonPhrase
		var message = errorMessage ?? apiException.ReasonPhrase ?? $"API request failed with status {statusCode}";

		// Map status codes to specific exception types
		return statusCode switch
		{
			400 => new ThousandEyesBadRequestException(
				message: message,
				validationErrors: validationErrors,
				statusCode: statusCode,
				errorCode: errorCode,
				details: details,
				requestUrl: requestUrl,
				requestMethod: requestMethod,
				innerException: apiException),

			401 => new ThousandEyesAuthenticationException(
				message: $"Authentication failed: {message}",
				statusCode: statusCode,
				errorCode: errorCode,
				details: details,
				requestUrl: requestUrl,
				requestMethod: requestMethod,
				innerException: apiException),

			403 => new ThousandEyesAuthorizationException(
				message: $"Authorization failed: {message}",
				statusCode: statusCode,
				errorCode: errorCode,
				details: details,
				requestUrl: requestUrl,
				requestMethod: requestMethod,
				innerException: apiException),

			404 => new ThousandEyesNotFoundException(
				message: $"Resource not found: {message}",
				resourceType: ExtractResourceTypeFromUrl(requestUrl),
				resourceId: ExtractResourceIdFromUrl(requestUrl),
				statusCode: statusCode,
				errorCode: errorCode,
				details: details,
				requestUrl: requestUrl,
				requestMethod: requestMethod,
				innerException: apiException),

			429 => new ThousandEyesRateLimitException(
				message: $"Rate limit exceeded: {message}",
				retryAfterSeconds: ExtractRetryAfterSeconds(apiException)),

			>= 500 => new ThousandEyesServerException(
				message: $"Server error: {message}",
				statusCode: statusCode,
				errorCode: errorCode,
				details: details,
				requestUrl: requestUrl,
				requestMethod: requestMethod,
				innerException: apiException),

			_ => new ThousandEyesApiException(
				message: $"API error: {message}",
				statusCode: statusCode,
				errorCode: errorCode,
				details: details,
				requestUrl: requestUrl,
				requestMethod: requestMethod,
				innerException: apiException)
		};
	}

	/// <summary>
	/// Extracts error details from JSON response
	/// </summary>
	/// <param name="element">The JSON element to extract from</param>
	/// <returns>Dictionary of error details</returns>
	private static Dictionary<string, object?> ExtractErrorDetails(JsonElement element)
	{
		var details = new Dictionary<string, object?>();

		foreach (var property in element.EnumerateObject())
		{
			details[property.Name] = property.Value.ValueKind switch
			{
				JsonValueKind.String => property.Value.GetString(),
				JsonValueKind.Number => property.Value.TryGetInt32(out var intVal) ? intVal : property.Value.GetDouble(),
				JsonValueKind.True => true,
				JsonValueKind.False => false,
				JsonValueKind.Null => null,
				_ => property.Value.GetRawText()
			};
		}

		return details;
	}

	/// <summary>
	/// Extracts resource type from URL (e.g., "account-groups", "users", "roles")
	/// </summary>
	/// <param name="url">The request URL</param>
	/// <returns>The resource type or null if not found</returns>
	private static string? ExtractResourceTypeFromUrl(string? url)
	{
		if (string.IsNullOrEmpty(url))
		{
			return null;
		}

		var uri = new Uri(url);
		var segments = uri.Segments;

		// Look for /v7/{resourceType} pattern for ThousandEyes API
		for (var i = 0; i < segments.Length - 1; i++)
		{
			if (segments[i].Equals("v7/", StringComparison.OrdinalIgnoreCase))
			{
				var resourceSegment = segments[i + 1].TrimEnd('/');
				return resourceSegment;
			}
		}

		return null;
	}

	/// <summary>
	/// Extracts resource ID from URL (e.g., the ID in /v7/users/123)
	/// </summary>
	/// <param name="url">The request URL</param>
	/// <returns>The resource ID or null if not found</returns>
	private static object? ExtractResourceIdFromUrl(string? url)
	{
		if (string.IsNullOrEmpty(url))
		{
			return null;
		}

		var uri = new Uri(url);
		var segments = uri.Segments;

		// Look for /v7/{resourceType}/{id} pattern for ThousandEyes API
		for (var i = 0; i < segments.Length - 2; i++)
		{
			if (segments[i].Equals("v7/", StringComparison.OrdinalIgnoreCase))
			{
				var idSegment = segments[i + 2].TrimEnd('/');
				return int.TryParse(idSegment, out var intId) ? intId : idSegment;
			}
		}

		return null;
	}

	/// <summary>
	/// Extracts Retry-After header value in seconds
	/// </summary>
	/// <param name="apiException">The API exception</param>
	/// <returns>Retry-After value in seconds or null</returns>
	private static int? ExtractRetryAfterSeconds(ApiException apiException)
	{
		if (apiException.Headers?.TryGetValues("Retry-After", out var values) == true)
		{
			var retryAfter = values.FirstOrDefault();
			if (int.TryParse(retryAfter, out var seconds))
			{
				return seconds;
			}
		}

		return null;
	}
}