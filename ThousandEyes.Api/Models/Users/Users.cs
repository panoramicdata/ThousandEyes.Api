namespace ThousandEyes.Api.Models.Users;

/// <summary>
/// Users response wrapper
/// </summary>
public class Users
{
	/// <summary>
	/// List of users
	/// </summary>
	public ExtendedUser[] UsersList { get; set; } = [];

	/// <summary>
	/// Navigation links
	/// </summary>
	public SelfLinks? Links { get; set; }
}
