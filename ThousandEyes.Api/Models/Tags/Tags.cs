using System.Text.Json.Serialization;

namespace ThousandEyes.Api.Models.Tags;

/// <summary>
/// Represents a collection of tags.
/// </summary>
public class Tags : ApiResource
{
	/// <summary>
	/// List of tags.
	/// </summary>
	[JsonPropertyName("tags")]
	public List<Tag> Items { get; set; } = [];
}
