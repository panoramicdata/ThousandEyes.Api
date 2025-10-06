namespace ThousandEyes.Api.Models.Roles;

/// <summary>
/// Roles response wrapper
/// </summary>
public class Roles
{
	/// <summary>
	/// List of roles
	/// </summary>
	public Role[] RolesList { get; set; } = [];

	/// <summary>
	/// Navigation links
	/// </summary>
	public SelfLinks? Links { get; set; }
}
