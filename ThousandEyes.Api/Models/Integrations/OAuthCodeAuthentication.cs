namespace ThousandEyes.Api.Models.Integrations;

/// <summary>
/// OAuth authorization code flow authentication
/// </summary>
public class OAuthCodeAuthentication : ConnectorAuthentication
{
	/// <summary>
	/// Access token
	/// </summary>
	public string? Token { get; set; }

	/// <summary>
	/// Refresh token
	/// </summary>
	public string? RefreshToken { get; set; }

	/// <summary>
	/// OAuth client ID
	/// </summary>
	public required string OAuthClientId { get; set; }

	/// <summary>
	/// OAuth authorization URL
	/// </summary>
	public required string OAuthAuthUrl { get; set; }

	/// <summary>
	/// OAuth token URL
	/// </summary>
	public required string OAuthTokenUrl { get; set; }

	/// <summary>
	/// OAuth client secret
	/// </summary>
	public required string OAuthClientSecret { get; set; }

	/// <summary>
	/// Authorization code
	/// </summary>
	public required string Code { get; set; }

	/// <summary>
	/// Redirect URI
	/// </summary>
	public required string RedirectUri { get; set; }
}
