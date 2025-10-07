using AwesomeAssertions;
using ThousandEyes.Api.Models.Alerts;

namespace ThousandEyes.Api.Test;

[Collection("Integration Tests")]
public class AlertsIntegrationTest(IntegrationTestFixture fixture) : TestBase(fixture)
{
	[Fact]
	public async Task GetAlerts_WithValidRequest_ReturnsAlerts()
	{
		// Act
		var result = await ThousandEyesClient.Alerts.Alerts.GetAllAsync(
			aid: null, 
			window: null, 
			fromDate: null, 
			toDate: null, 
			cancellationToken: CancellationToken);

		// Assert
		_ = result.Should().NotBeNull();
		_ = result.AlertsList.Should().NotBeNull();

		// If there are alerts, verify the structure
		if (result.AlertsList.Length > 0)
		{
			var firstAlert = result.AlertsList[0];
			_ = firstAlert.AlertId.Should().NotBeNullOrEmpty();
			_ = firstAlert.RuleId.Should().NotBeNullOrEmpty();
			_ = firstAlert.TestId.Should().NotBeNullOrEmpty();
			_ = firstAlert.TestName.Should().NotBeNullOrEmpty();
			_ = firstAlert.Type.Should().NotBeNullOrEmpty();
			_ = firstAlert.Status.Should().NotBeNullOrEmpty();
			_ = firstAlert.Severity.Should().NotBeNullOrEmpty();
			_ = firstAlert.DateStart.Should().BeAfter(DateTime.MinValue);
		}
	}

	[Fact]
	public async Task GetAlerts_WithTimeWindow_ReturnsFilteredAlerts()
	{
		// Act
		var result = await ThousandEyesClient.Alerts.Alerts.GetAllAsync(
			aid: null, 
			window: "7d", 
			fromDate: null, 
			toDate: null, 
			cancellationToken: CancellationToken);

		// Assert
		_ = result.Should().NotBeNull();
		_ = result.AlertsList.Should().NotBeNull();

		// If there are alerts, verify they're within the time window
		if (result.AlertsList.Length > 0)
		{
			var sevenDaysAgo = DateTime.UtcNow.AddDays(-7);
			var firstAlert = result.AlertsList[0];
			_ = firstAlert.DateStart.Should().BeAfter(sevenDaysAgo);
		}
	}

	[Fact]
	public async Task GetAlerts_WithDateRange_ReturnsFilteredAlerts()
	{
		// Arrange
		var fromDate = DateTime.UtcNow.AddDays(-30);
		var toDate = DateTime.UtcNow;

		// Act
		var result = await ThousandEyesClient.Alerts.Alerts.GetAllAsync(
			aid: null, 
			window: null, 
			fromDate: fromDate, 
			toDate: toDate, 
			cancellationToken: CancellationToken);

		// Assert
		_ = result.Should().NotBeNull();
		_ = result.AlertsList.Should().NotBeNull();

		// If there are alerts, verify they're within the date range
		if (result.AlertsList.Length > 0)
		{
			var firstAlert = result.AlertsList[0];
			_ = firstAlert.DateStart.Should().BeAfter(fromDate);
			_ = firstAlert.DateStart.Should().BeBefore(toDate);
		}
	}

	[Fact]
	public async Task GetAlertById_WithValidAlertId_ReturnsAlertDetails()
	{
		// Arrange - First get list of alerts to find a valid alert ID
		var alerts = await ThousandEyesClient.Alerts.Alerts.GetAllAsync(
			aid: null, 
			window: "30d", 
			fromDate: null, 
			toDate: null, 
			cancellationToken: CancellationToken);
		
		// Skip test if no alerts are available
		if (alerts.AlertsList.Length == 0)
		{
			return; // Skip test - no alerts available
		}

		var alertId = alerts.AlertsList[0].AlertId;

		// Act
		var result = await ThousandEyesClient.Alerts.Alerts.GetByIdAsync(
			alertId, 
			aid: null, 
			cancellationToken: CancellationToken);

		// Assert
		_ = result.Should().NotBeNull();
		_ = result.AlertId.Should().Be(alertId);
		_ = result.RuleId.Should().NotBeNullOrEmpty();
		_ = result.TestId.Should().NotBeNullOrEmpty();
		_ = result.TestName.Should().NotBeNullOrEmpty();
		_ = result.Type.Should().NotBeNullOrEmpty();
		_ = result.Status.Should().NotBeNullOrEmpty();
		_ = result.Severity.Should().NotBeNullOrEmpty();
		_ = result.DateStart.Should().BeAfter(DateTime.MinValue);
	}

