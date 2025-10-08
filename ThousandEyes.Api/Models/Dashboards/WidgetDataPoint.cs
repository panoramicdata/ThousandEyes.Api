namespace ThousandEyes.Api.Models.Dashboards;

/// <summary>
/// Individual widget data point
/// </summary>
public class WidgetDataPoint
{
	/// <summary>
	/// Timestamp (epoch seconds)
	/// </summary>
	public long? Timestamp { get; set; }

	/// <summary>
	/// Data value
	/// </summary>
	public double? Value { get; set; }

	/// <summary>
	/// Number of data points aggregated
	/// </summary>
	public long? NumberOfDataPoints { get; set; }
}
