using Refit;
using ThousandEyes.Api.Interfaces;

namespace ThousandEyes.Api.Modules;

/// <summary>
/// OpenTelemetry API module for data streaming configuration
/// </summary>
/// <remarks>
/// Phase 7 implementation - Complete OpenTelemetry functionality
/// </remarks>
public class OpenTelemetryModule
{
    /// <summary>
    /// Gets the Streams API for data stream management
    /// </summary>
    public IStreamsApi Streams { get; }

    /// <summary>
    /// Initializes a new instance of the OpenTelemetryModule
    /// </summary>
    /// <param name="httpClient">The HTTP client to use for API calls</param>
    /// <param name="refitSettings">Refit settings for JSON serialization</param>
    public OpenTelemetryModule(HttpClient httpClient, RefitSettings refitSettings)
    {
        // Create Refit API interface
        var streamsRefitApi = RestService.For<IStreamsRefitApi>(httpClient, refitSettings);

        // Initialize API implementation
        Streams = new StreamsApi(streamsRefitApi);
    }
}