using System.Text.Json.Serialization;
using ThousandEyes.Api.Models.Users;

namespace ThousandEyes.Api.Models.OpenTelemetry;

/// <summary>
/// Create stream response
/// </summary>
public class CreateStreamResponse : Stream
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