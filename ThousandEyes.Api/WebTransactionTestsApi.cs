using ThousandEyes.Api.Interfaces;
using ThousandEyes.Api.Models.Tests;

namespace ThousandEyes.Api;

/// <summary>
/// Implementation of Web Transaction Tests API using Refit
/// </summary>
internal class WebTransactionTestsApi(IWebTransactionTestsRefitApi refitApi) : IWebTransactionTestsApi
{
	private readonly IWebTransactionTestsRefitApi _refitApi = refitApi;

	/// <inheritdoc />
	public Task<WebTransactionTests> GetAllAsync(string? aid, CancellationToken cancellationToken) =>
		_refitApi.GetAllAsync(aid, cancellationToken);
}