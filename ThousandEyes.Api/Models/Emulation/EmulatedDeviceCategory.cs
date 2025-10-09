using System.Text.Json.Serialization;

namespace ThousandEyes.Api.Models.Emulation;

/// <summary>
/// The type of device being emulated
/// </summary>
[JsonConverter(typeof(JsonStringEnumConverter))]
public enum EmulatedDeviceCategory
{
	/// <summary>
	/// Desktop computer
	/// </summary>
	Desktop,

	/// <summary>
	/// Laptop computer
	/// </summary>
	Laptop,

	/// <summary>
	/// Mobile phone
	/// </summary>
	Phone,

	/// <summary>
	/// Tablet device
	/// </summary>
	Tablet
}