using Refit;
using ThousandEyes.Api.Models.Dashboards;

namespace ThousandEyes.Api.Interfaces;

/// <summary>
/// Internal Refit interface for Reports API
/// </summary>
internal interface IReportsRefitApi
{
	/// <summary>
	/// Get all reports
	/// </summary>
	[Get("/reports")]
	Task<Reports> GetAllAsync([Query] string? aid, CancellationToken cancellationToken);

	/// <summary>
	/// Get report by ID
	/// </summary>
	[Get("/reports/{reportId}")]
	Task<Report> GetByIdAsync(string reportId, [Query] string? aid, CancellationToken cancellationToken);

	/// <summary>
	/// Create report
	/// </summary>
	[Post("/reports")]
	Task<Report> CreateAsync([Body] ReportRequest request, [Query] string? aid, CancellationToken cancellationToken);

	/// <summary>
	/// Update report
	/// </summary>
	[Put("/reports/{reportId}")]
	Task<Report> UpdateAsync(string reportId, [Body] ReportRequest request, [Query] string? aid, CancellationToken cancellationToken);

	/// <summary>
	/// Delete report
	/// </summary>
	[Delete("/reports/{reportId}")]
	Task DeleteAsync(string reportId, [Query] string? aid, CancellationToken cancellationToken);

	/// <summary>
	/// Generate report on-demand
	/// </summary>
	[Post("/reports/{reportId}/generate")]
	Task<ApiResponse<object>> GenerateAsync(string reportId, [Query] string? aid, CancellationToken cancellationToken);
}