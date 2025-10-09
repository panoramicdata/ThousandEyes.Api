using System.Text.Json.Serialization;

namespace ThousandEyes.Api.Models.EndpointAgents;

/// <summary>
/// User profile information for endpoint agent
/// </summary>
public class EndpointUserProfile
{
	/// <summary>
	/// Username
	/// </summary>
	[JsonPropertyName("userName")]
	public required string UserName { get; set; }

	/// <summary>
	/// User principal name (UPN) - typically user@domain.com
	/// </summary>
	[JsonPropertyName("userPrincipalName")]
	public string? UserPrincipalName { get; set; }
}