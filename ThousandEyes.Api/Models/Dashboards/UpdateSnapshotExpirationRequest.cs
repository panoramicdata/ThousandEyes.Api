namespace ThousandEyes.Api.Models.Dashboards;

/// <summary>
/// Request to update a snapshot's expiration date
/// </summary>
public class UpdateSnapshotExpirationRequest
{
	/// <summary>
	/// New expiration date (ISO 8601 format, max 5 years from current date)
	/// </summary>
	public required DateTime SnapshotExpirationDate { get; set; }
}
