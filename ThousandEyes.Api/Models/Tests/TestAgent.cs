namespace ThousandEyes.Api.Models.Tests;

/// <summary>
/// Test agent assignment information
/// </summary>
public class TestAgent
{
	/// <summary>
	/// Agent ID
	/// </summary>
	public required string AgentId { get; set; }

	/// <summary>
	/// Agent name
	/// </summary>
	public string? AgentName { get; set; }

	/// <summary>
	/// Agent type (cloud, enterprise, enterprise-cluster)
	/// </summary>
	public string? AgentType { get; set; }

	/// <summary>
	/// Agent location
	/// </summary>
	public string? Location { get; set; }

	/// <summary>
	/// Country ID (2-digit ISO)
	/// </summary>
	public string? CountryId { get; set; }

	/// <summary>
	/// Whether the agent is enabled
	/// </summary>
	public bool Enabled { get; set; } = true;

	/// <summary>
	/// Source IP address for interface selection
	/// </summary>
	public string? SourceIpAddress { get; set; }

	/// <summary>
	/// Array of IP addresses
	/// </summary>
	public string[]? IpAddresses { get; set; }

	/// <summary>
	/// Array of public IP addresses
	/// </summary>
	public string[]? PublicIpAddresses { get; set; }

	/// <summary>
	/// Network information including ASN
	/// </summary>
	public string? Network { get; set; }

	/// <summary>
	/// IP prefix containing agent's public IP
	/// </summary>
	public string? Prefix { get; set; }
}