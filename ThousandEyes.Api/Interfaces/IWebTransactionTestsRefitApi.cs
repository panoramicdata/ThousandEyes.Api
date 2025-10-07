using Refit;
using ThousandEyes.Api.Models.Tests;

namespace ThousandEyes.Api.Interfaces;

/// <summary>
/// Internal Refit interface for Web Transaction Tests API
/// </summary>
internal interface IWebTransactionTestsRefitApi
{
	/// <summary>
	/// Get all Web Transaction tests
	/// </summary>
	[Get("/tests/web-transactions")]
	Task<WebTransactionTests> GetAllAsync([Query] string? aid, CancellationToken cancellationToken);
}