using System.Text.Json.Serialization;

namespace ThousandEyes.Api.Models.OpenTelemetry;

/// <summary>
/// Collection of streams
/// </summary>
public class StreamCollection : ApiResource
{
    /// <summary>
    /// List of streams
    /// </summary>
    [JsonPropertyName("streams")]
    public GetStreamResponse[] Streams { get; set; } = [];
}