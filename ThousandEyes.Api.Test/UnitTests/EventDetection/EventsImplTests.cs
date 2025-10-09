using AwesomeAssertions;
using Moq;
using ThousandEyes.Api.Implementations.EventDetection;
using ThousandEyes.Api.Models.EventDetection;
using ThousandEyes.Api.Refit.EventDetection;

namespace ThousandEyes.Api.Test.UnitTests.EventDetection;

public class EventsImplTests
{
	private readonly Mock<IEventsRefitApi> _refitApi;
	private readonly EventsImpl _sut;

	public EventsImplTests()
	{
		_refitApi = new Mock<IEventsRefitApi>();
		_sut = new EventsImpl(_refitApi.Object);
	}

	[Fact]
	public async Task GetAllAsync_CallsApi_AndReturnsData()
	{
		// Arrange
		var cancellationToken = new CancellationToken();
		var expectedResponse = new Events
		{
			EventsList = [
				new DetectedEvent { Id = "123" }
			]
		};
		_ = _refitApi.Setup(x => x.GetAllAsync(null, "7d", null, null, null, null, cancellationToken))
			.ReturnsAsync(expectedResponse);

		// Act
		var result = await _sut.GetAllAsync(null, "7d", null, null, null, null, cancellationToken);

		// Assert
		_ = result.Should().Be(expectedResponse);
		_refitApi.Verify(x => x.GetAllAsync(null, "7d", null, null, null, null, cancellationToken), Times.Once);
	}

	[Fact]
	public async Task GetByIdAsync_CallsApi_AndReturnsData()
	{
		// Arrange
		var eventId = "123";
		var cancellationToken = new CancellationToken();
		var expectedResponse = new EventDetail
		{
			Id = eventId
		};
		_ = _refitApi.Setup(x => x.GetByIdAsync(eventId, null, cancellationToken))
			.ReturnsAsync(expectedResponse);

		// Act
		var result = await _sut.GetByIdAsync(eventId, null, cancellationToken);

		// Assert
		_ = result.Should().Be(expectedResponse);
		_refitApi.Verify(x => x.GetByIdAsync(eventId, null, cancellationToken), Times.Once);
	}
}
