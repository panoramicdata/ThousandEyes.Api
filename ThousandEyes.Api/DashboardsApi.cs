using ThousandEyes.Api.Interfaces;
using ThousandEyes.Api.Models.Dashboards;

namespace ThousandEyes.Api;

/// <summary>
/// Implementation of Dashboards API using Refit
/// </summary>
internal class DashboardsApi(IDashboardsRefitApi refitApi) : IDashboardsApi
{
	private readonly IDashboardsRefitApi _refitApi = refitApi;

	/// <inheritdoc />
	public Task<Dashboard[]> GetAllAsync(string? aid, CancellationToken cancellationToken) =>
		_refitApi.GetAllAsync(aid, cancellationToken);

	/// <inheritdoc />
	public Task<Dashboard> GetByIdAsync(string dashboardId, string? aid, CancellationToken cancellationToken) =>
		_refitApi.GetByIdAsync(dashboardId, aid, cancellationToken);

	/// <inheritdoc />
	public Task<Dashboard> CreateAsync(DashboardRequest request, string? aid, CancellationToken cancellationToken) =>
		_refitApi.CreateAsync(request, aid, cancellationToken);

	/// <inheritdoc />
	public Task<Dashboard> UpdateAsync(string dashboardId, DashboardRequest request, string? aid, CancellationToken cancellationToken) =>
		_refitApi.UpdateAsync(dashboardId, request, aid, cancellationToken);

	/// <inheritdoc />
	public Task DeleteAsync(string dashboardId, string? aid, CancellationToken cancellationToken) =>
		_refitApi.DeleteAsync(dashboardId, aid, cancellationToken);
}