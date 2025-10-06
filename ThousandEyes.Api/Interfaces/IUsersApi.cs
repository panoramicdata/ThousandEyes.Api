using ThousandEyes.Api.Models.Users;

namespace ThousandEyes.Api.Interfaces;

/// <summary>Interface for Users API operations</summary>
public interface IUsersApi
{
	/// <summary>Get all users</summary>
	Task<Users> GetAllAsync(string? aid = null, CancellationToken cancellationToken = default);

	/// <summary>Get a specific user by ID</summary>
	Task<UserDetail> GetByIdAsync(string id, string? aid = null, CancellationToken cancellationToken = default);

	/// <summary>Get current user details</summary>
	Task<UserDetail> GetCurrentAsync(CancellationToken cancellationToken);

	/// <summary>Create a new user</summary>
	Task<CreatedUser> CreateAsync(UserRequest request, string? aid = null, CancellationToken cancellationToken = default);

	/// <summary>Update an existing user</summary>
	Task<UserDetail> UpdateAsync(string id, UserRequest request, string? aid = null, CancellationToken cancellationToken = default);

	/// <summary>Delete a user</summary>
	Task DeleteAsync(string id, string? aid = null, CancellationToken cancellationToken = default);
}
