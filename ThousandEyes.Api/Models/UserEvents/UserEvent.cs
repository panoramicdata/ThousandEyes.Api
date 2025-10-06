namespace ThousandEyes.Api.Models.UserEvents;

/// <summary>
/// User event information
/// </summary>
public class UserEvent
{
	/// <summary>
	/// Account group ID
	/// </summary>
	public string Aid { get; set; } = "";

	/// <summary>
	/// Account group name
	/// </summary>
	public string AccountGroupName { get; set; } = "";

	/// <summary>
	/// Event date
	/// </summary>
	public DateTime Date { get; set; }

	/// <summary>
	/// Event description
	/// </summary>
	public string Event { get; set; } = "";

	/// <summary>
	/// Source IP address
	/// </summary>
	public string? IpAddress { get; set; }

	/// <summary>
	/// User ID
	/// </summary>
	public string Uid { get; set; } = "";

	/// <summary>
	/// User display name and email
	/// </summary>
	public string User { get; set; } = "";

	/// <summary>
	/// Affected resources
	/// </summary>
	public Resource[]? Resources { get; set; }
}
