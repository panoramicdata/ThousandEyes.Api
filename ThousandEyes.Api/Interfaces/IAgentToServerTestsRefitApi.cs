using Refit;
using ThousandEyes.Api.Models.Tests;

namespace ThousandEyes.Api.Interfaces;

/// <summary>
/// Internal Refit interface for Agent to Server Tests API
/// </summary>
internal interface IAgentToServerTestsRefitApi
{
	/// <summary>
	/// Get all Agent to Server tests
	/// </summary>
	[Get("/tests/agent-to-server")]
	Task<AgentToServerTests> GetAllAsync([Query] string? aid, CancellationToken cancellationToken);
}