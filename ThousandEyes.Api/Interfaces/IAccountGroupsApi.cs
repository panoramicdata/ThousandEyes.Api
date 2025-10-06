using ThousandEyes.Api.Models.AccountGroups;

namespace ThousandEyes.Api.Interfaces;

/// <summary>Interface for Account Groups API operations</summary>
public interface IAccountGroupsApi
{
	/// <summary>Get all account groups</summary>
	Task<AccountGroups> GetAllAsync(CancellationToken cancellationToken);

	/// <summary>Get a specific account group by ID</summary>
	Task<AccountGroupDetail> GetByIdAsync(string id, string[]? expand = null, CancellationToken cancellationToken = default);

	/// <summary>Create a new account group</summary>
	Task<CreatedAccountGroup> CreateAsync(AccountGroupRequest request, string[]? expand = null, CancellationToken cancellationToken = default);

	/// <summary>Update an existing account group</summary>
	Task<AccountGroupDetail> UpdateAsync(string id, AccountGroupRequest request, string[]? expand = null, CancellationToken cancellationToken = default);

	/// <summary>Delete an account group</summary>
	Task DeleteAsync(string id, CancellationToken cancellationToken);
}
