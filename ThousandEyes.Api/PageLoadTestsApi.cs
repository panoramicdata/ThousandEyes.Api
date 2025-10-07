using ThousandEyes.Api.Interfaces;
using ThousandEyes.Api.Models.Tests;

namespace ThousandEyes.Api;

/// <summary>
/// Implementation of Page Load Tests API using Refit
/// </summary>
internal class PageLoadTestsApi(IPageLoadTestsRefitApi refitApi) : IPageLoadTestsApi
{
	private readonly IPageLoadTestsRefitApi _refitApi = refitApi;

	/// <inheritdoc />
	public Task<PageLoadTests> GetAllAsync(string? aid, CancellationToken cancellationToken) =>
		_refitApi.GetAllAsync(aid, cancellationToken);
}