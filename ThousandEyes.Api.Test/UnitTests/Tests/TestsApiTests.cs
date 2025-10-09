using AwesomeAssertions;
using Moq;
using ThousandEyes.Api.Interfaces;
using ThousandEyes.Api.Models.Tests;
using TestsCollection = ThousandEyes.Api.Models.Tests.Tests;

namespace ThousandEyes.Api.Test.UnitTests.Tests;

public class TestsApiTests
{
	private readonly Mock<ITestsRefitApi> _refitApi;
	private readonly TestsApi _sut;

	public TestsApiTests()
	{
		_refitApi = new Mock<ITestsRefitApi>();
		_sut = new TestsApi(_refitApi.Object);
	}

	[Fact]
	public async Task GetAllAsync_CallsApi_AndReturnsData()
	{
		// Arrange
		var cancellationToken = new CancellationToken();
		var expectedResponse = new TestsCollection
		{
			TestsList =
			[
				new SimpleTest
				{
					TestId = "123",
					TestName = "Test 1",
					Type = "http-server",
					Interval = 300
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
	public async Task GetVersionHistoryAsync_CallsApi_AndReturnsData()
	{
		// Arrange
		var testId = "123";
		var cancellationToken = new CancellationToken();
		var expectedResponse = new TestVersionHistoryResponse
		{
			TestVersionHistory = [
				new TestVersionHistory
				{
					VersionId = "v1",
					TestId = testId,
					VersionTimestamp = DateTime.UtcNow,
					CreatedBy = "user@example.com"
				}
			]
		};
		_ = _refitApi.Setup(x => x.GetVersionHistoryAsync(testId, null, null, cancellationToken))
			.ReturnsAsync(expectedResponse);

		// Act
		var result = await _sut.GetVersionHistoryAsync(testId, null, null, cancellationToken);

		// Assert
		_ = result.Should().Be(expectedResponse);
		_refitApi.Verify(x => x.GetVersionHistoryAsync(testId, null, null, cancellationToken), Times.Once);
	}
}
