using AwesomeAssertions;

namespace ThousandEyes.Api.Test;

[Collection("Integration Tests")]
[Trait("Category", "Integration")]
public class TestResultsIntegrationTest(IntegrationTestFixture fixture) : TestBase(fixture)
{
	/// <summary>
	/// Runs the body for the first test in the account, or returns when the account has none
	/// </summary>
	private async Task WithTestAsync(bool httpServerOnly, Func<string, DateTime, DateTime, Task> body)
	{
		var testId = httpServerOnly
			? (await ThousandEyesClient.Tests.HttpServerTests.GetAllAsync(aid: null, cancellationToken: CancellationToken)).Tests.FirstOrDefault()?.TestId
			: (await ThousandEyesClient.Tests.Tests.GetAllAsync(aid: null, cancellationToken: CancellationToken)).TestsList.FirstOrDefault()?.TestId;
		if (testId is null)
		{
			return;
		}

		await body(testId, DateTime.UtcNow.AddHours(-24), DateTime.UtcNow);
	}

	[Fact]
	public Task GetNetworkResults_WithValidTestId_ReturnsNetworkResults() => WithTestAsync(false, async (testId, from, to) =>
	{
		var result = await ThousandEyesClient.TestResults.TestResults.GetNetworkResultsAsync(testId, from, to, aid: null, cancellationToken: CancellationToken);

		_ = result.Results.Should().NotBeNull();
		if (result.Results.Length > 0)
		{
			var first = result.Results[0];
			_ = first.TestId.Should().Be(testId);
			_ = first.TestName.Should().NotBeNullOrEmpty();
			_ = first.AgentId.Should().NotBeNullOrEmpty();
			_ = first.AgentName.Should().NotBeNullOrEmpty();
			_ = first.RoundId.Should().NotBeNullOrEmpty();
			_ = first.Date.Should().BeAfter(from);
		}
	});

	[Fact]
	public Task GetHttpServerResults_WithValidTestId_ReturnsHttpServerResults() => WithTestAsync(true, async (testId, from, to) =>
	{
		var result = await ThousandEyesClient.TestResults.TestResults.GetHttpServerResultsAsync(testId, from, to, aid: null, cancellationToken: CancellationToken);

		_ = result.Results.Should().NotBeNull();
		if (result.Results.Length > 0)
		{
			var first = result.Results[0];
			_ = first.TestId.Should().Be(testId);
			_ = first.TestName.Should().NotBeNullOrEmpty();
			_ = first.RoundId.Should().NotBeNullOrEmpty();
			_ = first.Date.Should().BeAfter(from);
			_ = first.ResponseCode.Should().BePositive();
		}
	});

	[Fact]
	public Task GetPathVisualizationResults_WithValidTestId_ReturnsPathVisResults() => WithTestAsync(false, async (testId, from, to) =>
	{
		var result = await ThousandEyesClient.TestResults.TestResults.GetPathVisualizationResultsAsync(testId, from, to, aid: null, cancellationToken: CancellationToken);

		_ = result.Results.Should().NotBeNull();
		if (result.Results.Length > 0)
		{
			var first = result.Results[0];
			_ = first.TestId.Should().Be(testId);
			_ = first.Date.Should().BeAfter(from);
			if (first.PathVis?.Length > 0)
			{
				_ = first.PathVis[0].HopNumber.Should().BePositive();
				_ = first.PathVis[0].IpAddress.Should().NotBeNullOrEmpty();
			}
		}
	});
}
