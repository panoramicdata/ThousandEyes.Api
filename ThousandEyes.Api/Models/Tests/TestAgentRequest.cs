namespace ThousandEyes.Api.Models.Tests;

/// <summary>
/// Test agent assignment request
/// </summary>
public class TestAgentRequest
{
	/// <summary>
	/// Agent ID
	/// </summary>
	public required string AgentId { get; set; }

	/// <summary>
	/// Source IP address for interface selection (optional)
	/// </summary>
	public string? SourceIpAddress { get; set; }
}
