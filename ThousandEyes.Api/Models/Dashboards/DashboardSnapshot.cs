using System.Text.Json.Serialization;
using ThousandEyes.Api.Models;

namespace ThousandEyes.Api.Models.Dashboards;

/// <summary>
/// Dashboard snapshot - a point-in-time capture of dashboard data
/// </summary>
public class DashboardSnapshot : AuditableResource
{
	// Inherited from AuditableResource: Aid, Links, CreatedDate, ModifiedDate, CreatedBy, ModifiedBy

	/// <summary>
	/// Unique snapshot ID
	/// </summary>
	public required string SnapshotId { get; set; }

	/// <summary>
	/// Snapshot name/title
	/// </summary>
	public required string SnapshotName { get; set; }

	/// <summary>
	/// Whether the snapshot is shared
	/// </summary>
	public bool IsShared { get; set; }

	/// <summary>
	/// When the snapshot expires (ISO 8601 format)
	/// </summary>
	public DateTime? SnapshotExpirationDate { get; set; }

	/// <summary>
	/// Whether the snapshot was scheduled
	/// </summary>
	public bool IsScheduled { get; set; }

	/// <summary>
	/// Dashboard the snapshot was created from
	/// </summary>
	public Dashboard? Dashboard { get; set; }

	/// <summary>
	/// Widgets in the snapshot
	/// </summary>
	public DashboardWidget[] Widgets { get; set; } = [];

	/// <summary>
	/// Time span of the snapshot
	/// </summary>
	public SnapshotTimeSpan? TimeSpan { get; set; }
}
