using ThousandEyes.Api.Interfaces.InternetInsights;
using ThousandEyes.Api.Models.InternetInsights;
using ThousandEyes.Api.Refit.InternetInsights;

namespace ThousandEyes.Api.Implementations.InternetInsights;

/// <summary>
/// Implementation of Catalog Providers operations
/// </summary>
internal class CatalogProvidersImpl(ICatalogProvidersRefitApi refitApi) : ICatalogProviders
{
	private readonly ICatalogProvidersRefitApi _refitApi = refitApi;

	public async Task<CatalogProviderResponse> FilterAsync(
		CatalogProviderFilter filter,
		string? aid,
		CancellationToken cancellationToken)
		=> await _refitApi.FilterAsync(filter, aid, cancellationToken);

	public async Task<CatalogProviderDetails> GetByIdAsync(
		string providerId,
		string? aid,
		CancellationToken cancellationToken)
		=> await _refitApi.GetByIdAsync(providerId, aid, cancellationToken);
}
