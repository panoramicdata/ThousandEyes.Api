using System.Text.Json.Serialization;

namespace ThousandEyes.Api.Models.Tags;

/// <summary>
/// Represents an error that occurred during bulk tag creation.
/// </summary>
public class TagBulkCreateError
{
	/// <summary>
	/// The tag that failed to be created.
	/// </summary>
	[JsonPropertyName("tag")]
	public Dictionary<string, TagInfo>? Tag { get; set; }

	/// <summary>
	/// HTTP response code.
	/// </summary>
	[JsonPropertyName("responseCode")]
	public int? ResponseCode { get; set; }

	/// <summary>
	/// Status or error message.
	/// </summary>
	[JsonPropertyName("message")]
	public string? Message { get; set; }
}
