namespace ThousandEyes.Api.Models.Dashboards;

/// <summary>
/// Reports response wrapper
/// </summary>
public class Reports
{
	/// <summary>
	/// List of reports
	/// </summary>
	public Report[] ReportsList { get; set; } = [];

	/// <summary>
	/// Navigation links
	/// </summary>
	public DashboardLinks? Links { get; set; }
}