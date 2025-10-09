using System.Text.Json.Serialization;

namespace ThousandEyes.Api.Models.OpenTelemetry;

/// <summary>
/// Stream connection status
/// </summary>
public class StreamStatus
{
    /// <summary>
    /// Last timestamp when data was successfully sent
    /// </summary>
    [JsonPropertyName("lastSuccess")]
    public long? LastSuccess { get; set; }

    /// <summary>
    /// Last timestamp when data failed to send
    /// </summary>
    [JsonPropertyName("lastFailure")]
    public long? LastFailure { get; set; }

    /// <summary>
    /// The status of the stream integration
    /// </summary>
    [JsonPropertyName("status")]
    public StreamStatusType? Status { get; set; }
}