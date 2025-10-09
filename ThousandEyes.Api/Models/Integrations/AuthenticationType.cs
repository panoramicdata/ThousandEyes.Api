namespace ThousandEyes.Api.Models.Integrations;

/// <summary>
/// Authentication type for connectors
/// </summary>
public enum AuthenticationType
{
	/// <summary>
	/// Basic authentication (username/password)
	/// </summary>
	Basic,

	/// <summary>
	/// Bearer token authentication
	/// </summary>
	BearerToken,

	/// <summary>
	/// OAuth authorization code flow
	/// </summary>
	OAuthAuthCode,

	/// <summary>
	/// OAuth client credentials flow
	/// </summary>
	OAuthClientCredentials,

	/// <summary>
	/// Other token-based authentication
	/// </summary>
	OtherToken
}
