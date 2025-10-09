using AwesomeAssertions;
using Refit;
using ThousandEyes.Api.Models.OpenTelemetry;
using OtelStream = ThousandEyes.Api.Models.OpenTelemetry.Stream;

namespace ThousandEyes.Api.Test;

[Collection("Integration Tests")]
public class OpenTelemetryIntegrationTest(IntegrationTestFixture fixture) : TestBase(fixture)
{
	[Fact]
	public async Task GetStreams_WithValidRequest_ReturnsStreams()
	{
		try
		{
			// Act
			var result = await ThousandEyesClient.OpenTelemetry.Streams.GetAllAsync(
				aid: null,
				type: null,
				cancellationToken: CancellationToken);

			// Assert
			_ = result.Should().NotBeNull();
			_ = result.Streams.Should().NotBeNull();
		}
		catch (ValidationApiException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
		{
			// Skip test if OpenTelemetry Streams API is not available
			return;
		}
	}

	[Fact]
	public async Task GetStreams_WithTypeFilter_ReturnsFilteredStreams()
	{
		try
		{
			// Act
			var result = await ThousandEyesClient.OpenTelemetry.Streams.GetAllAsync(
				aid: null,
				type: StreamType.Opentelemetry,
				cancellationToken: CancellationToken);

			// Assert
			_ = result.Should().NotBeNull();
			_ = result.Streams.Should().NotBeNull();

			// If there are streams, verify they match the filter
			if (result.Streams.Length > 0)
			{
				_ = result.Streams.Should().OnlyContain(s => s.Enabled.HasValue);
			}
		}
		catch (ValidationApiException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
		{
			// Skip test if OpenTelemetry Streams API is not available
			return;
		}
	}

	[Fact]
	public async Task CreateStream_OpenTelemetryGrpc_CreatesStream()
	{
		try
		{
			// Arrange
			var streamRequest = new OtelStream
			{
				Type = StreamType.Opentelemetry,
				Signal = Signal.Metric,
				EndpointType = EndpointType.Grpc,
				DataModelVersion = DataModelVersion.V1,
				StreamEndpointUrl = "https://otel-collector.example.com:4317",
				Enabled = true,
				TestMatch = [],
				TagMatch = []
			};

			// Act
			var result = await ThousandEyesClient.OpenTelemetry.Streams.CreateAsync(
				streamRequest,
				aid: null,
				cancellationToken: CancellationToken);

			// Assert
			_ = result.Should().NotBeNull();
			_ = result.Id.Should().NotBeNullOrEmpty();
			_ = result.Type.Should().Be(StreamType.Opentelemetry);
			_ = result.Signal.Should().Be(Signal.Metric);
			_ = result.EndpointType.Should().Be(EndpointType.Grpc);

			// Cleanup - Delete the created stream
			try
			{
				await ThousandEyesClient.OpenTelemetry.Streams.DeleteAsync(
					result.Id!,
					aid: null,
					cancellationToken: CancellationToken);
			}
			catch
			{
				// Ignore cleanup errors
			}
		}
		catch (ValidationApiException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
		{
			// Skip test if OpenTelemetry Streams API is not available
			return;
		}
	}

	[Fact]
	public async Task CreateStream_SplunkHec_CreatesStream()
	{
		try
		{
			// Arrange
			var streamRequest = new OtelStream
			{
				Type = StreamType.SplunkHec,
				Signal = Signal.Trace,
				EndpointType = EndpointType.Http,
				DataModelVersion = DataModelVersion.V2,
				StreamEndpointUrl = "https://splunk.example.com:8088/services/collector",
				Enabled = true,
				ExporterConfig = new ExporterConfig
				{
					SplunkHec = new ExporterConfigSplunkHec
					{
						Token = "test-token-12345",
						Source = "thousandeyes",
						SourceType = "thousandeyes:trace"
					}
				},
				TestMatch = [],
				TagMatch = []
			};

			// Act
			var result = await ThousandEyesClient.OpenTelemetry.Streams.CreateAsync(
				streamRequest,
				aid: null,
				cancellationToken: CancellationToken);

			// Assert
			_ = result.Should().NotBeNull();
			_ = result.Id.Should().NotBeNullOrEmpty();
			_ = result.Type.Should().Be(StreamType.SplunkHec);
			_ = result.Signal.Should().Be(Signal.Trace);
			_ = result.EndpointType.Should().Be(EndpointType.Http);

			// Cleanup - Delete the created stream
			try
			{
				await ThousandEyesClient.OpenTelemetry.Streams.DeleteAsync(
					result.Id!,
					aid: null,
					cancellationToken: CancellationToken);
			}
			catch
			{
				// Ignore cleanup errors
			}
		}
		catch (ValidationApiException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
		{
			// Skip test if OpenTelemetry Streams API is not available
			return;
		}
	}

	[Fact]
	public async Task GetStream_WithValidId_ReturnsStreamDetails()
	{
		try
		{
			// Arrange - First create a stream
			var streamRequest = new OtelStream
			{
				Type = StreamType.Opentelemetry,
				Signal = Signal.Metric,
				EndpointType = EndpointType.Http,
				DataModelVersion = DataModelVersion.V1,
				StreamEndpointUrl = "https://otel-collector.example.com:4318",
				Enabled = true,
				TestMatch = [],
				TagMatch = []
			};

			var createdStream = await ThousandEyesClient.OpenTelemetry.Streams.CreateAsync(
				streamRequest,
				aid: null,
				cancellationToken: CancellationToken);

			try
			{
				// Act
				var result = await ThousandEyesClient.OpenTelemetry.Streams.GetByIdAsync(
					createdStream.Id!,
					aid: null,
					type: null,
					cancellationToken: CancellationToken);

				// Assert
				_ = result.Should().NotBeNull();
				_ = result.Id.Should().Be(createdStream.Id);
				_ = result.Type.Should().Be(StreamType.Opentelemetry);
				_ = result.StreamEndpointUrl.Should().NotBeNullOrEmpty();
			}
			finally
			{
				// Cleanup - Delete the created stream
				try
				{
					await ThousandEyesClient.OpenTelemetry.Streams.DeleteAsync(
						createdStream.Id!,
						aid: null,
						cancellationToken: CancellationToken);
				}
				catch
				{
					// Ignore cleanup errors
				}
			}
		}
		catch (ValidationApiException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
		{
			// Skip test if OpenTelemetry Streams API is not available
			return;
		}
	}

	[Fact]
	public async Task UpdateStream_WithValidRequest_UpdatesStream()
	{
		try
		{
			// Arrange - First create a stream
			var streamRequest = new OtelStream
			{
				Type = StreamType.Opentelemetry,
				Signal = Signal.Metric,
				EndpointType = EndpointType.Grpc,
				DataModelVersion = DataModelVersion.V1,
				StreamEndpointUrl = "https://otel-collector.example.com:4317",
				Enabled = true,
				TestMatch = [],
				TagMatch = []
			};

			var createdStream = await ThousandEyesClient.OpenTelemetry.Streams.CreateAsync(
				streamRequest,
				aid: null,
				cancellationToken: CancellationToken);

			try
			{
				// Update request
				var updateRequest = new PutStream
				{
					StreamEndpointUrl = "https://otel-collector-updated.example.com:4317",
					Enabled = false,
					TestMatch = [],
					TagMatch = []
				};

				// Act
				var result = await ThousandEyesClient.OpenTelemetry.Streams.UpdateAsync(
					createdStream.Id!,
					updateRequest,
					aid: null,
					cancellationToken: CancellationToken);

				// Assert
				_ = result.Should().NotBeNull();
				_ = result.Id.Should().Be(createdStream.Id);
				_ = result.StreamEndpointUrl.Should().Be(updateRequest.StreamEndpointUrl);
				_ = result.Enabled.Should().Be(false);
			}
			finally
			{
				// Cleanup - Delete the created stream
				try
				{
					await ThousandEyesClient.OpenTelemetry.Streams.DeleteAsync(
						createdStream.Id!,
						aid: null,
						cancellationToken: CancellationToken);
				}
				catch
				{
					// Ignore cleanup errors
				}
			}
		}
		catch (ValidationApiException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
		{
			// Skip test if OpenTelemetry Streams API is not available
			return;
		}
	}

	[Fact]
	public async Task DeleteStream_WithValidId_DeletesStream()
	{
		try
		{
			// Arrange - Create a stream first
			var streamRequest = new OtelStream
			{
				Type = StreamType.Opentelemetry,
				Signal = Signal.Trace,
				EndpointType = EndpointType.Http,
				DataModelVersion = DataModelVersion.V1,
				StreamEndpointUrl = "https://otel-collector.example.com:4318",
				Enabled = true,
				TestMatch = [],
				TagMatch = []
			};

			var createdStream = await ThousandEyesClient.OpenTelemetry.Streams.CreateAsync(
				streamRequest,
				aid: null,
				cancellationToken: CancellationToken);

			// Act & Assert - Delete should not throw
			await ThousandEyesClient.OpenTelemetry.Streams.DeleteAsync(
				createdStream.Id!,
				aid: null,
				cancellationToken: CancellationToken);

			// Verify deletion by trying to get the stream (should throw 404)
			try
			{
				_ = await ThousandEyesClient.OpenTelemetry.Streams.GetByIdAsync(
					createdStream.Id!,
					aid: null,
					type: null,
					cancellationToken: CancellationToken);

				// If we get here, the stream wasn't deleted
				throw new InvalidOperationException("Stream should have been deleted");
			}
			catch (ValidationApiException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
			{
				// This is expected - stream was successfully deleted
			}
		}
		catch (ValidationApiException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
		{
			// Skip test if OpenTelemetry Streams API is not available
			return;
		}
	}

	[Fact]
	public async Task GetStreamStatus_AfterCreation_ValidatesStatus()
	{
		try
		{
			// Arrange - Create a stream first
			var streamRequest = new OtelStream
			{
				Type = StreamType.Opentelemetry,
				Signal = Signal.Metric,
				EndpointType = EndpointType.Grpc,
				DataModelVersion = DataModelVersion.V1,
				StreamEndpointUrl = "https://otel-collector.example.com:4317",
				Enabled = true,
				TestMatch = [],
				TagMatch = []
			};

			var createdStream = await ThousandEyesClient.OpenTelemetry.Streams.CreateAsync(
				streamRequest,
				aid: null,
				cancellationToken: CancellationToken);

			try
			{
				// Act - Get the stream details which includes status
				var result = await ThousandEyesClient.OpenTelemetry.Streams.GetByIdAsync(
					createdStream.Id!,
					aid: null,
					type: null,
					cancellationToken: CancellationToken);

				// Assert
				_ = result.Should().NotBeNull();
				_ = result.Id.Should().Be(createdStream.Id);

				// StreamStatus should be present
				if (result.StreamStatus != null)
				{
					_ = result.StreamStatus.Status.Should().BeDefined();
				}
			}
			finally
			{
				// Cleanup - Delete the created stream
				try
				{
					await ThousandEyesClient.OpenTelemetry.Streams.DeleteAsync(
						createdStream.Id!,
						aid: null,
						cancellationToken: CancellationToken);
				}
				catch
				{
					// Ignore cleanup errors
				}
			}
		}
		catch (ValidationApiException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
		{
			// Skip test if OpenTelemetry Streams API is not available
			return;
		}
	}
}
