namespace ThousandEyes.Api.Exceptions;

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
