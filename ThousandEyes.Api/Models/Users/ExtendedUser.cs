namespace ThousandEyes.Api.Models.Users;

/// <summary>
/// Extended user information with last login
/// </summary>
public class ExtendedUser : User
{
	/// <summary>
	/// Last login date
	/// </summary>
	public DateTime? LastLogin { get; set; }
}
