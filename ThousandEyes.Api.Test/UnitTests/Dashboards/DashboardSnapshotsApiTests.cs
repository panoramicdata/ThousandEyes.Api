using AwesomeAssertions;
using Moq;
using ThousandEyes.Api.Interfaces;
using ThousandEyes.Api.Models.Dashboards;

namespace ThousandEyes.Api.Test.UnitTests.Dashboards;

public class DashboardSnapshotsApiTests
{
	private readonly Mock<IDashboardSnapshotsRefitApi> _refitApi;
	private readonly DashboardSnapshotsApi _sut;

	public DashboardSnapshotsApiTests()
	{
		_refitApi = new Mock<IDashboardSnapshotsRefitApi>();
		_sut = new DashboardSnapshotsApi(_refitApi.Object);
	}

	[Fact]
	public async Task GetAllAsync_CallsApi_AndReturnsData()
	{
		// Arrange
		var cancellationToken = new CancellationToken();
		var expectedResponse = new DashboardSnapshotsPage
		{
			DashboardSnapshots = [
				new DashboardSnapshot { SnapshotId = "123", SnapshotName = "Test Snapshot" }
			]
		};
		_ = _refitApi.Setup(x => x.GetAllAsync(null, null, null, cancellationToken))
			.ReturnsAsync(expectedResponse);

		// Act
		var result = await _sut.GetAllAsync(null, null, null, cancellationToken);

		// Assert
		_ = result.Should().Be(expectedResponse);
		_refitApi.Verify(x => x.GetAllAsync(null, null, null, cancellationToken), Times.Once);
	}

	[Fact]
	public async Task GetByIdAsync_CallsApi_AndReturnsData()
	{
		// Arrange
		var snapshotId = "123";
		var cancellationToken = new CancellationToken();
		var expectedResponse = new DashboardSnapshot
		{
			SnapshotId = snapshotId,
			SnapshotName = "Test Snapshot"
		};
		_ = _refitApi.Setup(x => x.GetByIdAsync(snapshotId, null, cancellationToken))
			.ReturnsAsync(expectedResponse);

		// Act
		var result = await _sut.GetByIdAsync(snapshotId, null, cancellationToken);

		// Assert
		_ = result.Should().Be(expectedResponse);
		_refitApi.Verify(x => x.GetByIdAsync(snapshotId, null, cancellationToken), Times.Once);
	}

	[Fact]
	public async Task CreateAsync_CallsApi_AndReturnsData()
	{
		// Arrange
		var cancellationToken = new CancellationToken();
		var request = new CreateDashboardSnapshotRequest
		{
			DashboardId = "dash-123",
			DisplayName = "Test Snapshot"
		};
		var expectedResponse = new DashboardSnapshotResponse { SnapshotId = "snap-123" };
		_ = _refitApi.Setup(x => x.CreateAsync(request, null, cancellationToken))
			.ReturnsAsync(expectedResponse);

		// Act
		var result = await _sut.CreateAsync(request, null, cancellationToken);

		// Assert
		_ = result.Should().Be(expectedResponse);
		_refitApi.Verify(x => x.CreateAsync(request, null, cancellationToken), Times.Once);
	}

	[Fact]
	public async Task UpdateExpirationAsync_CallsApi()
	{
		// Arrange
		var snapshotId = "123";
		var request = new UpdateSnapshotExpirationRequest
		{
			SnapshotExpirationDate = DateTime.UtcNow.AddDays(30)
		};
		var cancellationToken = new CancellationToken();
		_ = _refitApi.Setup(x => x.UpdateExpirationAsync(snapshotId, request, null, cancellationToken))
			.Returns(Task.CompletedTask);

		// Act
		await _sut.UpdateExpirationAsync(snapshotId, request, null, cancellationToken);

		// Assert
		_refitApi.Verify(x => x.UpdateExpirationAsync(snapshotId, request, null, cancellationToken), Times.Once);
	}

	[Fact]
	public async Task DeleteAsync_CallsApi()
	{
		// Arrange
		var snapshotId = "123";
		var cancellationToken = new CancellationToken();
		_ = _refitApi.Setup(x => x.DeleteAsync(snapshotId, null, cancellationToken))
			.Returns(Task.CompletedTask);

		// Act
		await _sut.DeleteAsync(snapshotId, null, cancellationToken);

		// Assert
		_refitApi.Verify(x => x.DeleteAsync(snapshotId, null, cancellationToken), Times.Once);
	}
}
