using AwesomeAssertions;

namespace ThousandEyes.Api.Test;

[Collection("Integration Tests")]
public class EventDetectionModuleTests(IntegrationTestFixture fixture) : TestBase(fixture)
{
	[Fact]
	public async Task GetEvents_WithTimeWindow_ReturnsEvents()
	{
		// Act
		var result = await ThousandEyesClient.EventDetection.Events.GetAllAsync(
			aid: null,
			window: "7d",
			startDate: null,
			endDate: null,
			max: null,
			cursor: null,
			CancellationToken);

		// Assert
		_ = result.Should().NotBeNull();
		_ = result.EventsList.Should().NotBeNull();
		// Note: Events list may be empty if no events in timeframe
	}

	[Fact]
	public async Task GetEvents_WithDateRange_ReturnsEvents()
	{
		// Act
		var result = await ThousandEyesClient.EventDetection.Events.GetAllAsync(
			aid: null,
			window: null,
			startDate: DateTime.UtcNow.AddDays(-7),
			endDate: DateTime.UtcNow,
			max: null,
			cursor: null,
			CancellationToken);

		// Assert
		_ = result.Should().NotBeNull();
		_ = result.EventsList.Should().NotBeNull();
	}

	[Fact]
	public async Task GetEvents_WithMaxLimit_ReturnsLimitedEvents()
	{
		// Act
		var result = await ThousandEyesClient.EventDetection.Events.GetAllAsync(
			aid: null,
			window: "30d",
			startDate: null,
			endDate: null,
			max: 5,
			cursor: null,
			CancellationToken);

		// Assert
		_ = result.Should().NotBeNull();
		_ = result.EventsList.Should().NotBeNull();
		if (result.EventsList.Length > 0)
		{
			_ = result.EventsList.Length.Should().BeLessThanOrEqualTo(5);
		}
	}

	[Fact]
	public async Task GetEventById_WithValidId_ReturnsEventDetails()
	{
		// First, get a list of events to find a valid event ID
		var events = await ThousandEyesClient.EventDetection.Events.GetAllAsync(
			aid: null,
			window: "30d",
			startDate: null,
			endDate: null,
			max: 1,
			cursor: null,
			CancellationToken);

		if (events.EventsList.Length == 0)
		{
			// Skip test if no events found
			return;
		}

		var testEventId = events.EventsList[0].Id;

		// Act
		var result = await ThousandEyesClient.EventDetection.Events.GetByIdAsync(
			testEventId,
			aid: null,
			CancellationToken);

		// Assert
		_ = result.Should().NotBeNull();
		_ = result.Id.Should().Be(testEventId);
		_ = result.TypeName.Should().NotBeNullOrWhiteSpace();
	}

	[Fact]
	public async Task GetEvents_ResponseHasLinks()
	{
		// Act
		var result = await ThousandEyesClient.EventDetection.Events.GetAllAsync(
			aid: null,
			window: "7d",
			startDate: null,
			endDate: null,
			max: null,
			cursor: null,
			CancellationToken);

		// Assert
		_ = result.Links.Should().NotBeNull();
		_ = result.Links?.Self.Should().NotBeNull();
		_ = result.Links?.Self?.Href.Should().NotBeNullOrWhiteSpace();
	}

	[Fact]
	public async Task GetEvents_ValidatesEventProperties()
	{
		// Act
		var result = await ThousandEyesClient.EventDetection.Events.GetAllAsync(
			aid: null,
			window: "30d",
			startDate: null,
			endDate: null,
			max: null,
			cursor: null,
			CancellationToken);

		// Assert
		if (result.EventsList.Length > 0)
		{
			var firstEvent = result.EventsList[0];
			_ = firstEvent.Id.Should().NotBeNullOrWhiteSpace();
			_ = firstEvent.TypeName.Should().NotBeNullOrWhiteSpace();
			_ = firstEvent.StateValue.Should().NotBeNull();
			_ = firstEvent.SeverityValue.Should().NotBeNull();
		}
	}
}
