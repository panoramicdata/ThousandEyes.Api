using ThousandEyes.Api.Interfaces;
using ThousandEyes.Api.Models.Permissions;

namespace ThousandEyes.Api;

/// <summary>
/// Implementation of Permissions API using Refit
/// </summary>
internal class PermissionsApi(IPermissionsRefitApi refitApi) : IPermissionsApi
{
	private readonly IPermissionsRefitApi _refitApi = refitApi;

	/// <inheritdoc />
	public async Task<Permissions> GetAllAsync(string? aid = null, CancellationToken cancellationToken = default)
	{
		return await _refitApi.GetAllAsync(aid, cancellationToken).ConfigureAwait(false);
	}
}