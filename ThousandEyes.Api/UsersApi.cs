using ThousandEyes.Api.Interfaces;
using ThousandEyes.Api.Models.Users;

namespace ThousandEyes.Api;

/// <summary>
/// Implementation of Users API using Refit
/// </summary>
internal class UsersApi(IUsersRefitApi refitApi) : IUsersApi
{
	private readonly IUsersRefitApi _refitApi = refitApi;

	/// <inheritdoc />
	public async Task<Users> GetAllAsync(string? aid = null, CancellationToken cancellationToken = default)
	{
		return await _refitApi.GetAllAsync(aid, cancellationToken).ConfigureAwait(false);
	}

	/// <inheritdoc />
	public async Task<UserDetail> GetByIdAsync(string id, string? aid = null, CancellationToken cancellationToken = default)
	{
		return await _refitApi.GetByIdAsync(id, aid, cancellationToken).ConfigureAwait(false);
	}

	/// <inheritdoc />
	public async Task<UserDetail> GetCurrentAsync(CancellationToken cancellationToken)
	{
		return await _refitApi.GetCurrentAsync(cancellationToken).ConfigureAwait(false);
	}

	/// <inheritdoc />
	public async Task<CreatedUser> CreateAsync(UserRequest request, string? aid = null, CancellationToken cancellationToken = default)
	{
		return await _refitApi.CreateAsync(request, aid, cancellationToken).ConfigureAwait(false);
	}

	/// <inheritdoc />
	public async Task<UserDetail> UpdateAsync(string id, UserRequest request, string? aid = null, CancellationToken cancellationToken = default)
	{
		return await _refitApi.UpdateAsync(id, request, aid, cancellationToken).ConfigureAwait(false);
	}

	/// <inheritdoc />
	public async Task DeleteAsync(string id, string? aid = null, CancellationToken cancellationToken = default)
	{
		await _refitApi.DeleteAsync(id, aid, cancellationToken).ConfigureAwait(false);
	}
}