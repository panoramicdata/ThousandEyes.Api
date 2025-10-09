using AwesomeAssertions;
using Moq;
using ThousandEyes.Api.Interfaces;
using ThousandEyes.Api.Models.Alerts;
using AlertsCollection = ThousandEyes.Api.Models.Alerts.Alerts;

namespace ThousandEyes.Api.Test.UnitTests.Alerts;

public class AlertsApiTests
{
	private readonly Mock<IAlertsRefitApi> _refitApi;
	private readonly AlertsApi _sut;

	public AlertsApiTests()
	{
		_refitApi = new Mock<IAlertsRefitApi>();
		_sut = new AlertsApi(_refitApi.Object);
	}

	[Fact]
	public async Task GetAllAsync_CallsApi_AndReturnsData()
	{
		// Arrange
		var cancellationToken = new CancellationToken();
		var expectedResponse = new AlertsCollection
		{
			AlertsList = [
				new Alert
				{
					AlertId = "123",
					RuleId = "rule-456",
					TestId = "test-789",
					TestName = "Test Alert",
					Type = "test",
					Status = "active",
					Severity = "critical",
					DateStart = DateTime.UtcNow
				}
			]
		};
		_ = _refitApi.Setup(x => x.GetAllAsync(null, null, null, null, cancellationToken))
			.ReturnsAsync(expectedResponse);

		// Act
		var result = await _sut.GetAllAsync(null, null, null, null, cancellationToken);

		// Assert
		_ = result.Should().Be(expectedResponse);
		_refitApi.Verify(x => x.GetAllAsync(null, null, null, null, cancellationToken), Times.Once);
	}

	[Fact]
	public async Task GetByIdAsync_CallsApi_AndReturnsData()
	{
		// Arrange
		var alertId = "123";
		var cancellationToken = new CancellationToken();
		var expectedResponse = new Alert
		{
			AlertId = alertId,
			RuleId = "rule-456",
			TestId = "test-789",
			TestName = "Test Alert",
			Type = "test",
			Status = "active",
			Severity = "critical",
			DateStart = DateTime.UtcNow
		};
		_ = _refitApi.Setup(x => x.GetByIdAsync(alertId, null, cancellationToken))
			.ReturnsAsync(expectedResponse);

		// Act
		var result = await _sut.GetByIdAsync(alertId, null, cancellationToken);

		// Assert
		_ = result.Should().Be(expectedResponse);
		_refitApi.Verify(x => x.GetByIdAsync(alertId, null, cancellationToken), Times.Once);
	}
}
