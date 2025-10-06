using ThousandEyes.Api.Models.Roles;

namespace ThousandEyes.Api.Models.AccountGroups;

/// <summary>
/// User in account group context
/// </summary>
public class UserAccountGroup
{
	/// <summary>
	/// User ID
	/// </summary>
	public string Uid { get; set; } = "";

	/// <summary>
	/// User name
	/// </summary>
	public string Name { get; set; } = "";

	/// <summary>
	/// User email
	/// </summary>
	public string Email { get; set; } = "";

	/// <summary>
	/// Last login date
	/// </summary>
	public DateTime? LastLogin { get; set; }

	/// <summary>
	/// Registration date
	/// </summary>
	public DateTime DateRegistered { get; set; }

	/// <summary>
	/// User roles in this account group
	/// </summary>
	public Role[]? Roles { get; set; }
}
