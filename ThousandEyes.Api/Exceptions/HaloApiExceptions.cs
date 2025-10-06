// Legacy aliases for backward compatibility during transition
using HaloApiException = ThousandEyes.Api.Exceptions.ThousandEyesApiException;
using HaloBadRequestException = ThousandEyes.Api.Exceptions.ThousandEyesBadRequestException;
using HaloAuthenticationException = ThousandEyes.Api.Exceptions.ThousandEyesAuthenticationException;
using HaloAuthorizationException = ThousandEyes.Api.Exceptions.ThousandEyesAuthorizationException;
using HaloNotFoundException = ThousandEyes.Api.Exceptions.ThousandEyesNotFoundException;
using HaloRateLimitException = ThousandEyes.Api.Exceptions.ThousandEyesRateLimitException;
using HaloServerException = ThousandEyes.Api.Exceptions.ThousandEyesServerException;

namespace ThousandEyes.Api.Exceptions;

/// <summary>
/// Contains error context information for ThousandEyes API exceptions
/// </summary>
public sealed record ThousandEyesApiErrorContext
{
	/// <summary>
	/// The HTTP status code associated with the error
	/// </summary>
	public int? StatusCode { get; init; }

	/// <summary>
	/// The error code from the API response
	/// </summary>
	public string? ErrorCode { get; init; }

	/// <summary>
	/// Additional error details from the API
	/// </summary>
	public Dictionary<string, object?>? Details { get; init; }

	/// <summary>
	/// The request URL that caused the error
	/// </summary>
	public string? RequestUrl { get; init; }

	/// <summary>
	/// The request method that caused the error
	/// </summary>
	public string? RequestMethod { get; init; }

	/// <summary>
	/// The exception that is the cause of the current exception
	/// </summary>
	public Exception? InnerException { get; init; }
}

/// <summary>
/// Base exception for all ThousandEyes API related errors
/// </summary>
public class ThousandEyesApiException : Exception
{
	/// <summary>
	/// The HTTP status code associated with the error
	/// </summary>
	public int? StatusCode { get; }

	/// <summary>
	/// The error code from the API response
	/// </summary>
	public string? ErrorCode { get; }

	/// <summary>
	/// Additional error details from the API
	/// </summary>
	public Dictionary<string, object?>? Details { get; }

	/// <summary>
	/// The request URL that caused the error
	/// </summary>
	public string? RequestUrl { get; }

	/// <summary>
	/// The request method that caused the error
	/// </summary>
	public string? RequestMethod { get; }

	/// <summary>
	/// Initializes a new instance of the ThousandEyesApiException class with a specified error message
	/// </summary>
	/// <param name="message">The message that describes the error</param>
	public ThousandEyesApiException(string message) : base(message)
	{
	}

	/// <summary>
	/// Initializes a new instance of the ThousandEyesApiException class with a specified error message and inner exception
	/// </summary>
	/// <param name="message">The message that describes the error</param>
	/// <param name="innerException">The exception that is the cause of the current exception</param>
	public ThousandEyesApiException(string message, Exception innerException) : base(message, innerException)
	{
	}

	/// <summary>
	/// Initializes a new instance of the ThousandEyesApiException class with message and status code
	/// </summary>
	/// <param name="message">The message that describes the error</param>
	/// <param name="statusCode">The HTTP status code associated with the error</param>
	public ThousandEyesApiException(string message, int statusCode) : base(message)
	{
		StatusCode = statusCode;
	}

	/// <summary>
	/// Initializes a new instance of the ThousandEyesApiException class with detailed error information
	/// </summary>
	/// <param name="message">The message that describes the error</param>
	/// <param name="statusCode">The HTTP status code associated with the error</param>
	/// <param name="errorCode">The error code from the API response</param>
	/// <param name="details">Additional error details from the API</param>
	/// <param name="requestUrl">The request URL that caused the error</param>
	/// <param name="requestMethod">The request method that caused the error</param>
	/// <param name="innerException">The exception that is the cause of the current exception</param>
	public ThousandEyesApiException(
		string message,
		int? statusCode,
		string? errorCode,
		Dictionary<string, object?>? details,
		string? requestUrl,
		string? requestMethod,
		Exception? innerException)
		: base(message, innerException)
	{
		StatusCode = statusCode;
		ErrorCode = errorCode;
		Details = details;
		RequestUrl = requestUrl;
		RequestMethod = requestMethod;
	}
}

/// <summary>
/// Exception thrown when authentication fails
/// </summary>
public class ThousandEyesAuthenticationException : ThousandEyesApiException
{
	/// <summary>
	/// Initializes a new instance of the ThousandEyesAuthenticationException class with a specified error message
	/// </summary>
	/// <param name="message">The message that describes the error</param>
	public ThousandEyesAuthenticationException(string message) : base(message)
	{
	}

