using System.Text.Json.Serialization;

namespace ThousandEyes.Api.Models.Emulation;

/// <summary>
/// Collection of emulated device responses
/// </summary>
public class EmulatedDeviceResponses : ApiResource
{
	/// <summary>
	/// List of emulated devices
	/// </summary>
	[JsonPropertyName("emulatedDevices")]
	public EmulatedDeviceResponse[] EmulatedDevicesList { get; set; } = [];
}