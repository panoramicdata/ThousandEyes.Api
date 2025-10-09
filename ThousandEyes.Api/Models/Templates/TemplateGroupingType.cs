using System.Text.Json.Serialization;

namespace ThousandEyes.Api.Models.Templates;

/// <summary>
/// The type of resources included in the grouping.
/// </summary>
[JsonConverter(typeof(JsonStringEnumConverter))]
public enum TemplateGroupingType
{
	/// <summary>
	/// Test grouping
	/// </summary>
	Test,

	/// <summary>
	/// User input grouping
	/// </summary>
	[JsonPropertyName("user-input")]
	UserInput
}
