using ThousandEyes.Api.Interfaces.BgpMonitors;
using ThousandEyes.Api.Models.BgpMonitors;
using ThousandEyes.Api.Refit.BgpMonitors;

namespace ThousandEyes.Api.Implementations.BgpMonitors;

/// <summary>
/// Implementation of BGP Monitors operations
/// </summary>
internal class BgpMonitorsImpl(IBgpMonitorsRefitApi refitApi) : IBgpMonitors
{
	private readonly IBgpMonitorsRefitApi _refitApi = refitApi;

	public async Task<Monitors> GetAllAsync(string? aid, CancellationToken cancellationToken)
		=> await _refitApi.GetAllAsync(aid, cancellationToken);
}
