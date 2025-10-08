using ThousandEyes.Api.Models.Dashboards;

namespace ThousandEyes.Api.Interfaces;

/// <summary>
/// Interface for Dashboard Snapshots API operations
/// </summary>
/// <remarks>
/// Phase 3 implementation - Dashboard snapshot management
/// </remarks>
public interface IDashboardSnapshotsApi
{
	/// <summary>
	/// Get all dashboard snapshots
	/// </summary>
	/// <param name="aid">Account group ID (optional)</param>
	/// <param name="dashboardId">Dashboard ID to filter snapshots (optional)</param>
	/// <param name="cursor">Pagination cursor (optional)</param>
	/// <param name="cancellationToken">Cancellation token</param>
	/// <returns>List of dashboard snapshots</returns>
	Task<DashboardSnapshotsPage> GetAllAsync(string? aid, string? dashboardId, string? cursor, CancellationToken cancellationToken);

	/// <summary>
	/// Get a specific dashboard snapshot by ID
	/// </summary>
	/// <param name="snapshotId">Snapshot ID</param>
	/// <param name="aid">Account group ID (optional)</param>
	/// <param name="cancellationToken">Cancellation token</param>
	/// <returns>Dashboard snapshot details</returns>
	Task<DashboardSnapshot> GetByIdAsync(string snapshotId, string? aid, CancellationToken cancellationToken);

	/// <summary>
	/// Create a new dashboard snapshot
	/// </summary>
	/// <param name="request">Snapshot configuration</param>
	/// <param name="aid">Account group ID (optional)</param>
	/// <param name="cancellationToken">Cancellation token</param>
	/// <returns>Created snapshot response with ID</returns>
	Task<DashboardSnapshotResponse> CreateAsync(CreateDashboardSnapshotRequest request, string? aid, CancellationToken cancellationToken);

	/// <summary>
	/// Update dashboard snapshot expiration date
	/// </summary>
	/// <param name="snapshotId">Snapshot ID</param>
	/// <param name="request">Updated expiration date</param>
	/// <param name="aid">Account group ID (optional)</param>
	/// <param name="cancellationToken">Cancellation token</param>
	Task UpdateExpirationAsync(string snapshotId, UpdateSnapshotExpirationRequest request, string? aid, CancellationToken cancellationToken);

	/// <summary>
	/// Delete a dashboard snapshot
	/// </summary>
	/// <param name="snapshotId">Snapshot ID</param>
	/// <param name="aid">Account group ID (optional)</param>
	/// <param name="cancellationToken">Cancellation token</param>
	Task DeleteAsync(string snapshotId, string? aid, CancellationToken cancellationToken);

	/// <summary>
	/// Get widget data from a dashboard snapshot
	/// </summary>
	/// <param name="snapshotId">Snapshot ID</param>
	/// <param name="widgetId">Widget ID</param>
	/// <param name="aid">Account group ID (optional)</param>
	/// <param name="cancellationToken">Cancellation token</param>
	/// <returns>Widget data snapshot</returns>
	Task<WidgetDataSnapshot> GetWidgetDataAsync(string snapshotId, string widgetId, string? aid, CancellationToken cancellationToken);
}
