using ThousandEyes.Api.Models.Alerts;

namespace ThousandEyes.Api.Interfaces;

/// <summary>
/// Interface for Alerts API operations
/// </summary>
/// <remarks>
/// Phase 3 implementation - Alert management and notifications
/// </remarks>
public interface IAlertsApi
{
	/// <summary>
	/// Get all alerts
	/// </summary>
	/// <param name="aid">Account group ID (optional)</param>
	/// <param name="window">Time window for alerts (optional, e.g., "1d", "7d")</param>
	/// <param name="fromDate">Start date for alert search (optional)</param>
	/// <param name="toDate">End date for alert search (optional)</param>
	/// <param name="cancellationToken">Cancellation token</param>
	/// <returns>List of alerts</returns>
	Task<Alerts> GetAllAsync(string? aid, string? window, DateTime? fromDate, DateTime? toDate, CancellationToken cancellationToken);

	/// <summary>
	/// Get a specific alert by ID
	/// </summary>
	/// <param name="alertId">Alert ID</param>
	/// <param name="aid">Account group ID (optional)</param>
	/// <param name="cancellationToken">Cancellation token</param>
	/// <returns>Alert details</returns>
	Task<Alert> GetByIdAsync(string alertId, string? aid, CancellationToken cancellationToken);
}