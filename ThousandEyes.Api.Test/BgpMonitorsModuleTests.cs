using AwesomeAssertions;
using ThousandEyes.Api.Models.BgpMonitors;

namespace ThousandEyes.Api.Test;

[Collection("Integration Tests")]
public class BgpMonitorsModuleTests(IntegrationTestFixture fixture)
{
	private readonly IntegrationTestFixture _fixture = fixture;

	[Fact]
	public async Task GetBgpMonitors_WithValidRequest_ReturnsMonitors()
	{
		// Arrange
		var client = _fixture.GetThousandEyesClient();
		var cancellationToken = CancellationToken.None;

		// Act
		var result = await client.BgpMonitors.BgpMonitors.GetAllAsync(
			aid: null,
			cancellationToken);

		// Assert
		result.Should().NotBeNull();
		result.MonitorsList.Should().NotBeEmpty();

		// Validate first monitor structure
		var firstMonitor = result.MonitorsList[0];
		firstMonitor.MonitorId.Should().NotBeNullOrWhiteSpace();
		firstMonitor.MonitorName.Should().NotBeNullOrWhiteSpace();
		firstMonitor.IpAddress.Should().NotBeNullOrWhiteSpace();
		firstMonitor.Network.Should().NotBeNullOrWhiteSpace();
		firstMonitor.CountryId.Should().NotBeNullOrWhiteSpace();
		firstMonitor.MonitorType.Should().NotBeNull();
	}

	[Fact]
	public async Task GetBgpMonitors_WithAccountGroupId_ReturnsFilteredMonitors()
	{
		// Arrange
		var client = _fixture.GetThousandEyesClient();
		var cancellationToken = CancellationToken.None;

		// Get account groups to get a valid AID
		var accountGroups = await client.AccountManagement.AccountGroups.GetAllAsync(cancellationToken);
		var testAid = accountGroups.AccountGroupsList[0].Aid;

		// Act
		var result = await client.BgpMonitors.BgpMonitors.GetAllAsync(
			aid: testAid,
			cancellationToken);

		// Assert
		result.Should().NotBeNull();
		result.MonitorsList.Should().NotBeEmpty();
	}

	[Fact]
	public async Task GetBgpMonitors_ValidatesPublicMonitors()
	{
		// Arrange
		var client = _fixture.GetThousandEyesClient();
		var cancellationToken = CancellationToken.None;

		// Act
		var result = await client.BgpMonitors.BgpMonitors.GetAllAsync(
			aid: null,
			cancellationToken);

		// Assert
		result.MonitorsList.Should().Contain(m => m.MonitorType == MonitorType.Public);
	}

	[Fact]
	public async Task GetBgpMonitors_ResponseHasLinks()
	{
		// Arrange
		var client = _fixture.GetThousandEyesClient();
		var cancellationToken = CancellationToken.None;

		// Act
		var result = await client.BgpMonitors.BgpMonitors.GetAllAsync(
			aid: null,
			cancellationToken);

		// Assert
		result.Links.Should().NotBeNull();
		result.Links?.Self.Should().NotBeNull();
		result.Links?.Self?.Href.Should().NotBeNullOrWhiteSpace();
	}
}
