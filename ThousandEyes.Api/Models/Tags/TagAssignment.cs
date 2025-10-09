using System.Text.Json.Serialization;

namespace ThousandEyes.Api.Models.Tags;

/// <summary>
/// Request for assigning a tag to one or more objects.
/// </summary>
public class TagAssignment
{
	/// <summary>
	/// List of object assignments.
	/// </summary>
	[JsonPropertyName("assignments")]
	public List<Assignment> Assignments { get; set; } = [];
}
