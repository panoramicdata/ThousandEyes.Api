using Refit;
using ThousandEyes.Api.Implementations.InternetInsights;
using ThousandEyes.Api.Interfaces.InternetInsights;
using ThousandEyes.Api.Refit.InternetInsights;

namespace ThousandEyes.Api.Modules;

/// <summary>
/// Internet Insights API module for global internet health monitoring
/// </summary>
/// <remarks>
/// ? Phase 4.2 - IMPLEMENTED
/// Global internet health monitoring including:
/// - Catalog provider discovery and management
/// - Network and application outage tracking
/// - Provider location and ASN information
/// - Outage impact analysis
/// </remarks>
public class InternetInsightsModule
{
	/// <summary>
	/// Gets the Catalog Providers interface for provider operations
	/// </summary>
	public ICatalogProviders CatalogProviders { get; }

	/// <summary>
	/// Gets the Outages interface for outage operations
	/// </summary>
	public IOutages Outages { get; }

	/// <summary>
	/// Initializes a new instance of the InternetInsightsModule
	/// </summary>
	/// <param name="httpClient">The HTTP client to use for API calls</param>
	/// <param name="refitSettings">Refit settings for JSON serialization</param>
	public InternetInsightsModule(HttpClient httpClient, RefitSettings refitSettings)
	{
		var catalogProvidersApi = RestService.For<ICatalogProvidersRefitApi>(httpClient, refitSettings);
		CatalogProviders = new CatalogProvidersImpl(catalogProvidersApi);

		var outagesApi = RestService.For<IOutagesRefitApi>(httpClient, refitSettings);
		Outages = new OutagesImpl(outagesApi);
	}
}
