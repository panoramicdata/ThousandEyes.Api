using ThousandEyes.Api.Models.TestResults;

namespace ThousandEyes.Api.Interfaces;

/// <summary>
/// Interface for Test Results API operations
/// </summary>
/// <remarks>
/// Phase 2 implementation - Monitoring data retrieval and analysis
/// </remarks>
public interface ITestResultsApi
{
	/// <summary>
	/// Get network test results for a specific test
	/// </summary>
	/// <param name="testId">Test ID</param>
	/// <param name="fromDate">Start date (optional)</param>
	/// <param name="toDate">End date (optional)</param>
	/// <param name="aid">Account group ID (optional)</param>
	/// <param name="cancellationToken">Cancellation token</param>
	/// <returns>Network test results</returns>
	Task<NetworkTestResults> GetNetworkResultsAsync(string testId, DateTime? fromDate, DateTime? toDate, string? aid, CancellationToken cancellationToken);

	/// <summary>
	/// Get HTTP Server test results for a specific test
	/// </summary>
	/// <param name="testId">Test ID</param>
	/// <param name="fromDate">Start date (optional)</param>
	/// <param name="toDate">End date (optional)</param>
	/// <param name="aid">Account group ID (optional)</param>
	/// <param name="cancellationToken">Cancellation token</param>
	/// <returns>HTTP Server test results</returns>
	Task<HttpServerTestResults> GetHttpServerResultsAsync(string testId, DateTime? fromDate, DateTime? toDate, string? aid, CancellationToken cancellationToken);

	/// <summary>
	/// Get path visualization results for a specific test
	/// </summary>
	/// <param name="testId">Test ID</param>
	/// <param name="fromDate">Start date (optional)</param>
	/// <param name="toDate">End date (optional)</param>
	/// <param name="aid">Account group ID (optional)</param>
	/// <param name="cancellationToken">Cancellation token</param>
	/// <returns>Path visualization results</returns>
	Task<NetworkTestResults> GetPathVisualizationResultsAsync(string testId, DateTime? fromDate, DateTime? toDate, string? aid, CancellationToken cancellationToken);
}