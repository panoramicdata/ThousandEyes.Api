using System.Text.Json.Serialization;

namespace ThousandEyes.Api.Models.Roles;

/// <summary>
/// List of roles
/// </summary>
public class Roles
{
	/// <summary>
	/// Roles
	/// </summary>
	public Role[] RolesList { get; set; } = [];

	/// <summary>
	/// Navigation links
	/// </summary>
	[JsonPropertyName("_links")]
	public Links? Links { get; set; }
}
