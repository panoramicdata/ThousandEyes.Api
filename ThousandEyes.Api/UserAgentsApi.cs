using ThousandEyes.Api.Interfaces;
using ThousandEyes.Api.Models.Emulation;

namespace ThousandEyes.Api;

/// <summary>
/// Implementation of User Agents API using Refit
/// </summary>
internal class UserAgentsApi(IUserAgentsRefitApi refitApi) : IUserAgentsApi
{
	private readonly IUserAgentsRefitApi _refitApi = refitApi;

	/// <inheritdoc />
	public Task<UserAgents> GetAllAsync(string? aid, CancellationToken cancellationToken) =>
		_refitApi.GetAllAsync(aid, cancellationToken);
}