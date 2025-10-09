using System.Text.Json.Serialization;

namespace ThousandEyes.Api.Models.EndpointAgents;

/// <summary>
/// Autonomous System Number details for the endpoint agent
/// </summary>
public class EndpointAsnDetails
{
	/// <summary>
	/// Autonomous system number
	/// </summary>
	[JsonPropertyName("asNumber")]
	public required int AsNumber { get; set; }

	/// <summary>
	/// Name of autonomous system
	/// </summary>
	[JsonPropertyName("asName")]
	public string? AsName { get; set; }
}