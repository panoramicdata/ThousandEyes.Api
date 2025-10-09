using Refit;
using ThousandEyes.Api.Models.InternetInsights;

namespace ThousandEyes.Api.Refit.InternetInsights;

/// <summary>
/// Internal Refit interface for Catalog Providers API
/// </summary>
internal interface ICatalogProvidersRefitApi
{
	/// <summary>
	/// Filter catalog providers
	/// </summary>
	[Post("/internet-insights/catalog/providers/filter")]
	Task<CatalogProviderResponse> FilterAsync(
		[Body] CatalogProviderFilter filter,
		[Query] string? aid,
		CancellationToken cancellationToken);

	/// <summary>
	/// Get catalog provider by ID
	/// </summary>
	[Get("/internet-insights/catalog/providers/{providerId}")]
	Task<CatalogProviderDetails> GetByIdAsync(
		string providerId,
		[Query] string? aid,
		CancellationToken cancellationToken);
}
