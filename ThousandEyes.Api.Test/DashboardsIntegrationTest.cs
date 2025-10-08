using AwesomeAssertions;
using ThousandEyes.Api.Models.Dashboards;
using Refit;
using Microsoft.Extensions.Logging;

namespace ThousandEyes.Api.Test;

[Collection("Integration Tests")]
public class DashboardsIntegrationTest(IntegrationTestFixture fixture) : TestBase(fixture)
{
	[Fact]
	public async Task Debug_InspectDashboardResponse()
	{
		// This test helps us see the EXACT JSON response from the API
		// to understand what property names it actually uses
		try
		{
			var result = await ThousandEyesClient.Dashboards.Dashboards.GetAllAsync(
				aid: null,
				cancellationToken: CancellationToken);

			// If we get here, serialization worked!
			Logger.LogInformation("Successfully retrieved {Count} dashboards", result.Length);
			
			if (result.Length > 0)
			{
				var firstDashboard = result[0];
				Logger.LogInformation("First Dashboard ID: {DashboardId}", firstDashboard.DashboardId);
				Logger.LogInformation("First Dashboard Title: {Title}", firstDashboard.Title);
				Logger.LogInformation("Aid: {Aid}", firstDashboard.Aid);
				Logger.LogInformation("IsBuiltIn: {IsBuiltIn}", firstDashboard.IsBuiltIn);
				Logger.LogInformation("IsPrivate: {IsPrivate}", firstDashboard.IsPrivate);
				Logger.LogInformation("DashboardCreatedBy: {CreatedBy}", firstDashboard.DashboardCreatedBy);
				Logger.LogInformation("DashboardModifiedDate: {ModifiedDate}", firstDashboard.DashboardModifiedDate);
				
				// Log all properties to understand the structure
				var json = System.Text.Json.JsonSerializer.Serialize(firstDashboard, 
					new System.Text.Json.JsonSerializerOptions { WriteIndented = true });
				Logger.LogInformation("Full JSON of first dashboard:\n{Json}", json);
			}
		}
		catch (System.Text.Json.JsonException ex)
		{
			Logger.LogError(ex, "JSON Serialization Error");
			throw;
		}
		catch (ValidationApiException ex)
		{
			Logger.LogError(ex, "API Error: {StatusCode}", ex.StatusCode);
			throw;
		}
	}

