using System.Text.Json.Serialization;

namespace ThousandEyes.Api.Models.OpenTelemetry;

/// <summary>
/// Complete stream configuration
/// </summary>
public class Stream : PutStream
{
    /// <summary>
    /// The type of data stream to configure
    /// </summary>
    [JsonPropertyName("type")]
    public StreamType? Type { get; set; }

    /// <summary>
    /// The OpenTelemetry signal of the stream integration
    /// </summary>
    [JsonPropertyName("signal")]
    public Signal? Signal { get; set; }

    /// <summary>
    /// The type of connection used to send data to the endpoint
    /// </summary>
    [JsonPropertyName("endpointType")]
    public EndpointType? EndpointType { get; set; }

    /// <summary>
    /// The version of the data model used in the data stream
    /// </summary>
    [JsonPropertyName("dataModelVersion")]
    public DataModelVersion? DataModelVersion { get; set; }
}