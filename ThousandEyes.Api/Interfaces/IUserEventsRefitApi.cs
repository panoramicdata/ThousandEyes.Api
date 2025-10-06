using Refit;
using ThousandEyes.Api.Models.UserEvents;

namespace ThousandEyes.Api.Interfaces;

/// <summary>Internal Refit interface for User Events API</summary>
internal interface IUserEventsRefitApi
{
	/// <summary>Get audit user events with query parameters</summary>
	[Get("/audit-user-events")]
	Task<AuditUserEvents> GetAllAsync(
		[Query] string? aid,
		[Query] bool useAllPermittedAids,
		[Query] string? window,
		[Query] string? startDate,
		[Query] string? endDate,
		[Query] string? cursor,
		CancellationToken cancellationToken);
}