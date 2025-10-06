using ThousandEyes.Api.Interfaces;
using ThousandEyes.Api.Models.AccountGroups;

namespace ThousandEyes.Api;

/// <summary>
/// Implementation of Account Groups API using Refit
/// </summary>
internal class AccountGroupsApi(IAccountGroupsRefitApi refitApi) : IAccountGroupsApi
{
	private readonly IAccountGroupsRefitApi _refitApi = refitApi;

	/// <inheritdoc />
	public async Task<AccountGroups> GetAllAsync(CancellationToken cancellationToken)
	{
		return await _refitApi.GetAllAsync(cancellationToken).ConfigureAwait(false);
	}

	/// <inheritdoc />
	public async Task<AccountGroupDetail> GetByIdAsync(string id, string[]? expand = null, CancellationToken cancellationToken = default)
	{
		var expandString = expand?.Length > 0 ? string.Join(",", expand) : null;
		return await _refitApi.GetByIdAsync(id, expandString, cancellationToken).ConfigureAwait(false);
	}

	/// <inheritdoc />
	public async Task<CreatedAccountGroup> CreateAsync(AccountGroupRequest request, string[]? expand = null, CancellationToken cancellationToken = default)
	{
		var expandString = expand?.Length > 0 ? string.Join(",", expand) : null;
		return await _refitApi.CreateAsync(request, expandString, cancellationToken).ConfigureAwait(false);
	}

	/// <inheritdoc />
	public async Task<AccountGroupDetail> UpdateAsync(string id, AccountGroupRequest request, string[]? expand = null, CancellationToken cancellationToken = default)
	{
		var expandString = expand?.Length > 0 ? string.Join(",", expand) : null;
		return await _refitApi.UpdateAsync(id, request, expandString, cancellationToken).ConfigureAwait(false);
	}

	/// <inheritdoc />
	public async Task DeleteAsync(string id, CancellationToken cancellationToken)
	{
		await _refitApi.DeleteAsync(id, cancellationToken).ConfigureAwait(false);
	}
}