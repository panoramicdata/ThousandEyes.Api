using ThousandEyes.Api.Interfaces;
using ThousandEyes.Api.Models.Roles;

namespace ThousandEyes.Api;

/// <summary>
/// Implementation of Roles API using Refit
/// </summary>
internal class RolesApi(IRolesRefitApi refitApi) : IRolesApi
{
	private readonly IRolesRefitApi _refitApi = refitApi;

	/// <inheritdoc />
	public async Task<Roles> GetAllAsync(string? aid = null, CancellationToken cancellationToken = default)
	{
		return await _refitApi.GetAllAsync(aid, cancellationToken).ConfigureAwait(false);
	}

	/// <inheritdoc />
	public async Task<RoleDetail> GetByIdAsync(string id, string? aid = null, CancellationToken cancellationToken = default)
	{
		return await _refitApi.GetByIdAsync(id, aid, cancellationToken).ConfigureAwait(false);
	}

	/// <inheritdoc />
	public async Task<RoleDetail> CreateAsync(RoleRequestBody request, string? aid = null, CancellationToken cancellationToken = default)
	{
		return await _refitApi.CreateAsync(request, aid, cancellationToken).ConfigureAwait(false);
	}

	/// <inheritdoc />
	public async Task<RoleDetail> UpdateAsync(string id, RoleRequestBody request, string? aid = null, CancellationToken cancellationToken = default)
	{
		return await _refitApi.UpdateAsync(id, request, aid, cancellationToken).ConfigureAwait(false);
	}

	/// <inheritdoc />
	public async Task DeleteAsync(string id, string? aid = null, CancellationToken cancellationToken = default)
	{
		await _refitApi.DeleteAsync(id, aid, cancellationToken).ConfigureAwait(false);
	}
}