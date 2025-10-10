namespace ThousandEyes.Api.Exceptions;

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
