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
	/// Gets the Reports API for report management and scheduling
	/// </summary>
	public IReportsApi Reports { get; }

	/// <summary>
	/// Initializes a new instance of the DashboardsModule
	/// </summary>
	/// <param name="httpClient">The HTTP client to use for API calls</param>
	/// <param name="refitSettings">Refit settings for JSON serialization</param>
	public DashboardsModule(HttpClient httpClient, RefitSettings refitSettings)
	{
		// Create Refit API interfaces
		var dashboardsRefitApi = RestService.For<IDashboardsRefitApi>(httpClient, refitSettings);
		var reportsRefitApi = RestService.For<IReportsRefitApi>(httpClient, refitSettings);

		// Initialize API implementations
		Dashboards = new DashboardsApi(dashboardsRefitApi);
		Reports = new ReportsApi(reportsRefitApi);
	}
}