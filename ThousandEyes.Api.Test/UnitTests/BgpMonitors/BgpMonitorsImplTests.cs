using AwesomeAssertions;
using Moq;
using ThousandEyes.Api.Implementations.BgpMonitors;
using ThousandEyes.Api.Models.BgpMonitors;
using ThousandEyes.Api.Refit.BgpMonitors;
using BgpMonitor = ThousandEyes.Api.Models.BgpMonitors.Monitor;

namespace ThousandEyes.Api.Test.UnitTests.BgpMonitors;

public class BgpMonitorsImplTests
{
	private readonly Mock<IBgpMonitorsRefitApi> _refitApi;
	private readonly BgpMonitorsImpl _sut;

	public BgpMonitorsImplTests()
	{
		_refitApi = new Mock<IBgpMonitorsRefitApi>();
		_sut = new BgpMonitorsImpl(_refitApi.Object);
	}

	[Fact]
	public async Task GetAllAsync_CallsApi_AndReturnsData()
	{
		// Arrange
		var cancellationToken = new CancellationToken();
		var expectedResponse = new Monitors
		{
			MonitorsList = [
				new BgpMonitor { MonitorId = "123", MonitorName = "Test Monitor" }
			]
		};
		_ = _refitApi.Setup(x => x.GetAllAsync(null, cancellationToken))
			.ReturnsAsync(expectedResponse);

		// Act
		var result = await _sut.GetAllAsync(null, cancellationToken);

		// Assert
		_ = result.Should().Be(expectedResponse);
		_refitApi.Verify(x => x.GetAllAsync(null, cancellationToken), Times.Once);
	}
}
