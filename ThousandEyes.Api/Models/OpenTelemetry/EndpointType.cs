using System.Text.Json.Serialization;

namespace ThousandEyes.Api.Models.OpenTelemetry;

/// <summary>
/// The type of connection used to send data to the endpoint
/// </summary>
[JsonConverter(typeof(JsonStringEnumConverter))]
public enum EndpointType
{
    /// <summary>
    /// GRPC connection
    /// </summary>
    [JsonPropertyName("grpc")]
    Grpc,

    /// <summary>
    /// HTTP connection
    /// </summary>
    [JsonPropertyName("http")]
    Http
}