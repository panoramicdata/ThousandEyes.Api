using System.Text.Json.Serialization;

namespace ThousandEyes.Api.Models.Tags;

/// <summary>
/// Represents a bulk tag assignment with tag ID and assignments.
/// </summary>
public class BulkTagAssignment
{
	/// <summary>
	/// The ID of the tag to assign.
	/// </summary>
	[JsonPropertyName("tagId")]
	public string? TagId { get; set; }

	/// <summary>
	/// List of object assignments.
	/// </summary>
	[JsonPropertyName("assignments")]
	public List<Assignment> Assignments { get; set; } = [];
}
