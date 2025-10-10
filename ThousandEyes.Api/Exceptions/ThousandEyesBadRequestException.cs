namespace ThousandEyes.Api.Exceptions;

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
