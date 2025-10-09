using System.Text.Json.Serialization;

namespace ThousandEyes.Api.Models.Templates;

/// <summary>
/// Represents a grouping of different template resources for display purposes in the UI.
/// This grouping does not affect template deployment.
/// </summary>
public class TemplateGrouping
{
	/// <summary>
	/// The name of the grouping
	/// </summary>
	[JsonPropertyName("name")]
	public required string Name { get; set; }

	/// <summary>
	/// The title of the grouping
	/// </summary>
	[JsonPropertyName("title")]
	public required string Title { get; set; }

	/// <summary>
	/// The type of resources included in the grouping
	/// </summary>
	[JsonPropertyName("type")]
	public required TemplateGroupingType Type { get; set; }

	/// <summary>
	/// Description of the grouping
	/// </summary>
	[JsonPropertyName("description")]
	public string? Description { get; set; }

	/// <summary>
	/// A list of items within the group, corresponding to the keys in the userInputs or tests fields.
	/// Each key must be unique within its respective group.
	/// </summary>
	[JsonPropertyName("items")]
	public List<string> Items { get; set; } = [];
}
