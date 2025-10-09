using System.Text.Json.Serialization;

namespace ThousandEyes.Api.Models.Emulation;

/// <summary>
/// Collection of user-agent strings
/// </summary>
public class UserAgents : ApiResource
{
	/// <summary>
	/// List of user-agent strings
	/// </summary>
	[JsonPropertyName("userAgents")]
	public UserAgent[] UserAgentsList { get; set; } = [];
}