using Refit;

namespace ThousandEyes.Api.Modules;

/// <summary>
/// Dashboards API module for reporting and data visualization
/// </summary>
/// <remarks>
/// Planned for Phase 3 implementation
/// Provides reporting and dashboard functionality
/// </remarks>
public class DashboardsModule
{
	/// <summary>
	/// Initializes a new instance of the DashboardsModule
	/// </summary>
	/// <param name="httpClient">The HTTP client to use for API calls</param>
	/// <param name="refitSettings">Refit settings for JSON serialization</param>
	public DashboardsModule(HttpClient httpClient, RefitSettings refitSettings)
	{
		// TODO: Phase 3 - Implement Dashboards/Reports API
		// Will include:
		// - Report creation and management
		// - Dashboard widgets and metrics
		// - Report data extraction
		// - Report scheduling and snapshots
		// - Custom data filtering and timespan selection
		throw new NotImplementedException("Dashboards API will be implemented in Phase 3");
	}
}