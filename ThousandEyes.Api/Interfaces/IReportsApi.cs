using ThousandEyes.Api.Models.Dashboards;

namespace ThousandEyes.Api.Interfaces;

/// <summary>
/// Interface for Reports API operations
/// </summary>
/// <remarks>
/// Phase 3 implementation - Report management and scheduling
/// </remarks>
public interface IReportsApi
{
	/// <summary>
	/// Get all reports
	/// </summary>
	/// <param name="aid">Account group ID (optional)</param>
	/// <param name="cancellationToken">Cancellation token</param>
	/// <returns>List of reports</returns>
	Task<Reports> GetAllAsync(string? aid, CancellationToken cancellationToken);

	/// <summary>
	/// Get a specific report by ID
	/// </summary>
	/// <param name="reportId">Report ID</param>
	/// <param name="aid">Account group ID (optional)</param>
	/// <param name="cancellationToken">Cancellation token</param>
	/// <returns>Report details</returns>
	Task<Report> GetByIdAsync(string reportId, string? aid, CancellationToken cancellationToken);

	/// <summary>
	/// Create a new report
	/// </summary>
	/// <param name="request">Report configuration</param>
	/// <param name="aid">Account group ID (optional)</param>
	/// <param name="cancellationToken">Cancellation token</param>
	/// <returns>Created report</returns>
	Task<Report> CreateAsync(ReportRequest request, string? aid, CancellationToken cancellationToken);

	/// <summary>
	/// Update an existing report
	/// </summary>
	/// <param name="reportId">Report ID</param>
	/// <param name="request">Updated report configuration</param>
	/// <param name="aid">Account group ID (optional)</param>
	/// <param name="cancellationToken">Cancellation token</param>
	/// <returns>Updated report</returns>
	Task<Report> UpdateAsync(string reportId, ReportRequest request, string? aid, CancellationToken cancellationToken);

	/// <summary>
	/// Delete a report
	/// </summary>
	/// <param name="reportId">Report ID</param>
	/// <param name="aid">Account group ID (optional)</param>
	/// <param name="cancellationToken">Cancellation token</param>
	Task DeleteAsync(string reportId, string? aid, CancellationToken cancellationToken);

	/// <summary>
	/// Generate a report on-demand
	/// </summary>
	/// <param name="reportId">Report ID</param>
	/// <param name="aid">Account group ID (optional)</param>
	/// <param name="cancellationToken">Cancellation token</param>
	/// <returns>Generated report download link</returns>
	Task<string> GenerateAsync(string reportId, string? aid, CancellationToken cancellationToken);
}