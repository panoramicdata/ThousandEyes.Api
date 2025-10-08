using System.Text.Json.Serialization;

namespace ThousandEyes.Api.Models.Alerts;

/// <summary>
/// List of alert rules
/// </summary>
public class AlertRules
{
	/// <summary>
	/// Alert rules
	/// </summary>
	public AlertRule[] AlertRulesList { get; set; } = [];

	/// <summary>
	/// Navigation links
	/// </summary>
	[JsonPropertyName("_links")]
	public Links? Links { get; set; }
}