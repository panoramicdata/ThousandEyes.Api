using System.Text.Json.Serialization;

namespace ThousandEyes.Api.Models.Tags;

/// <summary>
/// Request for bulk tag assignments.
/// </summary>
public class BulkTagAssignments
{
	/// <summary>
	/// List of bulk tag assignments.
	/// </summary>
	[JsonPropertyName("tags")]
	public List<BulkTagAssignment> Tags { get; set; } = [];
}
