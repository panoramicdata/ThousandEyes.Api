using ThousandEyes.Api.Models.OpenTelemetry;
using Stream = ThousandEyes.Api.Models.OpenTelemetry.Stream;

namespace ThousandEyes.Api.Interfaces;

/// <summary>
/// Interface for OpenTelemetry Streams API operations
/// </summary>
/// <remarks>
/// Phase 7 implementation - OpenTelemetry data streaming
/// </remarks>
public interface IStreamsApi
{
    /// <summary>
    /// Retrieves a list of configured data streams
    /// </summary>
    /// <param name="aid">Account group ID (optional)</param>
    /// <param name="type">Filter by stream type (optional)</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>List of data streams</returns>
    Task<StreamCollection> GetAllAsync(string? aid, StreamType? type, CancellationToken cancellationToken);

    /// <summary>
    /// Creates a new data stream
    /// </summary>
    /// <param name="request">Stream configuration</param>
    /// <param name="aid">Account group ID (optional)</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Created stream</returns>
    Task<CreateStreamResponse> CreateAsync(Stream request, string? aid, CancellationToken cancellationToken);

    /// <summary>
    /// Retrieves a configured data stream by ID
    /// </summary>
    /// <param name="id">Stream ID</param>
    /// <param name="aid">Account group ID (optional)</param>
    /// <param name="type">Filter by stream type (optional)</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Stream details</returns>
    Task<GetStreamResponse> GetByIdAsync(string id, string? aid, StreamType? type, CancellationToken cancellationToken);

    /// <summary>
    /// Updates a configured data stream by ID
    /// </summary>
    /// <param name="id">Stream ID</param>
    /// <param name="request">Update request</param>
    /// <param name="aid">Account group ID (optional)</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Updated stream</returns>
    Task<GetStreamResponse> UpdateAsync(string id, PutStream request, string? aid, CancellationToken cancellationToken);

    /// <summary>
    /// Deletes a configured data stream by ID
    /// </summary>
    /// <param name="id">Stream ID</param>
    /// <param name="aid">Account group ID (optional)</param>
    /// <param name="cancellationToken">Cancellation token</param>
    Task DeleteAsync(string id, string? aid, CancellationToken cancellationToken);
}