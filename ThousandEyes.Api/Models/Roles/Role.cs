namespace ThousandEyes.Api.Models.Roles;

/// <summary>
/// Basic role information
/// </summary>
public class Role
{
	/// <summary>
	/// Role ID
	/// </summary>
	public string RoleId { get; set; } = "";

	/// <summary>
	/// Role name
	/// </summary>
	public string Name { get; set; } = "";

	/// <summary>
	/// Whether this is a built-in role
	/// </summary>
	public bool IsBuiltin { get; set; }

	/// <summary>
	/// Whether the role has management permissions
	/// </summary>
	public bool HasManagementPermissions { get; set; }
}
