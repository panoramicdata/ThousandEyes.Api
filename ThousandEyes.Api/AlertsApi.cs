using ThousandEyes.Api.Interfaces;
using ThousandEyes.Api.Models.Alerts;

namespace ThousandEyes.Api;

/// <summary>
/// Implementation of Alerts API using Refit
/// </summary>
internal class AlertsApi(IAlertsRefitApi refitApi) : IAlertsApi
{
	private readonly IAlertsRefitApi _refitApi = refitApi;

	/// <inheritdoc />
	public Task<Alerts> GetAllAsync(string? aid, string? window, DateTime? fromDate, DateTime? toDate, CancellationToken cancellationToken) =>
		_refitApi.GetAllAsync(aid, window, fromDate, toDate, cancellationToken);

	/// <inheritdoc />
	public Task<Alert> GetByIdAsync(string alertId, string? aid, CancellationToken cancellationToken) =>
		_refitApi.GetByIdAsync(alertId, aid, cancellationToken);
}