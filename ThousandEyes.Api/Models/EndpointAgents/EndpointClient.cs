using System.Text.Json.Serialization;

namespace ThousandEyes.Api.Models.EndpointAgents;

/// <summary>
/// Information about a client (user) on the endpoint agent
/// </summary>
public class EndpointClient
{
	/// <summary>
	/// User profile information
	/// </summary>
	[JsonPropertyName("userProfile")]
	public EndpointUserProfile? UserProfile { get; set; }
}