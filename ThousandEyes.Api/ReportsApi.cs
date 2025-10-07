using ThousandEyes.Api.Interfaces;
using ThousandEyes.Api.Models.Dashboards;

namespace ThousandEyes.Api;

/// <summary>
/// Implementation of Reports API using Refit
/// </summary>
internal class ReportsApi(IReportsRefitApi refitApi) : IReportsApi
{
	private readonly IReportsRefitApi _refitApi = refitApi;

	/// <inheritdoc />
	public Task<Reports> GetAllAsync(string? aid, CancellationToken cancellationToken) =>
		_refitApi.GetAllAsync(aid, cancellationToken);

	/// <inheritdoc />
	public Task<Report> GetByIdAsync(string reportId, string? aid, CancellationToken cancellationToken) =>
		_refitApi.GetByIdAsync(reportId, aid, cancellationToken);

	/// <inheritdoc />
	public Task<Report> CreateAsync(ReportRequest request, string? aid, CancellationToken cancellationToken) =>
		_refitApi.CreateAsync(request, aid, cancellationToken);

	/// <inheritdoc />
	public Task<Report> UpdateAsync(string reportId, ReportRequest request, string? aid, CancellationToken cancellationToken) =>
		_refitApi.UpdateAsync(reportId, request, aid, cancellationToken);

	/// <inheritdoc />
	public Task DeleteAsync(string reportId, string? aid, CancellationToken cancellationToken) =>
		_refitApi.DeleteAsync(reportId, aid, cancellationToken);

	/// <inheritdoc />
	public async Task<string> GenerateAsync(string reportId, string? aid, CancellationToken cancellationToken)
	{
		var response = await _refitApi.GenerateAsync(reportId, aid, cancellationToken);
		
		// Extract download URL from response headers or body
		if (response.Headers.Location != null)
		{
			return response.Headers.Location.ToString();
		}
		
		// Fallback to constructing URL if not in headers
		return $"/reports/{reportId}/download";
	}
}