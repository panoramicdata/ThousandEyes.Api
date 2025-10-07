using Refit;
using ThousandEyes.Api.Interfaces;

namespace ThousandEyes.Api.Modules;

/// <summary>
/// Alerts API module for alert management and notifications
/// </summary>
/// <remarks>
/// Phase 3 implementation - Complete alert and notification functionality
/// Essential for monitoring automation and incident response
/// </remarks>
public class AlertsModule
{
	/// <summary>
	/// Gets the Alerts API for retrieving alert data
	/// </summary>
	public IAlertsApi Alerts { get; }

	/// <summary>
	/// Gets the Alert Rules API for managing alert rules and configurations
	/// </summary>
	public IAlertRulesApi AlertRules { get; }

	/// <summary>
	/// Initializes a new instance of the AlertsModule
	/// </summary>
	/// <param name="httpClient">The HTTP client to use for API calls</param>
	/// <param name="refitSettings">Refit settings for JSON serialization</param>
	public AlertsModule(HttpClient httpClient, RefitSettings refitSettings)
	{
		// Create Refit API interfaces
		var alertsRefitApi = RestService.For<IAlertsRefitApi>(httpClient, refitSettings);
		var alertRulesRefitApi = RestService.For<IAlertRulesRefitApi>(httpClient, refitSettings);

		// Initialize API implementations
		Alerts = new AlertsApi(alertsRefitApi);
		AlertRules = new AlertRulesApi(alertRulesRefitApi);
	}
}