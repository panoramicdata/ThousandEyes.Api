using ThousandEyes.Api.Models.Emulation;

namespace ThousandEyes.Api.Interfaces;

/// <summary>
/// Interface for User Agents API operations
/// </summary>
/// <remarks>
/// Phase 6.4 implementation - User agent string management
/// </remarks>
public interface IUserAgentsApi
{
	/// <summary>
	/// Retrieves a list of user-agent strings
	/// </summary>
	/// <param name="aid">Account group ID (optional)</param>
	/// <param name="cancellationToken">Cancellation token</param>
	/// <returns>List of user-agent strings</returns>
	Task<UserAgents> GetAllAsync(string? aid, CancellationToken cancellationToken);
}