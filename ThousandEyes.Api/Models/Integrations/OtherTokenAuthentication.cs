namespace ThousandEyes.Api.Models.Integrations;

/// <summary>
/// Other token-based authentication
/// </summary>
public class OtherTokenAuthentication : ConnectorAuthentication
{
	/// <summary>
	/// Authentication token
	/// </summary>
	public required string Token { get; set; }
}
