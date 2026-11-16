using AwesomeAssertions;
using ThousandEyes.Api.Models.Emulation;

namespace ThousandEyes.Api.Test;

[Collection("Integration Tests")]
[Trait("Category", "Integration")]
public class EmulationIntegrationTest(IntegrationTestFixture fixture) : TestBase(fixture)
{
	private Task CreateDeviceAndCheckAsync(EmulatedDeviceCategory category, int width, int height) => RunIfAvailableAsync(async () =>
	{
		var deviceRequest = new EmulatedDevice { Category = category, Width = width, Height = height };

		var result = await ThousandEyesClient.Emulation.EmulatedDevices.CreateAsync(deviceRequest, aid: null, cancellationToken: CancellationToken);

		_ = result.Id.Should().NotBeNullOrEmpty();
		_ = result.Category.Should().Be(category);
		_ = result.Width.Should().Be(width);
		_ = result.Height.Should().Be(height);
	});

	/// <summary>
	/// Lists emulated devices and returns the first one, or null when the account has none
	/// </summary>
	private async Task<EmulatedDeviceResponse?> FirstDeviceAsync(ExpandEmulatedDeviceOptions[]? expand)
	{
		var result = await ThousandEyesClient.Emulation.EmulatedDevices.GetAllAsync(expand: expand, cancellationToken: CancellationToken);
		_ = result.EmulatedDevicesList.Should().NotBeNull();
		return result.EmulatedDevicesList.FirstOrDefault();
	}

	[Fact]
	public Task GetUserAgents_WithValidRequest_ReturnsUserAgents() => RunIfAvailableAsync(async () =>
	{
		var result = await ThousandEyesClient.Emulation.UserAgents.GetAllAsync(aid: null, cancellationToken: CancellationToken);

		_ = result.UserAgentsList.Should().NotBeNull().And.OnlyContain(u => !string.IsNullOrEmpty(u.Value));
	});

	[Fact]
	public Task GetEmulatedDevices_WithValidRequest_ReturnsDevices() => RunIfAvailableAsync(async () =>
	{
		var device = await FirstDeviceAsync(null);

		_ = device?.Id.Should().NotBeNullOrEmpty();
		_ = device?.Name.Should().NotBeNullOrEmpty();
		_ = device?.Category.Should().BeDefined();
		_ = device?.Width.Should().BePositive();
		_ = device?.Height.Should().BePositive();
	});

	[Fact]
	public Task GetEmulatedDevices_WithExpandUserAgent_ReturnsDevicesWithUserAgents() => RunIfAvailableAsync(async () =>
	{
		var device = await FirstDeviceAsync([ExpandEmulatedDeviceOptions.UserAgent]);

		_ = device?.Id.Should().NotBeNullOrEmpty();
		_ = device?.AvailableUserAgents.Should().OnlyContain(a => !string.IsNullOrEmpty(a));
	});

	[Fact]
	public Task CreateEmulatedDevice_WithValidRequest_CreatesDevice() =>
		CreateDeviceAndCheckAsync(EmulatedDeviceCategory.Desktop, 1920, 1080);

	[Fact]
	public Task CreateEmulatedDevice_WithTabletCategory_CreatesTabletDevice() =>
		CreateDeviceAndCheckAsync(EmulatedDeviceCategory.Tablet, 768, 1024);
}
