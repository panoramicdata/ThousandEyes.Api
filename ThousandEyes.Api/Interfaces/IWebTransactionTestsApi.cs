using ThousandEyes.Api.Models.Tests;

namespace ThousandEyes.Api.Interfaces;

/// <summary>
/// Interface for Web Transaction Tests API operations
/// </summary>
/// <remarks>
/// Phase 2 implementation - Browser transaction automation tests
/// </remarks>
public interface IWebTransactionTestsApi
{
	/// <summary>
	/// Get all Web Transaction tests
	/// </summary>
	/// <param name="aid">Account group ID (optional)</param>
	/// <param name="cancellationToken">Cancellation token</param>
	/// <returns>List of Web Transaction tests</returns>
	Task<WebTransactionTests> GetAllAsync(string? aid, CancellationToken cancellationToken);
}