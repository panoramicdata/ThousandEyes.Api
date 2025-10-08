using Refit;
using ThousandEyes.Api.Interfaces;

namespace ThousandEyes.Api.Modules;

/// <summary>
/// Dashboards API module for reporting and data visualization
/// </summary>
/// <remarks>
/// Phase 3 implementation - Complete dashboard and reporting functionality
/// Essential for monitoring visualization and automated reporting
/// </remarks>
public class DashboardsModule
{
	/// <summary>
	/// Gets the Dashboards API for dashboard management and configuration
	/// </summary>
	public IDashboardsApi Dashboards { get; }

	/// <summary>
	/// Gets the Dashboard Snapshots API for snapshot management
	/// </summary>
	public IDashboardSnapshotsApi Snapshots { get; }

	/// <summary>
	/// Gets the Dashboard Filters API for filter management
	/// </summary>
	public IDashboardFiltersApi Filters { get; }

	/// <summary>
	/// Initializes a new instance of the DashboardsModule
	/// </summary>
	/// <param name="httpClient">The HTTP client to use for API calls</param>
	/// <param name="refitSettings">Refit settings for JSON serialization</param>
	public DashboardsModule(HttpClient httpClient, RefitSettings refitSettings)
	{
		// Create Refit API interfaces
		var dashboardsRefitApi = RestService.For<IDashboardsRefitApi>(httpClient, refitSettings);
		var snapshotsRefitApi = RestService.For<IDashboardSnapshotsRefitApi>(httpClient, refitSettings);
		var filtersRefitApi = RestService.For<IDashboardFiltersRefitApi>(httpClient, refitSettings);

		// Initialize API implementations
		Dashboards = new DashboardsApi(dashboardsRefitApi);
		Snapshots = new DashboardSnapshotsApi(snapshotsRefitApi);
		Filters = new DashboardFiltersApi(filtersRefitApi);
	}
}