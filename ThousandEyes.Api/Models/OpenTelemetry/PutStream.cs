using System.Text.Json.Serialization;

namespace ThousandEyes.Api.Models.OpenTelemetry;

/// <summary>
/// Request to update a stream
/// </summary>
public class PutStream
{
    /// <summary>
    /// Custom headers
    /// </summary>
    [JsonPropertyName("customHeaders")]
    public Dictionary<string, string>? CustomHeaders { get; set; }

    /// <summary>
    /// The URL ThousandEyes sends data stream to
    /// </summary>
    [JsonPropertyName("streamEndpointUrl")]
    public string? StreamEndpointUrl { get; set; }

    /// <summary>
    /// A collection of tags that determine what tests are included in the data stream
    /// </summary>
    [JsonPropertyName("tagMatch")]
    public TagMatch[] TagMatch { get; set; } = [];

    /// <summary>
    /// A collection of tests to be included in the data stream
    /// </summary>
    [JsonPropertyName("testMatch")]
    public TestMatch[] TestMatch { get; set; } = [];

    /// <summary>
    /// Flag to enable or disable the stream integration
    /// </summary>
    [JsonPropertyName("enabled")]
    public bool? Enabled { get; set; }

    /// <summary>
    /// Provides the ability to filter data points based on the specified test types
    /// </summary>
    [JsonPropertyName("filters")]
    public Filters? Filters { get; set; }

    /// <summary>
    /// Capability to set exporter configuration
    /// </summary>
    [JsonPropertyName("exporterConfig")]
    public ExporterConfig? ExporterConfig { get; set; }
}