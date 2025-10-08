namespace ThousandEyes.Api.Models.Dashboards;

/// <summary>
/// Request to create a dashboard snapshot
/// </summary>
public class CreateDashboardSnapshotRequest
{
	/// <summary>
	/// ID of the dashboard to snapshot
	/// </summary>
	public required string DashboardId { get; set; }

	/// <summary>
	/// Display name for the snapshot
	/// </summary>
	public required string DisplayName { get; set; }

	/// <summary>
	/// Start date/time for data aggregation (ISO 8601)
	/// </summary>
	public DateTime? StartDate { get; set; }

	/// <summary>
	/// End date/time for data aggregation (ISO 8601)
	/// </summary>
	public DateTime? EndDate { get; set; }

	/// <summary>
	/// Timezone for date fields (e.g., "PST", "UTC")
	/// </summary>
	public string? Timezone { get; set; }

	/// <summary>
	/// Whether to anonymize data in the snapshot
	/// </summary>
	public bool AnonymizeData { get; set; }

	/// <summary>
	/// Expiration date (defaults to 1 year if not specified, max 5 years)
	/// </summary>
	public DateTime? ExpirationDate { get; set; }
}
