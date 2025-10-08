using ThousandEyes.Api.Models.Dashboards;

namespace ThousandEyes.Api.Interfaces;

/// <summary>
/// Interface for Dashboards API operations
/// </summary>
/// <remarks>
/// Phase 3 implementation - Dashboard management and reporting
/// </remarks>
public interface IDashboardsApi
{
	/// <summary>
	/// Get all dashboards
	/// </summary>
	/// <param name="aid">Account group ID (optional)</param>
	/// <param name="cancellationToken">Cancellation token</param>
	/// <returns>Array of dashboards</returns>
	Task<Dashboard[]> GetAllAsync(string? aid, CancellationToken cancellationToken);

	/// <summary>
	/// Get a specific dashboard by ID
	/// </summary>
	/// <param name="dashboardId">Dashboard ID</param>
	/// <param name="aid">Account group ID (optional)</param>
	/// <param name="cancellationToken">Cancellation token</param>
	/// <returns>Dashboard details</returns>
	Task<Dashboard> GetByIdAsync(string dashboardId, string? aid, CancellationToken cancellationToken);

	/// <summary>
	/// Create a new dashboard
	/// </summary>
	/// <param name="request">Dashboard configuration</param>
	/// <param name="aid">Account group ID (optional)</param>
	/// <param name="cancellationToken">Cancellation token</param>
	/// <returns>Created dashboard</returns>
	Task<Dashboard> CreateAsync(DashboardRequest request, string? aid, CancellationToken cancellationToken);

	/// <summary>
	/// Update an existing dashboard
	/// </summary>
	/// <param name="dashboardId">Dashboard ID</param>
	/// <param name="request">Updated dashboard configuration</param>
	/// <param name="aid">Account group ID (optional)</param>
	/// <param name="cancellationToken">Cancellation token</param>
	/// <returns>Updated dashboard</returns>
	Task<Dashboard> UpdateAsync(string dashboardId, DashboardRequest request, string? aid, CancellationToken cancellationToken);

	/// <summary>
	/// Delete a dashboard
	/// </summary>
	/// <param name="dashboardId">Dashboard ID</param>
	/// <param name="aid">Account group ID (optional)</param>
	/// <param name="cancellationToken">Cancellation token</param>
	Task DeleteAsync(string dashboardId, string? aid, CancellationToken cancellationToken);
}