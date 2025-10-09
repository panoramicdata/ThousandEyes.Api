using System.Text.Json.Serialization;

namespace ThousandEyes.Api.Models.OpenTelemetry;

/// <summary>
/// The status of the stream integration
/// </summary>
[JsonConverter(typeof(JsonStringEnumConverter))]
public enum StreamStatusType
{
    /// <summary>
    /// Data is being sent successfully
    /// </summary>
    [JsonPropertyName("connected")]
    Connected,

    /// <summary>
    /// No data is currently being sent
    /// </summary>
    [JsonPropertyName("pending")]
    Pending,

    /// <summary>
    /// Data is being sent but not successfully reaching the endpoint
    /// </summary>
    [JsonPropertyName("failing")]
    Failing
}