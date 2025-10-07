using Refit;
using ThousandEyes.Api.Models.Tests;

namespace ThousandEyes.Api.Interfaces;

/// <summary>
/// Internal Refit interface for DNS Server Tests API
/// </summary>
internal interface IDnsServerTestsRefitApi
{
	/// <summary>
	/// Get all DNS Server tests
	/// </summary>
	[Get("/tests/dns-server")]
	Task<DnsServerTests> GetAllAsync([Query] string? aid, CancellationToken cancellationToken);
}