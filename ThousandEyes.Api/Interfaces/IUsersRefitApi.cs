using Refit;
using ThousandEyes.Api.Models.Users;

namespace ThousandEyes.Api.Interfaces;

/// <summary>Internal Refit interface for Users API</summary>
internal interface IUsersRefitApi
{
	/// <summary>Get all users</summary>
	[Get("/users")]
	Task<Users> GetAllAsync([Query] string? aid, CancellationToken cancellationToken);

	/// <summary>Get a specific user by ID</summary>
	[Get("/users/{id}")]
	Task<UserDetail> GetByIdAsync(string id, [Query] string? aid, CancellationToken cancellationToken);

	/// <summary>Get current user details</summary>
	[Get("/users/current")]
	Task<UserDetail> GetCurrentAsync(CancellationToken cancellationToken);

	/// <summary>Create a new user</summary>
	[Post("/users")]
	Task<CreatedUser> CreateAsync([Body] UserRequest request, [Query] string? aid, CancellationToken cancellationToken);

	/// <summary>Update an existing user</summary>
	[Put("/users/{id}")]
	Task<UserDetail> UpdateAsync(string id, [Body] UserRequest request, [Query] string? aid, CancellationToken cancellationToken);

	/// <summary>Delete a user</summary>
	[Delete("/users/{id}")]
	Task DeleteAsync(string id, [Query] string? aid, CancellationToken cancellationToken);
}