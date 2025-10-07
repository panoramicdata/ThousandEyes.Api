using Refit;
using ThousandEyes.Api.Models.Tests;

namespace ThousandEyes.Api.Interfaces;

/// <summary>
/// Internal Refit interface for HTTP Server Tests API
/// </summary>
internal interface IHttpServerTestsRefitApi
{
	/// <summary>
	/// Get all HTTP Server tests
	/// </summary>
	[Get("/tests/http-server")]
	Task<HttpServerTests> GetAllAsync(
		[Query] string? aid,
		CancellationToken cancellationToken);

	/// <summary>
	/// Get HTTP Server test by ID
	/// </summary>
	[Get("/tests/http-server/{testId}")]
	Task<HttpServerTest> GetByIdAsync(
		string testId,
		[Query] string? aid,
		[Query] string? versionId,
		[Query] string[]? expand,
		CancellationToken cancellationToken);

	/// <summary>
	/// Create HTTP Server test
	/// </summary>
	[Post("/tests/http-server")]
	Task<HttpServerTest> CreateAsync(
		[Body] HttpServerTestRequest request,
		[Query] string? aid,
		[Query] string[]? expand,
		CancellationToken cancellationToken);

	/// <summary>
	/// Update HTTP Server test
	/// </summary>
	[Put("/tests/http-server/{testId}")]
	Task<HttpServerTest> UpdateAsync(
		string testId,
		[Body] HttpServerTestRequest request,
		[Query] string? aid,
		[Query] string[]? expand,
		CancellationToken cancellationToken);

	/// <summary>
	/// Delete HTTP Server test
	/// </summary>
	[Delete("/tests/http-server/{testId}")]
	Task DeleteAsync(
		string testId,
		[Query] string? aid,
		CancellationToken cancellationToken);
}