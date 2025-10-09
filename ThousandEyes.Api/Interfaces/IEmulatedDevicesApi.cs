using ThousandEyes.Api.Models.Emulation;

namespace ThousandEyes.Api.Interfaces;

/// <summary>
/// Interface for Emulated Devices API operations
/// </summary>
/// <remarks>
/// Phase 6.4 implementation - Device emulation for browser tests
/// </remarks>
public interface IEmulatedDevicesApi
{
	/// <summary>
	/// Retrieves a list of emulated devices available for browser tests
	/// </summary>
	/// <param name="expand">Optional expansion parameters</param>
	/// <param name="cancellationToken">Cancellation token</param>
	/// <returns>List of emulated devices</returns>
	Task<EmulatedDeviceResponses> GetAllAsync(ExpandEmulatedDeviceOptions[]? expand, CancellationToken cancellationToken);

	/// <summary>
	/// Creates a new device for emulation
	/// </summary>
	/// <param name="request">Emulated device configuration</param>
	/// <param name="aid">Account group ID (optional)</param>
	/// <param name="cancellationToken">Cancellation token</param>
	/// <returns>Created emulated device</returns>
	Task<EmulatedDeviceResponse> CreateAsync(EmulatedDevice request, string? aid, CancellationToken cancellationToken);
}