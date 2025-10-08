using System.Text.Json.Serialization;

namespace ThousandEyes.Api.Models.AccountGroups;

/// <summary>
/// List of account groups
/// </summary>
public class AccountGroups
{
	/// <summary>
	/// Account groups
	/// </summary>
	public AccountGroupInfo[] AccountGroupsList { get; set; } = [];

	/// <summary>
	/// Navigation links
	/// </summary>
	[JsonPropertyName("_links")]
	public Links? Links { get; set; }
}