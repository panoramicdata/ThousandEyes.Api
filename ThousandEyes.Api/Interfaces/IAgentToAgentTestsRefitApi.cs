using Refit;
using ThousandEyes.Api.Models.Tests;

namespace ThousandEyes.Api.Interfaces;

/// <summary>
/// Internal Refit interface for Agent to Agent Tests API
/// </summary>
internal interface IAgentToAgentTestsRefitApi
{
	/// <summary>
	/// Get all Agent to Agent tests
	/// </summary>
	[Get("/tests/agent-to-agent")]
	Task<AgentToAgentTests> GetAllAsync([Query] string? aid, CancellationToken cancellationToken);
}