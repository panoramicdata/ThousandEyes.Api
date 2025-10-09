using Refit;
using ThousandEyes.Api.Models.BgpMonitors;

namespace ThousandEyes.Api.Refit.BgpMonitors;

/// <summary>
/// Internal Refit interface for BGP Monitors API
/// </summary>
internal interface IBgpMonitorsRefitApi
{
	/// <summary>
	/// Retrieves a list of BGP monitors
	/// </summary>
	[Get("/monitors")]
	Task<Monitors> GetAllAsync(
		[Query] string? aid,
		CancellationToken cancellationToken);
}
