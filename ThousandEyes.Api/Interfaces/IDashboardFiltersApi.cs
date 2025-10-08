using ThousandEyes.Api.Models.Dashboards;

namespace ThousandEyes.Api.Interfaces;

/// <summary>
/// Interface for Dashboard Filters API operations
/// </summary>
/// <remarks>
/// Phase 3 implementation - Dashboard filter management
/// </remarks>
public interface IDashboardFiltersApi
{
	/// <summary>
	/// Get all dashboard filters
	/// </summary>
	/// <param name="aid">Account group ID (optional)</param>
	/// <param name="searchPattern">Search pattern to filter by name or description (optional)</param>
	/// <param name="cancellationToken">Cancellation token</param>
	/// <returns>Dashboard filters response</returns>
	Task<DashboardFilters> GetAllAsync(string? aid, string? searchPattern, CancellationToken cancellationToken);

	/// <summary>
	/// Get a specific dashboard filter by ID
	/// </summary>
	/// <param name="filterId">Filter ID</param>
	/// <param name="aid">Account group ID (optional)</param>
	/// <param name="cancellationToken">Cancellation token</param>
	/// <returns>Dashboard filter details</returns>
	Task<DashboardFilterDetails> GetByIdAsync(string filterId, string? aid, CancellationToken cancellationToken);

	/// <summary>
	/// Create a new dashboard filter
	/// </summary>
	/// <param name="request">Filter configuration</param>
	/// <param name="aid">Account group ID (optional)</param>
	/// <param name="cancellationToken">Cancellation token</param>
	/// <returns>Created filter</returns>
	Task<DashboardFilterDetails> CreateAsync(DashboardFilterRequest request, string? aid, CancellationToken cancellationToken);

	/// <summary>
	/// Update an existing dashboard filter
	/// </summary>
	/// <param name="filterId">Filter ID</param>
	/// <param name="request">Updated filter configuration</param>
	/// <param name="aid">Account group ID (optional)</param>
	/// <param name="cancellationToken">Cancellation token</param>
	/// <returns>Updated filter</returns>
	Task<DashboardFilterDetails> UpdateAsync(string filterId, DashboardFilterRequest request, string? aid, CancellationToken cancellationToken);

	/// <summary>
	/// Delete a dashboard filter
	/// </summary>
	/// <param name="filterId">Filter ID</param>
	/// <param name="aid">Account group ID (optional)</param>
	/// <param name="cancellationToken">Cancellation token</param>
	Task DeleteAsync(string filterId, string? aid, CancellationToken cancellationToken);
}
