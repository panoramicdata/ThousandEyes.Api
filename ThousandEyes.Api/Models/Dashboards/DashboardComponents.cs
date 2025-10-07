using System.Text.Json.Serialization;

namespace ThousandEyes.Api.Models.Dashboards;

/// <summary>
/// Dashboard layout configuration
/// </summary>
public class DashboardLayout
{
	/// <summary>
	/// Layout type (grid, freeform)
	/// </summary>
	public string? Type { get; set; }

	/// <summary>
	/// Grid columns
	/// </summary>
	public int Columns { get; set; } = 12;

	/// <summary>
	/// Row height in pixels
	/// </summary>
	public int RowHeight { get; set; } = 100;

	/// <summary>
	/// Margin between widgets
	/// </summary>
	public int Margin { get; set; } = 10;

	/// <summary>
	/// Whether the layout is responsive
	/// </summary>
	public bool IsResponsive { get; set; } = true;
}

/// <summary>
/// Dashboard filter configuration
/// </summary>
public class DashboardFilter
{
	/// <summary>
	/// Filter ID
	/// </summary>
	public required string FilterId { get; set; }

	/// <summary>
	/// Filter name
	/// </summary>
	public required string Name { get; set; }

	/// <summary>
	/// Filter type (test, agent, location, etc.)
	/// </summary>
	public required string Type { get; set; }

	/// <summary>
	/// Filter values
	/// </summary>
	[JsonPropertyName("values")]
	public string[] Values { get; set; } = [];

	/// <summary>
	/// Filter operator (equals, contains, etc.)
	/// </summary>
	public string Operator { get; set; } = "equals";

	/// <summary>
	/// Whether the filter is enabled
	/// </summary>
	public bool IsEnabled { get; set; } = true;
}

/// <summary>
/// Dashboard time span configuration
/// </summary>
public class DashboardTimeSpan
{
	/// <summary>
	/// Time span type (relative, absolute)
	/// </summary>
	public required string Type { get; set; }

	/// <summary>
	/// Relative time span (1h, 24h, 7d, etc.) for relative type
	/// </summary>
	public string? RelativeSpan { get; set; }

	/// <summary>
	/// Start date for absolute type
	/// </summary>
	public DateTime? StartDate { get; set; }

	/// <summary>
	/// End date for absolute type
	/// </summary>
	public DateTime? EndDate { get; set; }

	/// <summary>
	/// Whether to auto-refresh the data
	/// </summary>
	public bool AutoRefresh { get; set; }

	/// <summary>
	/// Auto-refresh interval in seconds
	/// </summary>
	public int RefreshInterval { get; set; } = 300;
}