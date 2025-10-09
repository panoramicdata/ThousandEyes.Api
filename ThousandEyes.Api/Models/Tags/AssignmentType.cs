using System.Text.Json.Serialization;

namespace ThousandEyes.Api.Models.Tags;

/// <summary>
/// The type of object for tag assignment.
/// </summary>
[JsonConverter(typeof(JsonStringEnumConverter))]
public enum AssignmentType
{
	/// <summary>
	/// Test object type
	/// </summary>
	Test,

	/// <summary>
	/// Virtual agent object type
	/// </summary>
	[JsonPropertyName("v-agent")]
	VAgent,

	/// <summary>
	/// Endpoint test object type
	/// </summary>
	[JsonPropertyName("endpoint-test")]
	EndpointTest,

	/// <summary>
	/// Dashboard object type
	/// </summary>
	Dashboard
}
