using AwesomeAssertions;
using Moq;
using ThousandEyes.Api.Interfaces;
using ThousandEyes.Api.Models.Dashboards;

namespace ThousandEyes.Api.Test.UnitTests.Dashboards;

public class DashboardsApiTests
{
	private readonly Mock<IDashboardsRefitApi> _refitApi;
	private readonly DashboardsApi _sut;

	public DashboardsApiTests()
	{
		_refitApi = new Mock<IDashboardsRefitApi>();
		_sut = new DashboardsApi(_refitApi.Object);
	}

	[Fact]
	public async Task GetAllAsync_CallsApi_AndReturnsData()
	{
		// Arrange
		var cancellationToken = new CancellationToken();
		var expectedResponse = new Dashboard[]
		{
			new() { DashboardId = "123", Title = "Test Dashboard" }
		};
		_ = _refitApi.Setup(x => x.GetAllAsync(null, cancellationToken))
			.ReturnsAsync(expectedResponse);

		// Act
		var result = await _sut.GetAllAsync(null, cancellationToken);

		// Assert
		_ = result.Should().BeEquivalentTo(expectedResponse);
		_refitApi.Verify(x => x.GetAllAsync(null, cancellationToken), Times.Once);
	}

	[Fact]
	public async Task GetByIdAsync_CallsApi_AndReturnsData()
	{
		// Arrange
		var dashboardId = "123";
		var cancellationToken = new CancellationToken();
		var expectedResponse = new Dashboard
		{
			DashboardId = dashboardId,
			Title = "Test Dashboard"
		};
		_ = _refitApi.Setup(x => x.GetByIdAsync(dashboardId, null, cancellationToken))
			.ReturnsAsync(expectedResponse);

		// Act
		var result = await _sut.GetByIdAsync(dashboardId, null, cancellationToken);

		// Assert
		_ = result.Should().Be(expectedResponse);
		_refitApi.Verify(x => x.GetByIdAsync(dashboardId, null, cancellationToken), Times.Once);
	}

	[Fact]
	public async Task CreateAsync_CallsApi_AndReturnsData()
	{
		// Arrange
		var cancellationToken = new CancellationToken();
		var request = new DashboardRequest { Title = "New Dashboard" };
		var expectedResponse = new Dashboard { DashboardId = "123", Title = "New Dashboard" };
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
		var dashboardId = "123";
		var request = new DashboardRequest { Title = "Updated Dashboard" };
		var cancellationToken = new CancellationToken();
		var expectedResponse = new Dashboard { DashboardId = dashboardId, Title = "Updated Dashboard" };
		_ = _refitApi.Setup(x => x.UpdateAsync(dashboardId, request, null, cancellationToken))
			.ReturnsAsync(expectedResponse);

		// Act
		var result = await _sut.UpdateAsync(dashboardId, request, null, cancellationToken);

		// Assert
		_ = result.Should().Be(expectedResponse);
		_refitApi.Verify(x => x.UpdateAsync(dashboardId, request, null, cancellationToken), Times.Once);
	}

	[Fact]
	public async Task DeleteAsync_CallsApi()
	{
		// Arrange
		var dashboardId = "123";
		var cancellationToken = new CancellationToken();
		_ = _refitApi.Setup(x => x.DeleteAsync(dashboardId, null, cancellationToken))
			.Returns(Task.CompletedTask);

		// Act
		await _sut.DeleteAsync(dashboardId, null, cancellationToken);

		// Assert
		_refitApi.Verify(x => x.DeleteAsync(dashboardId, null, cancellationToken), Times.Once);
	}
}
