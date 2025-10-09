using ThousandEyes.Api.Interfaces;
using ThousandEyes.Api.Models.OpenTelemetry;
using Stream = ThousandEyes.Api.Models.OpenTelemetry.Stream;

namespace ThousandEyes.Api;

/// <summary>
/// Implementation of OpenTelemetry Streams API using Refit
/// </summary>
internal class StreamsApi(IStreamsRefitApi refitApi) : IStreamsApi
{
    private readonly IStreamsRefitApi _refitApi = refitApi;

    /// <inheritdoc />
    public Task<StreamCollection> GetAllAsync(string? aid, StreamType? type, CancellationToken cancellationToken) =>
        _refitApi.GetAllAsync(aid, type, cancellationToken);

    /// <inheritdoc />
    public Task<CreateStreamResponse> CreateAsync(Stream request, string? aid, CancellationToken cancellationToken) =>
        _refitApi.CreateAsync(request, aid, cancellationToken);

    /// <inheritdoc />
    public Task<GetStreamResponse> GetByIdAsync(string id, string? aid, StreamType? type, CancellationToken cancellationToken) =>
        _refitApi.GetByIdAsync(id, aid, type, cancellationToken);

    /// <inheritdoc />
    public Task<GetStreamResponse> UpdateAsync(string id, PutStream request, string? aid, CancellationToken cancellationToken) =>
        _refitApi.UpdateAsync(id, request, aid, cancellationToken);

    /// <inheritdoc />
    public Task DeleteAsync(string id, string? aid, CancellationToken cancellationToken) =>
        _refitApi.DeleteAsync(id, aid, cancellationToken);
}