using Refit;
using ThousandEyes.Api.Models.Tests;

namespace ThousandEyes.Api.Interfaces;

/// <summary>
/// Internal Refit interface for BGP Tests API
/// </summary>
internal interface IBgpTestsRefitApi
{
	/// <summary>
	/// Get all BGP tests
	/// </summary>
	[Get("/tests/bgp")]
	Task<BgpTests> GetAllAsync([Query] string? aid, CancellationToken cancellationToken);
}