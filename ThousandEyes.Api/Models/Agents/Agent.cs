using System.Text.Json.Serialization;

namespace ThousandEyes.Api.Models.Agents;

/// <summary>
/// Agent configuration and information
/// </summary>
public class Agent
{
	/// <summary>
	/// Unique agent ID
	/// </summary>
	public required string AgentId { get; set; }

	/// <summary>
	/// Agent name
	/// </summary>
	public required string AgentName { get; set; }

	/// <summary>
	/// Agent type (cloud, enterprise, enterprise-cluster)
	/// </summary>
	public required string AgentType { get; set; }

	/// <summary>
	/// Location of the agent
	/// </summary>
	public required string Location { get; set; }

	/// <summary>
	/// Country ID (2-digit ISO code)
	/// </summary>
	public required string CountryId { get; set; }

	/// <summary>
	/// Whether the agent is enabled
	/// </summary>
	public bool Enabled { get; set; } = true;

	/// <summary>
	/// Array of IPv4 addresses
	/// </summary>
	[JsonPropertyName("ipAddresses")]
	public string[] IpAddresses { get; set; } = [];

	/// <summary>
	/// Array of public IPv4 addresses
	/// </summary>
	[JsonPropertyName("publicIpAddresses")]
	public string[] PublicIpAddresses { get; set; } = [];

	/// <summary>
	/// Array of IPv6 addresses
	/// </summary>
	[JsonPropertyName("ipv6Addresses")]
	public string[] Ipv6Addresses { get; set; } = [];

	/// <summary>
	/// Array of public IPv6 addresses
	/// </summary>
	[JsonPropertyName("publicIpv6Addresses")]
	public string[] PublicIpv6Addresses { get; set; } = [];

	/// <summary>
	/// Network information including ASN
	/// </summary>
	public string? Network { get; set; }

	/// <summary>
	/// AS (Autonomous System) Number
	/// </summary>
	public int? AsNumber { get; set; }

	/// <summary>
	/// IP prefix containing agent's public IP
	/// </summary>
	public string? Prefix { get; set; }

	/// <summary>
	/// Last time the agent contacted ThousandEyes
	/// </summary>
	public DateTime? LastSeen { get; set; }

	/// <summary>
	/// Agent status (online, offline, disabled)
	/// </summary>
	public string? AgentState { get; set; }

	/// <summary>
	/// Whether the agent supports IPv6
	/// </summary>
	public bool Ipv6Policy { get; set; }

	/// <summary>
	/// Hostname of the agent
	/// </summary>
	public string? Hostname { get; set; }

	/// <summary>
	/// Utilization percentage (0-100)
	/// </summary>
	public int? Utilization { get; set; }

	/// <summary>
	/// Error details if agent is in error state
	/// </summary>
	public string? ErrorDetails { get; set; }

	/// <summary>
	/// Clock offset in seconds from ThousandEyes server time
	/// </summary>
	public double? ClockOffset { get; set; }

	/// <summary>
	/// Agent version information
	/// </summary>
	public string? Version { get; set; }

	/// <summary>
	/// Creation date (Enterprise Agents only)
	/// </summary>
	public DateTime? CreatedDate { get; set; }

	/// <summary>
	/// Cluster ID for Enterprise Agent clusters
	/// </summary>
	public string? ClusterId { get; set; }

	/// <summary>
	/// Target for agent-to-agent tests
	/// </summary>
	public string? TargetForTests { get; set; }

	/// <summary>
	/// List of test types supported by this agent
	/// </summary>
	public string[] SupportedTests { get; set; } = [];

	/// <summary>
	/// Navigation links
	/// </summary>
	[JsonPropertyName("_links")]
	public Links? Links { get; set; }
}