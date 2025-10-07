namespace ThousandEyes.Api.Models.Dashboards;

/// <summary>
/// Dashboards response wrapper
/// </summary>
public class Dashboards
{
	/// <summary>
	/// List of dashboards
	/// </summary>
	public Dashboard[] DashboardsList { get; set; } = [];

	/// <summary>
	/// Navigation links
	/// </summary>
	public DashboardLinks? Links { get; set; }
}