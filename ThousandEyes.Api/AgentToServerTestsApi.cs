using ThousandEyes.Api.Interfaces;
using ThousandEyes.Api.Models.Tests;

namespace ThousandEyes.Api;

/// <summary>
/// Implementation of Agent to Server Tests API using Refit
/// </summary>
internal class AgentToServerTestsApi(IAgentToServerTestsRefitApi refitApi) : IAgentToServerTestsApi
{
	private readonly IAgentToServerTestsRefitApi _refitApi = refitApi;

	/// <inheritdoc />
	public Task<AgentToServerTests> GetAllAsync(string? aid, CancellationToken cancellationToken) =>
		_refitApi.GetAllAsync(aid, cancellationToken);
}