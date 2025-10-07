using ThousandEyes.Api.Interfaces;
using ThousandEyes.Api.Models.Tests;

namespace ThousandEyes.Api;

/// <summary>
/// Implementation of Agent to Agent Tests API using Refit
/// </summary>
internal class AgentToAgentTestsApi(IAgentToAgentTestsRefitApi refitApi) : IAgentToAgentTestsApi
{
	private readonly IAgentToAgentTestsRefitApi _refitApi = refitApi;

	/// <inheritdoc />
	public Task<AgentToAgentTests> GetAllAsync(string? aid, CancellationToken cancellationToken) =>
		_refitApi.GetAllAsync(aid, cancellationToken);
}