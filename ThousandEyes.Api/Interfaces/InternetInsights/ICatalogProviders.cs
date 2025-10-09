using ThousandEyes.Api.Models.InternetInsights;

namespace ThousandEyes.Api.Interfaces.InternetInsights;

/// <summary>
/// Public interface for Catalog Providers operations
/// </summary>
public interface ICatalogProviders
{
	/// <summary>
	/// Returns a list of catalog providers using the specified filters.
	/// Returns high-level information about each catalog provider.
	/// </summary>
	/// <param name="filter">Filter criteria for catalog providers</param>
	/// <param name="aid">Optional account group ID for context</param>
	/// <param name="cancellationToken">Cancellation token</param>
	/// <returns>List of catalog providers matching the filter criteria</returns>
	Task<CatalogProviderResponse> FilterAsync(
		CatalogProviderFilter filter,
		string? aid,
		CancellationToken cancellationToken);

	/// <summary>
	/// Returns the details of a catalog provider.
	/// </summary>
	/// <param name="providerId">The catalog provider ID</param>
	/// <param name="aid">Optional account group ID for context</param>
	/// <param name="cancellationToken">Cancellation token</param>
	/// <returns>Detailed catalog provider information</returns>
	Task<CatalogProviderDetails> GetByIdAsync(
		string providerId,
		string? aid,
		CancellationToken cancellationToken);
}
