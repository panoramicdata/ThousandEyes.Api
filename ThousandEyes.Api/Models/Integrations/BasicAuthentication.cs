namespace ThousandEyes.Api.Models.Integrations;

/// <summary>
/// Basic authentication (username/password)
/// </summary>
public class BasicAuthentication : ConnectorAuthentication
{
	/// <summary>
	/// Username
	/// </summary>
	public required string Username { get; set; }

	/// <summary>
	/// Password
	/// </summary>
	public required string Password { get; set; }
}
