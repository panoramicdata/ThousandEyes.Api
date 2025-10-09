using System.Text.Json.Serialization;

namespace ThousandEyes.Api.Models.EndpointAgents;

/// <summary>
/// Collection of endpoint agents with pagination
/// </summary>
public class EndpointAgents : ApiResource
{
	/// <summary>
	/// Total number of agents
	/// </summary>
	[JsonPropertyName("totalAgents")]
	public int? TotalAgents { get; set; }

	/// <summary>
	/// List of endpoint agents
	/// </summary>
	[JsonPropertyName("agents")]
	public EndpointAgent[] AgentsList { get; set; } = [];
}