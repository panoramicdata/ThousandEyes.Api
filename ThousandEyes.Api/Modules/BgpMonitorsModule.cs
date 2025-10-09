using Refit;
using ThousandEyes.Api.Implementations.BgpMonitors;
using ThousandEyes.Api.Interfaces.BgpMonitors;
using ThousandEyes.Api.Refit.BgpMonitors;

namespace ThousandEyes.Api.Modules;

/// <summary>
/// BGP Monitors API module for network infrastructure monitoring
/// </summary>
/// <remarks>
/// ? Phase 4 - IMPLEMENTED
/// Specialized monitoring for BGP routing information including:
/// - Public BGP monitors (global routing visibility)
/// - Private BGP monitors (custom/internal routing)
/// - Monitor location and network information
/// - AS (Autonomous System) information
/// </remarks>
public class BgpMonitorsModule
{
	/// <summary>
	/// Gets the BGP Monitors interface for monitor operations
	/// </summary>
	public IBgpMonitors BgpMonitors { get; }

	/// <summary>
	/// Initializes a new instance of the BgpMonitorsModule
	/// </summary>
	/// <param name="httpClient">The HTTP client to use for API calls</param>
	/// <param name="refitSettings">Refit settings for JSON serialization</param>
	public BgpMonitorsModule(HttpClient httpClient, RefitSettings refitSettings)
	{
		var refitApi = RestService.For<IBgpMonitorsRefitApi>(httpClient, refitSettings);
		BgpMonitors = new BgpMonitorsImpl(refitApi);
	}
}