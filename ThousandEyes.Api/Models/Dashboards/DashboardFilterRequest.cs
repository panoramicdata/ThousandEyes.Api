namespace ThousandEyes.Api.Models.Dashboards;

/// <summary>
/// Request to create or update a dashboard filter
/// </summary>
public class DashboardFilterRequest
{
	/// <summary>
	/// Filter name (must be unique)
	/// </summary>
	public required string Name { get; set; }

	/// <summary>
	/// Filter description
	/// </summary>
	public string? Description { get; set; }

	/// <summary>
	/// Filter context (list of data source filters)
	/// </summary>
	public DataSourceFilter[] Context { get; set; } = [];
}
