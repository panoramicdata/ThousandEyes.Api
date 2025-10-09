using System.Text.Json.Serialization;

namespace ThousandEyes.Api.Models.Templates;

/// <summary>
/// Base properties for template responses.
/// </summary>
public class TemplateResponseBase
{
	/// <summary>
	/// The ID of the template
	/// </summary>
	[JsonPropertyName("id")]
	public string? Id { get; set; }

	/// <summary>
	/// The date and time the template was created
	/// </summary>
	[JsonPropertyName("dateCreated")]
	public DateTime? DateCreated { get; set; }
}
