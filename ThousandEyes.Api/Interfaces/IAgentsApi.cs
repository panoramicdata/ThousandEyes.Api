using ThousandEyes.Api.Models.Agents;

namespace ThousandEyes.Api.Interfaces;

/// <summary>
/// Interface for Agents API operations
/// </summary>
/// <remarks>
/// Phase 2 implementation - Cloud and Enterprise agent management
/// </remarks>
public interface IAgentsApi
{
	/// <summary>
	/// Get all agents
	/// </summary>
	/// <param name="aid">Account group ID (optional)</param>
	/// <param name="cancellationToken">Cancellation token</param>
	/// <returns>List of agents</returns>
	Task<Agents> GetAllAsync(string? aid, CancellationToken cancellationToken);

	/// <summary>
	/// Get a specific agent by ID
	/// </summary>
	/// <param name="agentId">Agent ID</param>
	/// <param name="aid">Account group ID (optional)</param>
	/// <param name="cancellationToken">Cancellation token</param>
	/// <returns>Agent details</returns>
	Task<Agent> GetByIdAsync(string agentId, string? aid, CancellationToken cancellationToken);

	/// <summary>
	/// Update an existing agent
	/// </summary>
	/// <param name="agentId">Agent ID</param>
	/// <param name="request">Updated agent configuration</param>
	/// <param name="aid">Account group ID (optional)</param>
	/// <param name="cancellationToken">Cancellation token</param>
	/// <returns>Updated agent</returns>
	Task<Agent> UpdateAsync(string agentId, AgentRequest request, string? aid, CancellationToken cancellationToken);

	/// <summary>
	/// Delete an agent (Enterprise Agents only)
	/// </summary>
	/// <param name="agentId">Agent ID</param>
	/// <param name="aid">Account group ID (optional)</param>
	/// <param name="cancellationToken">Cancellation token</param>
	Task DeleteAsync(string agentId, string? aid, CancellationToken cancellationToken);

	/// <summary>
	/// Create a new Enterprise Agent
	/// </summary>
	/// <param name="request">Agent configuration</param>
	/// <param name="aid">Account group ID (optional)</param>
	/// <param name="cancellationToken">Cancellation token</param>
	/// <returns>Created agent</returns>
	Task<Agent> CreateAsync(AgentRequest request, string? aid, CancellationToken cancellationToken);

	/// <summary>
	/// Get supported test types for an agent
	/// </summary>
	/// <param name="agentId">Agent ID</param>
	/// <param name="aid">Account group ID (optional)</param>
	/// <param name="cancellationToken">Cancellation token</param>
	/// <returns>List of supported test types</returns>
	Task<string[]> GetSupportedTestsAsync(string agentId, string? aid, CancellationToken cancellationToken);
}