namespace ThousandEyes.Api.Models.Users;

/// <summary>
/// User account group role assignment
/// </summary>
public class UserAccountGroupRole
{
	/// <summary>
	/// Account group ID
	/// </summary>
	public string AccountGroupId { get; set; } = "";

	/// <summary>
	/// Role IDs
	/// </summary>
	public string[] RoleIds { get; set; } = [];
}
