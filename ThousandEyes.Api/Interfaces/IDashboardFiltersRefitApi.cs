using Refit;
using ThousandEyes.Api.Models.Dashboards;

namespace ThousandEyes.Api.Interfaces;

/// <summary>
/// Internal Refit interface for Dashboard Filters API
/// </summary>
internal interface IDashboardFiltersRefitApi
{
	/// <summary>
	/// Get all dashboard filters (returns wrapped response)
	/// </summary>
	[Get("/dashboards/filters")]
	Task<DashboardFilters> GetAllAsync([Query] string? aid, [Query] string? searchPattern, CancellationToken cancellationToken);

	/// <summary>
	/// Get dashboard filter by ID
	/// </summary>
	[Get("/dashboards/filters/{id}")]
	Task<DashboardFilterDetails> GetByIdAsync(string id, [Query] string? aid, CancellationToken cancellationToken);

	/// <summary>
	/// Create dashboard filter
	/// </summary>
	[Post("/dashboards/filters")]
	Task<DashboardFilterDetails> CreateAsync([Body] DashboardFilterRequest request, [Query] string? aid, CancellationToken cancellationToken);

	/// <summary>
	/// Update dashboard filter
	/// </summary>
	[Put("/dashboards/filters/{id}")]
	Task<DashboardFilterDetails> UpdateAsync(string id, [Body] DashboardFilterRequest request, [Query] string? aid, CancellationToken cancellationToken);

	/// <summary>
	/// Delete dashboard filter
	/// </summary>
	[Delete("/dashboards/filters/{id}")]
	Task DeleteAsync(string id, [Query] string? aid, CancellationToken cancellationToken);
}
