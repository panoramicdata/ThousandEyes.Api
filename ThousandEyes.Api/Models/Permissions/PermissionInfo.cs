namespace ThousandEyes.Api.Models.Permissions;

/// <summary>
/// Permission information
/// </summary>
public class PermissionInfo
{
	/// <summary>
	/// Permission ID
	/// </summary>
	public string PermissionId { get; set; } = "";

	/// <summary>
	/// Permission label
	/// </summary>
	public string Label { get; set; } = "";

	/// <summary>
	/// Permission name
	/// </summary>
	public string PermissionName { get; set; } = "";

	/// <summary>
	/// Whether this is a management permission
	/// </summary>
	public bool IsManagementPermission { get; set; }
}
