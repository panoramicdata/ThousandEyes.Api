using System.Text.Json.Serialization;

namespace ThousandEyes.Api.Models.OpenTelemetry;

/// <summary>
/// The version of the data model used in the data stream
/// </summary>
[JsonConverter(typeof(JsonStringEnumConverter))]
public enum DataModelVersion
{
    /// <summary>
    /// Version 1
    /// </summary>
    [JsonPropertyName("v1")]
    V1,

    /// <summary>
    /// Version 2
    /// </summary>
    [JsonPropertyName("v2")]
    V2
}