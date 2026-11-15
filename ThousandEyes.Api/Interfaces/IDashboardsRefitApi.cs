using Refit;
using ThousandEyes.Api.Models.Dashboards;

namespace ThousandEyes.Api.Interfaces;

/// <summary>
/// Internal Refit interface for Dashboards API
/// </summary>
internal interface IDashboardsRefitApi
{
	/// <summary>
	/// Get all dashboards (returns array directly)
	/// </summary>
	[Get("/dashboards")]
	Task<Dashboard[]> GetAllAsync([Query] string? aid, CancellationToken ct);

	/// <summary>
	/// Get dashboard by ID
	/// </summary>
	[Get("/dashboards/{dashboardId}")]
	Task<Dashboard> GetByIdAsync(string dashboardId, [Query] string? aid, CancellationToken ct);

	/// <summary>
	/// Create dashboard
	/// </summary>
	[Post("/dashboards")]
	Task<Dashboard> CreateAsync([Body] DashboardRequest request, [Query] string? aid, CancellationToken ct);

	/// <summary>
	/// Update dashboard
	/// </summary>
	[Put("/dashboards/{dashboardId}")]
	Task<Dashboard> UpdateAsync(string dashboardId, [Body] DashboardRequest request, [Query] string? aid, CancellationToken ct);

	/// <summary>
	/// Delete dashboard
	/// </summary>
	[Delete("/dashboards/{dashboardId}")]
	Task DeleteAsync(string dashboardId, [Query] string? aid, CancellationToken ct);
}