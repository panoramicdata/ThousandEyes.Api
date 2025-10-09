using AwesomeAssertions;
using Moq;
using ThousandEyes.Api.Interfaces;
using ThousandEyes.Api.Models.Emulation;

namespace ThousandEyes.Api.Test.UnitTests.Emulation;

public class UserAgentsApiTests
{
	private readonly Mock<IUserAgentsRefitApi> _refitApi;
	private readonly UserAgentsApi _sut;

	public UserAgentsApiTests()
	{
		_refitApi = new Mock<IUserAgentsRefitApi>();
		_sut = new UserAgentsApi(_refitApi.Object);
	}

	[Fact]
	public async Task GetAllAsync_CallsApi_AndReturnsData()
	{
		// Arrange
		var cancellationToken = new CancellationToken();
		var expectedResponse = new UserAgents
		{
			UserAgentsList = [
				new UserAgent
				{
					Browser = "Chrome",
					Os = "Windows",
					Value = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36"
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
}
