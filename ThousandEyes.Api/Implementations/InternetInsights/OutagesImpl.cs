using ThousandEyes.Api.Interfaces.InternetInsights;
using ThousandEyes.Api.Models.InternetInsights;
using ThousandEyes.Api.Refit.InternetInsights;

namespace ThousandEyes.Api.Implementations.InternetInsights;

/// <summary>
/// Implementation of Outages operations
/// </summary>
internal class OutagesImpl(IOutagesRefitApi refitApi) : IOutages
{
	private readonly IOutagesRefitApi _refitApi = refitApi;

	public async Task<OutagesResponse> FilterAsync(
		OutageFilter filter,
		string? aid,
		CancellationToken cancellationToken)
		=> await _refitApi.FilterAsync(filter, aid, cancellationToken);

	public async Task<NetworkOutageDetails> GetNetworkOutageAsync(
		string outageId,
		string? aid,
		CancellationToken cancellationToken)
		=> await _refitApi.GetNetworkOutageAsync(outageId, aid, cancellationToken);

	public async Task<ApplicationOutageDetails> GetApplicationOutageAsync(
		string outageId,
		string? aid,
		CancellationToken cancellationToken)
		=> await _refitApi.GetApplicationOutageAsync(outageId, aid, cancellationToken);
}
