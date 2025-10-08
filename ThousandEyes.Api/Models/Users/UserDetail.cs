using System.Text.Json.Serialization;
using ThousandEyes.Api.Models.Roles;

namespace ThousandEyes.Api.Models.Users;

/// <summary>
/// Detailed user information
/// </summary>
public class UserDetail : ExtendedUser
{
	/// <summary>
	/// Account group roles
	/// </summary>
	public AccountGroupRole[]? AccountGroupRoles { get; set; }

	/// <summary>
	/// All account group roles
	/// </summary>
	public Role[]? AllAccountGroupRoles { get; set; }

	/// <summary>
	/// Navigation links
	/// </summary>
	[JsonPropertyName("_links")]
	public Links? Links { get; set; }
}
