using System.Text.Json.Serialization;

namespace ThousandEyes.Api.Models.EndpointAgents;

/// <summary>
/// License type for endpoint agents
/// </summary>
[JsonConverter(typeof(JsonStringEnumConverter))]
public enum AgentLicenseType
{
	/// <summary>
	/// Essentials license
	/// </summary>
	Essentials,

	/// <summary>
	/// Advantage license
	/// </summary>
	Advantage,

	/// <summary>
	/// Embedded license
	/// </summary>
	Embedded
}