using Refit;
using ThousandEyes.Api.Models.Tests;

namespace ThousandEyes.Api.Interfaces;

/// <summary>
/// Internal Refit interface for general Tests API
/// </summary>
internal interface ITestsRefitApi
{
	/// <summary>
	/// Get all tests
	/// </summary>
	[Get("/tests")]
	Task<Tests> GetAllAsync(
		[Query] string? aid,
		CancellationToken ct);

	/// <summary>
	/// Get test version history
	/// </summary>
	[Get("/tests/{testId}/history")]
	Task<TestVersionHistoryResponse> GetVersionHistoryAsync(
		string testId,
		[Query] string? aid,
		[Query] int? limit,
		CancellationToken ct);
}