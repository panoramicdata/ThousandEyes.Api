using Refit;
using ThousandEyes.Api.Models.Alerts;

namespace ThousandEyes.Api.Interfaces;

/// <summary>
/// Internal Refit interface for Alerts API
/// </summary>
internal interface IAlertsRefitApi
{
	/// <summary>
	/// Get all alerts
	/// </summary>
	[Get("/alerts")]
	Task<Alerts> GetAllAsync([Query] string? aid, [Query] string? window, [Query("from")] DateTime? fromDate, [Query("to")] DateTime? toDate, CancellationToken cancellationToken);

	/// <summary>
	/// Get alert by ID
	/// </summary>
	[Get("/alerts/{alertId}")]
	Task<Alert> GetByIdAsync(string alertId, [Query] string? aid, CancellationToken cancellationToken);
}