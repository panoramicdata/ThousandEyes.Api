using System.Text.Json.Serialization;

namespace ThousandEyes.Api.Models.OpenTelemetry;

/// <summary>
/// The type of data stream to configure
/// </summary>
[JsonConverter(typeof(JsonStringEnumConverter))]
public enum StreamType
{
    /// <summary>
    /// OpenTelemetry stream
    /// </summary>
    [JsonPropertyName("opentelemetry")]
    Opentelemetry,

    /// <summary>
    /// Splunk HEC stream
    /// </summary>
    [JsonPropertyName("splunk-hec")]
    SplunkHec
}