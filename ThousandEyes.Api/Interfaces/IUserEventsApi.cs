using System.Runtime.InteropServices;
using ThousandEyes.Api.Models.UserEvents;

namespace ThousandEyes.Api.Interfaces;

/// <summary>Interface for User Events (Audit Logs) API operations</summary>
public interface IUserEventsApi
{
	/// <summary>Get audit user events with time window filtering</summary>
	Task<AuditUserEvents> GetAllAsync(
		[Optional] string? aid,
		[Optional] bool useAllPermittedAids,
		[Optional] string? window,
		[Optional] string? cursor,
		[Optional] CancellationToken cancellationToken);

	/// <summary>Get audit user events with date range filtering</summary>
	Task<AuditUserEvents> GetAllAsync(
		DateTime startDate,
		DateTime endDate,
		[Optional] string? aid,
		[Optional] bool useAllPermittedAids,
		[Optional] string? cursor,
		[Optional] CancellationToken cancellationToken);
}