namespace ThousandEyes.Api.Models.Dashboards;

/// <summary>
/// Time span information for a snapshot
/// </summary>
public class SnapshotTimeSpan
{
	/// <summary>
	/// Start date of the snapshot time span (ISO 8601)
	/// </summary>
	public DateTime? Start { get; set; }

	/// <summary>
	/// Duration in seconds
	/// </summary>
	public long? Duration { get; set; }
}
