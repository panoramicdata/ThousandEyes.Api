using Refit;
using ThousandEyes.Api.Models.TestResults;

namespace ThousandEyes.Api.Interfaces;

/// <summary>
/// Internal Refit interface for Test Results API
/// </summary>
internal interface ITestResultsRefitApi
{
	/// <summary>
	/// Get network test results
	/// </summary>
	[Get("/test-results/{testId}/network")]
	Task<NetworkTestResults> GetNetworkResultsAsync(string testId, [Query("from")] DateTime? fromDate, [Query("to")] DateTime? toDate, [Query] string? aid, CancellationToken cancellationToken);

	/// <summary>
	/// Get HTTP Server test results
	/// </summary>
	[Get("/test-results/{testId}/http-server")]
	Task<HttpServerTestResults> GetHttpServerResultsAsync(string testId, [Query("from")] DateTime? fromDate, [Query("to")] DateTime? toDate, [Query] string? aid, CancellationToken cancellationToken);

	/// <summary>
	/// Get path visualization results
	/// </summary>
	[Get("/test-results/{testId}/path-vis")]
	Task<NetworkTestResults> GetPathVisualizationResultsAsync(string testId, [Query("from")] DateTime? fromDate, [Query("to")] DateTime? toDate, [Query] string? aid, CancellationToken cancellationToken);
}