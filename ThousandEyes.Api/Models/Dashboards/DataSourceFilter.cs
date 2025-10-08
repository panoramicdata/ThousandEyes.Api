namespace ThousandEyes.Api.Models.Dashboards;

/// <summary>
/// Data source filter configuration
/// </summary>
public class DataSourceFilter
{
	/// <summary>
	/// Data source ID (e.g., "VIRTUAL_AGENT", "TEST_LABEL")
	/// </summary>
	public required string DataSourceId { get; set; }

	/// <summary>
	/// Individual filter properties
	/// </summary>
	public FilterProperty[] Filters { get; set; } = [];
}
