using System.Text.Json.Serialization;

namespace ThousandEyes.Api.Models.Emulation;

/// <summary>
/// Emulated device configuration for pageload and transaction tests
/// </summary>
public class EmulatedDevice
{
	/// <summary>
	/// The type of device being emulated
	/// </summary>
	[JsonPropertyName("category")]
	public required EmulatedDeviceCategory Category { get; set; }

	/// <summary>
	/// The width of the display of the emulated device
	/// </summary>
	[JsonPropertyName("width")]
	public required int Width { get; set; }

	/// <summary>
	/// The height of the display of the emulated device
	/// </summary>
	[JsonPropertyName("height")]
	public required int Height { get; set; }
}