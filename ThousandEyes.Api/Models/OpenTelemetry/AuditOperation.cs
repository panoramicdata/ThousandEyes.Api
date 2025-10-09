using System.Text.Json.Serialization;

namespace ThousandEyes.Api.Models.OpenTelemetry;

/// <summary>
/// Audit operation details
/// </summary>
public class AuditOperation
{
    /// <summary>
    /// ID of the user who created the integration
    /// </summary>
    [JsonPropertyName("createdBy")]
    public long? CreatedBy { get; set; }

    /// <summary>
    /// Creation date of the integration
    /// </summary>
    [JsonPropertyName("createdDate")]
    public long? CreatedDate { get; set; }
}