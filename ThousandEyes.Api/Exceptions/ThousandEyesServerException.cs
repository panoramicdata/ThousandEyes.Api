namespace ThousandEyes.Api.Exceptions;

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