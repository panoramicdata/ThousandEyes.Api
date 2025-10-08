using System.Text.Json.Serialization;

namespace ThousandEyes.Api.Models.AccountGroups;

/// <summary>
/// Detailed account group information
/// </summary>
public class AccountGroupDetail : AccountGroupInfo
{
	/// <summary>
	/// Account group token
	/// </summary>
	public string? AccountToken { get; set; }

	/// <summary>
	/// Users in the account group
	/// </summary>
	public UserAccountGroup[]? Users { get; set; }

	/// <summary>
	/// Agents assigned to the account group
	/// </summary>
	public EnterpriseAgent[]? Agents { get; set; }

	/// <summary>
	/// Navigation links
	/// </summary>
	[JsonPropertyName("_links")]
	public Links? Links { get; set; }
}
