namespace ThousandEyes.Api.Models.Dashboards;

/// <summary>
/// Filter property configuration
/// </summary>
public class FilterProperty
{
	/// <summary>
	/// Filter ID (property to filter by)
	/// </summary>
	public required string FilterId { get; set; }

	/// <summary>
	/// Values to filter by
	/// </summary>
	public string[] Values { get; set; } = [];

	/// <summary>
	/// Metrics associated with this filter
	/// </summary>
	public string[] MetricIds { get; set; } = [];
}
