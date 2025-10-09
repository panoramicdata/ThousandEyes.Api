using System.Text.Json.Serialization;

namespace ThousandEyes.Api.Models.OpenTelemetry;

/// <summary>
/// Full stream response with audit and status
/// </summary>
public class GetStreamResponse : Stream
{
    /// <summary>
    /// The data stream ID
    /// </summary>
    [JsonPropertyName("id")]
    public string? Id { get; set; }

    /// <summary>
    /// Audit operation details
    /// </summary>
    [JsonPropertyName("auditOperation")]
    public AuditOperation? AuditOperation { get; set; }

    /// <summary>
    /// Stream connection status
    /// </summary>
    [JsonPropertyName("streamStatus")]
    public StreamStatus? StreamStatus { get; set; }
}