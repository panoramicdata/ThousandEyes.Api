using System.Text.Json.Serialization;

namespace ThousandEyes.Api.Models.Emulation;

/// <summary>
/// Emulated device response with ID and additional properties
/// </summary>
public class EmulatedDeviceResponse : EmulatedDevice
{
	/// <summary>
	/// The device name
	/// </summary>
	[JsonPropertyName("name")]
	public string? Name { get; set; }

	/// <summary>
	/// A code corresponding to the device name
	/// </summary>
	[JsonPropertyName("codeName")]
	public string? CodeName { get; set; }

	/// <summary>
	/// ID of the emulated device
	/// </summary>
	[JsonPropertyName("id")]
	public string? Id { get; set; }

	/// <summary>
	/// A list of user-agent strings for this emulated device
	/// </summary>
	[JsonPropertyName("availableUserAgents")]
	public string[] AvailableUserAgents { get; set; } = [];

	/// <summary>
	/// The default user-agent template to use for this device
	/// </summary>
	[JsonPropertyName("defaultUserAgentTemplate")]
	public string? DefaultUserAgentTemplate { get; set; }
}