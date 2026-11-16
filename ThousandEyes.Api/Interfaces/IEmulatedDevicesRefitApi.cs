using Refit;
using ThousandEyes.Api.Models.Emulation;

namespace ThousandEyes.Api.Interfaces;

/// <summary>
/// Internal Refit interface for Emulated Devices API
/// </summary>
internal interface IEmulatedDevicesRefitApi
{
	/// <summary>
	/// Get all emulated devices
	/// </summary>
	[Get("/emulated-devices")]
	Task<EmulatedDeviceResponses> GetAllAsync([Query] ExpandEmulatedDeviceOptions[]? expand, CancellationToken ct);

	/// <summary>
	/// Create emulated device
	/// </summary>
	[Post("/emulated-devices")]
	Task<EmulatedDeviceResponse> CreateAsync([Body] EmulatedDevice request, [Query] string? aid, CancellationToken ct);
}