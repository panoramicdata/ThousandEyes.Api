using Refit;

namespace ThousandEyes.Api.Modules;

/// <summary>
/// Test Results API module for retrieving monitoring data and metrics
/// </summary>
/// <remarks>
/// Planned for Phase 2 implementation
/// Essential for monitoring data retrieval and analysis
/// </remarks>
public class TestResultsModule
{
	/// <summary>
	/// Initializes a new instance of the TestResultsModule
	/// </summary>
	/// <param name="httpClient">The HTTP client to use for API calls</param>
	/// <param name="refitSettings">Refit settings for JSON serialization</param>
	public TestResultsModule(HttpClient httpClient, RefitSettings refitSettings)
	{
		// TODO: Phase 2 - Implement Test Results API
		// Will include:
		// - Network test results
		// - HTTP Server results
		// - Page Load results
		// - Web Transaction results
		// - Path visualization results
		// - Real-time and historical data
		throw new NotImplementedException("Test Results API will be implemented in Phase 2");
	}
}