using System.Text.Json.Serialization;
using System.Text.Json.Serialization.Metadata;

namespace ThousandEyes.Api.Models.Integrations;

/// <summary>
/// Operation category classification
/// </summary>
[JsonConverter(typeof(JsonStringEnumConverter))]
public enum OperationCategory
{
	/// <summary>
	/// Alert-based operations
	/// </summary>
	[JsonPropertyName("alerts")]
	Alerts,

	/// <summary>
	/// Recommendation-based operations
	/// </summary>
	[JsonPropertyName("recommendations")]
	Recommendations,

	/// <summary>
	/// Traffic monitoring operations
	/// </summary>
	[JsonPropertyName("traffic-monitoring")]
	TrafficMonitoring
}
