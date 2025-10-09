namespace ThousandEyes.Api.Models.Integrations;

/// <summary>
/// Bearer token authentication
/// </summary>
public class BearerTokenAuthentication : ConnectorAuthentication
{
	/// <summary>
	/// Bearer token
	/// </summary>
	public required string Token { get; set; }
}
