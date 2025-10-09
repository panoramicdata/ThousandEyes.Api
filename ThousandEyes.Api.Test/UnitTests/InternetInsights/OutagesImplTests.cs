using AwesomeAssertions;
using Moq;
using ThousandEyes.Api.Implementations.InternetInsights;
using ThousandEyes.Api.Models.InternetInsights;
using ThousandEyes.Api.Refit.InternetInsights;

namespace ThousandEyes.Api.Test.UnitTests.InternetInsights;

public class OutagesImplTests
{
	private readonly Mock<IOutagesRefitApi> _refitApi;
	private readonly OutagesImpl _sut;

	public OutagesImplTests()
	{
		_refitApi = new Mock<IOutagesRefitApi>();
		_sut = new OutagesImpl(_refitApi.Object);
	}

	[Fact]
	public async Task FilterAsync_CallsApi_AndReturnsData()
	{
		// Arrange
		var filter = new OutageFilter();
		var cancellationToken = new CancellationToken();
		var expectedResponse = new OutagesResponse
		{
			OutagesList = [
				new Outage { Id = "123" }
			]
		};
		_ = _refitApi.Setup(x => x.FilterAsync(filter, null, cancellationToken))
			.ReturnsAsync(expectedResponse);

		// Act
		var result = await _sut.FilterAsync(filter, null, cancellationToken);

		// Assert
		_ = result.Should().Be(expectedResponse);
		_refitApi.Verify(x => x.FilterAsync(filter, null, cancellationToken), Times.Once);
	}

	[Fact]
	public async Task GetNetworkOutageAsync_CallsApi_AndReturnsData()
	{
		// Arrange
		var outageId = "123";
		var cancellationToken = new CancellationToken();
		var expectedResponse = new NetworkOutageDetails { Id = outageId };
		_ = _refitApi.Setup(x => x.GetNetworkOutageAsync(outageId, null, cancellationToken))
			.ReturnsAsync(expectedResponse);

		// Act
		var result = await _sut.GetNetworkOutageAsync(outageId, null, cancellationToken);

		// Assert
		_ = result.Should().Be(expectedResponse);
		_refitApi.Verify(x => x.GetNetworkOutageAsync(outageId, null, cancellationToken), Times.Once);
	}

	[Fact]
	public async Task GetApplicationOutageAsync_CallsApi_AndReturnsData()
	{
		// Arrange
		var outageId = "123";
		var cancellationToken = new CancellationToken();
		var expectedResponse = new ApplicationOutageDetails { Id = outageId };
		_ = _refitApi.Setup(x => x.GetApplicationOutageAsync(outageId, null, cancellationToken))
			.ReturnsAsync(expectedResponse);

		// Act
		var result = await _sut.GetApplicationOutageAsync(outageId, null, cancellationToken);

		// Assert
		_ = result.Should().Be(expectedResponse);
		_refitApi.Verify(x => x.GetApplicationOutageAsync(outageId, null, cancellationToken), Times.Once);
	}
}
