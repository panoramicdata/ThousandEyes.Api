using Refit;
using ThousandEyes.Api.Models.EndpointAgents;

namespace ThousandEyes.Api.Interfaces;

/// <summary>
/// Internal Refit interface for Endpoint Agents API
/// </summary>
internal interface IEndpointAgentsRefitApi
{
	/// <summary>
	/// Get all endpoint agents
	/// </summary>
	[Get("/endpoint/agents")]
	Task<EndpointAgents> GetAllAsync(
		[Query] string? aid,
		[Query] int? max,
		[Query] string? cursor,
		[Query] ExpandEndpointAgentOptions[]? expand,
		[Query] bool? includeDeleted,
		[Query] bool? useAllPermittedAids,
		[Query] string? agentName,
		[Query] string? computerName,
		CancellationToken cancellationToken);

	/// <summary>
	/// Get endpoint agent by ID
	/// </summary>
	[Get("/endpoint/agents/{agentId}")]
	Task<EndpointAgent> GetByIdAsync(
		string agentId,
		[Query] string? aid,
		[Query] ExpandEndpointAgentOptions[]? expand,
		[Query] bool? includeDeleted,
		CancellationToken cancellationToken);

	/// <summary>
	/// Update endpoint agent
	/// </summary>
	[Patch("/endpoint/agents/{agentId}")]
	Task<EndpointAgent> UpdateAsync(
		string agentId,
		[Body] EndpointAgentUpdate request,
		[Query] string? aid,
		[Query] ExpandEndpointAgentOptions[]? expand,
		CancellationToken cancellationToken);

	/// <summary>
	/// Delete endpoint agent
	/// </summary>
	[Delete("/endpoint/agents/{agentId}")]
	Task DeleteAsync(string agentId, [Query] string? aid, CancellationToken cancellationToken);

	/// <summary>
	/// Filter endpoint agents
	/// </summary>
	[Post("/endpoint/agents/filter")]
	Task<EndpointAgents> FilterAsync(
		[Body] AgentSearchRequest request,
		[Query] string? aid,
		[Query] int? max,
		[Query] string? cursor,
		[Query] ExpandEndpointAgentOptions[]? expand,
		[Query] bool? includeDeleted,
		CancellationToken cancellationToken);

	/// <summary>
	/// Get connection string
	/// </summary>
	[Get("/endpoint/agents/connection-string")]
	Task<ConnectionString> GetConnectionStringAsync([Query] string? aid, CancellationToken cancellationToken);

	/// <summary>
	/// Enable endpoint agent
	/// </summary>
	[Post("/endpoint/agents/{agentId}/enable")]
	Task<EndpointAgent> EnableAsync(string agentId, [Query] string? aid, CancellationToken cancellationToken);

	/// <summary>
	/// Disable endpoint agent
	/// </summary>
	[Post("/endpoint/agents/{agentId}/disable")]
	Task<EndpointAgent> DisableAsync(string agentId, [Query] string? aid, CancellationToken cancellationToken);

	/// <summary>
	/// Transfer endpoint agent
	/// </summary>
	[Post("/endpoint/agents/{agentId}/transfer")]
	Task TransferAsync(string agentId, [Body] AgentTransferRequest request, [Query] string? aid, CancellationToken cancellationToken);
}