	/// <summary>
	/// Initializes a new instance of the ThousandEyesAuthenticationException class with a specified error message and inner exception
	/// </summary>
	/// <param name="message">The message that describes the error</param>
	/// <param name="innerException">The exception that is the cause of the current exception</param>
	public ThousandEyesAuthenticationException(string message, Exception innerException) : base(message, innerException)
	{
	}

	/// <summary>
	/// Initializes a new instance of the ThousandEyesAuthenticationException class with message and status code
	/// </summary>
	/// <param name="message">The message that describes the error</param>
	/// <param name="statusCode">The HTTP status code associated with the error</param>
	public ThousandEyesAuthenticationException(string message, int statusCode) : base(message, statusCode)
	{
	}

	/// <summary>
	/// Initializes a new instance of the ThousandEyesAuthenticationException class with detailed error information
	/// </summary>
	/// <param name="message">The message that describes the error</param>
	/// <param name="statusCode">The HTTP status code associated with the error</param>
	/// <param name="errorCode">The error code from the API response</param>
	/// <param name="details">Additional error details from the API</param>
	/// <param name="requestUrl">The request URL that caused the error</param>
	/// <param name="requestMethod">The request method that caused the error</param>
	/// <param name="innerException">The exception that is the cause of the current exception</param>
	public ThousandEyesAuthenticationException(
		string message,
		int? statusCode,
		string? errorCode,
		Dictionary<string, object?>? details,
		string? requestUrl,
		string? requestMethod,
		Exception? innerException)
		: base(message, statusCode, errorCode, details, requestUrl, requestMethod, innerException)
	{
	}
}

/// <summary>
/// Exception thrown when authorization fails (403 Forbidden)
/// </summary>
public class ThousandEyesAuthorizationException : ThousandEyesApiException
{
	/// <summary>
	/// Initializes a new instance of the ThousandEyesAuthorizationException class with a specified error message
	/// </summary>
	/// <param name="message">The message that describes the error</param>
	public ThousandEyesAuthorizationException(string message) : base(message)
	{
	}

	/// <summary>
	/// Initializes a new instance of the ThousandEyesAuthorizationException class with a specified error message and inner exception
	/// </summary>
	/// <param name="message">The message that describes the error</param>
	/// <param name="innerException">The exception that is the cause of the current exception</param>
	public ThousandEyesAuthorizationException(string message, Exception innerException) : base(message, innerException)
	{
	}

	/// <summary>
	/// Initializes a new instance of the ThousandEyesAuthorizationException class with message and status code
	/// </summary>
	/// <param name="message">The message that describes the error</param>
	/// <param name="statusCode">The HTTP status code associated with the error</param>
	public ThousandEyesAuthorizationException(string message, int statusCode) : base(message, statusCode)
	{
	}

	/// <summary>
	/// Initializes a new instance of the ThousandEyesAuthorizationException class with detailed error information
	/// </summary>
	/// <param name="message">The message that describes the error</param>
	/// <param name="statusCode">The HTTP status code associated with the error</param>
	/// <param name="errorCode">The error code from the API response</param>
	/// <param name="details">Additional error details from the API</param>
	/// <param name="requestUrl">The request URL that caused the error</param>
	/// <param name="requestMethod">The request method that caused the error</param>
	/// <param name="innerException">The exception that is the cause of the current exception</param>
	public ThousandEyesAuthorizationException(
		string message,
		int? statusCode,
		string? errorCode,
		Dictionary<string, object?>? details,
		string? requestUrl,
		string? requestMethod,
		Exception? innerException)
		: base(message, statusCode, errorCode, details, requestUrl, requestMethod, innerException)
	{
	}
}

/// <summary>
/// Exception thrown when a requested resource is not found (404 Not Found)
/// </summary>
public class ThousandEyesNotFoundException : ThousandEyesApiException
{
	/// <summary>
	/// The type of resource that was not found
	/// </summary>
	public string? ResourceType { get; }

	/// <summary>
	/// The ID of the resource that was not found
	/// </summary>
	public object? ResourceId { get; }

	/// <summary>
	/// Initializes a new instance of the ThousandEyesNotFoundException class with a specified error message
	/// </summary>
	/// <param name="message">The message that describes the error</param>
	public ThousandEyesNotFoundException(string message) : base(message)
	{
	}

	/// <summary>
	/// Initializes a new instance of the ThousandEyesNotFoundException class with resource information
	/// </summary>
	/// <param name="message">The message that describes the error</param>
	/// <param name="resourceType">The type of resource that was not found</param>
	/// <param name="resourceId">The ID of the resource that was not found</param>
	public ThousandEyesNotFoundException(string message, string? resourceType, object? resourceId) : base(message)
	{
		ResourceType = resourceType;
		ResourceId = resourceId;
	}

