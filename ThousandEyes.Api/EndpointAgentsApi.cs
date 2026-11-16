using ThousandEyes.Api.Interfaces;
using ThousandEyes.Api.Models.EndpointAgents;

namespace ThousandEyes.Api;

/// <summary>
/// Implementation of Endpoint Agents API using Refit
/// </summary>
internal class EndpointAgentsApi(IEndpointAgentsRefitApi refitApi) : IEndpointAgentsApi
{
	private readonly IEndpointAgentsRefitApi _refitApi = refitApi;

	/// <inheritdoc />
	public Task<EndpointAgents> GetAllAsync(EndpointAgentListOptions? options, CancellationToken cancellationToken)
	{
		var o = options ?? new EndpointAgentListOptions();
		return _refitApi.GetAllAsync(o.Aid, o.Max, o.Cursor, o.Expand, o.IncludeDeleted, o.UseAllPermittedAids, o.AgentName, o.ComputerName, cancellationToken);
	}

	/// <inheritdoc />
	public Task<EndpointAgent> GetByIdAsync(
		string agentId,
		string? aid,
		ExpandEndpointAgentOptions[]? expand,
		bool? includeDeleted,
		CancellationToken cancellationToken) =>
		_refitApi.GetByIdAsync(agentId, aid, expand, includeDeleted, cancellationToken);

	/// <inheritdoc />
	public Task<EndpointAgent> UpdateAsync(
		string agentId,
		EndpointAgentUpdate request,
		string? aid,
		ExpandEndpointAgentOptions[]? expand,
		CancellationToken cancellationToken) =>
		_refitApi.UpdateAsync(agentId, request, aid, expand, cancellationToken);

	/// <inheritdoc />
	public Task DeleteAsync(string agentId, string? aid, CancellationToken cancellationToken) =>
		_refitApi.DeleteAsync(agentId, aid, cancellationToken);

	/// <inheritdoc />
	public Task<EndpointAgents> FilterAsync(
		AgentSearchRequest request,
		string? aid,
		int? max,
		string? cursor,
		ExpandEndpointAgentOptions[]? expand,
		bool? includeDeleted,
		CancellationToken cancellationToken) =>
		_refitApi.FilterAsync(request, aid, max, cursor, expand, includeDeleted, cancellationToken);

	/// <inheritdoc />
	public Task<ConnectionString> GetConnectionStringAsync(string? aid, CancellationToken cancellationToken) =>
		_refitApi.GetConnectionStringAsync(aid, cancellationToken);

	/// <inheritdoc />
	public Task<EndpointAgent> EnableAsync(string agentId, string? aid, CancellationToken cancellationToken) =>
		_refitApi.EnableAsync(agentId, aid, cancellationToken);

	/// <inheritdoc />
	public Task<EndpointAgent> DisableAsync(string agentId, string? aid, CancellationToken cancellationToken) =>
		_refitApi.DisableAsync(agentId, aid, cancellationToken);

	/// <inheritdoc />
	public Task TransferAsync(string agentId, AgentTransferRequest request, string? aid, CancellationToken cancellationToken) =>
		_refitApi.TransferAsync(agentId, request, aid, cancellationToken);
}