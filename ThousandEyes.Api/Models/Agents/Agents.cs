namespace ThousandEyes.Api.Models.Agents;

/// <summary>
/// Agents response wrapper
/// </summary>
public class Agents
{
	/// <summary>
	/// List of agents
	/// </summary>
	public Agent[] AgentsList { get; set; } = [];

	/// <summary>
	/// Navigation links
	/// </summary>
	public AgentLinks? Links { get; set; }
}