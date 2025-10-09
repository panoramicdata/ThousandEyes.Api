using System.Text.Json.Serialization;

namespace ThousandEyes.Api.Models.Templates;

/// <summary>
/// A user input is a value that the user must provide when deploying a template.
/// User Input values are provided by the user in the UI under the Global Settings section.
/// When deploying via the API, User Inputs values are specified in the payload using the userInputValues field.
/// </summary>
public class UserInput
{
	/// <summary>
	/// The name of the user input field
	/// </summary>
	[JsonPropertyName("name")]
	public required string Name { get; set; }

	/// <summary>
	/// The type of user input
	/// </summary>
	[JsonPropertyName("type")]
	public required UserInputType Type { get; set; }

	/// <summary>
	/// The title of the user input field; may be used by UI
	/// </summary>
	[JsonPropertyName("title")]
	public string? Title { get; set; }

	/// <summary>
	/// Description of the user input field; used by UI
	/// </summary>
	[JsonPropertyName("description")]
	public string? Description { get; set; }

	/// <summary>
	/// Default value for the user input
	/// </summary>
	[JsonPropertyName("defaultValue")]
	public UserInputValue? DefaultValue { get; set; }

	/// <summary>
	/// Allowed values for the User Input.
	/// An array of name/value pairs that specify specific values that can be used.
	/// In the UI, user inputs with allowedValues will be displayed as a drop down selector.
	/// </summary>
	[JsonPropertyName("allowedValues")]
	public List<UserInputAllowedValue> AllowedValues { get; set; } = [];
}
