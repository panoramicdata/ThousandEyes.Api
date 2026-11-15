using System.Runtime.InteropServices;
using ThousandEyes.Api.Models.Users;

namespace ThousandEyes.Api.Interfaces;

/// <summary>Interface for Users API operations</summary>
public interface IUsersApi
{
	/// <summary>Get all users</summary>
	Task<Users> GetAllAsync([Optional] string? aid, [Optional] CancellationToken cancellationToken);

	/// <summary>Get a specific user by ID</summary>
	Task<UserDetail> GetByIdAsync(string id, [Optional] string? aid, [Optional] CancellationToken cancellationToken);

	/// <summary>Get current user details</summary>
	Task<UserDetail> GetCurrentAsync(CancellationToken cancellationToken);

	/// <summary>Create a new user</summary>
	Task<CreatedUser> CreateAsync(UserRequest request, [Optional] string? aid, [Optional] CancellationToken cancellationToken);

	/// <summary>Update an existing user</summary>
	Task<UserDetail> UpdateAsync(string id, UserRequest request, [Optional] string? aid, [Optional] CancellationToken cancellationToken);

	/// <summary>Delete a user</summary>
	Task DeleteAsync(string id, [Optional] string? aid, [Optional] CancellationToken cancellationToken);
}
