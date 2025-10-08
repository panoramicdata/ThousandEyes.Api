namespace ThousandEyes.Api.Models.Dashboards;

/// <summary>
/// List of dashboard filters response
/// </summary>
public class DashboardFilters
{
	/// <summary>
	/// Array of dashboard filters
	/// </summary>
	public DashboardFilterDetails[] Filters { get; set; } = [];
}
