using AwesomeAssertions;
using Moq;
using ThousandEyes.Api.Interfaces;
using ThousandEyes.Api.Models.Dashboards;

namespace ThousandEyes.Api.Test.UnitTests.Dashboards;

public class DashboardFiltersApiTests
{
	private readonly Mock<IDashboardFiltersRefitApi> _refitApi;
	private readonly DashboardFiltersApi _sut;

	public DashboardFiltersApiTests()
	{
		_refitApi = new Mock<IDashboardFiltersRefitApi>();
		_sut = new DashboardFiltersApi(_refitApi.Object);
	}

	[Fact]
	public async Task GetAllAsync_CallsApi_AndReturnsData()
	{
		// Arrange
		var cancellationToken = new CancellationToken();
		var expectedResponse = new DashboardFilters
		{
			Filters = [
				new DashboardFilterDetails { Id = "123", Name = "Test Filter" }
			]
		};
		_ = _refitApi.Setup(x => x.GetAllAsync(null, null, cancellationToken))
			.ReturnsAsync(expectedResponse);

		// Act
		var result = await _sut.GetAllAsync(null, null, cancellationToken);

		// Assert
		_ = result.Should().Be(expectedResponse);
		_refitApi.Verify(x => x.GetAllAsync(null, null, cancellationToken), Times.Once);
	}

	[Fact]
	public async Task GetByIdAsync_CallsApi_AndReturnsData()
	{
		// Arrange
		var id = "123";
		var cancellationToken = new CancellationToken();
		var expectedResponse = new DashboardFilterDetails
		{
			Id = id,
			Name = "Test Filter"
		};
		_ = _refitApi.Setup(x => x.GetByIdAsync(id, null, cancellationToken))
			.ReturnsAsync(expectedResponse);

		// Act
		var result = await _sut.GetByIdAsync(id, null, cancellationToken);

		// Assert
		_ = result.Should().Be(expectedResponse);
		_refitApi.Verify(x => x.GetByIdAsync(id, null, cancellationToken), Times.Once);
	}

	[Fact]
	public async Task CreateAsync_CallsApi_AndReturnsData()
	{
		// Arrange
		var cancellationToken = new CancellationToken();
		var request = new DashboardFilterRequest { Name = "New Filter" };
		var expectedResponse = new DashboardFilterDetails { Id = "123", Name = "New Filter" };
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
		var id = "123";
		var request = new DashboardFilterRequest { Name = "Updated Filter" };
		var cancellationToken = new CancellationToken();
		var expectedResponse = new DashboardFilterDetails { Id = id, Name = "Updated Filter" };
		_ = _refitApi.Setup(x => x.UpdateAsync(id, request, null, cancellationToken))
			.ReturnsAsync(expectedResponse);

		// Act
		var result = await _sut.UpdateAsync(id, request, null, cancellationToken);

		// Assert
		_ = result.Should().Be(expectedResponse);
		_refitApi.Verify(x => x.UpdateAsync(id, request, null, cancellationToken), Times.Once);
	}

	[Fact]
	public async Task DeleteAsync_CallsApi()
	{
		// Arrange
		var id = "123";
		var cancellationToken = new CancellationToken();
		_ = _refitApi.Setup(x => x.DeleteAsync(id, null, cancellationToken))
			.Returns(Task.CompletedTask);

		// Act
		await _sut.DeleteAsync(id, null, cancellationToken);

		// Assert
		_refitApi.Verify(x => x.DeleteAsync(id, null, cancellationToken), Times.Once);
	}
}
