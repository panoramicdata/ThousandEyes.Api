using System.Text.Json.Serialization;

namespace ThousandEyes.Api.Models.EndpointAgents;

/// <summary>
/// Platform type for endpoint agents
/// </summary>
[JsonConverter(typeof(JsonStringEnumConverter))]
public enum Platform
{
	/// <summary>
	/// Windows operating system
	/// </summary>
	Windows,

	/// <summary>
	/// macOS operating system
	/// </summary>
	Mac,

	/// <summary>
	/// Linux operating system
	/// </summary>
	Linux,

	/// <summary>
	/// RoomOS (Cisco collaboration endpoints)
	/// </summary>
	RoomOs,

	/// <summary>
	/// Android operating system
	/// </summary>
	Android,

	/// <summary>
	/// Unknown platform
	/// </summary>
	Unknown
}