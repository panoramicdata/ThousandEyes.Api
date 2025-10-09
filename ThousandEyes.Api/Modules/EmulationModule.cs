using Refit;
using ThousandEyes.Api.Interfaces;

namespace ThousandEyes.Api.Modules;

/// <summary>
/// Emulation API module for device emulation and user-agent management
/// </summary>
/// <remarks>
/// Phase 6.4 implementation - Complete emulation functionality
/// The Emulation API facilitates the retrieval of user-agent strings for HTTP, pageload,
/// and transaction tests. It also enables the retrieval and addition of emulated devices
/// for pageload and transaction tests.
/// </remarks>
public class EmulationModule
{
	/// <summary>
	/// Gets the User Agents API for user-agent string management
	/// </summary>
	public IUserAgentsApi UserAgents { get; }

	/// <summary>
	/// Gets the Emulated Devices API for device emulation management
	/// </summary>
	public IEmulatedDevicesApi EmulatedDevices { get; }

	/// <summary>
	/// Initializes a new instance of the EmulationModule
	/// </summary>
	/// <param name="httpClient">The HTTP client to use for API calls</param>
	/// <param name="refitSettings">Refit settings for JSON serialization</param>
	public EmulationModule(HttpClient httpClient, RefitSettings refitSettings)
	{
		// Create Refit API interfaces
		var userAgentsRefitApi = RestService.For<IUserAgentsRefitApi>(httpClient, refitSettings);
		var emulatedDevicesRefitApi = RestService.For<IEmulatedDevicesRefitApi>(httpClient, refitSettings);

		// Initialize API implementations
		UserAgents = new UserAgentsApi(userAgentsRefitApi);
		EmulatedDevices = new EmulatedDevicesApi(emulatedDevicesRefitApi);
	}
}