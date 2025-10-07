using ThousandEyes.Api.Models.Tests;

namespace ThousandEyes.Api.Interfaces;

/// <summary>
/// Interface for Page Load Tests API operations
/// </summary>
/// <remarks>
/// Phase 2 implementation - Browser-based page loading tests
/// </remarks>
public interface IPageLoadTestsApi
{
	/// <summary>
	/// Get all Page Load tests
	/// </summary>
	/// <param name="aid">Account group ID (optional)</param>
	/// <param name="cancellationToken">Cancellation token</param>
	/// <returns>List of Page Load tests</returns>
	Task<PageLoadTests> GetAllAsync(string? aid, CancellationToken cancellationToken);
}