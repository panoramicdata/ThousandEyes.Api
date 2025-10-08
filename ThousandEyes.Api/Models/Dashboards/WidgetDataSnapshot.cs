using System.Text.Json.Serialization;
using ThousandEyes.Api.Models;

namespace ThousandEyes.Api.Models.Dashboards;

/// <summary>
/// Widget data snapshot
/// </summary>
public class WidgetDataSnapshot
{
	/// <summary>
	/// Widget ID
	/// </summary>
	public string? WidgetId { get; set; }

	/// <summary>
	/// Data points
	/// </summary>
	public WidgetDataPoint[] Points { get; set; } = [];

	/// <summary>
	/// Start date of data (ISO 8601)
	/// </summary>
	public DateTime? StartDate { get; set; }

	/// <summary>
	/// End date of data (ISO 8601)
	/// </summary>
	public DateTime? EndDate { get; set; }

	/// <summary>
	/// Bin size in seconds
	/// </summary>
	public long? BinSize { get; set; }

	/// <summary>
	/// Status message
	/// </summary>
	public string? Status { get; set; }

	/// <summary>
	/// Navigation links
	/// </summary>
	[JsonPropertyName("_links")]
	public Links? Links { get; set; }
}
