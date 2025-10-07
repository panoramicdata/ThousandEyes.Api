using Refit;
using ThousandEyes.Api.Models.Tests;

namespace ThousandEyes.Api.Interfaces;

/// <summary>
/// Internal Refit interface for Page Load Tests API
/// </summary>
internal interface IPageLoadTestsRefitApi
{
	/// <summary>
	/// Get all Page Load tests
	/// </summary>
	[Get("/tests/page-load")]
	Task<PageLoadTests> GetAllAsync([Query] string? aid, CancellationToken cancellationToken);
}