using Refit;
using ThousandEyes.Api.Models.OpenTelemetry;
using Stream = ThousandEyes.Api.Models.OpenTelemetry.Stream;

namespace ThousandEyes.Api.Interfaces;

/// <summary>
/// Internal Refit interface for OpenTelemetry Streams API
/// </summary>
internal interface IStreamsRefitApi
{
    /// <summary>
    /// Get all streams
    /// </summary>
    [Get("/streams")]
    Task<StreamCollection> GetAllAsync([Query] string? aid, [Query] StreamType? type, CancellationToken cancellationToken);

    /// <summary>
    /// Create stream
    /// </summary>
    [Post("/streams")]
    Task<CreateStreamResponse> CreateAsync([Body] Stream request, [Query] string? aid, CancellationToken cancellationToken);

    /// <summary>
    /// Get stream by ID
    /// </summary>
    [Get("/streams/{id}")]
    Task<GetStreamResponse> GetByIdAsync(string id, [Query] string? aid, [Query] StreamType? type, CancellationToken cancellationToken);

    /// <summary>
    /// Update stream
    /// </summary>
    [Put("/streams/{id}")]
    Task<GetStreamResponse> UpdateAsync(string id, [Body] PutStream request, [Query] string? aid, CancellationToken cancellationToken);

    /// <summary>
    /// Delete stream
    /// </summary>
    [Delete("/streams/{id}")]
    Task DeleteAsync(string id, [Query] string? aid, CancellationToken cancellationToken);
}