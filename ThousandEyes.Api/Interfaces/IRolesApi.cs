using ThousandEyes.Api.Models.Roles;

namespace ThousandEyes.Api.Interfaces;

/// <summary>Interface for Roles API operations</summary>
public interface IRolesApi
{
	/// <summary>Get all roles</summary>
	Task<Roles> GetAllAsync(string? aid = null, CancellationToken cancellationToken = default);

	/// <summary>Get a specific role by ID</summary>
	Task<RoleDetail> GetByIdAsync(string id, string? aid = null, CancellationToken cancellationToken = default);

	/// <summary>Create a new role</summary>
	Task<RoleDetail> CreateAsync(RoleRequestBody request, string? aid = null, CancellationToken cancellationToken = default);

	/// <summary>Update an existing role</summary>
	Task<RoleDetail> UpdateAsync(string id, RoleRequestBody request, string? aid = null, CancellationToken cancellationToken = default);

	/// <summary>Delete a role</summary>
	Task DeleteAsync(string id, string? aid = null, CancellationToken cancellationToken = default);
}
