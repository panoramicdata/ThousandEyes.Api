using AwesomeAssertions;
using ThousandEyes.Api.Models.TestSnapshots;

namespace ThousandEyes.Api.Test;

/// <summary>
/// Integration tests for the Test Snapshots API module
/// </summary>
[Collection("Integration Tests")]
public class TestSnapshotsModuleTests(IntegrationTestFixture fixture) : TestBase(fixture)
{
	[Fact]
	public async Task CreateTestSnapshot_WithValidRequest_CreatesSnapshot()
	{
		// Arrange
		// First, get a test to snapshot
		var testsModule = ThousandEyesClient.Tests;
		var testsResult = await testsModule.Tests.GetAllAsync(aid: null, CancellationToken);
		testsResult.Should().NotBeNull();
		testsResult.TestsList.Should().NotBeEmpty();
		
		var testId = testsResult.TestsList.First().TestId;
		testId.Should().NotBeNullOrEmpty();

		var request = new SnapshotRequest
		{
			DisplayName = $"API Test Snapshot - {DateTimeOffset.UtcNow.ToUnixTimeSeconds()}",
			StartDate = DateTime.UtcNow.AddHours(-2),
			EndDate = DateTime.UtcNow.AddHours(-1),
			IsPublic = false
		};

		try
		{
			// Act
			var result = await ThousandEyesClient.TestSnapshots.CreateAsync(testId!, request, aid: null, CancellationToken);

			// Assert
			result.Should().NotBeNull();
			result.Id.Should().NotBeNullOrEmpty();
			result.DisplayName.Should().Be(request.DisplayName);
			result.SourceTestId.Should().Be(testId);
		}
		catch (Exception)
		{
			// Test may fail if no tests exist, rate limit exceeded, or permissions insufficient
			// This is expected in some environments
			throw;
		}
	}

	[Fact]
	public async Task CreateTestSnapshot_WithOneHourRange_CreatesSnapshot()
	{
		// Arrange
		var testsModule = ThousandEyesClient.Tests;
		var testsResult = await testsModule.Tests.GetAllAsync(aid: null, CancellationToken);
		testsResult.Should().NotBeNull();
		testsResult.TestsList.Should().NotBeEmpty();
		
		var testId = testsResult.TestsList.First().TestId;

		var request = new SnapshotRequest
		{
			DisplayName = $"1-Hour Snapshot - {DateTimeOffset.UtcNow.ToUnixTimeSeconds()}",
			StartDate = DateTime.UtcNow.AddHours(-1),
			EndDate = DateTime.UtcNow,
			IsPublic = false
		};

		try
		{
			// Act
			var result = await ThousandEyesClient.TestSnapshots.CreateAsync(testId!, request, aid: null, CancellationToken);

			// Assert
			result.Should().NotBeNull();
			result.Id.Should().NotBeNullOrEmpty();
			result.StartRoundId.Should().NotBeNull();
			result.StartRoundId!.Value.Should().BeGreaterThan(0);
			result.EndRoundId.Should().NotBeNull();
			result.EndRoundId!.Value.Should().BeGreaterThan(0);
			result.EndRoundId.Value.Should().BeGreaterThan(result.StartRoundId.Value);
		}
		catch (Exception)
		{
			// Test may fail for environmental reasons
			throw;
		}
	}

	[Fact]
	public async Task CreateTestSnapshot_PublicSnapshot_CreatesPublicSnapshot()
	{
		// Arrange
		var testsModule = ThousandEyesClient.Tests;
		var testsResult = await testsModule.Tests.GetAllAsync(aid: null, CancellationToken);
		testsResult.Should().NotBeNull();
		testsResult.TestsList.Should().NotBeEmpty();
		
		var testId = testsResult.TestsList.First().TestId;

		var request = new SnapshotRequest
		{
			DisplayName = $"Public Snapshot - {DateTimeOffset.UtcNow.ToUnixTimeSeconds()}",
			StartDate = DateTime.UtcNow.AddHours(-2),
			EndDate = DateTime.UtcNow,
			IsPublic = true
		};

		try
		{
			// Act
			var result = await ThousandEyesClient.TestSnapshots.CreateAsync(testId!, request, aid: null, CancellationToken);

			// Assert
			result.Should().NotBeNull();
			result.Id.Should().NotBeNullOrEmpty();
			result.TestId.Should().NotBeNullOrEmpty();
			result.Uid.Should().NotBeNullOrEmpty();
		}
		catch (Exception)
		{
			// Public snapshots may not be enabled in all regions for compliance reasons
			throw;
		}
	}

	[Fact]
	public async Task CreateTestSnapshot_With24HourRange_CreatesSnapshot()
	{
		// Arrange
		var testsModule = ThousandEyesClient.Tests;
		var testsResult = await testsModule.Tests.GetAllAsync(aid: null, CancellationToken);
		testsResult.Should().NotBeNull();
		testsResult.TestsList.Should().NotBeEmpty();
		
		var testId = testsResult.TestsList.First().TestId;

		var request = new SnapshotRequest
		{
			DisplayName = $"24-Hour Snapshot - {DateTimeOffset.UtcNow.ToUnixTimeSeconds()}",
			StartDate = DateTime.UtcNow.AddHours(-24),
			EndDate = DateTime.UtcNow,
			IsPublic = false
		};

		try
		{
			// Act
			var result = await ThousandEyesClient.TestSnapshots.CreateAsync(testId!, request, aid: null, CancellationToken);

			// Assert
			result.Should().NotBeNull();
			result.ShareDate.Should().NotBeNull();
			result.DisplayName.Should().Be(request.DisplayName);
		}
		catch (Exception)
		{
			// Test may fail for environmental reasons
			throw;
		}
	}
}
