using System.Text.Json.Serialization;

namespace ThousandEyes.Api.Models.Templates;

/// <summary>
/// Collection of templates.
/// </summary>
public class Templates : ApiResource
{
	/// <summary>
	/// List of templates
	/// </summary>
	[JsonPropertyName("templates")]
	public List<TemplateResponse> Items { get; set; } = [];
}
