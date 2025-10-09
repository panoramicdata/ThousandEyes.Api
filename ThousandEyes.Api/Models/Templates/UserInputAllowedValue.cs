using System.Text.Json.Serialization;

namespace ThousandEyes.Api.Models.Templates;

/// <summary>
/// The list of values that the user is allowed to use as inputs.
/// </summary>
public class UserInputAllowedValue
{
	/// <summary>
	/// The value of the allowed input
	/// </summary>
	[JsonPropertyName("value")]
	public UserInputValue? Value { get; set; }

	/// <summary>
	/// The name of the value, which will be used for display in the UI and API messages
	/// </summary>
	[JsonPropertyName("name")]
	public string? Name { get; set; }
}
