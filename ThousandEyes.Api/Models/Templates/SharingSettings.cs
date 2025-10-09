using System.Text.Json.Serialization;

namespace ThousandEyes.Api.Models.Templates;

/// <summary>
/// The Template sharing settings.
/// </summary>
public class SharingSettings
{
	/// <summary>
	/// The scope of the template sharing
	/// </summary>
	[JsonPropertyName("scope")]
	public required SharingScope Scope { get; set; }
}
