using Refit;
using ThousandEyes.Api.Models.Dashboards;

namespace ThousandEyes.Api.Interfaces;

/// <summary>
/// Internal Refit interface for Dashboard Snapshots API
/// </summary>
internal interface IDashboardSnapshotsRefitApi
{
	/// <summary>
	/// Get all dashboard snapshots
	/// </summary>
	[Get("/dashboard-snapshots")]
	Task<DashboardSnapshotsPage> GetAllAsync([Query] string? aid, [Query] string? dashboardId, [Query] string? cursor, CancellationToken cancellationToken);

	/// <summary>
	/// Get dashboard snapshot by ID
	/// </summary>
	[Get("/dashboard-snapshots/{snapshotId}")]
	Task<DashboardSnapshot> GetByIdAsync(string snapshotId, [Query] string? aid, CancellationToken cancellationToken);

	/// <summary>
	/// Create dashboard snapshot
	/// </summary>
	[Post("/dashboard-snapshots")]
	Task<DashboardSnapshotResponse> CreateAsync([Body] CreateDashboardSnapshotRequest request, [Query] string? aid, CancellationToken cancellationToken);

	/// <summary>
	/// Update snapshot expiration date
	/// </summary>
	[Patch("/dashboard-snapshots/{snapshotId}")]
	Task UpdateExpirationAsync(string snapshotId, [Body] UpdateSnapshotExpirationRequest request, [Query] string? aid, CancellationToken cancellationToken);

	/// <summary>
	/// Delete dashboard snapshot
	/// </summary>
	[Delete("/dashboard-snapshots/{snapshotId}")]
	Task DeleteAsync(string snapshotId, [Query] string? aid, CancellationToken cancellationToken);

	/// <summary>
	/// Get widget data from snapshot
	/// </summary>
	[Get("/dashboard-snapshots/{snapshotId}/widgets/{widgetId}")]
	Task<WidgetDataSnapshot> GetWidgetDataAsync(string snapshotId, string widgetId, [Query] string? aid, CancellationToken cancellationToken);
}
