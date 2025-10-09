using AwesomeAssertions;
using Moq;
using ThousandEyes.Api.Interfaces;
using ThousandEyes.Api.Models.Alerts;

namespace ThousandEyes.Api.Test.UnitTests.Alerts;

public class AlertRulesApiTests
{
	private readonly Mock<IAlertRulesRefitApi> _refitApi;
	private readonly AlertRulesApi _sut;

	public AlertRulesApiTests()
	{
		_refitApi = new Mock<IAlertRulesRefitApi>();
		_sut = new AlertRulesApi(_refitApi.Object);
	}

	[Fact]
	public async Task GetAllAsync_CallsApi_AndReturnsData()
	{
		// Arrange
		var cancellationToken = new CancellationToken();
		var expectedResponse = new AlertRules
		{
			AlertRulesList = [
				new AlertRule
				{
					RuleId = "123",
					RuleName = "Test Rule",
					Expression = "test expression",
					AlertType = "agent",
					Severity = "critical"
				}
			]
		};
		_ = _refitApi.Setup(x => x.GetAllAsync(null, cancellationToken))
			.ReturnsAsync(expectedResponse);

		// Act
		var result = await _sut.GetAllAsync(null, cancellationToken);

		// Assert
		_ = result.Should().Be(expectedResponse);
		_refitApi.Verify(x => x.GetAllAsync(null, cancellationToken), Times.Once);
	}

	[Fact]
	public async Task GetByIdAsync_CallsApi_AndReturnsData()
	{
		// Arrange
		var ruleId = "123";
		var cancellationToken = new CancellationToken();
		var expectedResponse = new AlertRule
		{
			RuleId = ruleId,
			RuleName = "Test Rule",
			Expression = "test expression",
			AlertType = "agent",
			Severity = "critical"
		};
		_ = _refitApi.Setup(x => x.GetByIdAsync(ruleId, null, cancellationToken))
			.ReturnsAsync(expectedResponse);

		// Act
		var result = await _sut.GetByIdAsync(ruleId, null, cancellationToken);

		// Assert
		_ = result.Should().Be(expectedResponse);
		_refitApi.Verify(x => x.GetByIdAsync(ruleId, null, cancellationToken), Times.Once);
	}

	[Fact]
	public async Task CreateAsync_CallsApi_AndReturnsData()
	{
		// Arrange
		var cancellationToken = new CancellationToken();
		var request = new AlertRuleRequest
		{
			RuleName = "Test Rule",
			Expression = "test expression",
			AlertType = "agent",
			Severity = "critical"
		};
		var expectedResponse = new AlertRule
		{
			RuleId = "123",
			RuleName = "Test Rule",
			Expression = "test expression",
			AlertType = "agent",
			Severity = "critical"
		};
		_ = _refitApi.Setup(x => x.CreateAsync(request, null, cancellationToken))
			.ReturnsAsync(expectedResponse);

		// Act
		var result = await _sut.CreateAsync(request, null, cancellationToken);

		// Assert
		_ = result.Should().Be(expectedResponse);
		_refitApi.Verify(x => x.CreateAsync(request, null, cancellationToken), Times.Once);
	}

	[Fact]
	public async Task UpdateAsync_CallsApi_AndReturnsData()
	{
		// Arrange
		var ruleId = "123";
		var request = new AlertRuleRequest
		{
			RuleName = "Updated Rule",
			Expression = "updated expression",
			AlertType = "agent",
			Severity = "warning"
		};
		var cancellationToken = new CancellationToken();
		var expectedResponse = new AlertRule
		{
			RuleId = ruleId,
			RuleName = "Updated Rule",
			Expression = "updated expression",
			AlertType = "agent",
			Severity = "warning"
		};
		_ = _refitApi.Setup(x => x.UpdateAsync(ruleId, request, null, cancellationToken))
			.ReturnsAsync(expectedResponse);

		// Act
		var result = await _sut.UpdateAsync(ruleId, request, null, cancellationToken);

		// Assert
		_ = result.Should().Be(expectedResponse);
		_refitApi.Verify(x => x.UpdateAsync(ruleId, request, null, cancellationToken), Times.Once);
	}

	[Fact]
	public async Task DeleteAsync_CallsApi()
	{
		// Arrange
		var ruleId = "123";
		var cancellationToken = new CancellationToken();
		_ = _refitApi.Setup(x => x.DeleteAsync(ruleId, null, cancellationToken))
			.Returns(Task.CompletedTask);

		// Act
		await _sut.DeleteAsync(ruleId, null, cancellationToken);

		// Assert
		_refitApi.Verify(x => x.DeleteAsync(ruleId, null, cancellationToken), Times.Once);
	}
}
