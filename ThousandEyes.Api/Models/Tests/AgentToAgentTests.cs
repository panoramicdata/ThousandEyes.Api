namespace ThousandEyes.Api.Models.Tests;

/// <summary>
/// Agent to Agent tests response wrapper
/// </summary>
public class AgentToAgentTests
{
	/// <summary>
	/// List of Agent to Agent tests
	/// </summary>
	public AgentToAgentTest[] Tests { get; set; } = [];

	/// <summary>
	/// Navigation links
	/// </summary>
	public TestLinks? Links { get; set; }
}