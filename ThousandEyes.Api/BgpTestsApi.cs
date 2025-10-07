using ThousandEyes.Api.Interfaces;
using ThousandEyes.Api.Models.Tests;

namespace ThousandEyes.Api;

/// <summary>
/// Implementation of BGP Tests API using Refit
/// </summary>
internal class BgpTestsApi(IBgpTestsRefitApi refitApi) : IBgpTestsApi
{
	private readonly IBgpTestsRefitApi _refitApi = refitApi;

	/// <inheritdoc />
	public Task<BgpTests> GetAllAsync(string? aid, CancellationToken cancellationToken) =>
		_refitApi.GetAllAsync(aid, cancellationToken);
}