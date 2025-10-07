using ThousandEyes.Api.Interfaces;
using ThousandEyes.Api.Models.Tests;

namespace ThousandEyes.Api;

/// <summary>
/// Implementation of HTTP Server Tests API using Refit
/// </summary>
internal class HttpServerTestsApi(IHttpServerTestsRefitApi refitApi) : IHttpServerTestsApi
{
	private readonly IHttpServerTestsRefitApi _refitApi = refitApi;

	/// <inheritdoc />
	public Task<HttpServerTests> GetAllAsync(string? aid, CancellationToken cancellationToken) =>
		_refitApi.GetAllAsync(aid, cancellationToken);

	/// <inheritdoc />
	public Task<HttpServerTest> GetByIdAsync(string testId, string? aid, string? versionId, string[]? expand, CancellationToken cancellationToken) =>
		_refitApi.GetByIdAsync(testId, aid, versionId, expand, cancellationToken);

	/// <inheritdoc />
	public Task<HttpServerTest> CreateAsync(HttpServerTestRequest request, string? aid, string[]? expand, CancellationToken cancellationToken) =>
		_refitApi.CreateAsync(request, aid, expand, cancellationToken);

	/// <inheritdoc />
	public Task<HttpServerTest> UpdateAsync(string testId, HttpServerTestRequest request, string? aid, string[]? expand, CancellationToken cancellationToken) =>
		_refitApi.UpdateAsync(testId, request, aid, expand, cancellationToken);

	/// <inheritdoc />
	public Task DeleteAsync(string testId, string? aid, CancellationToken cancellationToken) =>
		_refitApi.DeleteAsync(testId, aid, cancellationToken);
}