	[Fact]
	public async Task GetDashboards_WithValidRequest_ReturnsDashboards()
	{
		try
		{
			// Act
			var result = await ThousandEyesClient.Dashboards.Dashboards.GetAllAsync(
				aid: null,
				cancellationToken: CancellationToken);

			// Assert
			_ = result.Should().NotBeNull();

			// If there are dashboards, verify the structure
			if (result.Length > 0)
			{
				var firstDashboard = result[0];
				_ = firstDashboard.DashboardId.Should().NotBeNullOrEmpty();
				_ = firstDashboard.Title.Should().NotBeNullOrEmpty();
			}
		}
		catch (ValidationApiException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
		{
			// Skip test if Dashboards API is not available in ThousandEyes API v7
			return;
		}
	}

	[Fact]
	public async Task GetDashboardById_WithValidDashboardId_ReturnsDashboardDetails()
	{
		try
		{
			// Arrange - First get list of dashboards to find a valid dashboard ID
			var dashboards = await ThousandEyesClient.Dashboards.Dashboards.GetAllAsync(
				aid: null,
				cancellationToken: CancellationToken);

			// Skip test if no dashboards are available
			if (dashboards.Length == 0)
			{
				return;
			}

			var dashboardId = dashboards[0].DashboardId;

			// Act
			var result = await ThousandEyesClient.Dashboards.Dashboards.GetByIdAsync(
				dashboardId,
				aid: null,
				cancellationToken: CancellationToken);

			// Assert
			_ = result.Should().NotBeNull();
			_ = result.DashboardId.Should().Be(dashboardId);
			_ = result.Title.Should().NotBeNullOrEmpty();
		}
		catch (ValidationApiException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
		{
			// Skip test if Dashboards API is not available
			return;
		}
	}

	[Fact]
	public async Task CreateDashboard_WithValidRequest_CreatesDashboard()
	{
		try
		{
			// Arrange
			var dashboardRequest = new DashboardRequest
			{
				Title = $"Test Dashboard - {DateTime.UtcNow:yyyyMMdd-HHmmss}",
				Description = "Integration test dashboard",
				IsPrivate = true,
				IsDefaultForUser = false
			};

			// Act
			var result = await ThousandEyesClient.Dashboards.Dashboards.CreateAsync(
				dashboardRequest,
				aid: null,
				cancellationToken: CancellationToken);

			// Assert
			_ = result.Should().NotBeNull();
			_ = result.DashboardId.Should().NotBeNullOrEmpty();
			_ = result.Title.Should().Be(dashboardRequest.Title);
			_ = result.IsPrivate.Should().Be(dashboardRequest.IsPrivate);

			// Cleanup - Delete the created dashboard
			try
			{
				await ThousandEyesClient.Dashboards.Dashboards.DeleteAsync(
					result.DashboardId,
					aid: null,
					cancellationToken: CancellationToken);
			}
			catch
			{
				// Ignore cleanup errors
			}
		}
		catch (ValidationApiException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
		{
			// Skip test if Dashboards API is not available
			return;
		}
	}

	[Fact]
	public async Task GetDashboardSnapshots_WithValidRequest_ReturnsSnapshots()
	{
		try
		{
			// Act
			var result = await ThousandEyesClient.Dashboards.Snapshots.GetAllAsync(
				aid: null,
				dashboardId: null,
				cursor: null,
				cancellationToken: CancellationToken);

			// Assert
			_ = result.Should().NotBeNull();
			_ = result.DashboardSnapshots.Should().NotBeNull();
		}
		catch (ValidationApiException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
		{
			// Skip test if Dashboard Snapshots API is not available
			return;
		}
	}

	[Fact]
	public async Task CreateDashboardSnapshot_WithValidRequest_CreatesSnapshot()
	{
		try
		{
			// Arrange - First get list of dashboards to find a valid dashboard ID
			var dashboards = await ThousandEyesClient.Dashboards.Dashboards.GetAllAsync(
				aid: null,
				cancellationToken: CancellationToken);

			// Skip test if no dashboards are available
			if (dashboards.Length == 0)
			{
				return;
			}

			var dashboardId = dashboards[0].DashboardId;

			var snapshotRequest = new CreateDashboardSnapshotRequest
			{
				DashboardId = dashboardId,
				DisplayName = $"Test Snapshot - {DateTime.UtcNow:yyyyMMdd-HHmmss}",
				StartDate = DateTime.UtcNow.AddDays(-1),
				EndDate = DateTime.UtcNow,
				Timezone = "UTC",
				AnonymizeData = false
			};

			// Act
			var result = await ThousandEyesClient.Dashboards.Snapshots.CreateAsync(
				snapshotRequest,
				aid: null,
				cancellationToken: CancellationToken);

			// Assert
			_ = result.Should().NotBeNull();
			_ = result.SnapshotId.Should().NotBeNullOrEmpty();

			// Cleanup - Delete the created snapshot
			try
			{
				await ThousandEyesClient.Dashboards.Snapshots.DeleteAsync(
					result.SnapshotId,
					aid: null,
					cancellationToken: CancellationToken);
			}
			catch
			{
				// Ignore cleanup errors
			}
		}
		catch (ValidationApiException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
		{
			// Skip test if Dashboard Snapshots API is not available
			return;
		}
	}

	[Fact]
	public async Task GetDashboardFilters_WithValidRequest_ReturnsFilters()
	{
		try
		{
			// Act
			var result = await ThousandEyesClient.Dashboards.Filters.GetAllAsync(
				aid: null,
				searchPattern: null,
				cancellationToken: CancellationToken);

			// Assert
			_ = result.Should().NotBeNull();
			_ = result.Filters.Should().NotBeNull();

			// If there are filters, verify the structure
			if (result.Filters.Length > 0)
			{
				var firstFilter = result.Filters[0];
				_ = firstFilter.Id.Should().NotBeNullOrEmpty();
				_ = firstFilter.Name.Should().NotBeNullOrEmpty();
			}
		}
		catch (ValidationApiException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
		{
			// Skip test if Dashboard Filters API is not available
			return;
		}
	}

	[Fact]
	public async Task CreateDashboardFilter_WithValidRequest_CreatesFilter()
	{
		try
		{
			// Arrange - First get real test IDs to use in the filter
			var tests = await ThousandEyesClient.Tests.Tests.GetAllAsync(aid: null, CancellationToken);
			
			// Skip if no tests available to create filter with
			if (tests.TestsList.Length == 0)
			{
				Logger.LogWarning("Skipping CreateDashboardFilter test - no tests available");
				return;
			}

			// Use the first test ID for the filter
			var testId = tests.TestsList[0].TestId ?? "0";

			var filterRequest = new DashboardFilterRequest
			{
				Name = $"Test Filter - {DateTime.UtcNow:yyyyMMdd-HHmmss}",
				Description = "Integration test filter",
				Context = [
					new DataSourceFilter
					{
						DataSourceId = "CLOUD_AND_ENTERPRISE_AGENTS",
						Filters = [
							new FilterProperty
							{
								FilterId = "TEST",
								Values = [testId], // Use actual test ID
								MetricIds = ["WEB_AVAILABILITY"]
							}
						]
					}
				]
			};

			// Act
			var result = await ThousandEyesClient.Dashboards.Filters.CreateAsync(
				filterRequest,
				aid: null,
				cancellationToken: CancellationToken);

			// Assert
			_ = result.Should().NotBeNull();
			_ = result.Id.Should().NotBeNullOrEmpty();
			_ = result.Name.Should().Be(filterRequest.Name);

			// Cleanup - Delete the created filter
			try
			{
				await ThousandEyesClient.Dashboards.Filters.DeleteAsync(
					result.Id,
					aid: null,
					cancellationToken: CancellationToken);
			}
			catch
			{
				// Ignore cleanup errors
			}
		}
		catch (ValidationApiException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
		{
			// Skip test if Dashboard Filters API is not available
			return;
		}
	}
}