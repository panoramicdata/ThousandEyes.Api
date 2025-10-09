using ThousandEyes.Api.Interfaces;
using ThousandEyes.Api.Models.Emulation;

namespace ThousandEyes.Api;

/// <summary>
/// Implementation of Emulated Devices API using Refit
/// </summary>
internal class EmulatedDevicesApi(IEmulatedDevicesRefitApi refitApi) : IEmulatedDevicesApi
{
	private readonly IEmulatedDevicesRefitApi _refitApi = refitApi;

	/// <inheritdoc />
	public Task<EmulatedDeviceResponses> GetAllAsync(ExpandEmulatedDeviceOptions[]? expand, CancellationToken cancellationToken) =>
		_refitApi.GetAllAsync(expand, cancellationToken);

	/// <inheritdoc />
	public Task<EmulatedDeviceResponse> CreateAsync(EmulatedDevice request, string? aid, CancellationToken cancellationToken) =>
		_refitApi.CreateAsync(request, aid, cancellationToken);
}