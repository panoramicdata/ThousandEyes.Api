using AwesomeAssertions;

namespace ThousandEyes.Api.Test;

[Collection("Integration Tests")]
[Trait("Category", "Integration")]
public class TestResultsIntegrationTest(IntegrationTestFixture fixture) : TestBase(fixture)
{
	private static readonly TimeSpan Window = TimeSpan.FromHours(24);

	private async Task<string?> FirstTestIdAsync() =>
		(await ThousandEyesClient.Tests.Tests.GetAllAsync(aid: null, cancellationToken: CancellationToken)).TestsList.FirstOrDefault()?.TestId;

	private async Task<string?> FirstHttpServerTestIdAsync() =>
		(await ThousandEyesClient.Tests.HttpServerTests.GetAllAsync(aid: null, cancellationToken: CancellationToken)).Tests.FirstOrDefault()?.TestId;

	private static void CheckHop(Models.TestResults.PathVisualization[]? pathVis)
	{
		if (pathVis?.Length > 0)
		{
			_ = pathVis[0].HopNumber.Should().BePositive();
			_ = pathVis[0].IpAddress.Should().NotBeNullOrEmpty();
		}
	}

	/// <summary>
	/// Checks the first result, when there is one, against the identity fields every result type carries
	/// </summary>
	private static void CheckFirst<T>(T[] results, string testId, DateTime from, Func<T, (string TestId, string TestName, string RoundId, DateTime Date)> project, Action<T>? extra = null)
	{
		_ = results.Should().NotBeNull();
		if (results.Length == 0)
		{
			return;
		}

		var (resultTestId, testName, roundId, date) = project(results[0]);
		_ = resultTestId.Should().Be(testId);
		_ = testName.Should().NotBeNullOrEmpty();
		_ = roundId.Should().NotBeNullOrEmpty();
		_ = date.Should().BeAfter(from);
		extra?.Invoke(results[0]);
	}

	[Fact]
	public async Task GetNetworkResults_WithValidTestId_ReturnsNetworkResults()
	{
		var testId = await FirstTestIdAsync();
		if (testId is null)
		{
			return;
		}

		var from = DateTime.UtcNow - Window;
		var result = await ThousandEyesClient.TestResults.TestResults.GetNetworkResultsAsync(testId, from, DateTime.UtcNow, aid: null, cancellationToken: CancellationToken);

		CheckFirst(result.Results, testId, from, r => (r.TestId, r.TestName, r.RoundId, r.Date), r => _ = r.AgentName.Should().NotBeNullOrEmpty());
	}

	[Fact]
	public async Task GetHttpServerResults_WithValidTestId_ReturnsHttpServerResults()
	{
		var testId = await FirstHttpServerTestIdAsync();
		if (testId is null)
		{
			return;
		}

		var from = DateTime.UtcNow - Window;
		var result = await ThousandEyesClient.TestResults.TestResults.GetHttpServerResultsAsync(testId, from, DateTime.UtcNow, aid: null, cancellationToken: CancellationToken);

		CheckFirst(result.Results, testId, from, r => (r.TestId, r.TestName, r.RoundId, r.Date), r => _ = r.ResponseCode.Should().BePositive());
	}

	[Fact]
	public async Task GetPathVisualizationResults_WithValidTestId_ReturnsPathVisResults()
	{
		var testId = await FirstTestIdAsync();
		if (testId is null)
		{
			return;
		}

		var from = DateTime.UtcNow - Window;
		var result = await ThousandEyesClient.TestResults.TestResults.GetPathVisualizationResultsAsync(testId, from, DateTime.UtcNow, aid: null, cancellationToken: CancellationToken);

		CheckFirst(result.Results, testId, from, r => (r.TestId, r.TestName, r.RoundId, r.Date), r => CheckHop(r.PathVis));
	}
}
