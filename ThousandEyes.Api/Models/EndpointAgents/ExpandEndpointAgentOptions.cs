using System.Text.Json.Serialization;

namespace ThousandEyes.Api.Models.EndpointAgents;

/// <summary>
/// Expand options for endpoint agent operations
/// </summary>
[JsonConverter(typeof(JsonStringEnumConverter))]
public enum ExpandEndpointAgentOptions
{
	/// <summary>
	/// Include client (user) information
	/// </summary>
	Clients,

	/// <summary>
	/// Include VPN profile information
	/// </summary>
	VpnProfiles,

	/// <summary>
	/// Include network interface profile information
	/// </summary>
	NetworkInterfaceProfiles
}