using System.Text.Json.Serialization;

namespace ThousandEyes.Api.Models.Tags;

/// <summary>
/// Represents tag information without the _links property.
/// Used for create and update operations.
/// </summary>
public class TagInfo : ApiResource
{
	/// <summary>
	/// The tag ID.
	/// </summary>
	[JsonPropertyName("id")]
	public string? Id { get; set; }

	/// <summary>
	/// The account group ID.
	/// </summary>
	[JsonPropertyName("aid")]
	public long? Aid { get; set; }

	/// <summary>
	/// The tag's key.
	/// </summary>
	[JsonPropertyName("key")]
	public string? Key { get; set; }

	/// <summary>
	/// The tag's value.
	/// </summary>
	[JsonPropertyName("value")]
	public string? Value { get; set; }

	/// <summary>
	/// Tag color in hex format (e.g., #FF0000).
	/// </summary>
	[JsonPropertyName("color")]
	public string? Color { get; set; }

	/// <summary>
	/// Tag icon identifier.
	/// </summary>
	[JsonPropertyName("icon")]
	public string? Icon { get; set; }

	/// <summary>
	/// The tag's description.
	/// </summary>
	[JsonPropertyName("description")]
	public string? Description { get; set; }

	/// <summary>
	/// The object type associated with the tag.
	/// </summary>
	[JsonPropertyName("objectType")]
	public ObjectType? ObjectType { get; set; }

	/// <summary>
	/// The access level of the tag.
	/// </summary>
	[JsonPropertyName("accessType")]
	public AccessType? AccessType { get; set; }

	/// <summary>
	/// Tag creation date in ISO 8601 format.
	/// </summary>
	[JsonPropertyName("createDate")]
	public string? CreateDate { get; set; }

	/// <summary>
	/// Legacy tag ID for backwards compatibility.
	/// </summary>
	[JsonPropertyName("legacyId")]
	public long? LegacyId { get; set; }

	/// <summary>
	/// List of assignments for this tag (only populated when expand=assignments).
	/// </summary>
	[JsonPropertyName("assignments")]
	public List<Assignment> Assignments { get; set; } = [];
}
