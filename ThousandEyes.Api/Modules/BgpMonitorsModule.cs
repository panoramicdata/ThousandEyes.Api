using Refit;

namespace ThousandEyes.Api.Modules;

/// <summary>
/// BGP Monitors API module for network infrastructure monitoring
/// </summary>
/// <remarks>
/// Planned for Phase 4 implementation
/// Specialized monitoring for BGP routing information
/// </remarks>
public class BgpMonitorsModule
{
	/// <summary>
	/// Initializes a new instance of the BgpMonitorsModule
	/// </summary>
	/// <param name="httpClient">The HTTP client to use for API calls</param>
	/// <param name="refitSettings">Refit settings for JSON serialization</param>
	public BgpMonitorsModule(HttpClient httpClient, RefitSettings refitSettings)
	{
		// TODO: Phase 4 - Implement BGP Monitors API
		// Will include:
		// - BGP monitor management
		// - BGP route information
		// - BGP path analysis
		// - AS path information
		// - BGP community data
		throw new NotImplementedException("BGP Monitors API will be implemented in Phase 4");
	}
}