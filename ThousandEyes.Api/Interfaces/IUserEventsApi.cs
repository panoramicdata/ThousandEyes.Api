using ThousandEyes.Api.Models.UserEvents;

namespace ThousandEyes.Api.Interfaces;

/// <summary>Interface for User Events (Audit Logs) API operations</summary>
public interface IUserEventsApi
{
	/// <summary>Get audit user events with time window filtering</summary>
	Task<AuditUserEvents> GetAllAsync(
		string? aid = null,
		bool useAllPermittedAids = false,
		string? window = null,
		string? cursor = null,
		CancellationToken cancellationToken = default);

	/// <summary>Get audit user events with date range filtering</summary>
	Task<AuditUserEvents> GetAllAsync(
		DateTime startDate,
		DateTime endDate,
		string? aid = null,
		bool useAllPermittedAids = false,
		string? cursor = null,
		CancellationToken cancellationToken = default);
}