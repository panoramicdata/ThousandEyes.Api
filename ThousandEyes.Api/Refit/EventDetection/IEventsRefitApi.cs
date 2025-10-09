using Refit;
using ThousandEyes.Api.Models.EventDetection;

namespace ThousandEyes.Api.Refit.EventDetection;

/// <summary>
/// Internal Refit interface for Event Detection API
/// </summary>
internal interface IEventsRefitApi
{
	/// <summary>
	/// Get list of events
	/// </summary>
	[Get("/events")]
	Task<Events> GetAllAsync(
		[Query] string? aid,
		[Query] string? window,
		[Query] DateTime? startDate,
		[Query] DateTime? endDate,
		[Query] int? max,
		[Query] string? cursor,
		CancellationToken cancellationToken);

	/// <summary>
	/// Get event by ID
	/// </summary>
	[Get("/events/{eventId}")]
	Task<EventDetail> GetByIdAsync(
		string eventId,
		[Query] string? aid,
		CancellationToken cancellationToken);
}
