using System.Text.Json.Serialization;

namespace ThousandEyes.Api.Models.Tags;

/// <summary>
/// The object type associated with the tag.
/// </summary>
[JsonConverter(typeof(JsonStringEnumConverter))]
public enum ObjectType
{
	/// <summary>
	/// Test object type
	/// </summary>
	Test,

	/// <summary>
	/// Dashboard object type
	/// </summary>
	Dashboard,

	/// <summary>
	/// Endpoint test object type
	/// </summary>
	[JsonPropertyName("endpoint-test")]
	EndpointTest,

	/// <summary>
	/// Virtual agent object type
	/// </summary>
	[JsonPropertyName("v-agent")]
	VAgent
}
