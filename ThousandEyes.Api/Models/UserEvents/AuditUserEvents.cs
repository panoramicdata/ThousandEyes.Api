namespace ThousandEyes.Api.Models.UserEvents;

/// <summary>
/// Audit user events response wrapper
/// </summary>
public class AuditUserEvents
{
	/// <summary>
	/// List of audit events
	/// </summary>
	public UserEvent[] AuditEvents { get; set; } = [];

	/// <summary>
	/// Start date of the data range
	/// </summary>
	public DateTime? StartDate { get; set; }

	/// <summary>
	/// End date of the data range
	/// </summary>
	public DateTime? EndDate { get; set; }

	/// <summary>
	/// Navigation links with pagination
	/// </summary>
	public PaginationLinks? Links { get; set; }
}
