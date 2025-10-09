using System.Text.Json.Serialization;

namespace ThousandEyes.Api.Models.Tags;

/// <summary>
/// Represents a tag assignment to an object.
/// </summary>
public class Assignment
{
	/// <summary>
	/// Object ID to assign the tag to.
	/// </summary>
	[JsonPropertyName("id")]
	public string? Id { get; set; }

	/// <summary>
	/// The type of object being assigned.
	/// </summary>
	[JsonPropertyName("type")]
	public AssignmentType? Type { get; set; }
}
