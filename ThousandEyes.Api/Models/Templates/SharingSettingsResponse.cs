using System.Text.Json.Serialization;

namespace ThousandEyes.Api.Models.Templates;

/// <summary>
/// Sharing settings response with hypermedia links.
/// </summary>
public class SharingSettingsResponse : ApiResource
{
	/// <summary>
	/// The scope of the template sharing
	/// </summary>
	[JsonPropertyName("scope")]
	public required SharingScope Scope { get; set; }
}
