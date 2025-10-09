using ThousandEyes.Api.Models.EndpointAgents;

namespace ThousandEyes.Api.Interfaces;

/// <summary>
/// Interface for Endpoint Agents API operations
/// </summary>
/// <remarks>
/// Phase 6.5 implementation - Endpoint agent management and monitoring
/// </remarks>
public interface IEndpointAgentsApi
{
	/// <summary>
	/// Retrieves a list of endpoint agents in the account group
	/// </summary>
	/// <param name="aid">Account group ID (optional)</param>
	/// <param name="max">Maximum number of objects to return (optional)</param>
	/// <param name="cursor">Pagination cursor (optional)</param>
	/// <param name="expand">Optional expansion parameters</param>
	/// <param name="includeDeleted">Include deleted agents (optional)</param>
	/// <param name="useAllPermittedAids">Load data from all permitted accounts (optional)</param>
	/// <param name="agentName">Filter by agent name (optional)</param>
	/// <param name="computerName">Filter by computer name (optional)</param>
	/// <param name="cancellationToken">Cancellation token</param>
	/// <returns>List of endpoint agents</returns>
	Task<EndpointAgents> GetAllAsync(
		string? aid,
		int? max,
		string? cursor,
		ExpandEndpointAgentOptions[]? expand,
		bool? includeDeleted,
		bool? useAllPermittedAids,
		string? agentName,
		string? computerName,
		CancellationToken cancellationToken);

	/// <summary>
	/// Retrieves details of an agent with the specified agent ID
	/// </summary>
	/// <param name="agentId">Agent ID</param>
	/// <param name="aid">Account group ID (optional)</param>
	/// <param name="expand">Optional expansion parameters</param>
	/// <param name="includeDeleted">Include deleted agents (optional)</param>
	/// <param name="cancellationToken">Cancellation token</param>
	/// <returns>Endpoint agent details</returns>
	Task<EndpointAgent> GetByIdAsync(
		string agentId,
		string? aid,
		ExpandEndpointAgentOptions[]? expand,
		bool? includeDeleted,
		CancellationToken cancellationToken);

	/// <summary>
	/// Updates the agent with the specified agent ID
	/// </summary>
	/// <param name="agentId">Agent ID</param>
	/// <param name="request">Update request</param>
	/// <param name="aid">Account group ID (optional)</param>
	/// <param name="expand">Optional expansion parameters</param>
	/// <param name="cancellationToken">Cancellation token</param>
	/// <returns>Updated agent</returns>
	Task<EndpointAgent> UpdateAsync(
		string agentId,
		EndpointAgentUpdate request,
		string? aid,
		ExpandEndpointAgentOptions[]? expand,
		CancellationToken cancellationToken);

	/// <summary>
	/// Deletes the agent with the specified agent ID
	/// </summary>
	/// <param name="agentId">Agent ID</param>
	/// <param name="aid">Account group ID (optional)</param>
	/// <param name="cancellationToken">Cancellation token</param>
	Task DeleteAsync(string agentId, string? aid, CancellationToken cancellationToken);

	/// <summary>
	/// Filters endpoint agents based on search criteria
	/// </summary>
	/// <param name="request">Filter request</param>
	/// <param name="aid">Account group ID (optional)</param>
	/// <param name="max">Maximum number of objects to return (optional)</param>
	/// <param name="cursor">Pagination cursor (optional)</param>
	/// <param name="expand">Optional expansion parameters</param>
	/// <param name="includeDeleted">Include deleted agents (optional)</param>
	/// <param name="cancellationToken">Cancellation token</param>
	/// <returns>Filtered list of endpoint agents</returns>
	Task<EndpointAgents> FilterAsync(
		AgentSearchRequest request,
		string? aid,
		int? max,
		string? cursor,
		ExpandEndpointAgentOptions[]? expand,
		bool? includeDeleted,
		CancellationToken cancellationToken);

	/// <summary>
	/// Gets the connection string for agent installation
	/// </summary>
	/// <param name="aid">Account group ID (optional)</param>
	/// <param name="cancellationToken">Cancellation token</param>
	/// <returns>Connection string</returns>
	Task<ConnectionString> GetConnectionStringAsync(string? aid, CancellationToken cancellationToken);

	/// <summary>
	/// Enables an endpoint agent
	/// </summary>
	/// <param name="agentId">Agent ID</param>
	/// <param name="aid">Account group ID (optional)</param>
	/// <param name="cancellationToken">Cancellation token</param>
	/// <returns>Updated agent</returns>
	Task<EndpointAgent> EnableAsync(string agentId, string? aid, CancellationToken cancellationToken);

	/// <summary>
	/// Disables an endpoint agent
	/// </summary>
	/// <param name="agentId">Agent ID</param>
	/// <param name="aid">Account group ID (optional)</param>
	/// <param name="cancellationToken">Cancellation token</param>
	/// <returns>Updated agent</returns>
	Task<EndpointAgent> DisableAsync(string agentId, string? aid, CancellationToken cancellationToken);

	/// <summary>
	/// Transfers an agent to another account
	/// </summary>
	/// <param name="agentId">Agent ID</param>
	/// <param name="request">Transfer request</param>
	/// <param name="aid">Account group ID (optional)</param>
	/// <param name="cancellationToken">Cancellation token</param>
	Task TransferAsync(string agentId, AgentTransferRequest request, string? aid, CancellationToken cancellationToken);
}