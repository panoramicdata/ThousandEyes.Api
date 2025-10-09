using System.Text.Json.Serialization;

namespace ThousandEyes.Api.Models.Templates;

/// <summary>
/// The deployment strategy to apply to the asset.
/// </summary>
[JsonConverter(typeof(JsonStringEnumConverter))]
public enum DeploymentStrategy
{
	/// <summary>
	/// Always create a new asset. Generates an error if asset already exists.
	/// This is the default behavior.
	/// </summary>
	Create,

	/// <summary>
	/// Use existing asset if found, apply latest configuration from template.
	/// </summary>
	Update,

	/// <summary>
	/// Use existing asset if found, ignore configuration from template.
	/// </summary>
	Ignore
}
