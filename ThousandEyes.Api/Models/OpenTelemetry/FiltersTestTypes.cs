using System.Text.Json.Serialization;
using ThousandEyes.Api.Models.Tests;

namespace ThousandEyes.Api.Models.OpenTelemetry;

/// <summary>
/// Test types to filter data points
/// </summary>
public class FiltersTestTypes
{
    /// <summary>
    /// A list of test types to filter data points
    /// </summary>
    [JsonPropertyName("values")]
    public TestType[] Values { get; set; } = [];
}