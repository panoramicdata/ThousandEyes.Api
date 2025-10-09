using System.Text.Json.Serialization;

namespace ThousandEyes.Api.Models.EndpointAgents;

/// <summary>
/// Search filters for endpoint agents
/// </summary>
public class AgentSearchFilters
{
	/// <summary>
	/// Filter by agent IDs
	/// </summary>
	[JsonPropertyName("id")]
	public string[] Id { get; set; } = [];

	/// <summary>
	/// Filter by agent names (exact match)
	/// </summary>
	[JsonPropertyName("agentName")]
	public string[] AgentName { get; set; } = [];

	/// <summary>
	/// Filter by computer names (exact match)
	/// </summary>
	[JsonPropertyName("computerName")]
	public string[] ComputerName { get; set; } = [];

	/// <summary>
	/// Filter by usernames (prefix match, case-insensitive)
	/// </summary>
	[JsonPropertyName("username")]
	public string[] Username { get; set; } = [];

	/// <summary>
	/// Filter by platform
	/// </summary>
	[JsonPropertyName("platform")]
	public Platform[] Platform { get; set; } = [];

	/// <summary>
	/// Filter by OS version (prefix match, case-insensitive)
	/// </summary>
	[JsonPropertyName("osVersion")]
	public string[] OsVersion { get; set; } = [];

	/// <summary>
	/// Filter by license type
	/// </summary>
	[JsonPropertyName("licenseType")]
	public AgentLicenseType[] LicenseType { get; set; } = [];
}