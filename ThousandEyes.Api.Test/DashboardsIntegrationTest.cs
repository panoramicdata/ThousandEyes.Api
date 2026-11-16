using AwesomeAssertions;
using System.Text.Json;
using ThousandEyes.Api.Interfaces;
using ThousandEyes.Api.Models.Dashboards;

namespace ThousandEyes.Api.Test;

[Collection("Integration Tests")]
[Trait("Category", "Integration")]
public class DashboardsIntegrationTest(IntegrationTestFixture fixture) : TestBase(fixture)
{
	private IDashboardsApi Api => ThousandEyesClient.Dashboards.Dashboards;

	private Task<Dashboard[]> ListAsync() => Api.GetAllAsync(aid: null, cancellationToken: CancellationToken);

	/// <summary>
	/// Runs the body for the first dashboard, or returns when the account has none
	/// </summary>
	private Task WithFirstDashboardAsync(Func<string, Task> body) => RunIfAvailableAsync(async () =>
	{
		var dashboards = await ListAsync();
		if (dashboards.Length > 0)
		{
			await body(dashboards[0].DashboardId);
		}
	});

	[Fact]
	public async Task Debug_InspectDashboardResponse()
	{
		var result = await ListAsync();

		Logger.LogInformation("Successfully retrieved {Count} dashboards", result.Length);
		if (result.Length > 0)
		{
			var first = result[0];
			Logger.LogInformation("First Dashboard {DashboardId} {Title} aid {Aid}", first.DashboardId, first.Title, first.Aid);
			Logger.LogInformation("Built-in {IsBuiltIn}, private {IsPrivate}, created by {CreatedBy}, modified {ModifiedDate}", first.IsBuiltIn, first.IsPrivate, first.DashboardCreatedBy, first.DashboardModifiedDate);
			Logger.LogInformation("Full JSON of first dashboard:\n{Json}", JsonSerializer.Serialize(first, JsonSerializerOptions));
		}
	}

	[Fact]
	public Task GetDashboards_WithValidRequest_ReturnsDashboards() => RunIfAvailableAsync(async () =>
	{
		var result = await ListAsync();

		_ = result.Should().NotBeNull();
		if (result.Length > 0)
		{
			_ = result[0].DashboardId.Should().NotBeNullOrEmpty();
			_ = result[0].Title.Should().NotBeNullOrEmpty();
		}
	});

	[Fact]
	public Task GetDashboardById_WithValidDashboardId_ReturnsDashboardDetails() => WithFirstDashboardAsync(async dashboardId =>
	{
		var result = await Api.GetByIdAsync(dashboardId, aid: null, cancellationToken: CancellationToken);

		_ = result.DashboardId.Should().Be(dashboardId);
		_ = result.Title.Should().NotBeNullOrEmpty();
	});

	[Fact]
	public Task CreateDashboard_WithValidRequest_CreatesDashboard() => RunIfAvailableAsync(async () =>
	{
		var dashboardRequest = new DashboardRequest
		{
			Title = $"Test Dashboard - {DateTime.UtcNow:yyyyMMdd-HHmmss}",
			Description = "Integration test dashboard",
			IsPrivate = true,
			IsDefaultForUser = false
		};

		var result = await Api.CreateAsync(dashboardRequest, aid: null, cancellationToken: CancellationToken);

		_ = result.DashboardId.Should().NotBeNullOrEmpty();
		_ = result.Title.Should().Be(dashboardRequest.Title);
		_ = result.IsPrivate.Should().Be(dashboardRequest.IsPrivate);

		await IgnoreErrorsAsync(() => Api.DeleteAsync(result.DashboardId, aid: null, cancellationToken: CancellationToken));
	});

	[Fact]
	public Task GetDashboardSnapshots_WithValidRequest_ReturnsSnapshots() => RunIfAvailableAsync(async () =>
	{
		var result = await ThousandEyesClient.Dashboards.Snapshots.GetAllAsync(aid: null, dashboardId: null, cursor: null, cancellationToken: CancellationToken);

		_ = result.DashboardSnapshots.Should().NotBeNull();
	});

	[Fact]
	public Task CreateDashboardSnapshot_WithValidRequest_CreatesSnapshot() => WithFirstDashboardAsync(async dashboardId =>
	{
		var snapshotRequest = new CreateDashboardSnapshotRequest
		{
			DashboardId = dashboardId,
			DisplayName = $"Test Snapshot - {DateTime.UtcNow:yyyyMMdd-HHmmss}",
			StartDate = DateTime.UtcNow.AddDays(-1),
			EndDate = DateTime.UtcNow,
			Timezone = "UTC",
			AnonymizeData = false
		};

		var result = await ThousandEyesClient.Dashboards.Snapshots.CreateAsync(snapshotRequest, aid: null, cancellationToken: CancellationToken);

		_ = result.SnapshotId.Should().NotBeNullOrEmpty();

		await IgnoreErrorsAsync(() => ThousandEyesClient.Dashboards.Snapshots.DeleteAsync(result.SnapshotId, aid: null, cancellationToken: CancellationToken));
	});

	[Fact]
	public Task GetDashboardFilters_WithValidRequest_ReturnsFilters() => RunIfAvailableAsync(async () =>
	{
		var result = await ThousandEyesClient.Dashboards.Filters.GetAllAsync(aid: null, searchPattern: null, cancellationToken: CancellationToken);

		_ = result.Filters.Should().NotBeNull();
		if (result.Filters.Length > 0)
		{
			_ = result.Filters[0].Id.Should().NotBeNullOrEmpty();
			_ = result.Filters[0].Name.Should().NotBeNullOrEmpty();
		}
	});

	[Fact]
	public Task CreateDashboardFilter_WithValidRequest_CreatesFilter() => RunIfAvailableAsync(async () =>
	{
		var tests = await ThousandEyesClient.Tests.Tests.GetAllAsync(aid: null, CancellationToken);
		if (tests.TestsList.Length == 0)
		{
			Logger.LogWarning("Skipping CreateDashboardFilter test - no tests available");
			return;
		}

		var filterRequest = NewFilterRequest(tests.TestsList[0].TestId ?? "0");

		var result = await ThousandEyesClient.Dashboards.Filters.CreateAsync(filterRequest, aid: null, cancellationToken: CancellationToken);

		_ = result.Id.Should().NotBeNullOrEmpty();
		_ = result.Name.Should().Be(filterRequest.Name);

		await IgnoreErrorsAsync(() => ThousandEyesClient.Dashboards.Filters.DeleteAsync(result.Id, aid: null, cancellationToken: CancellationToken));
	});

	private static DashboardFilterRequest NewFilterRequest(string testId) => new()
	{
		Name = $"Test Filter - {DateTime.UtcNow:yyyyMMdd-HHmmss}",
		Description = "Integration test filter",
		Context =
		[
			new DataSourceFilter
			{
				DataSourceId = "CLOUD_AND_ENTERPRISE_AGENTS",
				Filters =
				[
					new FilterProperty
					{
						FilterId = "TEST",
						Values = [testId],
						MetricIds = ["WEB_AVAILABILITY"]
					}
				]
			}
		]
	};
}
