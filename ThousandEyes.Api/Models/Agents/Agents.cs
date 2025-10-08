using System.Text.Json.Serialization;

namespace ThousandEyes.Api.Models.Agents;

/// <summary>
/// List of agents
/// </summary>
public class Agents
{
	/// <summary>
	/// Agents
	/// </summary>
	public Agent[] AgentsList { get; set; } = [];

	/// <summary>
	/// Navigation links
	/// </summary>
	[JsonPropertyName("_links")]
	public Links? Links { get; set; }
}