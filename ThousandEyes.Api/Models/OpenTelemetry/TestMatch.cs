using System.Text.Json.Serialization;

namespace ThousandEyes.Api.Models.OpenTelemetry;

/// <summary>
/// Test filtering configuration
/// </summary>
public class TestMatch
{
    /// <summary>
    /// The ID of the test to match
    /// </summary>
    [JsonPropertyName("id")]
    public string? Id { get; set; }

    /// <summary>
    /// The domain of the test to match
    /// </summary>
    [JsonPropertyName("domain")]
    public TestMatchDomain? Domain { get; set; }
}