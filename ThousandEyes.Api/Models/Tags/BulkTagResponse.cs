using System.Text.Json.Serialization;

namespace ThousandEyes.Api.Models.Tags;

/// <summary>
/// Response for bulk tag creation operations.
/// Includes both successfully created tags and errors for failed operations.
/// </summary>
public class BulkTagResponse
{
	/// <summary>
	/// List of successfully created tags.
	/// </summary>
	[JsonPropertyName("tags")]
	public List<Tag> Tags { get; set; } = [];

	/// <summary>
	/// List of errors for failed tag creations.
	/// </summary>
	[JsonPropertyName("errors")]
	public List<TagBulkCreateError> Errors { get; set; } = [];
}
