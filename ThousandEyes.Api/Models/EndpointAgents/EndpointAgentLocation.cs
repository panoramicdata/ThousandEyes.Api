using System.Text.Json.Serialization;

namespace ThousandEyes.Api.Models.EndpointAgents;

/// <summary>
/// Approximate location of the endpoint agent
/// </summary>
public class EndpointAgentLocation
{
	/// <summary>
	/// Latitude coordinate
	/// </summary>
	[JsonPropertyName("latitude")]
	public double? Latitude { get; set; }

	/// <summary>
	/// Longitude coordinate
	/// </summary>
	[JsonPropertyName("longitude")]
	public double? Longitude { get; set; }

	/// <summary>
	/// Location name (city, region, country)
	/// </summary>
	[JsonPropertyName("locationName")]
	public string? LocationName { get; set; }
}