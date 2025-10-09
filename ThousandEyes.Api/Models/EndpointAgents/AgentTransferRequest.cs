using System.Text.Json.Serialization;

namespace ThousandEyes.Api.Models.EndpointAgents;

/// <summary>
/// Request to transfer an endpoint agent to another account
/// </summary>
public class AgentTransferRequest
{
	/// <summary>
	/// Target account group ID
	/// </summary>
	[JsonPropertyName("toAid")]
	public required string ToAid { get; set; }
}