using System.Text.Json.Serialization;

namespace ThousandEyes.Api.Models.Dashboards;

/// <summary>
/// Dashboard widget configuration
/// </summary>
public class DashboardWidget
{
	/// <summary>
	/// Widget ID
	/// </summary>
	public required string WidgetId { get; set; }

	/// <summary>
	/// Widget title
	/// </summary>
	public required string Title { get; set; }

	/// <summary>
	/// Widget type (table, timeseries, number, etc.)
	/// </summary>
	public required string Type { get; set; }

	/// <summary>
	/// Widget position and size
	/// </summary>
	public WidgetPosition? Position { get; set; }

	/// <summary>
	/// Widget data source configuration
	/// </summary>
	public WidgetDataSource? DataSource { get; set; }

	/// <summary>
	/// Widget visualization configuration
	/// </summary>
	public WidgetVisualization? Visualization { get; set; }

	/// <summary>
	/// Widget-specific filters
	/// </summary>
	[JsonPropertyName("filters")]
	public DashboardFilter[] Filters { get; set; } = [];

	/// <summary>
	/// Whether the widget is embedded
	/// </summary>
	public bool IsEmbedded { get; set; }

	/// <summary>
	/// Widget configuration options
	/// </summary>
	public Dictionary<string, object> Options { get; set; } = [];
}

/// <summary>
/// Widget position and size configuration
/// </summary>
public class WidgetPosition
{
	/// <summary>
	/// Widget X coordinate
	/// </summary>
	public int X { get; set; }

	/// <summary>
	/// Widget Y coordinate
	/// </summary>
	public int Y { get; set; }

	/// <summary>
	/// Widget width
	/// </summary>
	public int Width { get; set; }

	/// <summary>
	/// Widget height
	/// </summary>
	public int Height { get; set; }
}

/// <summary>
/// Widget data source configuration
/// </summary>
public class WidgetDataSource
{
	/// <summary>
	/// Data source type (test, agent, alert)
	/// </summary>
	public required string Type { get; set; }

	/// <summary>
	/// Test IDs for test-based widgets
	/// </summary>
	[JsonPropertyName("testIds")]
	public string[] TestIds { get; set; } = [];

	/// <summary>
	/// Agent IDs for agent-based widgets
	/// </summary>
	[JsonPropertyName("agentIds")]
	public string[] AgentIds { get; set; } = [];

	/// <summary>
	/// Metrics to display
	/// </summary>
	[JsonPropertyName("metrics")]
	public string[] Metrics { get; set; } = [];

	/// <summary>
	/// Time range for data
	/// </summary>
	public DashboardTimeSpan? TimeRange { get; set; }

	/// <summary>
	/// Data aggregation method
	/// </summary>
	public string? Aggregation { get; set; }
}

/// <summary>
/// Widget visualization configuration
/// </summary>
public class WidgetVisualization
{
	/// <summary>
	/// Chart type (line, bar, pie, etc.)
	/// </summary>
	public string? ChartType { get; set; }

	/// <summary>
	/// Color scheme
	/// </summary>
	public string? ColorScheme { get; set; }

	/// <summary>
	/// Show legend
	/// </summary>
	public bool ShowLegend { get; set; }

	/// <summary>
	/// Show grid
	/// </summary>
	public bool ShowGrid { get; set; }

	/// <summary>
	/// Y-axis configuration
	/// </summary>
	public AxisConfiguration? YAxis { get; set; }

	/// <summary>
	/// X-axis configuration
	/// </summary>
	public AxisConfiguration? XAxis { get; set; }
}

/// <summary>
/// Chart axis configuration
/// </summary>
public class AxisConfiguration
{
	/// <summary>
	/// Axis title
	/// </summary>
	public string? Title { get; set; }

	/// <summary>
	/// Minimum value
	/// </summary>
	public double? Min { get; set; }

	/// <summary>
	/// Maximum value
	/// </summary>
	public double? Max { get; set; }

	/// <summary>
	/// Scale type (linear, logarithmic)
	/// </summary>
	public string? Scale { get; set; }
}