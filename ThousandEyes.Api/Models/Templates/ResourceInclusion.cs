using System.Text.Json.Serialization;

namespace ThousandEyes.Api.Models.Templates;

/// <summary>
/// The deployment inclusion configuration for the asset.
/// </summary>
[JsonConverter(typeof(JsonStringEnumConverter))]
public enum ResourceInclusion
{
	/// <summary>
	/// The system will not create the asset
	/// </summary>
	Skipped,

	/// <summary>
	/// The system will always create the asset
	/// </summary>
	Included
}
