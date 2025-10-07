namespace ThousandEyes.Api.Models.Dashboards;

/// <summary>
/// Dashboard navigation links
/// </summary>
public class DashboardLinks
{
	/// <summary>
	/// Self reference link
	/// </summary>
	public DashboardLink? Self { get; set; }

	/// <summary>
	/// Next page link
	/// </summary>
	public DashboardLink? Next { get; set; }

	/// <summary>
	/// Previous page link
	/// </summary>
	public DashboardLink? Previous { get; set; }
}

/// <summary>
/// Dashboard navigation link
/// </summary>
public class DashboardLink
{
	/// <summary>
	/// The href URL of the link
	/// </summary>
	public required string Href { get; set; }
}