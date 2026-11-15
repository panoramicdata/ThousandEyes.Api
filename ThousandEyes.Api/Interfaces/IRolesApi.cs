using System.Runtime.InteropServices;
using ThousandEyes.Api.Models.Roles;

namespace ThousandEyes.Api.Interfaces;

/// <summary>Interface for Roles API operations</summary>
public interface IRolesApi
{
	/// <summary>Get all roles</summary>
	Task<Roles> GetAllAsync([Optional] string? aid, [Optional] CancellationToken cancellationToken);

	/// <summary>Get a specific role by ID</summary>
	Task<RoleDetail> GetByIdAsync(string id, [Optional] string? aid, [Optional] CancellationToken cancellationToken);

	/// <summary>Create a new role</summary>
	Task<RoleDetail> CreateAsync(RoleRequestBody request, [Optional] string? aid, [Optional] CancellationToken cancellationToken);

	/// <summary>Update an existing role</summary>
	Task<RoleDetail> UpdateAsync(string id, RoleRequestBody request, [Optional] string? aid, [Optional] CancellationToken cancellationToken);

	/// <summary>Delete a role</summary>
	Task DeleteAsync(string id, [Optional] string? aid, [Optional] CancellationToken cancellationToken);
}
