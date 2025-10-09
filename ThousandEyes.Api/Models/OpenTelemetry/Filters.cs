using System.Text.Json.Serialization;

namespace ThousandEyes.Api.Models.OpenTelemetry;

/// <summary>
/// Provides the ability to filter data points based on the specified test types
/// </summary>
public class Filters
{
    /// <summary>
    /// Test types that can be used for filtering data points
    /// </summary>
    [JsonPropertyName("testTypes")]
    public FiltersTestTypes? TestTypes { get; set; }
}