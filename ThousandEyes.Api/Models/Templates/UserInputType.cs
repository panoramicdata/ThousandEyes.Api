using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace ThousandEyes.Api.Models.Templates;

/// <summary>
/// The type of user input.
/// This defines the value the user can provide as well as the UI component displayed.
/// </summary>
[JsonConverter(typeof(JsonStringEnumConverter))]
[SuppressMessage("Naming", "CA1720:Identifier contains type name", Justification = "API-defined enum values match specification")]
public enum UserInputType
{
	/// <summary>
	/// User input expects a string value; UI rendered as a text box
	/// </summary>
	String,

	/// <summary>
	/// User input expects a numeric value; UI rendered as a text box
	/// </summary>
	Number,

	/// <summary>
	/// User input expects a boolean value; UI rendered as a check box
	/// </summary>
	Boolean,

	/// <summary>
	/// User input expects an array of string values; UI rendered as drop down
	/// </summary>
	[JsonPropertyName("string[]")]
	StringArray,

	/// <summary>
	/// User input expects an array of numeric values; UI rendered as drop down
	/// </summary>
	[JsonPropertyName("number[]")]
	NumberArray,

	/// <summary>
	/// User input expects an array of boolean values; UI rendered as drop down
	/// </summary>
	[JsonPropertyName("boolean[]")]
	BooleanArray,

	/// <summary>
	/// User input expects an array of agent IDs; UI rendered as Agent Selector drop down
	/// </summary>
	Agents,

	/// <summary>
	/// User input expects an array of test IDs; UI rendered as Test Selector drop down
	/// </summary>
	Tests,

	/// <summary>
	/// For any other user inputs that don't belong to the types listed above
	/// </summary>
	Any
}
