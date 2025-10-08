using ThousandEyes.Api.Interfaces;
using ThousandEyes.Api.Models.Dashboards;

namespace ThousandEyes.Api;

/// <summary>
/// Implementation of Dashboard Filters API using Refit
/// </summary>
internal class DashboardFiltersApi(IDashboardFiltersRefitApi refitApi) : IDashboardFiltersApi
{
	private readonly IDashboardFiltersRefitApi _refitApi = refitApi;

	/// <inheritdoc />
	public Task<DashboardFilters> GetAllAsync(string? aid, string? searchPattern, CancellationToken cancellationToken) =>
		_refitApi.GetAllAsync(aid, searchPattern, cancellationToken);

	/// <inheritdoc />
	public Task<DashboardFilterDetails> GetByIdAsync(string filterId, string? aid, CancellationToken cancellationToken) =>
		_refitApi.GetByIdAsync(filterId, aid, cancellationToken);

	/// <inheritdoc />
	public Task<DashboardFilterDetails> CreateAsync(DashboardFilterRequest request, string? aid, CancellationToken cancellationToken) =>
		_refitApi.CreateAsync(request, aid, cancellationToken);

	/// <inheritdoc />
	public Task<DashboardFilterDetails> UpdateAsync(string filterId, DashboardFilterRequest request, string? aid, CancellationToken cancellationToken) =>
		_refitApi.UpdateAsync(filterId, request, aid, cancellationToken);

	/// <inheritdoc />
	public Task DeleteAsync(string filterId, string? aid, CancellationToken cancellationToken) =>
		_refitApi.DeleteAsync(filterId, aid, cancellationToken);
}
