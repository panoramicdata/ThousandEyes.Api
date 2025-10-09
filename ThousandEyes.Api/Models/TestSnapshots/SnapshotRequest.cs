using System.Text.Json.Serialization;

namespace ThousandEyes.Api.Models.TestSnapshots;

/// <summary>
/// Request for creating a test snapshot.
/// </summary>
public class SnapshotRequest
{
	/// <summary>
	/// Snapshot title.
	/// </summary>
	[JsonPropertyName("displayName")]
	public required string DisplayName { get; set; }

	/// <summary>
	/// The start date for the snapshot in UTC time, formatted in ISO date-time.
	/// </summary>
	[JsonPropertyName("startDate")]
	public required DateTime StartDate { get; set; }

	/// <summary>
	/// The end date for the snapshot in UTC time, formatted in ISO date-time.
	/// The endDate must be set to the present or a past date.
	/// </summary>
	[JsonPropertyName("endDate")]
	public required DateTime EndDate { get; set; }

	/// <summary>
	/// Set to true for private snapshots (saved events) and false for public share links.
	/// Default value is false.
	/// Note: Saved Events are now called Private Snapshots in the user interface.
	/// </summary>
	[JsonPropertyName("isPublic")]
	public bool IsPublic { get; set; } = false;
}
