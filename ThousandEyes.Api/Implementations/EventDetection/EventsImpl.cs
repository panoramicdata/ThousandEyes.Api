using ThousandEyes.Api.Interfaces.EventDetection;
using ThousandEyes.Api.Models.EventDetection;
using ThousandEyes.Api.Refit.EventDetection;

namespace ThousandEyes.Api.Implementations.EventDetection;

/// <summary>
/// Implementation of Event Detection operations
/// </summary>
internal class EventsImpl(IEventsRefitApi refitApi) : IEvents
{
	private readonly IEventsRefitApi _refitApi = refitApi;

	public async Task<Events> GetAllAsync(
		string? aid,
		string? window,
		DateTime? startDate,
		DateTime? endDate,
		int? max,
		string? cursor,
		CancellationToken cancellationToken)
		=> await _refitApi.GetAllAsync(aid, window, startDate, endDate, max, cursor, cancellationToken);

	public async Task<EventDetail> GetByIdAsync(
		string eventId,
		string? aid,
		CancellationToken cancellationToken)
		=> await _refitApi.GetByIdAsync(eventId, aid, cancellationToken);
}
