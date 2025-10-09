using AwesomeAssertions;
using Moq;
using ThousandEyes.Api.Implementations.TestSnapshots;
using ThousandEyes.Api.Models.TestSnapshots;
using ThousandEyes.Api.Refit.TestSnapshots;

namespace ThousandEyes.Api.Test.UnitTests.TestSnapshots;

public class TestSnapshotsImplTests
{
	private readonly Mock<ITestSnapshotsRefitApi> _refitApi;
	private readonly TestSnapshotsImpl _sut;

	public TestSnapshotsImplTests()
	{
		_refitApi = new Mock<ITestSnapshotsRefitApi>();
		_sut = new TestSnapshotsImpl(_refitApi.Object);
	}

	[Fact]
	public async Task CreateAsync_CallsApi_AndReturnsData()
	{
		// Arrange
		var testId = "123";
		var request = new SnapshotRequest
		{
			DisplayName = "Test Snapshot",
			StartDate = DateTime.UtcNow.AddHours(-1),
			EndDate = DateTime.UtcNow
		};
		var cancellationToken = new CancellationToken();
		var expectedResponse = new SnapshotResponse
		{
			Id = "snapshot-123"
		};
		_ = _refitApi.Setup(x => x.CreateAsync(testId, request, null, cancellationToken))
			.ReturnsAsync(expectedResponse);

		// Act
		var result = await _sut.CreateAsync(testId, request, null, cancellationToken);

		// Assert
		_ = result.Should().Be(expectedResponse);
		_refitApi.Verify(x => x.CreateAsync(testId, request, null, cancellationToken), Times.Once);
	}
}
