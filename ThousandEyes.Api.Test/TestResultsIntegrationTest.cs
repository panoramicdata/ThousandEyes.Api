using AwesomeAssertions;

namespace ThousandEyes.Api.Test;

[Collection("Integration Tests")]
public class TestResultsIntegrationTest(IntegrationTestFixture fixture) : TestBase(fixture)
{
	[Fact]
	public async Task GetNetworkResults_WithValidTestId_ReturnsNetworkResults()
	{
		// Arrange - First get list of tests to find a valid test ID
		var tests = await ThousandEyesClient.Tests.Tests.GetAllAsync(aid: null, cancellationToken: CancellationToken);
		
		// Skip test if no tests are available
		if (tests.TestsList.Length == 0)
		{
			return; // Skip test - no tests available
		}

		var testId = tests.TestsList[0].TestId;
		var fromDate = DateTime.UtcNow.AddHours(-24); // Last 24 hours
		var toDate = DateTime.UtcNow;

		// Act
		var result = await ThousandEyesClient.TestResults.TestResults.GetNetworkResultsAsync(
			testId, 
			fromDate: fromDate, 
			toDate: toDate, 
			aid: null, 
			cancellationToken: CancellationToken);

		// Assert
		_ = result.Should().NotBeNull();
		_ = result.Results.Should().NotBeNull();

		// If there are results, verify the structure
		if (result.Results.Length > 0)
		{
			var firstResult = result.Results[0];
			_ = firstResult.TestId.Should().Be(testId);
			_ = firstResult.TestName.Should().NotBeNullOrEmpty();
			_ = firstResult.AgentId.Should().NotBeNullOrEmpty();
			_ = firstResult.AgentName.Should().NotBeNullOrEmpty();
			_ = firstResult.RoundId.Should().NotBeNullOrEmpty();
			_ = firstResult.Date.Should().BeAfter(fromDate);
		}
	}

	[Fact]
	public async Task GetHttpServerResults_WithValidTestId_ReturnsHttpServerResults()
	{
		// Arrange - First get list of HTTP Server tests to find a valid test ID
		var httpTests = await ThousandEyesClient.Tests.HttpServerTests.GetAllAsync(aid: null, cancellationToken: CancellationToken);
		
		// Skip test if no HTTP Server tests are available
		if (httpTests.Tests.Length == 0)
		{
			return; // Skip test - no HTTP Server tests available
		}

		var testId = httpTests.Tests[0].TestId;
		var fromDate = DateTime.UtcNow.AddHours(-24); // Last 24 hours
		var toDate = DateTime.UtcNow;

		// Act
		var result = await ThousandEyesClient.TestResults.TestResults.GetHttpServerResultsAsync(
			testId, 
			fromDate: fromDate, 
			toDate: toDate, 
			aid: null, 
			cancellationToken: CancellationToken);

		// Assert
		_ = result.Should().NotBeNull();
		_ = result.Results.Should().NotBeNull();

		// If there are results, verify the structure
		if (result.Results.Length > 0)
		{
			var firstResult = result.Results[0];
			_ = firstResult.TestId.Should().Be(testId);
			_ = firstResult.TestName.Should().NotBeNullOrEmpty();
			_ = firstResult.AgentId.Should().NotBeNullOrEmpty();
			_ = firstResult.AgentName.Should().NotBeNullOrEmpty();
			_ = firstResult.RoundId.Should().NotBeNullOrEmpty();
			_ = firstResult.Date.Should().BeAfter(fromDate);
			_ = firstResult.ResponseCode.Should().BePositive();
		}
	}

	[Fact]
	public async Task GetPathVisualizationResults_WithValidTestId_ReturnsPathVisResults()
	{
		// Arrange - First get list of tests to find a valid test ID
		var tests = await ThousandEyesClient.Tests.Tests.GetAllAsync(aid: null, cancellationToken: CancellationToken);
		
		// Skip test if no tests are available
		if (tests.TestsList.Length == 0)
		{
			return; // Skip test - no tests available
		}

		var testId = tests.TestsList[0].TestId;
		var fromDate = DateTime.UtcNow.AddHours(-24); // Last 24 hours
		var toDate = DateTime.UtcNow;

		// Act
		var result = await ThousandEyesClient.TestResults.TestResults.GetPathVisualizationResultsAsync(
			testId, 
			fromDate: fromDate, 
			toDate: toDate, 
			aid: null, 
			cancellationToken: CancellationToken);

		// Assert
		_ = result.Should().NotBeNull();
		_ = result.Results.Should().NotBeNull();

		// If there are results, verify the structure
		if (result.Results.Length > 0)
		{
			var firstResult = result.Results[0];
			_ = firstResult.TestId.Should().Be(testId);
			_ = firstResult.TestName.Should().NotBeNullOrEmpty();
			_ = firstResult.AgentId.Should().NotBeNullOrEmpty();
			_ = firstResult.AgentName.Should().NotBeNullOrEmpty();
			_ = firstResult.RoundId.Should().NotBeNullOrEmpty();
			_ = firstResult.Date.Should().BeAfter(fromDate);
			
			// Verify path visualization data if present
			if (firstResult.PathVis?.Length > 0)
			{
				var firstHop = firstResult.PathVis[0];
				_ = firstHop.HopNumber.Should().BePositive();
				_ = firstHop.IpAddress.Should().NotBeNullOrEmpty();
			}
		}
	}
}