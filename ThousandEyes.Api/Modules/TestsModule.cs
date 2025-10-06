using Refit;

namespace ThousandEyes.Api.Modules;

/// <summary>
/// Tests API module for managing ThousandEyes tests
/// </summary>
/// <remarks>
/// Planned for Phase 2 implementation
/// Provides comprehensive test management functionality
/// </remarks>
public class TestsModule
{
	/// <summary>
	/// Initializes a new instance of the TestsModule
	/// </summary>
	/// <param name="httpClient">The HTTP client to use for API calls</param>
	/// <param name="refitSettings">Refit settings for JSON serialization</param>
	public TestsModule(HttpClient httpClient, RefitSettings refitSettings)
	{
		// TODO: Phase 2 - Implement Tests API
		// Will include:
		// - HTTP Server tests
		// - Page Load tests  
		// - Web Transaction tests
		// - Agent-to-Server tests
		// - Agent-to-Agent tests
		// - BGP tests
		// - DNS tests
		// - Voice (RTP Stream) tests
		// - SIP Server tests
		throw new NotImplementedException("Tests API will be implemented in Phase 2");
	}
}