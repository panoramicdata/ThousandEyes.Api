using System.Text.Json.Serialization;

namespace ThousandEyes.Api.Models.Integrations;

/// <summary>
/// Base class for connector authentication
/// </summary>
[JsonPolymorphic(TypeDiscriminatorPropertyName = "type")]
[JsonDerivedType(typeof(BasicAuthentication), "basic")]
[JsonDerivedType(typeof(BearerTokenAuthentication), "bearer-token")]
[JsonDerivedType(typeof(OtherTokenAuthentication), "other-token")]
[JsonDerivedType(typeof(OAuthCodeAuthentication), "oauth-auth-code")]
[JsonDerivedType(typeof(OAuthClientCredentialsAuthentication), "oauth-client-credentials")]
public abstract class ConnectorAuthentication
{
	/// <summary>
	/// Authentication type
	/// </summary>
	[JsonPropertyName("type")]
	public required AuthenticationType AuthenticationTypeValue { get; set; }
}
