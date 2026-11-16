using AwesomeAssertions;
using ThousandEyes.Api.Models.TestSnapshots;

namespace ThousandEyes.Api.Test;

/// <summary>
/// Integration tests for the Test Snapshots API module
/// </summary>
[Collection("Integration Tests")]
[Trait("Category", "Integration")]
public class TestSnapshotsModuleTests(IntegrationTestFixture fixture) : TestBase(fixture)
{
	private async Task<SnapshotResponse> CreateSnapshotAsync(string label, TimeSpan from, TimeSpan to, bool isPublic, Action<string>? checkTestId = null)
	{
		var tests = await ThousandEyesClient.Tests.Tests.GetAllAsync(aid: null, CancellationToken);
		_ = tests.TestsList.Should().NotBeEmpty();
		var testId = tests.TestsList.First().TestId;
		checkTestId?.Invoke(testId!);

		var request = new SnapshotRequest
		{
			DisplayName = $"{label} - {DateTimeOffset.UtcNow.ToUnixTimeSeconds()}",
			StartDate = DateTime.UtcNow - from,
			EndDate = DateTime.UtcNow - to,
			IsPublic = isPublic
		};

		var result = await ThousandEyesClient.TestSnapshots.CreateAsync(testId!, request, aid: null, CancellationToken);

		_ = result.Should().NotBeNull();
		_ = result.DisplayName.Should().Be(request.DisplayName);
		return result;
	}

	[Fact]
	public async Task CreateTestSnapshot_WithValidRequest_CreatesSnapshot()
	{
		string? testId = null;

		var result = await CreateSnapshotAsync("API Test Snapshot", TimeSpan.FromHours(2), TimeSpan.FromHours(1), false, id => testId = id);

		_ = result.Id.Should().NotBeNullOrEmpty();
		_ = result.SourceTestId.Should().Be(testId);
	}

	[Fact]
	public async Task CreateTestSnapshot_WithOneHourRange_CreatesSnapshot()
	{
		var result = await CreateSnapshotAsync("1-Hour Snapshot", TimeSpan.FromHours(1), TimeSpan.Zero, false);

		_ = result.Id.Should().NotBeNullOrEmpty();
		_ = result.StartRoundId.Should().BePositive();
		_ = result.EndRoundId.Should().BeGreaterThan(result.StartRoundId!.Value);
	}

	[Fact]
	public async Task CreateTestSnapshot_PublicSnapshot_CreatesPublicSnapshot()
	{
		var result = await CreateSnapshotAsync("Public Snapshot", TimeSpan.FromHours(2), TimeSpan.Zero, true);

		_ = result.Id.Should().NotBeNullOrEmpty();
		_ = result.TestId.Should().NotBeNullOrEmpty();
		_ = result.Uid.Should().NotBeNullOrEmpty();
	}

	[Fact]
	public async Task CreateTestSnapshot_With24HourRange_CreatesSnapshot()
	{
		var result = await CreateSnapshotAsync("24-Hour Snapshot", TimeSpan.FromHours(24), TimeSpan.Zero, false);

		_ = result.ShareDate.Should().NotBeNull();
	}
}
