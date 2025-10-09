using System.Text.Json.Serialization;

namespace ThousandEyes.Api.Models.EventDetection;

/// <summary>
/// Detected event summary information
/// </summary>
public class DetectedEvent
{
	/// <summary>
	/// Unique event ID
	/// </summary>
	public required string Id { get; set; }

	/// <summary>
	/// Event title
	/// </summary>
	public string? Title { get; set; }

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
	/// Number of affected tests
	/// </summary>
	public AffectedCount? AffectedTests { get; set; }

	/// <summary>
	/// Number of affected targets
	/// </summary>
	public AffectedTargets? AffectedTargets { get; set; }

	/// <summary>
	/// Number of affected agents
	/// </summary>
	public AffectedCount? AffectedAgents { get; set; }
}
