namespace ThousandEyes.Api.Models.Roles;

/// <summary>
/// Detailed role information
/// </summary>
public class RoleDetail : Role
{
	/// <summary>
	/// Role permissions
	/// </summary>
	public PermissionInfo[]? Permissions { get; set; }

	/// <summary>
	/// Navigation links
	/// </summary>
	public SelfLinks? Links { get; set; }
}
