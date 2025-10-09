using System.Text.Json.Serialization;
using ThousandEyes.Api.Models.Users;

namespace ThousandEyes.Api.Models.OpenTelemetry;

/// <summary>
/// Create stream response
/// </summary>
public class CreateStreamResponse : Stream
{
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