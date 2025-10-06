namespace ThousandEyes.Api.Models.Users;

/// <summary>
/// Basic user information
/// </summary>
public class User
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
	/// Registration date
	/// </summary>
	public DateTime DateRegistered { get; set; }

	/// <summary>
	/// Login account group
	/// </summary>
	public AccountGroup? LoginAccountGroup { get; set; }
}
