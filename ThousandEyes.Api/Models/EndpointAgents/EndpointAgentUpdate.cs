using System.Text.Json.Serialization;

namespace ThousandEyes.Api.Models.EndpointAgents;

/// <summary>
/// Request to update an endpoint agent
/// </summary>
public class EndpointAgentUpdate
{
	/// <summary>
	/// New agent name
	/// </summary>
	[JsonPropertyName("name")]
	public string? Name { get; set; }

	/// <summary>
	/// License type
	/// </summary>
	[JsonPropertyName("licenseType")]
	public AgentLicenseType? LicenseType { get; set; }
}