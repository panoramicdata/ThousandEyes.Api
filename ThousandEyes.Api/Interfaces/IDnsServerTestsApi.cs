using ThousandEyes.Api.Models.Tests;

namespace ThousandEyes.Api.Interfaces;

/// <summary>
/// Interface for DNS Server Tests API operations
/// </summary>
/// <remarks>
/// Phase 2 implementation - DNS resolution and server response tests
/// </remarks>
public interface IDnsServerTestsApi
{
	/// <summary>
	/// Get all DNS Server tests
	/// </summary>
	/// <param name="aid">Account group ID (optional)</param>
	/// <param name="cancellationToken">Cancellation token</param>
	/// <returns>List of DNS Server tests</returns>
	Task<DnsServerTests> GetAllAsync(string? aid, CancellationToken cancellationToken);
}