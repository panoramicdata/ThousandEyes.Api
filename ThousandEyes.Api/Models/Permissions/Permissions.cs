namespace ThousandEyes.Api.Models.Permissions;

/// <summary>
/// Permissions response wrapper
/// </summary>
public class Permissions
{
	/// <summary>
	/// List of permissions
	/// </summary>
	public PermissionInfo[] PermissionsList { get; set; } = [];

	/// <summary>
	/// Navigation links
	/// </summary>
	public SelfLinks? Links { get; set; }
}
