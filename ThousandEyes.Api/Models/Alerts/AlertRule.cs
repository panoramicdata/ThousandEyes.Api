using System.Text.Json.Serialization;

namespace ThousandEyes.Api.Models.Alerts;

/// <summary>
/// Alert rule configuration
/// </summary>
public class AlertRule
{
	/// <summary>
	/// Unique alert rule ID
	/// </summary>
	public required string RuleId { get; set; }

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
	/// Minimum duration in seconds before triggering
	/// </summary>
	public int MinimumSources { get; set; } = 1;

	/// <summary>
	/// Number of sources that must meet criteria
	/// </summary>
	public int MinimumSourcesPct { get; set; } = 100;

	/// <summary>
	/// Rounds before the alert clears
	/// </summary>
	public int RoundsBelowThreshold { get; set; } = 1;

	/// <summary>
	/// Tests this rule applies to
	/// </summary>
	[JsonPropertyName("tests")]
	public AlertRuleTest[] Tests { get; set; } = [];

	/// <summary>
	/// Agents this rule applies to (if applicable)
	/// </summary>
	[JsonPropertyName("agents")]
	public AlertRuleAgent[] Agents { get; set; } = [];

	/// <summary>
	/// BGP monitors this rule applies to (if applicable)
	/// </summary>
	[JsonPropertyName("monitors")]
	public AlertRuleMonitor[] Monitors { get; set; } = [];

	/// <summary>
	/// Notification settings
	/// </summary>
	public AlertRuleNotifications? Notifications { get; set; }

	/// <summary>
	/// When this rule was created
	/// </summary>
	public DateTime? CreatedDate { get; set; }

	/// <summary>
	/// When this rule was last modified
	/// </summary>
	public DateTime? ModifiedDate { get; set; }

	/// <summary>
	/// User who created this rule
	/// </summary>
	public string? CreatedBy { get; set; }

	/// <summary>
	/// User who last modified this rule
	/// </summary>
	public string? ModifiedBy { get; set; }

	/// <summary>
	/// Navigation links
	/// </summary>
	public AlertLinks? Links { get; set; }
}