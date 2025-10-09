using System.Text.Json.Serialization;

namespace ThousandEyes.Api.Models.EndpointAgents;

/// <summary>
/// Filter request for endpoint agents
/// </summary>
public class AgentSearchRequest
{
	/// <summary>
	/// Search filters
	/// </summary>
	[JsonPropertyName("searchFilters")]
	public AgentSearchFilters? SearchFilters { get; set; }
}