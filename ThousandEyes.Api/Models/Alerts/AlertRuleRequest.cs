namespace ThousandEyes.Api.Models.Alerts;

/// <summary>
/// Alert rule request for create/update operations
/// </summary>
public class AlertRuleRequest
{
	/// <summary>
	/// Alert rule name
	/// </summary>
	public required string RuleName { get; set; }

	/// <summary>
	/// Alert rule description
	/// </summary>
	public string? Description { get; set; }

	/// <summary>
	/// Expression that defines when the alert triggers
	/// </summary>
	public required string Expression { get; set; }

	/// <summary>
	/// Whether the alert rule is enabled
	/// </summary>
	public bool Enabled { get; set; } = true;

	/// <summary>
	/// Alert type (agent, test, bgp, etc.)
	/// </summary>
	public required string AlertType { get; set; }

	/// <summary>
	/// Severity level (critical, warning, info)
	/// </summary>
	public required string Severity { get; set; }

	/// <summary>
	/// Minimum number of sources that must meet criteria
	/// </summary>
	public int MinimumSources { get; set; } = 1;

	/// <summary>
	/// Percentage of sources that must meet criteria
	/// </summary>
	public int MinimumSourcesPct { get; set; } = 100;

	/// <summary>
	/// Rounds below threshold before clearing
	/// </summary>
	public int RoundsBelowThreshold { get; set; } = 1;

	/// <summary>
	/// Tests this rule applies to
	/// </summary>
	public AlertRuleTest[] Tests { get; set; } = [];

	/// <summary>
	/// Agents this rule applies to (if applicable)
	/// </summary>
	public AlertRuleAgent[] Agents { get; set; } = [];

	/// <summary>
	/// BGP monitors this rule applies to (if applicable)
	/// </summary>
	public AlertRuleMonitor[] Monitors { get; set; } = [];

	/// <summary>
	/// Notification settings
	/// </summary>
	public AlertRuleNotifications? Notifications { get; set; }
}