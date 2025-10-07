namespace ThousandEyes.Api.Models.Alerts;

/// <summary>
/// Alert Rules response wrapper
/// </summary>
public class AlertRules
{
	/// <summary>
	/// List of alert rules
	/// </summary>
	public AlertRule[] AlertRulesList { get; set; } = [];

	/// <summary>
	/// Navigation links
	/// </summary>
	public AlertLinks? Links { get; set; }
}