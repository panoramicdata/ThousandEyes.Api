using System.Text.Json.Serialization;

namespace ThousandEyes.Api.Models.EventDetection;

/// <summary>
/// Detailed event information
/// </summary>
public class EventDetail
{
	/// <summary>
	/// Unique event ID
	/// </summary>
	public required string Id { get; set; }

	/// <summary>
	/// Account group ID
	/// </summary>
	public string? Aid { get; set; }

	/// <summary>
	/// Event type name (human-readable)
	/// </summary>
	public string? TypeName { get; set; }

	/// <summary>
	/// Event type (machine-readable)
	/// </summary>
	[JsonPropertyName("type")]
	public EventType? EventTypeValue { get; set; }

	/// <summary>
	/// Event state (active or resolved)
	/// </summary>
	[JsonPropertyName("state")]
	public EventState? StateValue { get; set; }

	/// <summary>
	/// Event severity
	/// </summary>
	[JsonPropertyName("severity")]
	public EventSeverity? SeverityValue { get; set; }

	/// <summary>
	/// Start date and time when event was first detected (UTC)
	/// </summary>
	public DateTime? StartDate { get; set; }

	/// <summary>
	/// End date and time when event was resolved (UTC)
	/// </summary>
	public DateTime? EndDate { get; set; }

	/// <summary>
	/// Brief summary describing the cause of the event
	/// </summary>
	public string? Summary { get; set; }

	/// <summary>
	/// Affected tests details
	/// </summary>
	public AffectedCount? AffectedTests { get; set; }

	/// <summary>
	/// Affected targets details
	/// </summary>
	public AffectedTargets? AffectedTargets { get; set; }

	/// <summary>
	/// Affected agents details
	/// </summary>
	public AffectedCount? AffectedAgents { get; set; }

	/// <summary>
	/// Causes of the error
	/// </summary>
	public string[] Cause { get; set; } = [];

	/// <summary>
	/// Event grouping information (type-specific)
	/// </summary>
	public EventGrouping? Grouping { get; set; }
}
