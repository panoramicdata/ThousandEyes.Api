using ThousandEyes.Api.Interfaces;
using ThousandEyes.Api.Models.Agents;

namespace ThousandEyes.Api;

/// <summary>
/// Implementation of Agents API using Refit
/// </summary>
internal class AgentsApi(IAgentsRefitApi refitApi) : IAgentsApi
{
	private readonly IAgentsRefitApi _refitApi = refitApi;

	/// <inheritdoc />
	public Task<Agents> GetAllAsync(string? aid, CancellationToken cancellationToken) =>
		_refitApi.GetAllAsync(aid, cancellationToken);

	/// <inheritdoc />
	public Task<Agent> GetByIdAsync(string agentId, string? aid, CancellationToken cancellationToken) =>
		_refitApi.GetByIdAsync(agentId, aid, cancellationToken);

	/// <inheritdoc />
	public Task<Agent> UpdateAsync(string agentId, AgentRequest request, string? aid, CancellationToken cancellationToken) =>
		_refitApi.UpdateAsync(agentId, request, aid, cancellationToken);

	/// <inheritdoc />
	public Task DeleteAsync(string agentId, string? aid, CancellationToken cancellationToken) =>
		_refitApi.DeleteAsync(agentId, aid, cancellationToken);

	/// <inheritdoc />
	public Task<Agent> CreateAsync(AgentRequest request, string? aid, CancellationToken cancellationToken) =>
		_refitApi.CreateAsync(request, aid, cancellationToken);

	/// <inheritdoc />
	public Task<string[]> GetSupportedTestsAsync(string agentId, string? aid, CancellationToken cancellationToken) =>
		_refitApi.GetSupportedTestsAsync(agentId, aid, cancellationToken);
}