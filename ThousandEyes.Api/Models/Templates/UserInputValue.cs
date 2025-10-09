using System.Text.Json;
using System.Text.Json.Serialization;

namespace ThousandEyes.Api.Models.Templates;

/// <summary>
/// A user input value. The type of object depends on the user input type field.
/// Can be string, number, object, or arrays of these types.
/// </summary>
public class UserInputValue
{
	/// <summary>
	/// The underlying JSON element that can represent any type
	/// </summary>
	[JsonPropertyName("value")]
	public JsonElement? Value { get; set; }

	/// <summary>
	/// Gets the value as a string, if applicable
	/// </summary>
	public string? AsString() => Value?.ValueKind == JsonValueKind.String ? Value.Value.GetString() : null;

	/// <summary>
	/// Gets the value as a number, if applicable
	/// </summary>
	public double? AsNumber() => Value?.ValueKind == JsonValueKind.Number ? Value.Value.GetDouble() : null;

	/// <summary>
	/// Gets the value as an object, if applicable
	/// </summary>
	public JsonElement? AsObject() => Value?.ValueKind == JsonValueKind.Object ? Value : null;

	/// <summary>
	/// Gets the value as an array, if applicable
	/// </summary>
	public JsonElement? AsArray() => Value?.ValueKind == JsonValueKind.Array ? Value : null;
}
