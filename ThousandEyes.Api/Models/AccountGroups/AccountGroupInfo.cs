namespace ThousandEyes.Api.Models.AccountGroups;

/// <summary>
/// Account group information
/// </summary>
public class AccountGroupInfo
{
	/// <summary>
	/// Account group ID
	/// </summary>
	public string Aid { get; set; } = "";

	/// <summary>
	/// Account group name
	/// </summary>
	public string AccountGroupName { get; set; } = "";

	/// <summary>
	/// Organization name
	/// </summary>
	public string? OrganizationName { get; set; }

	/// <summary>
	/// Organization ID
	/// </summary>
	public string? OrgId { get; set; }

	/// <summary>
	/// Whether this is the current account group
	/// </summary>
	public bool IsCurrentAccountGroup { get; set; }

	/// <summary>
	/// Whether this is the default account group
	/// </summary>
	public bool IsDefaultAccountGroup { get; set; }
}
