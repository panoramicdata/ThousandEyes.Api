using System.Text.Json.Serialization;
using ThousandEyes.Api.Models;

namespace ThousandEyes.Api.Models.Dashboards;

/// <summary>
/// Paginated list of dashboard snapshots
/// </summary>
public class DashboardSnapshotsPage
{
	/// <summary>
	/// Array of dashboard snapshots
	/// </summary>
	public DashboardSnapshot[] DashboardSnapshots { get; set; } = [];

	/// <summary>
	/// Pagination links
	/// </summary>
	[JsonPropertyName("_links")]
	public Links? Links { get; set; }
}
