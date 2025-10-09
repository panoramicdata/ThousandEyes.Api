using Refit;
using ThousandEyes.Api.Models.Emulation;

namespace ThousandEyes.Api.Interfaces;

/// <summary>
/// Internal Refit interface for User Agents API
/// </summary>
internal interface IUserAgentsRefitApi
{
	/// <summary>
	/// Get all user agents
	/// </summary>
	[Get("/user-agents")]
	Task<UserAgents> GetAllAsync([Query] string? aid, CancellationToken cancellationToken);
}