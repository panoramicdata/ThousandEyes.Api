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
