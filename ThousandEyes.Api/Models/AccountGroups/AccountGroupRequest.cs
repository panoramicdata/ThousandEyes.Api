namespace ThousandEyes.Api.Models.AccountGroups;

/// <summary>
/// Account group request for create/update operations
/// </summary>
public class AccountGroupRequest
{
	/// <summary>
	/// Account group name
	/// </summary>
	public required string AccountGroupName { get; set; }

	/// <summary>
	/// List of agent IDs to assign to the account group
	/// </summary>
	public string[]? Agents { get; set; }
}
