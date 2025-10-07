using ThousandEyes.Api.Interfaces;
using ThousandEyes.Api.Models.Tests;

namespace ThousandEyes.Api;

/// <summary>
/// Implementation of DNS Server Tests API using Refit
/// </summary>
internal class DnsServerTestsApi(IDnsServerTestsRefitApi refitApi) : IDnsServerTestsApi
{
	private readonly IDnsServerTestsRefitApi _refitApi = refitApi;

	/// <inheritdoc />
	public Task<DnsServerTests> GetAllAsync(string? aid, CancellationToken cancellationToken) =>
		_refitApi.GetAllAsync(aid, cancellationToken);
}