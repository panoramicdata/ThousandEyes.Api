using Refit;
using ThousandEyes.Api.Interfaces;

namespace ThousandEyes.Api.Modules;

/// <summary>
/// Test Results API module for retrieving monitoring data
/// </summary>
/// <remarks>
/// Phase 2 implementation - Complete monitoring data retrieval functionality
/// Essential for accessing test results, metrics, and path visualization data
/// </remarks>
public class TestResultsModule
{
	/// <summary>
	/// Gets the Test Results API for retrieving monitoring data
	/// </summary>
	public ITestResultsApi TestResults { get; }

	/// <summary>
	/// Initializes a new instance of the TestResultsModule
	/// </summary>
	/// <param name="httpClient">The HTTP client to use for API calls</param>
	/// <param name="refitSettings">Refit settings for JSON serialization</param>
	public TestResultsModule(HttpClient httpClient, RefitSettings refitSettings)
	{
		// Create Refit API interfaces
		var testResultsRefitApi = RestService.For<ITestResultsRefitApi>(httpClient, refitSettings);

		// Initialize API implementations
		TestResults = new TestResultsApi(testResultsRefitApi);
	}
}