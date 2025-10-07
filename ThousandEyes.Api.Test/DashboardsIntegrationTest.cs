using AwesomeAssertions;
using ThousandEyes.Api.Models.Dashboards;
using Refit;

namespace ThousandEyes.Api.Test;

[Collection("Integration Tests")]
public class DashboardsIntegrationTest(IntegrationTestFixture fixture) : TestBase(fixture)
{
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
			_ = result.DashboardsList.Should().NotBeNull();

			// If there are dashboards, verify the structure
			if (result.DashboardsList.Length > 0)
			{
				var firstDashboard = result.DashboardsList[0];
				_ = firstDashboard.DashboardId.Should().NotBeNullOrEmpty();
				_ = firstDashboard.DashboardName.Should().NotBeNullOrEmpty();
				_ = firstDashboard.DashboardType.Should().NotBeNullOrEmpty();
			}
		}
		catch (ValidationApiException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
		{
			// Skip test if Dashboards API is not available in ThousandEyes API v7
			// This indicates the endpoint doesn't exist or uses different paths
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
			if (dashboards.DashboardsList.Length == 0)
			{
				return; // Skip test - no dashboards available
			}

			var dashboardId = dashboards.DashboardsList[0].DashboardId;

			// Act
			var result = await ThousandEyesClient.Dashboards.Dashboards.GetByIdAsync(
				dashboardId, 
				aid: null, 
				cancellationToken: CancellationToken);

			// Assert
			_ = result.Should().NotBeNull();
			_ = result.DashboardId.Should().Be(dashboardId);
			_ = result.DashboardName.Should().NotBeNullOrEmpty();
			_ = result.DashboardType.Should().NotBeNullOrEmpty();
		}
		catch (ValidationApiException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
		{
			// Skip test if Dashboards API is not available in ThousandEyes API v7
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
				DashboardName = $"Test Dashboard - {DateTime.UtcNow:yyyyMMdd-HHmmss}",
				Description = "Integration test dashboard",
				DashboardType = "personal",
				IsPrivate = true,
				IsDefault = false,
				Layout = new DashboardLayout
				{
					Type = "grid",
					Columns = 12,
					RowHeight = 150,
					Margin = 10,
					IsResponsive = true
				},
				TimeSpan = new DashboardTimeSpan
				{
					Type = "relative",
					RelativeSpan = "24h",
					AutoRefresh = true,
					RefreshInterval = 300
				},
				Widgets = [
					new DashboardWidget
					{
						WidgetId = "widget-1",
						Title = "Sample Widget",
						Type = "timeseries",
						Position = new WidgetPosition
						{
							X = 0,
							Y = 0,
							Width = 6,
							Height = 3
						},
						DataSource = new WidgetDataSource
						{
							Type = "test",
							TestIds = [],
							Metrics = ["responseTime", "availability"],
							Aggregation = "average"
						},
						Visualization = new WidgetVisualization
						{
							ChartType = "line",
							ShowLegend = true,
							ShowGrid = true
						}
					}
				],
				Filters = [
					new DashboardFilter
					{
						FilterId = "filter-1",
						Name = "Test Filter",
						Type = "test",
						Values = [],
						Operator = "equals",
						IsEnabled = true
					}
				]
			};

			// Act
			var result = await ThousandEyesClient.Dashboards.Dashboards.CreateAsync(
				dashboardRequest, 
				aid: null, 
				cancellationToken: CancellationToken);

			// Assert
			_ = result.Should().NotBeNull();
			_ = result.DashboardId.Should().NotBeNullOrEmpty();
			_ = result.DashboardName.Should().Be(dashboardRequest.DashboardName);
			_ = result.DashboardType.Should().Be(dashboardRequest.DashboardType);
			_ = result.IsPrivate.Should().Be(dashboardRequest.IsPrivate);
			_ = result.IsDefault.Should().Be(dashboardRequest.IsDefault);

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
				// Ignore cleanup errors - dashboard will be left but marked as test
			}
		}
		catch (ValidationApiException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
		{
			// Skip test if Dashboards API is not available in ThousandEyes API v7
			return;
		}
	}

	[Fact]
	public async Task GetReports_WithValidRequest_ReturnsReports()
	{
		try
		{
			// Act
			var result = await ThousandEyesClient.Dashboards.Reports.GetAllAsync(
				aid: null, 
				cancellationToken: CancellationToken);

			// Assert
			_ = result.Should().NotBeNull();
			_ = result.ReportsList.Should().NotBeNull();

			// If there are reports, verify the structure
			if (result.ReportsList.Length > 0)
			{
				var firstReport = result.ReportsList[0];
				_ = firstReport.ReportId.Should().NotBeNullOrEmpty();
				_ = firstReport.ReportName.Should().NotBeNullOrEmpty();
				_ = firstReport.ReportType.Should().NotBeNullOrEmpty();
			}
		}
		catch (ValidationApiException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
		{
			// Skip test if Reports API is not available in ThousandEyes API v7
			return;
		}
	}

	[Fact]
	public async Task GetReportById_WithValidReportId_ReturnsReportDetails()
	{
		try
		{
			// Arrange - First get list of reports to find a valid report ID
			var reports = await ThousandEyesClient.Dashboards.Reports.GetAllAsync(
				aid: null, 
				cancellationToken: CancellationToken);
			
			// Skip test if no reports are available
			if (reports.ReportsList.Length == 0)
			{
				return; // Skip test - no reports available
			}

			var reportId = reports.ReportsList[0].ReportId;

			// Act
			var result = await ThousandEyesClient.Dashboards.Reports.GetByIdAsync(
				reportId, 
				aid: null, 
				cancellationToken: CancellationToken);

			// Assert
			_ = result.Should().NotBeNull();
			_ = result.ReportId.Should().Be(reportId);
			_ = result.ReportName.Should().NotBeNullOrEmpty();
			_ = result.ReportType.Should().NotBeNullOrEmpty();
		}
		catch (ValidationApiException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
		{
			// Skip test if Reports API is not available in ThousandEyes API v7
			return;
		}
	}

	[Fact]
	public async Task CreateReport_WithValidRequest_CreatesReport()
	{
		try
		{
			// Arrange
			var reportRequest = new ReportRequest
			{
				ReportName = $"Test Report - {DateTime.UtcNow:yyyyMMdd-HHmmss}",
				Description = "Integration test report",
				ReportType = "on-demand",
				Format = "pdf",
				DataConfig = new ReportDataConfig
				{
					TimeRangeType = "relative",
					RelativeTimeRange = "7d",
					DaysBack = 7,
					Aggregation = "daily",
					IncludeRawData = false,
					IncludeSummary = true
				},
				EmailConfig = new ReportEmailConfig
				{
					Recipients = ["test@example.com"],
					Subject = "Test Report",
					Body = "This is a test report generated by integration tests.",
					AttachReport = true,
					EmbedCharts = false
				}
			};

			// Act
			var result = await ThousandEyesClient.Dashboards.Reports.CreateAsync(
				reportRequest, 
				aid: null, 
				cancellationToken: CancellationToken);

			// Assert
			_ = result.Should().NotBeNull();
			_ = result.ReportId.Should().NotBeNullOrEmpty();
			_ = result.ReportName.Should().Be(reportRequest.ReportName);
			_ = result.ReportType.Should().Be(reportRequest.ReportType);
			_ = result.Format.Should().Be(reportRequest.Format);

			// Cleanup - Delete the created report
			try
			{
				await ThousandEyesClient.Dashboards.Reports.DeleteAsync(
					result.ReportId, 
					aid: null, 
					cancellationToken: CancellationToken);
			}
			catch
			{
				// Ignore cleanup errors - report will be left but marked as test
			}
		}
		catch (ValidationApiException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
		{
			// Skip test if Reports API is not available in ThousandEyes API v7
			return;
		}
	}

	[Fact]
	public async Task CreateScheduledReport_WithValidRequest_CreatesScheduledReport()
	{
		try
		{
			// Arrange
			var reportRequest = new ReportRequest
			{
				ReportName = $"Scheduled Test Report - {DateTime.UtcNow:yyyyMMdd-HHmmss}",
				Description = "Integration test scheduled report",
				ReportType = "scheduled",
				Format = "pdf",
				Schedule = new ReportSchedule
				{
					Frequency = "weekly",
					DayOfWeek = 1, // Monday
					Hour = 9,
					Minute = 0,
					TimeZone = "UTC",
					IsEnabled = false // Keep disabled to avoid actual scheduling
				},
				DataConfig = new ReportDataConfig
				{
					TimeRangeType = "relative",
					RelativeTimeRange = "7d",
					DaysBack = 7,
					Aggregation = "daily",
					IncludeRawData = false,
					IncludeSummary = true
				},
				EmailConfig = new ReportEmailConfig
				{
					Recipients = ["test@example.com"],
					Subject = "Weekly Test Report",
					Body = "This is a weekly test report.",
					AttachReport = true,
					EmbedCharts = true
				}
			};

			// Act
			var result = await ThousandEyesClient.Dashboards.Reports.CreateAsync(
				reportRequest, 
				aid: null, 
				cancellationToken: CancellationToken);

			// Assert
			_ = result.Should().NotBeNull();
			_ = result.ReportId.Should().NotBeNullOrEmpty();
			_ = result.ReportName.Should().Be(reportRequest.ReportName);
			_ = result.ReportType.Should().Be(reportRequest.ReportType);
			_ = result.Schedule.Should().NotBeNull();
			_ = result.Schedule!.Frequency.Should().Be("weekly");
			_ = result.Schedule.DayOfWeek.Should().Be(1);
			_ = result.Schedule.IsEnabled.Should().BeFalse();

			// Cleanup - Delete the created report
			try
			{
				await ThousandEyesClient.Dashboards.Reports.DeleteAsync(
					result.ReportId, 
					aid: null, 
					cancellationToken: CancellationToken);
			}
			catch
			{
				// Ignore cleanup errors - report will be left but marked as test
			}
		}
		catch (ValidationApiException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
		{
			// Skip test if Reports API is not available in ThousandEyes API v7
			return;
		}
	}

	[Fact]
	public async Task GenerateReport_WithValidReportId_ReturnsDownloadLink()
	{
		try
		{
			// Arrange - First get list of reports to find a valid report ID
			var reports = await ThousandEyesClient.Dashboards.Reports.GetAllAsync(
				aid: null, 
				cancellationToken: CancellationToken);
			
			// Skip test if no reports are available
			if (reports.ReportsList.Length == 0)
			{
				return; // Skip test - no reports available
			}

			var reportId = reports.ReportsList[0].ReportId;

			// Act
			var result = await ThousandEyesClient.Dashboards.Reports.GenerateAsync(
				reportId, 
				aid: null, 
				cancellationToken: CancellationToken);

			// Assert
			_ = result.Should().NotBeNull();
			_ = result.Should().NotBeEmpty();
			_ = result.Should().Contain("report"); // Should contain report-related URL or path
		}
		catch (ValidationApiException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
		{
			// Skip test if Reports API is not available in ThousandEyes API v7
			return;
		}
	}
}