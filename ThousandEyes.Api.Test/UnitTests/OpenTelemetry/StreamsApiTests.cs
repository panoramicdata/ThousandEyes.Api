using AwesomeAssertions;
using Moq;
using ThousandEyes.Api.Interfaces;
using ThousandEyes.Api.Models.OpenTelemetry;
using Stream = ThousandEyes.Api.Models.OpenTelemetry.Stream;

namespace ThousandEyes.Api.Test.UnitTests.OpenTelemetry;

public class StreamsApiTests
{
	private readonly Mock<IStreamsRefitApi> _refitApi;
	private readonly StreamsApi _sut;

	public StreamsApiTests()
	{
		_refitApi = new Mock<IStreamsRefitApi>();
		_sut = new StreamsApi(_refitApi.Object);
	}

	[Fact]
	public async Task GetAllAsync_WithoutFilters_CallsApi_AndReturnsData()
	{
		// Arrange
		var cancellationToken = new CancellationToken();
		var expectedResponse = new StreamCollection
		{
			Streams = [
				new GetStreamResponse { Enabled = true }
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
	public async Task GetAllAsync_WithAccountGroupId_CallsApi_AndReturnsData()
	{
		// Arrange
		var aid = "12345";
		var cancellationToken = new CancellationToken();
		var expectedResponse = new StreamCollection
		{
			Streams = [
				new GetStreamResponse { Enabled = true }
			]
		};
		_ = _refitApi.Setup(x => x.GetAllAsync(aid, null, cancellationToken))
			.ReturnsAsync(expectedResponse);

		// Act
		var result = await _sut.GetAllAsync(aid, null, cancellationToken);

		// Assert
		_ = result.Should().Be(expectedResponse);
		_refitApi.Verify(x => x.GetAllAsync(aid, null, cancellationToken), Times.Once);
	}

	[Fact]
	public async Task GetAllAsync_WithStreamType_CallsApi_AndReturnsData()
	{
		// Arrange
		var streamType = StreamType.Opentelemetry;
		var cancellationToken = new CancellationToken();
		var expectedResponse = new StreamCollection
		{
			Streams = [
				new GetStreamResponse { Enabled = true, Type = streamType }
			]
		};
		_ = _refitApi.Setup(x => x.GetAllAsync(null, streamType, cancellationToken))
			.ReturnsAsync(expectedResponse);

		// Act
		var result = await _sut.GetAllAsync(null, streamType, cancellationToken);

		// Assert
		_ = result.Should().Be(expectedResponse);
		_refitApi.Verify(x => x.GetAllAsync(null, streamType, cancellationToken), Times.Once);
	}

	[Fact]
	public async Task GetByIdAsync_CallsApi_AndReturnsData()
	{
		// Arrange
		var streamId = "stream-123";
		var cancellationToken = new CancellationToken();
		var expectedResponse = new GetStreamResponse
		{
			Enabled = true
		};
		_ = _refitApi.Setup(x => x.GetByIdAsync(streamId, null, null, cancellationToken))
			.ReturnsAsync(expectedResponse);

		// Act
		var result = await _sut.GetByIdAsync(streamId, null, null, cancellationToken);

		// Assert
		_ = result.Should().Be(expectedResponse);
		_refitApi.Verify(x => x.GetByIdAsync(streamId, null, null, cancellationToken), Times.Once);
	}

	[Fact]
	public async Task GetByIdAsync_WithFilters_CallsApi_AndReturnsData()
	{
		// Arrange
		var streamId = "stream-123";
		var aid = "12345";
		var streamType = StreamType.Opentelemetry;
		var cancellationToken = new CancellationToken();
		var expectedResponse = new GetStreamResponse
		{
			Enabled = true,
			Type = streamType
		};
		_ = _refitApi.Setup(x => x.GetByIdAsync(streamId, aid, streamType, cancellationToken))
			.ReturnsAsync(expectedResponse);

		// Act
		var result = await _sut.GetByIdAsync(streamId, aid, streamType, cancellationToken);

		// Assert
		_ = result.Should().Be(expectedResponse);
		_refitApi.Verify(x => x.GetByIdAsync(streamId, aid, streamType, cancellationToken), Times.Once);
	}

	[Fact]
	public async Task CreateAsync_CallsApi_AndReturnsData()
	{
		// Arrange
		var cancellationToken = new CancellationToken();
		var request = new Stream
		{
			Type = StreamType.Opentelemetry,
			Signal = Signal.Metric,
			EndpointType = EndpointType.Grpc,
			DataModelVersion = DataModelVersion.V1
		};
		var expectedResponse = new CreateStreamResponse
		{
			Enabled = true
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
	public async Task CreateAsync_WithAccountGroupId_CallsApi_AndReturnsData()
	{
		// Arrange
		var aid = "12345";
		var cancellationToken = new CancellationToken();
		var request = new Stream
		{
			Type = StreamType.Opentelemetry
		};
		var expectedResponse = new CreateStreamResponse
		{
			Enabled = true
		};
		_ = _refitApi.Setup(x => x.CreateAsync(request, aid, cancellationToken))
			.ReturnsAsync(expectedResponse);

		// Act
		var result = await _sut.CreateAsync(request, aid, cancellationToken);

		// Assert
		_ = result.Should().Be(expectedResponse);
		_refitApi.Verify(x => x.CreateAsync(request, aid, cancellationToken), Times.Once);
	}

	[Fact]
	public async Task UpdateAsync_CallsApi_AndReturnsData()
	{
		// Arrange
		var streamId = "stream-123";
		var cancellationToken = new CancellationToken();
		var request = new PutStream
		{
			Enabled = false
		};
		var expectedResponse = new GetStreamResponse
		{
			Enabled = false
		};
		_ = _refitApi.Setup(x => x.UpdateAsync(streamId, request, null, cancellationToken))
			.ReturnsAsync(expectedResponse);

		// Act
		var result = await _sut.UpdateAsync(streamId, request, null, cancellationToken);

		// Assert
		_ = result.Should().Be(expectedResponse);
		_refitApi.Verify(x => x.UpdateAsync(streamId, request, null, cancellationToken), Times.Once);
	}

	[Fact]
	public async Task UpdateAsync_WithAccountGroupId_CallsApi_AndReturnsData()
	{
		// Arrange
		var streamId = "stream-123";
		var aid = "12345";
		var cancellationToken = new CancellationToken();
		var request = new PutStream
		{
			Enabled = false
		};
		var expectedResponse = new GetStreamResponse
		{
			Enabled = false
		};
		_ = _refitApi.Setup(x => x.UpdateAsync(streamId, request, aid, cancellationToken))
			.ReturnsAsync(expectedResponse);

		// Act
		var result = await _sut.UpdateAsync(streamId, request, aid, cancellationToken);

		// Assert
		_ = result.Should().Be(expectedResponse);
		_refitApi.Verify(x => x.UpdateAsync(streamId, request, aid, cancellationToken), Times.Once);
	}

	[Fact]
	public async Task DeleteAsync_CallsApi()
	{
		// Arrange
		var streamId = "stream-123";
		var cancellationToken = new CancellationToken();
		_ = _refitApi.Setup(x => x.DeleteAsync(streamId, null, cancellationToken))
			.Returns(Task.CompletedTask);

		// Act
		await _sut.DeleteAsync(streamId, null, cancellationToken);

		// Assert
		_refitApi.Verify(x => x.DeleteAsync(streamId, null, cancellationToken), Times.Once);
	}

	[Fact]
	public async Task DeleteAsync_WithAccountGroupId_CallsApi()
	{
		// Arrange
		var streamId = "stream-123";
		var aid = "12345";
		var cancellationToken = new CancellationToken();
		_ = _refitApi.Setup(x => x.DeleteAsync(streamId, aid, cancellationToken))
			.Returns(Task.CompletedTask);

		// Act
		await _sut.DeleteAsync(streamId, aid, cancellationToken);

		// Assert
		_refitApi.Verify(x => x.DeleteAsync(streamId, aid, cancellationToken), Times.Once);
	}
}
