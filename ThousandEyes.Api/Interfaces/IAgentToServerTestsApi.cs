using ThousandEyes.Api.Models.Tests;

namespace ThousandEyes.Api.Interfaces;

/// <summary>
/// Interface for Agent to Server Tests API operations
/// </summary>
/// <remarks>
/// Phase 2 implementation - Network connectivity tests between agents and servers
/// </remarks>
public interface IAgentToServerTestsApi
{
	/// <summary>
	/// Get all Agent to Server tests
	/// </summary>
	/// <param name="aid">Account group ID (optional)</param>
	/// <param name="cancellationToken">Cancellation token</param>
	/// <returns>List of Agent to Server tests</returns>
	Task<AgentToServerTests> GetAllAsync(string? aid, CancellationToken cancellationToken);
}