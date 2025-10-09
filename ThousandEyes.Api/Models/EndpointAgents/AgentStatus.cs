using System.Text.Json.Serialization;

namespace ThousandEyes.Api.Models.EndpointAgents;

/// <summary>
/// Status of the endpoint agent in ThousandEyes
/// </summary>
[JsonConverter(typeof(JsonStringEnumConverter))]
public enum AgentStatus
{
	/// <summary>
	/// Agent is enabled and reporting data
	/// </summary>
	Enabled,

	/// <summary>
	/// Agent is disabled and not reporting data
	/// </summary>
	Disabled
}