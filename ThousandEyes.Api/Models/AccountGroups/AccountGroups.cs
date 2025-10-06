namespace ThousandEyes.Api.Models.AccountGroups;

/// <summary>
/// Account Groups response wrapper
/// </summary>
public class AccountGroups
{
	/// <summary>
	/// List of account groups
	/// </summary>
	public AccountGroupInfo[] AccountGroupsList { get; set; } = [];

	/// <summary>
	/// Navigation links
	/// </summary>
	public SelfLinks? Links { get; set; }
}