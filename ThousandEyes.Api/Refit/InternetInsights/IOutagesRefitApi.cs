using Refit;
using ThousandEyes.Api.Models.InternetInsights;

namespace ThousandEyes.Api.Refit.InternetInsights;

/// <summary>
/// Internal Refit interface for Outages API
/// </summary>
internal interface IOutagesRefitApi
{
	/// <summary>
	/// Filter outages
	/// </summary>
	[Post("/internet-insights/outages/filter")]
	Task<OutagesResponse> FilterAsync(
		[Body] OutageFilter filter,
		[Query] string? aid,
		CancellationToken cancellationToken);

	/// <summary>
	/// Get network outage by ID
	/// </summary>
	[Get("/internet-insights/outages/net/{outageId}")]
	Task<NetworkOutageDetails> GetNetworkOutageAsync(
		string outageId,
		[Query] string? aid,
		CancellationToken cancellationToken);

	/// <summary>
	/// Get application outage by ID
	/// </summary>
	[Get("/internet-insights/outages/app/{outageId}")]
	Task<ApplicationOutageDetails> GetApplicationOutageAsync(
		string outageId,
		[Query] string? aid,
		CancellationToken cancellationToken);
}
