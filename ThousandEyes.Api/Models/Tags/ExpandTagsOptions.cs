using System.Text.Json.Serialization;

namespace ThousandEyes.Api.Models.Tags;

/// <summary>
/// Options for expanding tag data.
/// </summary>
[JsonConverter(typeof(JsonStringEnumConverter))]
public enum ExpandTagsOptions
{
	/// <summary>
	/// Include tag assignments
	/// </summary>
	Assignments
}
