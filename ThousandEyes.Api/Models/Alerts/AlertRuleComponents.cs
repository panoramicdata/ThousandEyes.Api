using System.Text.Json.Serialization;

namespace ThousandEyes.Api.Models.Alerts;

/// <summary>
/// Test reference in alert rule
/// </summary>
public class AlertRuleTest
{
	/// <summary>
	/// Test ID
	/// </summary>
	[JsonPropertyName("testId")]
	public required string TestId { get; set; }

	/// <summary>
	/// Test name
	/// </summary>
	[JsonPropertyName("testName")]
	public string? TestName { get; set; }
}

/// <summary>
/// Agent reference in alert rule
/// </summary>
public class AlertRuleAgent
{
	/// <summary>
	/// Agent ID
	/// </summary>
	[JsonPropertyName("agentId")]
	public required string AgentId { get; set; }

	/// <summary>
	/// Agent name
	/// </summary>
	[JsonPropertyName("agentName")]
	public string? AgentName { get; set; }
}

/// <summary>
/// BGP Monitor reference in alert rule
/// </summary>
public class AlertRuleMonitor
{
	/// <summary>
	/// Monitor ID
	/// </summary>
	[JsonPropertyName("monitorId")]
	public required string MonitorId { get; set; }

	/// <summary>
	/// Monitor name
	/// </summary>
	[JsonPropertyName("monitorName")]
	public string? MonitorName { get; set; }
}

/// <summary>
/// Alert rule notification configuration
/// </summary>
public class AlertRuleNotifications
{
	/// <summary>
	/// Email addresses to notify
	/// </summary>
	[JsonPropertyName("emails")]
	public string[] Emails { get; set; } = [];

	/// <summary>
	/// Webhook URLs to call
	/// </summary>
	[JsonPropertyName("webhooks")]
	public AlertRuleWebhook[] Webhooks { get; set; } = [];

	/// <summary>
	/// Third-party integrations
	/// </summary>
	[JsonPropertyName("integrations")]
	public AlertRuleIntegration[] Integrations { get; set; } = [];
}

/// <summary>
/// Webhook configuration for alert notifications
/// </summary>
public class AlertRuleWebhook
{
	/// <summary>
	/// Webhook URL
	/// </summary>
	[JsonPropertyName("url")]
	public required string Url { get; set; }

	/// <summary>
	/// HTTP method (GET, POST)
	/// </summary>
	[JsonPropertyName("method")]
	public string Method { get; set; } = "POST";

	/// <summary>
	/// Custom headers
	/// </summary>
	[JsonPropertyName("headers")]
	public Dictionary<string, string> Headers { get; set; } = [];
}

/// <summary>
/// Integration configuration for alert notifications
/// </summary>
public class AlertRuleIntegration
{
	/// <summary>
	/// Integration type (slack, pagerduty, etc.)
	/// </summary>
	[JsonPropertyName("type")]
	public required string Type { get; set; }

	/// <summary>
	/// Integration channel or target
	/// </summary>
	[JsonPropertyName("target")]
	public required string Target { get; set; }
}