using System.Text.Json.Serialization;

namespace ThousandEyes.Api.Models.EventDetection;

/// <summary>
/// Response containing list of events
/// </summary>
public class Events : ApiResource
{
	/// <summary>
	/// Account group ID
	/// </summary>
	public string? Aid { get; set; }

	/// <summary>
	/// Start date of the time range being retrieved (UTC)
	/// </summary>
	public DateTime? StartDate { get; set; }

	/// <summary>
	/// End date of the time range being retrieved (UTC)
	/// </summary>
	public DateTime? EndDate { get; set; }

	/// <summary>
	/// List of events
	/// </summary>
	[JsonPropertyName("events")]
	public DetectedEvent[] EventsList { get; set; } = [];
}
