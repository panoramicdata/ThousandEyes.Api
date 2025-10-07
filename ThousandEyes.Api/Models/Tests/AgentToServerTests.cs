namespace ThousandEyes.Api.Models.Tests;

/// <summary>
/// Agent to Server tests response wrapper
/// </summary>
public class AgentToServerTests
{
	/// <summary>
	/// List of Agent to Server tests
	/// </summary>
	public AgentToServerTest[] Tests { get; set; } = [];

	/// <summary>
	/// Navigation links
	/// </summary>
	public TestLinks? Links { get; set; }
}