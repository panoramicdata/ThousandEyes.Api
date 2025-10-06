using System.Globalization;
using ThousandEyes.Api.Interfaces;
using ThousandEyes.Api.Models.UserEvents;

namespace ThousandEyes.Api;

/// <summary>
/// Implementation of User Events API using Refit
/// </summary>
internal class UserEventsApi(IUserEventsRefitApi refitApi) : IUserEventsApi
{
	private readonly IUserEventsRefitApi _refitApi = refitApi;

	/// <inheritdoc />
	public Task<AuditUserEvents> GetAllAsync(
		string? aid = null,
		bool useAllPermittedAids = false,
		string? window = null,
		string? cursor = null,
		CancellationToken cancellationToken = default) => _refitApi.GetAllAsync(
			aid,
			useAllPermittedAids,
			window,
			startDate: null,
			endDate: null,
			cursor,
			cancellationToken);

	/// <inheritdoc />
	public Task<AuditUserEvents> GetAllAsync(
		DateTime startDate,
		DateTime endDate,
		string? aid = null,
		bool useAllPermittedAids = false,
		string? cursor = null,
		CancellationToken cancellationToken = default) => _refitApi.GetAllAsync(
			aid,
			useAllPermittedAids,
			window: null,
			startDate: startDate.ToString("yyyy-MM-ddTHH:mm:ssZ", CultureInfo.InvariantCulture),
			endDate: endDate.ToString("yyyy-MM-ddTHH:mm:ssZ", CultureInfo.InvariantCulture),
			cursor,
			cancellationToken);
}