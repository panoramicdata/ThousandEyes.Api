namespace ThousandEyes.Api.Exceptions;

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
	/// <param name="context">The HTTP status, error code, details, request and inner exception</param>
	public ThousandEyesNotFoundException(string message, string? resourceType, object? resourceId, ThousandEyesApiErrorContext context)
		: base(message, context.StatusCode, context.ErrorCode, context.Details, context.RequestUrl, context.RequestMethod, context.InnerException)
	{
		ResourceType = resourceType;
		ResourceId = resourceId;
	}
}
