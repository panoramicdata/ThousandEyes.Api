using System.Text.Json.Serialization;

namespace ThousandEyes.Api.Models.OpenTelemetry;

/// <summary>
/// Splunk HEC configuration
/// </summary>
public class ExporterConfigSplunkHec
{
    /// <summary>
    /// The Splunk HEC token
    /// </summary>
    [JsonPropertyName("token")]
    public required string Token { get; set; }

    /// <summary>
    /// The Splunk HEC source
    /// </summary>
    [JsonPropertyName("source")]
    public string? Source { get; set; }

    /// <summary>
    /// The Splunk HEC sourceType
    /// </summary>
    [JsonPropertyName("sourceType")]
    public string? SourceType { get; set; }

    /// <summary>
    /// The name of the Splunk HEC index
    /// </summary>
    [JsonPropertyName("index")]
    public string? Index { get; set; }
}