using AwesomeAssertions;
using ThousandEyes.Api.Models.OpenTelemetry;
using OtelStream = ThousandEyes.Api.Models.OpenTelemetry.Stream;

namespace ThousandEyes.Api.Test;

[Collection("Integration Tests")]
[Trait("Category", "Integration")]
public class OpenTelemetryIntegrationTest(IntegrationTestFixture fixture) : TestBase(fixture)
{
	private const string GrpcUrl = "https://otel-collector.example.com:4317";
	private const string HttpUrl = "https://otel-collector.example.com:4318";

	private static OtelStream NewStream(EndpointType endpoint, string url, Signal signal = Signal.Metric) => new()
	{
		Type = StreamType.Opentelemetry,
		Signal = signal,
		EndpointType = endpoint,
		DataModelVersion = DataModelVersion.V1,
		StreamEndpointUrl = url,
		Enabled = true,
		TestMatch = [],
		TagMatch = []
	};

	private Task RunWhenAvailableAsync(Func<Task> body) => RunWhenAvailableAsync(
		"OpenTelemetry Streams",
		() => ThousandEyesClient.OpenTelemetry.Streams.GetAllAsync(aid: null, type: null, cancellationToken: CancellationToken),
		body);

	private Task<CreateStreamResponse> CreateAsync(OtelStream request) =>
		ThousandEyesClient.OpenTelemetry.Streams.CreateAsync(request, aid: null, cancellationToken: CancellationToken);

	private Task<GetStreamResponse> GetAsync(string id) =>
		ThousandEyesClient.OpenTelemetry.Streams.GetByIdAsync(id, aid: null, type: null, cancellationToken: CancellationToken);

	private Task DeleteAsync(string id) =>
		ThousandEyesClient.OpenTelemetry.Streams.DeleteAsync(id, aid: null, cancellationToken: CancellationToken);

	/// <summary>
	/// Creates a stream, runs the body against it and always deletes it afterwards
	/// </summary>
	private async Task WithStreamAsync(OtelStream request, Func<CreateStreamResponse, Task> body)
	{
		var created = await CreateAsync(request);
		try
		{
			await body(created);
		}
		finally
		{
			await IgnoreErrorsAsync(() => DeleteAsync(created.Id!));
		}
	}

	[Fact]
	public Task GetStreams_WithValidRequest_ReturnsStreams() => RunWhenAvailableAsync(async () =>
	{
		var result = await ThousandEyesClient.OpenTelemetry.Streams.GetAllAsync(aid: null, type: null, cancellationToken: CancellationToken);

		_ = result.Should().NotBeNull();
		_ = result.Streams.Should().NotBeNull();
	});

	[Fact]
	public Task GetStreams_WithTypeFilter_ReturnsFilteredStreams() => RunWhenAvailableAsync(async () =>
	{
		var result = await ThousandEyesClient.OpenTelemetry.Streams.GetAllAsync(aid: null, type: StreamType.Opentelemetry, cancellationToken: CancellationToken);

		_ = result.Should().NotBeNull();
		_ = result.Streams.Should().NotBeNull();
		if (result.Streams.Length > 0)
		{
			_ = result.Streams.Should().OnlyContain(s => s.Enabled.HasValue);
		}
	});

	[Fact]
	public Task CreateStream_OpenTelemetryGrpc_CreatesStream() => RunWhenAvailableAsync(() =>
		WithStreamAsync(NewStream(EndpointType.Grpc, GrpcUrl), result =>
		{
			_ = result.Should().NotBeNull();
			_ = result.Id.Should().NotBeNullOrEmpty();
			_ = result.Type.Should().Be(StreamType.Opentelemetry);
			_ = result.Signal.Should().Be(Signal.Metric);
			_ = result.EndpointType.Should().Be(EndpointType.Grpc);
			return Task.CompletedTask;
		}));

	[Fact]
	public Task CreateStream_SplunkHec_CreatesStream() => RunWhenAvailableAsync(() =>
		WithStreamAsync(NewSplunkStream(), result =>
		{
			_ = result.Should().NotBeNull();
			_ = result.Id.Should().NotBeNullOrEmpty();
			_ = result.Type.Should().Be(StreamType.SplunkHec);
			_ = result.Signal.Should().Be(Signal.Trace);
			_ = result.EndpointType.Should().Be(EndpointType.Http);
			return Task.CompletedTask;
		}));

	[Fact]
	public Task GetStream_WithValidId_ReturnsStreamDetails() => RunWhenAvailableAsync(() =>
		WithStreamAsync(NewStream(EndpointType.Http, HttpUrl), async created =>
		{
			var result = await GetAsync(created.Id!);

			_ = result.Should().NotBeNull();
			_ = result.Id.Should().Be(created.Id);
			_ = result.Type.Should().Be(StreamType.Opentelemetry);
			_ = result.StreamEndpointUrl.Should().NotBeNullOrEmpty();
		}));

	[Fact]
	public Task UpdateStream_WithValidRequest_UpdatesStream() => RunWhenAvailableAsync(() =>
		WithStreamAsync(NewStream(EndpointType.Grpc, GrpcUrl), async created =>
		{
			var updateRequest = new PutStream
			{
				StreamEndpointUrl = "https://otel-collector-updated.example.com:4317",
				Enabled = false,
				TestMatch = [],
				TagMatch = []
			};

			var result = await ThousandEyesClient.OpenTelemetry.Streams.UpdateAsync(
				created.Id!,
				updateRequest,
				aid: null,
				cancellationToken: CancellationToken);

			_ = result.Should().NotBeNull();
			_ = result.Id.Should().Be(created.Id);
			_ = result.StreamEndpointUrl.Should().Be(updateRequest.StreamEndpointUrl);
			_ = result.Enabled.Should().Be(false);
		}));

	[Fact]
	public Task DeleteStream_WithValidId_DeletesStream() => RunWhenAvailableAsync(async () =>
	{
		var created = await CreateAsync(NewStream(EndpointType.Http, HttpUrl, Signal.Trace));

		await DeleteAsync(created.Id!);

		await RunIfAvailableAsync(async () =>
		{
			_ = await GetAsync(created.Id!);
			throw new InvalidOperationException("Stream should have been deleted");
		});
	});

	[Fact]
	public Task GetStreamStatus_AfterCreation_ValidatesStatus() => RunWhenAvailableAsync(() =>
		WithStreamAsync(NewStream(EndpointType.Grpc, GrpcUrl), async created =>
		{
			var result = await GetAsync(created.Id!);

			_ = result.Should().NotBeNull();
			_ = result.Id.Should().Be(created.Id);
			if (result.StreamStatus != null)
			{
				_ = result.StreamStatus.Status.Should().BeDefined();
			}
		}));

	private static OtelStream NewSplunkStream() => new()
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
}
