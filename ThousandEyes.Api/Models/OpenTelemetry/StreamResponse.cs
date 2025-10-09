using System.Text.Json.Serialization;

namespace ThousandEyes.Api.Models.OpenTelemetry;

/// <summary>
/// Base response for a stream
/// </summary>
public class StreamResponse : ApiResource
{
    /// <summary>
    /// The data stream ID
    /// </summary>
    [JsonPropertyName("id")]
    public string? Id { get; set; }

    /// <summary>
    /// Flag to indicate if the stream integration is currently enabled
    /// </summary>
    [JsonPropertyName("enabled")]
    public bool? Enabled { get; set; }
}