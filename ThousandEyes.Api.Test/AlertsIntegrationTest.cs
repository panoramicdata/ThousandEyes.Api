using AwesomeAssertions;
using ThousandEyes.Api.Models.Alerts;

namespace ThousandEyes.Api.Test;

[Collection("Integration Tests")]
[Trait("Category", "Integration")]
public class AlertsIntegrationTest(IntegrationTestFixture fixture) : TestBase(fixture)
{
	private Task<Models.Alerts.Alerts> ListAlertsAsync(string? window = null, DateTime? from = null, DateTime? to = null) =>
		ThousandEyesClient.Alerts.Alerts.GetAllAsync(aid: null, window: window, fromDate: from, toDate: to, cancellationToken: CancellationToken);

	private static void CheckAlert(Alert alert)
	{
		_ = alert.AlertId.Should().NotBeNullOrEmpty();
		_ = alert.RuleId.Should().NotBeNullOrEmpty();
		_ = alert.TestId.Should().NotBeNullOrEmpty();
		_ = alert.TestName.Should().NotBeNullOrEmpty();
		_ = alert.Type.Should().NotBeNullOrEmpty();
		_ = alert.Status.Should().NotBeNullOrEmpty();
		_ = alert.Severity.Should().NotBeNullOrEmpty();
		_ = alert.DateStart.Should().BeAfter(DateTime.MinValue);
	}

	private static void CheckRule(AlertRule rule)
	{
		_ = rule.RuleId.Should().NotBeNullOrEmpty();
		_ = rule.RuleName.Should().NotBeNullOrEmpty();
		_ = rule.Expression.Should().NotBeNullOrEmpty();
		_ = rule.AlertType.Should().NotBeNullOrEmpty();
		_ = rule.Severity.Should().NotBeNullOrEmpty();
	}

	[Fact]
	public async Task GetAlerts_WithValidRequest_ReturnsAlerts()
	{
		var result = await ListAlertsAsync();

		_ = result.AlertsList.Should().NotBeNull();
		if (result.AlertsList.Length > 0)
		{
			CheckAlert(result.AlertsList[0]);
		}
	}

	[Fact]
	public async Task GetAlerts_WithTimeWindow_ReturnsFilteredAlerts()
	{
		var result = await ListAlertsAsync(window: "7d");

		_ = result.AlertsList.Should().NotBeNull();
		if (result.AlertsList.Length > 0)
		{
			_ = result.AlertsList[0].DateStart.Should().BeAfter(DateTime.UtcNow.AddDays(-7));
		}
	}

	[Fact]
	public async Task GetAlerts_WithDateRange_ReturnsFilteredAlerts()
	{
		var fromDate = DateTime.UtcNow.AddDays(-30);
		var toDate = DateTime.UtcNow;

		var result = await ListAlertsAsync(from: fromDate, to: toDate);

		_ = result.AlertsList.Should().NotBeNull();
		if (result.AlertsList.Length > 0)
		{
			_ = result.AlertsList[0].DateStart.Should().BeAfter(fromDate).And.BeBefore(toDate);
		}
	}

	[Fact]
	public async Task GetAlertById_WithValidAlertId_ReturnsAlertDetails()
	{
		var alerts = await ListAlertsAsync(window: "30d");
		if (alerts.AlertsList.Length == 0)
		{
			return;
		}

		var alertId = alerts.AlertsList[0].AlertId;

		var result = await ThousandEyesClient.Alerts.Alerts.GetByIdAsync(alertId, aid: null, cancellationToken: CancellationToken);

		_ = result.AlertId.Should().Be(alertId);
		CheckAlert(result);
	}

	[Fact]
	public async Task GetAlertRules_WithValidRequest_ReturnsAlertRules()
	{
		var result = await ThousandEyesClient.Alerts.AlertRules.GetAllAsync(aid: null, cancellationToken: CancellationToken);

		_ = result.AlertRulesList.Should().NotBeNull();
		if (result.AlertRulesList.Length > 0)
		{
			CheckRule(result.AlertRulesList[0]);
		}
	}

	[Fact]
	public async Task GetAlertRuleById_WithValidRuleId_ReturnsAlertRuleDetails()
	{
		var rules = await ThousandEyesClient.Alerts.AlertRules.GetAllAsync(aid: null, cancellationToken: CancellationToken);
		if (rules.AlertRulesList.Length == 0)
		{
			return;
		}

		var ruleId = rules.AlertRulesList[0].RuleId;

		var result = await ThousandEyesClient.Alerts.AlertRules.GetByIdAsync(ruleId, aid: null, cancellationToken: CancellationToken);

		_ = result.RuleId.Should().Be(ruleId);
		CheckRule(result);
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

		var alertRuleRequest = NewAlertRuleRequest(testId, testName);


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
		await IgnoreErrorsAsync(async () =>
		{
			await ThousandEyesClient.Alerts.AlertRules.DeleteAsync(
				result.RuleId,
				aid: null,
				cancellationToken: CancellationToken);
		});
	}

	private static AlertRuleRequest NewAlertRuleRequest(string testId, string testName) => new()
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
		Tests = [new AlertRuleTest { TestId = testId, TestName = testName }],
		Notifications = new AlertRuleNotifications
		{
			Emails = ["test@example.com"]
		}
	};
}