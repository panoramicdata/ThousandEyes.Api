using System.Text.Json;
using System.Text.Json.Serialization;

namespace ThousandEyes.Api.Models.Templates;

/// <summary>
/// Request for deploying a template.
/// </summary>
public class DeployTemplate
{
	/// <summary>
	/// Specifies the values to be used for all userInputs defined in the template.
	/// User must provide a value for each User Input defined in the template being deployed.
	/// </summary>
	[JsonPropertyName("userInputValues")]
	public Dictionary<string, JsonElement> UserInputValues { get; set; } = [];

	/// <summary>
	/// The name of the deployed template.
	/// This value will be used anywhere the {{name}} expression is used in the template being deployed.
	/// </summary>
	[JsonPropertyName("name")]
	public string? Name { get; set; }
}
