using System.Text.Json.Serialization;

namespace ThousandEyes.Api.Models.Users;

/// <summary>
/// List of users
/// </summary>
public class Users
{
	/// <summary>
	/// Users
	/// </summary>
	public User[] UsersList { get; set; } = [];

	/// <summary>
	/// Navigation links
	/// </summary>
	[JsonPropertyName("_links")]
	public Links? Links { get; set; }
}
