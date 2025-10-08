using System.Text.Json.Serialization;

namespace ThousandEyes.Api.Models.Alerts;

/// <summary>
/// List of alerts
/// </summary>
public class Alerts
{
	/// <summary>
	/// Alerts
	/// </summary>
	public Alert[] AlertsList { get; set; } = [];

	/// <summary>
	/// Navigation links
	/// </summary>
	[JsonPropertyName("_links")]
	public Links? Links { get; set; }
}