using System.Text.Json.Serialization;

namespace ThousandEyes.Api.Models.Permissions;

/// <summary>
/// List of permissions
/// </summary>
public class Permissions
{
	/// <summary>
	/// Permissions
	/// </summary>
	public PermissionInfo[] PermissionsList { get; set; } = [];

	/// <summary>
	/// Navigation links
	/// </summary>
	[JsonPropertyName("_links")]
	public Links? Links { get; set; }
}
