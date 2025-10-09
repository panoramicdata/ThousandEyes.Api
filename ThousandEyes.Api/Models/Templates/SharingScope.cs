using System.Text.Json.Serialization;

namespace ThousandEyes.Api.Models.Templates;

/// <summary>
/// The scope of the template sharing.
/// </summary>
[JsonConverter(typeof(JsonStringEnumConverter))]
public enum SharingScope
{
	/// <summary>
	/// The template is visible only within the account group that owns it
	/// </summary>
	Default,

	/// <summary>
	/// The template is visible by all the users within the organization that owns it
	/// </summary>
	Organization
}
