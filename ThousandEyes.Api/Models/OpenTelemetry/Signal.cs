using System.Text.Json.Serialization;

namespace ThousandEyes.Api.Models.OpenTelemetry;

/// <summary>
/// The OpenTelemetry signal of the stream integration
/// </summary>
[JsonConverter(typeof(JsonStringEnumConverter))]
public enum Signal
{
    /// <summary>
    /// Metric signal
    /// </summary>
    [JsonPropertyName("metric")]
    Metric,

    /// <summary>
    /// Trace signal
    /// </summary>
    [JsonPropertyName("trace")]
    Trace
}