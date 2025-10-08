using System.Text.Json.Serialization;
using ThousandEyes.Api.Models;

namespace ThousandEyes.Api.Models.Dashboards;

/// <summary>
/// Response after creating a dashboard snapshot
/// </summary>
public class DashboardSnapshotResponse
{
	/// <summary>
	/// ID of the created snapshot
	/// </summary>
	public required string SnapshotId { get; set; }

	/// <summary>
	/// Navigation links
	/// </summary>
	[JsonPropertyName("_links")]
	public Links? Links { get; set; }
}
