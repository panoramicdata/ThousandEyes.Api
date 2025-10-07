namespace ThousandEyes.Api.Models.Agents;

/// <summary>
/// Agent request for create/update operations
/// </summary>
public class AgentRequest
{
	/// <summary>
	/// Agent name
	/// </summary>
	public required string AgentName { get; set; }

	/// <summary>
	/// Whether the agent is enabled
	/// </summary>
	public bool Enabled { get; set; } = true;

	/// <summary>
	/// Whether the agent supports IPv6
	/// </summary>
	public bool Ipv6Policy { get; set; }

	/// <summary>
	/// Target for agent-to-agent tests
	/// </summary>
	public string? TargetForTests { get; set; }
}