using System.Text.Json.Serialization;

namespace ThousandEyes.Api.Models.Templates;

/// <summary>
/// A ThousandEyes module this template belongs to.
/// Regular users can only set to default.
/// </summary>
[JsonConverter(typeof(JsonStringEnumConverter))]
public enum TemplateModule
{
	/// <summary>
	/// Default module (only option for regular users)
	/// </summary>
	Default
}
