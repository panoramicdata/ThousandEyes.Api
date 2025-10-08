using ThousandEyes.Api.Interfaces;
using ThousandEyes.Api.Models.Dashboards;

namespace ThousandEyes.Api;

/// <summary>
/// Implementation of Dashboard Snapshots API using Refit
/// </summary>
internal class DashboardSnapshotsApi(IDashboardSnapshotsRefitApi refitApi) : IDashboardSnapshotsApi
{
	private readonly IDashboardSnapshotsRefitApi _refitApi = refitApi;

	/// <inheritdoc />
	public Task<DashboardSnapshotsPage> GetAllAsync(string? aid, string? dashboardId, string? cursor, CancellationToken cancellationToken) =>
		_refitApi.GetAllAsync(aid, dashboardId, cursor, cancellationToken);

	/// <inheritdoc />
	public Task<DashboardSnapshot> GetByIdAsync(string snapshotId, string? aid, CancellationToken cancellationToken) =>
		_refitApi.GetByIdAsync(snapshotId, aid, cancellationToken);

	/// <inheritdoc />
	public Task<DashboardSnapshotResponse> CreateAsync(CreateDashboardSnapshotRequest request, string? aid, CancellationToken cancellationToken) =>
		_refitApi.CreateAsync(request, aid, cancellationToken);

	/// <inheritdoc />
	public Task UpdateExpirationAsync(string snapshotId, UpdateSnapshotExpirationRequest request, string? aid, CancellationToken cancellationToken) =>
		_refitApi.UpdateExpirationAsync(snapshotId, request, aid, cancellationToken);

	/// <inheritdoc />
	public Task DeleteAsync(string snapshotId, string? aid, CancellationToken cancellationToken) =>
		_refitApi.DeleteAsync(snapshotId, aid, cancellationToken);

	/// <inheritdoc />
	public Task<WidgetDataSnapshot> GetWidgetDataAsync(string snapshotId, string widgetId, string? aid, CancellationToken cancellationToken) =>
		_refitApi.GetWidgetDataAsync(snapshotId, widgetId, aid, cancellationToken);
}
