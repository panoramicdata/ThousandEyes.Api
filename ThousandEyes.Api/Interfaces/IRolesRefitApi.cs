using Refit;
using ThousandEyes.Api.Models.Roles;

namespace ThousandEyes.Api.Interfaces;

/// <summary>Internal Refit interface for Roles API</summary>
internal interface IRolesRefitApi
{
	/// <summary>Get all roles</summary>
	[Get("/roles")]
	Task<Roles> GetAllAsync([Query] string? aid, CancellationToken cancellationToken);

	/// <summary>Get a specific role by ID</summary>
	[Get("/roles/{id}")]
	Task<RoleDetail> GetByIdAsync(string id, [Query] string? aid, CancellationToken cancellationToken);

	/// <summary>Create a new role</summary>
	[Post("/roles")]
	Task<RoleDetail> CreateAsync([Body] RoleRequestBody request, [Query] string? aid, CancellationToken cancellationToken);

	/// <summary>Update an existing role</summary>
	[Put("/roles/{id}")]
	Task<RoleDetail> UpdateAsync(string id, [Body] RoleRequestBody request, [Query] string? aid, CancellationToken cancellationToken);

	/// <summary>Delete a role</summary>
	[Delete("/roles/{id}")]
	Task DeleteAsync(string id, [Query] string? aid, CancellationToken cancellationToken);
}