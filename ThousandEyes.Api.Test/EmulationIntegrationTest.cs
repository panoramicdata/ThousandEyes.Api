using AwesomeAssertions;
using ThousandEyes.Api.Models.Emulation;

namespace ThousandEyes.Api.Test;

[Collection("Integration Tests")]
[Trait("Category", "Integration")]
public class EmulationIntegrationTest(IntegrationTestFixture fixture) : TestBase(fixture)
{
	private async Task CreateDeviceAndCheckAsync(EmulatedDeviceCategory category, int width, int height)
	{
		var deviceRequest = new EmulatedDevice { Category = category, Width = width, Height = height };

		var result = await ThousandEyesClient.Emulation.EmulatedDevices.CreateAsync(deviceRequest, aid: null, cancellationToken: CancellationToken);

		_ = result.Id.Should().NotBeNullOrEmpty();
		_ = result.Category.Should().Be(category);
		_ = result.Width.Should().Be(width);
		_ = result.Height.Should().Be(height);
	}

	[Fact]
	public Task GetUserAgents_WithValidRequest_ReturnsUserAgents() => RunIfAvailableAsync(async () =>
	{
		var result = await ThousandEyesClient.Emulation.UserAgents.GetAllAsync(aid: null, cancellationToken: CancellationToken);

		_ = result.UserAgentsList.Should().NotBeNull();
		if (result.UserAgentsList.Length > 0)
		{
			_ = result.UserAgentsList[0].Value.Should().NotBeNullOrEmpty();
		}
	});

	[Fact]
	public Task GetEmulatedDevices_WithValidRequest_ReturnsDevices() => RunIfAvailableAsync(async () =>
	{
		var result = await ThousandEyesClient.Emulation.EmulatedDevices.GetAllAsync(expand: null, cancellationToken: CancellationToken);

		_ = result.EmulatedDevicesList.Should().NotBeNull();
		if (result.EmulatedDevicesList.Length > 0)
		{
			var firstDevice = result.EmulatedDevicesList[0];
			_ = firstDevice.Id.Should().NotBeNullOrEmpty();
			_ = firstDevice.Name.Should().NotBeNullOrEmpty();
			_ = firstDevice.Category.Should().BeDefined();
			_ = firstDevice.Width.Should().BePositive();
			_ = firstDevice.Height.Should().BePositive();
		}
	});

	[Fact]
	public Task GetEmulatedDevices_WithExpandUserAgent_ReturnsDevicesWithUserAgents() => RunIfAvailableAsync(async () =>
	{
		var result = await ThousandEyesClient.Emulation.EmulatedDevices.GetAllAsync(
			expand: [ExpandEmulatedDeviceOptions.UserAgent],
			cancellationToken: CancellationToken);

		_ = result.EmulatedDevicesList.Should().NotBeNull();
		if (result.EmulatedDevicesList.Length > 0)
		{
			var firstDevice = result.EmulatedDevicesList[0];
			_ = firstDevice.Id.Should().NotBeNullOrEmpty();
			_ = firstDevice.Name.Should().NotBeNullOrEmpty();
			if (firstDevice.AvailableUserAgents.Length > 0)
			{
				_ = firstDevice.AvailableUserAgents[0].Should().NotBeNullOrEmpty();
			}
		}
	});

	[Fact]
	public Task CreateEmulatedDevice_WithValidRequest_CreatesDevice() =>
		RunIfAvailableAsync(() => CreateDeviceAndCheckAsync(EmulatedDeviceCategory.Desktop, 1920, 1080));

	[Fact]
	public Task CreateEmulatedDevice_WithTabletCategory_CreatesTabletDevice() =>
		RunIfAvailableAsync(() => CreateDeviceAndCheckAsync(EmulatedDeviceCategory.Tablet, 768, 1024));
}
