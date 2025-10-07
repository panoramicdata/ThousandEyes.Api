using ThousandEyes.Api.Models.Tests;

namespace ThousandEyes.Api.Interfaces;

/// <summary>
/// Interface for HTTP Server Tests API operations
/// </summary>
public interface IHttpServerTestsApi
{
	/// <summary>
	/// Get all HTTP Server tests
	/// </summary>
	/// <param name="aid">Account group ID (optional)</param>
	/// <param name="cancellationToken">Cancellation token</param>
	/// <returns>List of HTTP Server tests</returns>
	Task<HttpServerTests> GetAllAsync(string? aid, CancellationToken cancellationToken);

	/// <summary>
	/// Get a specific HTTP Server test by ID
	/// </summary>
	/// <param name="testId">Test ID</param>
	/// <param name="aid">Account group ID (optional)</param>
	/// <param name="versionId">Version ID for historical test configuration (optional)</param>
	/// <param name="expand">Expand sub-resources (optional)</param>
	/// <param name="cancellationToken">Cancellation token</param>
	/// <returns>HTTP Server test details</returns>
	Task<HttpServerTest> GetByIdAsync(
		string testId,
		string? aid,
		string? versionId,
		string[]? expand,
		CancellationToken cancellationToken);

	/// <summary>
	/// Create a new HTTP Server test
	/// </summary>
	/// <param name="request">HTTP Server test configuration</param>
	/// <param name="aid">Account group ID (optional)</param>
	/// <param name="expand">Expand sub-resources (optional)</param>
	/// <param name="cancellationToken">Cancellation token</param>
	/// <returns>Created HTTP Server test</returns>
	Task<HttpServerTest> CreateAsync(
		HttpServerTestRequest request,
		string? aid,
		string[]? expand,
		CancellationToken cancellationToken);

	/// <summary>
	/// Update an existing HTTP Server test
	/// </summary>
	/// <param name="testId">Test ID</param>
	/// <param name="request">Updated HTTP Server test configuration</param>
	/// <param name="aid">Account group ID (optional)</param>
	/// <param name="expand">Expand sub-resources (optional)</param>
	/// <param name="cancellationToken">Cancellation token</param>
	/// <returns>Updated HTTP Server test</returns>
	Task<HttpServerTest> UpdateAsync(
		string testId,
		HttpServerTestRequest request,
		string? aid,
		string[]? expand,
		CancellationToken cancellationToken);

	/// <summary>
	/// Delete an HTTP Server test
	/// </summary>
	/// <param name="testId">Test ID</param>
	/// <param name="aid">Account group ID (optional)</param>
	/// <param name="cancellationToken">Cancellation token</param>
	Task DeleteAsync(
		string testId,
		string? aid,
		CancellationToken cancellationToken);
}