using ThousandEyes.Api.Models.EventDetection;

namespace ThousandEyes.Api.Interfaces.EventDetection;

/// <summary>
/// Public interface for Event Detection operations
/// </summary>
public interface IEvents
{
	/// <summary>
	/// Retrieves a list of events within the specified time window.
	/// You must provide either a time window or specify startDate and endDate.
	/// </summary>
	/// <param name="aid">Optional account group ID for context</param>
	/// <param name="window">Time window (e.g., "12h", "7d")</param>
	/// <param name="startDate">Start date (use with endDate)</param>
	/// <param name="endDate">End date (use with startDate)</param>
	/// <param name="max">Maximum number of events to return</param>
	/// <param name="cursor">Pagination cursor</param>
	/// <param name="cancellationToken">Cancellation token</param>
	/// <returns>List of events</returns>
	Task<Events> GetAllAsync(
		string? aid,
		string? window,
		DateTime? startDate,
		DateTime? endDate,
		int? max,
		string? cursor,
		CancellationToken cancellationToken);

	/// <summary>
	/// Returns detailed information about an event using its ID.
	/// </summary>
	/// <param name="eventId">Unique event ID</param>
	/// <param name="aid">Optional account group ID for context</param>
	/// <param name="cancellationToken">Cancellation token</param>
	/// <returns>Detailed event information</returns>
	Task<EventDetail> GetByIdAsync(
		string eventId,
		string? aid,
		CancellationToken cancellationToken);
}
