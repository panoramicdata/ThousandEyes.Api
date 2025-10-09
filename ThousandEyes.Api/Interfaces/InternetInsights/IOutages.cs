using ThousandEyes.Api.Models.InternetInsights;

namespace ThousandEyes.Api.Interfaces.InternetInsights;

/// <summary>
/// Public interface for Outages operations
/// </summary>
public interface IOutages
{
	/// <summary>
	/// Returns a list of network and application outages using the specified filter.
	/// </summary>
	/// <param name="filter">Filter criteria for outages</param>
	/// <param name="aid">Optional account group ID for context</param>
	/// <param name="cancellationToken">Cancellation token</param>
	/// <returns>List of outages matching the filter criteria</returns>
	Task<OutagesResponse> FilterAsync(
		OutageFilter filter,
		string? aid,
		CancellationToken cancellationToken);

	/// <summary>
	/// Returns the details of a network outage.
	/// </summary>
	/// <param name="outageId">The network outage ID</param>
	/// <param name="aid">Optional account group ID for context</param>
	/// <param name="cancellationToken">Cancellation token</param>
	/// <returns>Detailed network outage information</returns>
	Task<NetworkOutageDetails> GetNetworkOutageAsync(
		string outageId,
		string? aid,
		CancellationToken cancellationToken);

	/// <summary>
	/// Returns the details of an application outage.
	/// </summary>
	/// <param name="outageId">The application outage ID</param>
	/// <param name="aid">Optional account group ID for context</param>
	/// <param name="cancellationToken">Cancellation token</param>
	/// <returns>Detailed application outage information</returns>
	Task<ApplicationOutageDetails> GetApplicationOutageAsync(
		string outageId,
		string? aid,
		CancellationToken cancellationToken);
}
