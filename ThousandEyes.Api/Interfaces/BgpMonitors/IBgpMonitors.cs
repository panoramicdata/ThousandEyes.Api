using ThousandEyes.Api.Models.BgpMonitors;

namespace ThousandEyes.Api.Interfaces.BgpMonitors;

/// <summary>
/// Public interface for BGP Monitors operations
/// </summary>
public interface IBgpMonitors
{
	/// <summary>
	/// Retrieves a list of BGP monitors available to your account in ThousandEyes,
	/// including public and private feeds.
	/// </summary>
	/// <param name="aid">Optional account group ID for context</param>
	/// <param name="cancellationToken">Cancellation token</param>
	/// <returns>List of BGP monitors</returns>
	Task<Monitors> GetAllAsync(string? aid, CancellationToken cancellationToken);
}
