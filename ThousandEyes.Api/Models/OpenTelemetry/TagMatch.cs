using System.Text.Json.Serialization;

namespace ThousandEyes.Api.Models.OpenTelemetry;

/// <summary>
/// Tag filtering configuration
/// </summary>
public class TagMatch
{
    /// <summary>
    /// The name of the tag key to match
    /// </summary>
    [JsonPropertyName("key")]
    public string? Key { get; set; }

    /// <summary>
    /// The value of the tag to match
    /// </summary>
    [JsonPropertyName("value")]
    public string? Value { get; set; }
}