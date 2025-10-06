using ThousandEyes.Api.Models.AccountGroups;
using ThousandEyes.Api.Models.Roles;

namespace ThousandEyes.Api.Models.Users;

/// <summary>
/// Account group role
/// </summary>
public class AccountGroupRole
{
	/// <summary>
	/// Account group
	/// </summary>
	public AccountGroup? AccountGroup { get; set; }

	/// <summary>
	/// Roles
	/// </summary>
	public Role[]? Roles { get; set; }
}