	/// <summary>
	/// Initializes a new instance of the ThousandEyesNotFoundException class with detailed error information
	/// </summary>
	/// <param name="message">The message that describes the error</param>
	/// <param name="resourceType">The type of resource that was not found</param>
	/// <param name="resourceId">The ID of the resource that was not found</param>
	/// <param name="statusCode">The HTTP status code associated with the error</param>
	/// <param name="errorCode">The error code from the API response</param>
	/// <param name="details">Additional error details from the API</param>
	/// <param name="requestUrl">The request URL that caused the error</param>
	/// <param name="requestMethod">The request method that caused the error</param>
	/// <param name="innerException">The exception that is the cause of the current exception</param>
	public ThousandEyesNotFoundException(
		string message,
		string? resourceType,
		object? resourceId,
		int? statusCode,
		string? errorCode,
		Dictionary<string, object?>? details,
		string? requestUrl,
		string? requestMethod,
		Exception? innerException)
		: base(message, statusCode, errorCode, details, requestUrl, requestMethod, innerException)
	{
		ResourceType = resourceType;
		ResourceId = resourceId;
	}
}

/// <summary>
/// Exception thrown when the request is malformed or invalid (400 Bad Request)
/// </summary>
public class ThousandEyesBadRequestException : ThousandEyesApiException
{
	/// <summary>
	/// Validation errors from the API
	/// </summary>
	public IReadOnlyList<string>? ValidationErrors { get; }

	/// <summary>
	/// Initializes a new instance of the ThousandEyesBadRequestException class with a specified error message
	/// </summary>
	/// <param name="message">The message that describes the error</param>
	public ThousandEyesBadRequestException(string message) : base(message)
	{
	}

	/// <summary>
	/// Initializes a new instance of the ThousandEyesBadRequestException class with validation errors
	/// </summary>
	/// <param name="message">The message that describes the error</param>
	/// <param name="validationErrors">Validation errors from the API</param>
	public ThousandEyesBadRequestException(string message, IReadOnlyList<string>? validationErrors) : base(message)
	{
		ValidationErrors = validationErrors;
	}

	/// <summary>
	/// Initializes a new instance of the ThousandEyesBadRequestException class with detailed error information
	/// </summary>
	/// <param name="message">The message that describes the error</param>
	/// <param name="validationErrors">Validation errors from the API</param>
	/// <param name="statusCode">The HTTP status code associated with the error</param>
	/// <param name="errorCode">The error code from the API response</param>
	/// <param name="details">Additional error details from the API</param>
	/// <param name="requestUrl">The request URL that caused the error</param>
	/// <param name="requestMethod">The request method that caused the error</param>
	/// <param name="innerException">The exception that is the cause of the current exception</param>
	public ThousandEyesBadRequestException(
		string message,
		IReadOnlyList<string>? validationErrors,
		int? statusCode,
		string? errorCode,
		Dictionary<string, object?>? details,
		string? requestUrl,
		string? requestMethod,
		Exception? innerException)
		: base(message, statusCode, errorCode, details, requestUrl, requestMethod, innerException)
	{
		ValidationErrors = validationErrors;
	}
}

/// <summary>
/// Exception thrown when rate limiting is enforced (429 Too Many Requests)
/// </summary>
public class ThousandEyesRateLimitException : ThousandEyesApiException
{
	/// <summary>
	/// Number of seconds to wait before retrying
	/// </summary>
	public int? RetryAfterSeconds { get; }

	/// <summary>
	/// Initializes a new instance of the ThousandEyesRateLimitException class with a specified error message
	/// </summary>
	/// <param name="message">The message that describes the error</param>
	public ThousandEyesRateLimitException(string message) : base(message)
	{
	}

	/// <summary>
	/// Initializes a new instance of the ThousandEyesRateLimitException class with rate limit information
	/// </summary>
	/// <param name="message">The message that describes the error</param>
	/// <param name="retryAfterSeconds">Number of seconds to wait before retrying</param>
	public ThousandEyesRateLimitException(string message, int? retryAfterSeconds) : base(message)
	{
		RetryAfterSeconds = retryAfterSeconds;
	}
}

/// <summary>
/// Exception thrown when the server encounters an internal error (500 Internal Server Error)
/// </summary>
public class ThousandEyesServerException : ThousandEyesApiException
{
	/// <summary>
	/// Initializes a new instance of the ThousandEyesServerException class with a specified error message
	/// </summary>
	/// <param name="message">The message that describes the error</param>
	public ThousandEyesServerException(string message) : base(message)
	{
	}

	/// <summary>
	/// Initializes a new instance of the ThousandEyesServerException class with detailed error information
	/// </summary>
	/// <param name="message">The message that describes the error</param>
	/// <param name="statusCode">The HTTP status code associated with the error</param>
	/// <param name="errorCode">The error code from the API response</param>
	/// <param name="details">Additional error details from the API</param>
	/// <param name="requestUrl">The request URL that caused the error</param>
	/// <param name="requestMethod">The request method that caused the error</param>
	/// <param name="innerException">The exception that is the cause of the current exception</param>
	public ThousandEyesServerException(
		string message,
		int? statusCode,
		string? errorCode,
		Dictionary<string, object?>? details,
		string? requestUrl,
		string? requestMethod,
		Exception? innerException)
		: base(message, statusCode, errorCode, details, requestUrl, requestMethod, innerException)
	{
	}
}