using Refit;
using ThousandEyes.Api.Models.AccountGroups;

namespace ThousandEyes.Api.Interfaces;

/// <summary>Internal Refit interface for Account Groups API</summary>
internal interface IAccountGroupsRefitApi
{
	/// <summary>Get all account groups</summary>
	[Get("/account-groups")]
	Task<AccountGroups> GetAllAsync(CancellationToken cancellationToken);

	/// <summary>Get a specific account group by ID</summary>
	[Get("/account-groups/{id}")]
	Task<AccountGroupDetail> GetByIdAsync(
		string id, 
		[Query] string? expand, 
		CancellationToken cancellationToken);

	/// <summary>Create a new account group</summary>
	[Post("/account-groups")]
	Task<CreatedAccountGroup> CreateAsync(
		[Body] AccountGroupRequest request,
		[Query] string? expand,
		CancellationToken cancellationToken);

	/// <summary>Update an existing account group</summary>
	[Put("/account-groups/{id}")]
	Task<AccountGroupDetail> UpdateAsync(
		string id,
		[Body] AccountGroupRequest request,
		[Query] string? expand,
		CancellationToken cancellationToken);

	/// <summary>Delete an account group</summary>
	[Delete("/account-groups/{id}")]
	Task DeleteAsync(string id, CancellationToken cancellationToken);
}