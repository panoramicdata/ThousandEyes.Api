namespace ThousandEyes.Api.Models.Dashboards;

/// <summary>
/// Dashboard request for create/update operations
/// </summary>
public class DashboardRequest
{
	/// <summary>
	/// Dashboard name
	/// </summary>
	public required string DashboardName { get; set; }

	/// <summary>
	/// Dashboard description
	/// </summary>
	public string? Description { get; set; }

	/// <summary>
	/// Dashboard type (global, personal, shared)
	/// </summary>
	public required string DashboardType { get; set; }

	/// <summary>
	/// Whether the dashboard is private
	/// </summary>
	public bool IsPrivate { get; set; }

	/// <summary>
	/// Whether the dashboard is default for the user
	/// </summary>
	public bool IsDefault { get; set; }

	/// <summary>
	/// Dashboard widgets configuration
	/// </summary>
	public DashboardWidget[] Widgets { get; set; } = [];

	/// <summary>
	/// Dashboard layout configuration
	/// </summary>
	public DashboardLayout? Layout { get; set; }

	/// <summary>
	/// Dashboard filters
	/// </summary>
	public DashboardFilter[] Filters { get; set; } = [];

	/// <summary>
	/// Dashboard time span configuration
	/// </summary>
	public DashboardTimeSpan? TimeSpan { get; set; }
}