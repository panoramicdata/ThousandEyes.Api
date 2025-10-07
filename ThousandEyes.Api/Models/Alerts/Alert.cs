using System.Text.Json.Serialization;

namespace ThousandEyes.Api.Models.Alerts;

/// <summary>
/// Alert configuration and status
/// </summary>
public class Alert
{
	/// <summary>
	/// Unique alert ID
	/// </summary>
	public required string AlertId { get; set; }

	/// <summary>
	/// Alert rule ID that triggered this alert
	/// </summary>
	public required string RuleId { get; set; }

	/// <summary>
	/// Test ID associated with this alert
	/// </summary>
	public required string TestId { get; set; }

	/// <summary>
	/// Test name
	/// </summary>
	public required string TestName { get; set; }

	/// <summary>
	/// Alert type (agent, test, bgp, etc.)
	/// </summary>
	public required string Type { get; set; }

	/// <summary>
	/// Alert status (active, inactive, triggered)
	/// </summary>
	public required string Status { get; set; }

	/// <summary>
	/// Alert severity level (critical, warning, info)
	/// </summary>
	public required string Severity { get; set; }

	/// <summary>
	/// When the alert was first triggered
	/// </summary>
	public required DateTime DateStart { get; set; }

	/// <summary>
	/// When the alert was resolved (null if still active)
	/// </summary>
	public DateTime? DateEnd { get; set; }

	/// <summary>
	/// Duration of the alert in seconds
	/// </summary>
	public int? Duration { get; set; }

	/// <summary>
	/// Agent ID if this is an agent-specific alert
	/// </summary>
	public string? AgentId { get; set; }

	/// <summary>
	/// Agent name if this is an agent-specific alert
	/// </summary>
	public string? AgentName { get; set; }

	/// <summary>
	/// BGP monitor ID if this is a BGP alert
	/// </summary>
	public string? MonitorId { get; set; }

	/// <summary>
	/// BGP monitor name if this is a BGP alert
	/// </summary>
	public string? MonitorName { get; set; }

	/// <summary>
	/// Metric that violated the threshold
	/// </summary>
	public string? ViolationMetric { get; set; }

	/// <summary>
	/// Threshold value that was violated
	/// </summary>
	public double? ThresholdValue { get; set; }

	/// <summary>
	/// Actual value that caused the violation
	/// </summary>
	public double? ActualValue { get; set; }

	/// <summary>
	/// Round ID when the alert was triggered
	/// </summary>
	public string? RoundId { get; set; }

	/// <summary>
	/// Alert condition details
	/// </summary>
	public string? AlertCondition { get; set; }

	/// <summary>
	/// Permalink to this alert
	/// </summary>
	public string? Permalink { get; set; }

	/// <summary>
	/// Navigation links
	/// </summary>
	public AlertLinks? Links { get; set; }
}