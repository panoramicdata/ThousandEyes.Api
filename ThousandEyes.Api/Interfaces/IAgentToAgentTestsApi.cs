using ThousandEyes.Api.Models.Tests;

namespace ThousandEyes.Api.Interfaces;

/// <summary>
/// Interface for Agent to Agent Tests API operations
/// </summary>
/// <remarks>
/// Phase 2 implementation - Point-to-point network tests between agents
/// </remarks>
public interface IAgentToAgentTestsApi
{
	/// <summary>
	/// Get all Agent to Agent tests
	/// </summary>
	/// <param name="aid">Account group ID (optional)</param>
	/// <param name="cancellationToken">Cancellation token</param>
	/// <returns>List of Agent to Agent tests</returns>
	Task<AgentToAgentTests> GetAllAsync(string? aid, CancellationToken cancellationToken);
}