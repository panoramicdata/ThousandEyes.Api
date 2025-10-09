using System.Text.Json.Serialization;

namespace ThousandEyes.Api.Models.OpenTelemetry;

/// <summary>
/// The domain of the test to match
/// </summary>
[JsonConverter(typeof(JsonStringEnumConverter))]
public enum TestMatchDomain
{
    /// <summary>
    /// Cloud and Enterprise Agent
    /// </summary>
    [JsonPropertyName("cea")]
    Cea,

    /// <summary>
    /// Endpoint Agent
    /// </summary>
    [JsonPropertyName("endpoint")]
    Endpoint
}