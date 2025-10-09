using AwesomeAssertions;
using ThousandEyes.Api.Models.Emulation;
using Refit;

namespace ThousandEyes.Api.Test;

[Collection("Integration Tests")]
public class EmulationIntegrationTest(IntegrationTestFixture fixture) : TestBase(fixture)
{
	[Fact]
	public async Task GetUserAgents_WithValidRequest_ReturnsUserAgents()
	{
		try
		{
			// Act
			var result = await ThousandEyesClient.Emulation.UserAgents.GetAllAsync(
				aid: null,
				cancellationToken: CancellationToken);

			// Assert
			_ = result.Should().NotBeNull();
			_ = result.UserAgentsList.Should().NotBeNull();

			// If there are user agents, verify the structure
			if (result.UserAgentsList.Length > 0)
			{
				var firstUserAgent = result.UserAgentsList[0];
				_ = firstUserAgent.Value.Should().NotBeNullOrEmpty();
				// Browser and OS may be null for some user agents
			}
		}
		catch (ValidationApiException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
		{
			// Skip test if Emulation API is not available in ThousandEyes API v7
			return;
		}
	}

	[Fact]
	public async Task GetEmulatedDevices_WithValidRequest_ReturnsDevices()
	{
		try
		{
			// Act
			var result = await ThousandEyesClient.Emulation.EmulatedDevices.GetAllAsync(
				expand: null,
				cancellationToken: CancellationToken);

			// Assert
			_ = result.Should().NotBeNull();
			_ = result.EmulatedDevicesList.Should().NotBeNull();

			// If there are emulated devices, verify the structure
			if (result.EmulatedDevicesList.Length > 0)
			{
				var firstDevice = result.EmulatedDevicesList[0];
				_ = firstDevice.Id.Should().NotBeNullOrEmpty();
				_ = firstDevice.Name.Should().NotBeNullOrEmpty();
				_ = firstDevice.Category.Should().BeDefined();
				_ = firstDevice.Width.Should().BeGreaterThan(0);
				_ = firstDevice.Height.Should().BeGreaterThan(0);
			}
		}
		catch (ValidationApiException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
		{
			// Skip test if Emulation API is not available
			return;
		}
	}

	[Fact]
	public async Task GetEmulatedDevices_WithExpandUserAgent_ReturnsDevicesWithUserAgents()
	{
		try
		{
			// Act
			var result = await ThousandEyesClient.Emulation.EmulatedDevices.GetAllAsync(
				expand: [ExpandEmulatedDeviceOptions.UserAgent],
				cancellationToken: CancellationToken);

			// Assert
			_ = result.Should().NotBeNull();
			_ = result.EmulatedDevicesList.Should().NotBeNull();

			// If there are emulated devices, verify the structure with user agents
			if (result.EmulatedDevicesList.Length > 0)
			{
				var firstDevice = result.EmulatedDevicesList[0];
				_ = firstDevice.Id.Should().NotBeNullOrEmpty();
				_ = firstDevice.Name.Should().NotBeNullOrEmpty();
				
				// When expand=user-agent is used, devices should have user agent info
				if (firstDevice.AvailableUserAgents.Length > 0)
				{
					_ = firstDevice.AvailableUserAgents[0].Should().NotBeNullOrEmpty();
				}
			}
		}
		catch (ValidationApiException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
		{
			// Skip test if Emulation API is not available
			return;
		}
	}

	[Fact]
	public async Task CreateEmulatedDevice_WithValidRequest_CreatesDevice()
	{
		try
		{
			// Arrange
			var deviceRequest = new EmulatedDevice
			{
				Category = EmulatedDeviceCategory.Desktop,
				Width = 1920,
				Height = 1080
			};

			// Act
			var result = await ThousandEyesClient.Emulation.EmulatedDevices.CreateAsync(
				deviceRequest,
				aid: null,
				cancellationToken: CancellationToken);

			// Assert
			_ = result.Should().NotBeNull();
			_ = result.Id.Should().NotBeNullOrEmpty();
			_ = result.Category.Should().Be(deviceRequest.Category);
			_ = result.Width.Should().Be(deviceRequest.Width);
			_ = result.Height.Should().Be(deviceRequest.Height);

			// Note: This test doesn't clean up the created device as there's no delete endpoint
			// The device will remain in the system as per the API design
		}
		catch (ValidationApiException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
		{
			// Skip test if Emulation API is not available
			return;
		}
	}

	[Fact]
	public async Task CreateEmulatedDevice_WithTabletCategory_CreatesTabletDevice()
	{
		try
		{
			// Arrange
			var deviceRequest = new EmulatedDevice
			{
				Category = EmulatedDeviceCategory.Tablet,
				Width = 768,
				Height = 1024
			};

			// Act
			var result = await ThousandEyesClient.Emulation.EmulatedDevices.CreateAsync(
				deviceRequest,
				aid: null,
				cancellationToken: CancellationToken);

			// Assert
			_ = result.Should().NotBeNull();
			_ = result.Id.Should().NotBeNullOrEmpty();
			_ = result.Category.Should().Be(EmulatedDeviceCategory.Tablet);
			_ = result.Width.Should().Be(768);
			_ = result.Height.Should().Be(1024);
		}
		catch (ValidationApiException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
		{
			// Skip test if Emulation API is not available
			return;
		}
	}
}