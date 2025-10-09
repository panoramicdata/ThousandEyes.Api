using System.Text.Json.Serialization;

namespace ThousandEyes.Api.Models.OpenTelemetry;

/// <summary>
/// Exporter configuration
/// </summary>
public class ExporterConfig
{
    /// <summary>
    /// Splunk HEC configuration
    /// </summary>
    [JsonPropertyName("splunkHec")]
    public ExporterConfigSplunkHec? SplunkHec { get; set; }
}