	[Fact]
	public async Task GetAlertRules_WithValidRequest_ReturnsAlertRules()
	{
		// Act
		var result = await ThousandEyesClient.Alerts.AlertRules.GetAllAsync(
			aid: null, 
			cancellationToken: CancellationToken);

		// Assert
		_ = result.Should().NotBeNull();
		_ = result.AlertRulesList.Should().NotBeNull();

		// If there are alert rules, verify the structure
		if (result.AlertRulesList.Length > 0)
		{
			var firstRule = result.AlertRulesList[0];
			_ = firstRule.RuleId.Should().NotBeNullOrEmpty();
			_ = firstRule.RuleName.Should().NotBeNullOrEmpty();
			_ = firstRule.Expression.Should().NotBeNullOrEmpty();
			_ = firstRule.AlertType.Should().NotBeNullOrEmpty();
			_ = firstRule.Severity.Should().NotBeNullOrEmpty();
		}
	}

	[Fact]
	public async Task GetAlertRuleById_WithValidRuleId_ReturnsAlertRuleDetails()
	{
		// Arrange - First get list of alert rules to find a valid rule ID
		var rules = await ThousandEyesClient.Alerts.AlertRules.GetAllAsync(
			aid: null, 
			cancellationToken: CancellationToken);
		
		// Skip test if no alert rules are available
		if (rules.AlertRulesList.Length == 0)
		{
			return; // Skip test - no alert rules available
		}

		var ruleId = rules.AlertRulesList[0].RuleId;

		// Act
		var result = await ThousandEyesClient.Alerts.AlertRules.GetByIdAsync(
			ruleId, 
			aid: null, 
			cancellationToken: CancellationToken);

		// Assert
		_ = result.Should().NotBeNull();
		_ = result.RuleId.Should().Be(ruleId);
		_ = result.RuleName.Should().NotBeNullOrEmpty();
		_ = result.Expression.Should().NotBeNullOrEmpty();
		_ = result.AlertType.Should().NotBeNullOrEmpty();
		_ = result.Severity.Should().NotBeNullOrEmpty();
	}

	[Fact]
	public async Task CreateAlertRule_WithValidRequest_CreatesAlertRule()
	{
		// Arrange - First get a test to use in the alert rule
		var tests = await ThousandEyesClient.Tests.Tests.GetAllAsync(aid: null, cancellationToken: CancellationToken);
		
		// Skip test if no tests are available
		if (tests.TestsList.Length == 0)
		{
			return; // Skip test - no tests available for alert rule
		}

		var testId = tests.TestsList[0].TestId;
		var testName = tests.TestsList[0].TestName;

		var alertRuleRequest = new AlertRuleRequest
		{
			RuleName = $"Test Alert Rule - {DateTime.UtcNow:yyyyMMdd-HHmmss}",
			Description = "Integration test alert rule",
			Expression = "((loss >= 5%))",
			Enabled = false, // Keep disabled to avoid triggering real alerts
			AlertType = "test",
			Severity = "warning",
			MinimumSources = 1,
			MinimumSourcesPct = 100,
			RoundsBelowThreshold = 2,
			Tests = [
				new AlertRuleTest { TestId = testId, TestName = testName }
			],
			Notifications = new AlertRuleNotifications
			{
				Emails = ["test@example.com"]
			}
		};

		// Act
		var result = await ThousandEyesClient.Alerts.AlertRules.CreateAsync(
			alertRuleRequest, 
			aid: null, 
			cancellationToken: CancellationToken);

		// Assert
		_ = result.Should().NotBeNull();
		_ = result.RuleId.Should().NotBeNullOrEmpty();
		_ = result.RuleName.Should().Be(alertRuleRequest.RuleName);
		_ = result.Expression.Should().Be(alertRuleRequest.Expression);
		_ = result.AlertType.Should().Be(alertRuleRequest.AlertType);
		_ = result.Severity.Should().Be(alertRuleRequest.Severity);
		_ = result.Enabled.Should().BeFalse();

		// Cleanup - Delete the created alert rule
		try
		{
			await ThousandEyesClient.Alerts.AlertRules.DeleteAsync(
				result.RuleId, 
				aid: null, 
				cancellationToken: CancellationToken);
		}
		catch
		{
			// Ignore cleanup errors - rule will be left disabled
		}
	}
}