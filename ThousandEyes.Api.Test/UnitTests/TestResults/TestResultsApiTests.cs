using AwesomeAssertions;
using Moq;
using ThousandEyes.Api.Interfaces;
using ThousandEyes.Api.Models.TestResults;

namespace ThousandEyes.Api.Test.UnitTests.TestResults;

public class TestResultsApiTests
{
	private readonly Mock<ITestResultsRefitApi> _refitApi;
	private readonly TestResultsApi _sut;

	public TestResultsApiTests()
	{
		_refitApi = new Mock<ITestResultsRefitApi>();
		_sut = new TestResultsApi(_refitApi.Object);
	}

	[Fact]
	public async Task GetNetworkResultsAsync_CallsApi_AndReturnsData()
	{
		// Arrange
		var testId = "123";
		var fromDate = DateTime.UtcNow.AddDays(-1);
		var toDate = DateTime.UtcNow;
		var cancellationToken = new CancellationToken();
		var expectedResponse = new NetworkTestResults
		{
			Results = [
				new NetworkTestResult
				{
					TestId = testId,
					TestName = "Test Network",
					AgentId = "agent-123",
					AgentName = "Test Agent",
					RoundId = "round-123",
					Date = DateTime.UtcNow
				}
			]
		};
		_ = _refitApi.Setup(x => x.GetNetworkResultsAsync(testId, fromDate, toDate, null, cancellationToken))
			.ReturnsAsync(expectedResponse);

		// Act
		var result = await _sut.GetNetworkResultsAsync(testId, fromDate, toDate, null, cancellationToken);

		// Assert
		_ = result.Should().Be(expectedResponse);
		_refitApi.Verify(x => x.GetNetworkResultsAsync(testId, fromDate, toDate, null, cancellationToken), Times.Once);
	}

	[Fact]
	public async Task GetHttpServerResultsAsync_CallsApi_AndReturnsData()
	{
		// Arrange
		var testId = "123";
		var fromDate = DateTime.UtcNow.AddDays(-1);
		var toDate = DateTime.UtcNow;
		var cancellationToken = new CancellationToken();
		var expectedResponse = new HttpServerTestResults
		{
			Results = [
				new HttpServerTestResult
				{
					TestId = testId,
					TestName = "Test HTTP",
					AgentId = "agent-123",
					AgentName = "Test Agent",
					RoundId = "round-123",
					Date = DateTime.UtcNow
				}
			]
		};
		_ = _refitApi.Setup(x => x.GetHttpServerResultsAsync(testId, fromDate, toDate, null, cancellationToken))
			.ReturnsAsync(expectedResponse);

		// Act
		var result = await _sut.GetHttpServerResultsAsync(testId, fromDate, toDate, null, cancellationToken);

		// Assert
		_ = result.Should().Be(expectedResponse);
		_refitApi.Verify(x => x.GetHttpServerResultsAsync(testId, fromDate, toDate, null, cancellationToken), Times.Once);
	}

	[Fact]
	public async Task GetPathVisualizationResultsAsync_CallsApi_AndReturnsData()
	{
		// Arrange
		var testId = "123";
		var fromDate = DateTime.UtcNow.AddDays(-1);
		var toDate = DateTime.UtcNow;
		var cancellationToken = new CancellationToken();
		var expectedResponse = new NetworkTestResults
		{
			Results = [
				new NetworkTestResult
				{
					TestId = testId,
					TestName = "Test Path",
					AgentId = "agent-123",
					AgentName = "Test Agent",
					RoundId = "round-123",
					Date = DateTime.UtcNow
				}
			]
		};
		_ = _refitApi.Setup(x => x.GetPathVisualizationResultsAsync(testId, fromDate, toDate, null, cancellationToken))
			.ReturnsAsync(expectedResponse);

		// Act
		var result = await _sut.GetPathVisualizationResultsAsync(testId, fromDate, toDate, null, cancellationToken);

		// Assert
		_ = result.Should().Be(expectedResponse);
		_refitApi.Verify(x => x.GetPathVisualizationResultsAsync(testId, fromDate, toDate, null, cancellationToken), Times.Once);
	}
}
