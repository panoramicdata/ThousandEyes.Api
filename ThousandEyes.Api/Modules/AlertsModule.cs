using Refit;

namespace ThousandEyes.Api.Modules;

/// <summary>
/// Alerts API module for alert management and notification workflows
/// </summary>
/// <remarks>
/// Planned for Phase 3 implementation
/// Important for alerting and notification functionality
/// </remarks>
public class AlertsModule
{
	/// <summary>
	/// Initializes a new instance of the AlertsModule
	/// </summary>
	/// <param name="httpClient">The HTTP client to use for API calls</param>
	/// <param name="refitSettings">Refit settings for JSON serialization</param>
	public AlertsModule(HttpClient httpClient, RefitSettings refitSettings)
	{
		// TODO: Phase 3 - Implement Alerts API
		// Will include:
		// - Alert management
		// - Alert rules and conditions
		// - Alert notifications (email, webhook, integrations)
		// - Alert metrics and thresholds
		// - Alert history and clearing
		throw new NotImplementedException("Alerts API will be implemented in Phase 3");
	}
}