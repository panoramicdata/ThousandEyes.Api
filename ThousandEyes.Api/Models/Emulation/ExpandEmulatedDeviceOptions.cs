using System.Text.Json.Serialization;

namespace ThousandEyes.Api.Models.Emulation;

/// <summary>
/// Expand options for emulated device operations
/// </summary>
[JsonConverter(typeof(JsonStringEnumConverter))]
public enum ExpandEmulatedDeviceOptions
{
	/// <summary>
	/// Include user-agent templates in the response
	/// </summary>
	[JsonPropertyName("user-agent")]
	UserAgent
}