namespace ThousandEyes.Api.Models.AccountGroups;

/// <summary>
/// Enterprise agent information
/// </summary>
public class EnterpriseAgent
{
	/// <summary>
	/// Agent ID
	/// </summary>
	public string AgentId { get; set; } = "";

	/// <summary>
	/// Agent name
	/// </summary>
	public string AgentName { get; set; } = "";

	/// <summary>
	/// Agent location
	/// </summary>
	public string? Location { get; set; }

	/// <summary>
	/// Whether the agent is enabled
	/// </summary>
	public bool Enabled { get; set; }
}
