using ThousandEyes.Api.Models.Tests;

namespace ThousandEyes.Api.Interfaces;

/// <summary>
/// Interface for general Tests API operations
/// </summary>
public interface ITestsApi
{
	/// <summary>
	/// Get all tests configured in the account
	/// </summary>
	/// <param name="aid">Account group ID (optional)</param>
	/// <param name="cancellationToken">Cancellation token</param>
	/// <returns>List of all tests</returns>
	Task<Tests> GetAllAsync(string? aid, CancellationToken cancellationToken);

	/// <summary>
	/// Get test version history for a specific test
	/// </summary>
	/// <param name="testId">Test ID</param>
	/// <param name="aid">Account group ID (optional)</param>
	/// <param name="limit">Maximum number of versions to return (default 50, max 500)</param>
	/// <param name="cancellationToken">Cancellation token</param>
	/// <returns>Test version history</returns>
	Task<TestVersionHistoryResponse> GetVersionHistoryAsync(
		string testId,
		string? aid,
		int? limit,
		CancellationToken cancellationToken);
}