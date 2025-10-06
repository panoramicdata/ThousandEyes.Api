namespace ThousandEyes.Api.Models.Users;

/// <summary>
/// User request for create/update operations
/// </summary>
public class UserRequest
{
	/// <summary>
	/// User name
	/// </summary>
	public string? Name { get; set; }

	/// <summary>
	/// User email
	/// </summary>
	public string? Email { get; set; }

	/// <summary>
	/// Login account group ID
	/// </summary>
	public string? LoginAccountGroupId { get; set; }

	/// <summary>
	/// Account group roles
	/// </summary>
	public UserAccountGroupRole[]? AccountGroupRoles { get; set; }

	/// <summary>
	/// All account group role IDs
	/// </summary>
	public string[]? AllAccountGroupRoleIds { get; set; }
}
