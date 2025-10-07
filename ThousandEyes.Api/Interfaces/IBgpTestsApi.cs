using ThousandEyes.Api.Models.Tests;

namespace ThousandEyes.Api.Interfaces;

/// <summary>
/// Interface for BGP Tests API operations
/// </summary>
/// <remarks>
/// Phase 2 implementation - BGP routing and reachability tests
/// </remarks>
public interface IBgpTestsApi
{
	/// <summary>
	/// Get all BGP tests
	/// </summary>
	/// <param name="aid">Account group ID (optional)</param>
	/// <param name="cancellationToken">Cancellation token</param>
	/// <returns>List of BGP tests</returns>
	Task<BgpTests> GetAllAsync(string? aid, CancellationToken cancellationToken);
}