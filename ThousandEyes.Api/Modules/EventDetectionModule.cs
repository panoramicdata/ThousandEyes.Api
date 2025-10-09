using Refit;
using ThousandEyes.Api.Implementations.EventDetection;
using ThousandEyes.Api.Interfaces.EventDetection;
using ThousandEyes.Api.Refit.EventDetection;

namespace ThousandEyes.Api.Modules;

/// <summary>
/// Event Detection API module for automated anomaly detection
/// </summary>
/// <remarks>
/// ? Phase 4.3 - IMPLEMENTED
/// Automated event detection including:
/// - Event list retrieval with time-based filtering
/// - Detailed event information
/// - Affected tests, targets, and agents tracking
/// - Event grouping by type (target, network, proxy, DNS, agent)
/// </remarks>
public class EventDetectionModule
{
	/// <summary>
	/// Gets the Events interface for event operations
	/// </summary>
	public IEvents Events { get; }

	/// <summary>
	/// Initializes a new instance of the EventDetectionModule
	/// </summary>
	/// <param name="httpClient">The HTTP client to use for API calls</param>
	/// <param name="refitSettings">Refit settings for JSON serialization</param>
	public EventDetectionModule(HttpClient httpClient, RefitSettings refitSettings)
	{
		var refitApi = RestService.For<IEventsRefitApi>(httpClient, refitSettings);
		Events = new EventsImpl(refitApi);
	}
}
