using AwesomeAssertions;
using Moq;
using ThousandEyes.Api.Interfaces;
using ThousandEyes.Api.Models.Emulation;

namespace ThousandEyes.Api.Test.UnitTests.Emulation;

public class EmulatedDevicesApiTests
{
	private readonly Mock<IEmulatedDevicesRefitApi> _refitApi;
	private readonly EmulatedDevicesApi _sut;

	public EmulatedDevicesApiTests()
	{
		_refitApi = new Mock<IEmulatedDevicesRefitApi>();
		_sut = new EmulatedDevicesApi(_refitApi.Object);
	}

	[Fact]
	public async Task GetAllAsync_CallsApi_AndReturnsData()
	{
		// Arrange
		var cancellationToken = new CancellationToken();
		var expectedResponse = new EmulatedDeviceResponses
		{
			EmulatedDevicesList = [
				new EmulatedDeviceResponse
				{
					Id = "123",
					Name = "iPhone 13",
					Category = EmulatedDeviceCategory.Phone,
					Width = 390,
					Height = 844
				}
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

	[Fact]
	public async Task CreateAsync_CallsApi_AndReturnsData()
	{
		// Arrange
		var cancellationToken = new CancellationToken();
		var request = new EmulatedDevice
		{
			Category = EmulatedDeviceCategory.Tablet,
			Width = 768,
			Height = 1024
		};
		var expectedResponse = new EmulatedDeviceResponse
		{
			Id = "123",
			Name = "Custom Tablet",
			Category = EmulatedDeviceCategory.Tablet,
			Width = 768,
			Height = 1024
		};
		_ = _refitApi.Setup(x => x.CreateAsync(request, null, cancellationToken))
			.ReturnsAsync(expectedResponse);

		// Act
		var result = await _sut.CreateAsync(request, null, cancellationToken);

		// Assert
		_ = result.Should().Be(expectedResponse);
		_refitApi.Verify(x => x.CreateAsync(request, null, cancellationToken), Times.Once);
	}
}
