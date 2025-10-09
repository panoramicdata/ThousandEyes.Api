namespace ThousandEyes.Api.Models.Integrations;

/// <summary>
/// OAuth client credentials flow authentication
/// </summary>
public class OAuthClientCredentialsAuthentication : ConnectorAuthentication
{
	/// <summary>
	/// Access token
	/// </summary>
	public string? Token { get; set; }

	/// <summary>
	/// OAuth client ID
	/// </summary>
	public required string OAuthClientId { get; set; }

	/// <summary>
	/// OAuth token URL
	/// </summary>
	public required string OAuthTokenUrl { get; set; }

	/// <summary>
	/// OAuth client secret
	/// </summary>
	public required string OAuthClientSecret { get; set; }
}
