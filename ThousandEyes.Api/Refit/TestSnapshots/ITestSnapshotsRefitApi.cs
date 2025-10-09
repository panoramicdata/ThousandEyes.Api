using Refit;
using ThousandEyes.Api.Models.TestSnapshots;

namespace ThousandEyes.Api.Refit.TestSnapshots;

/// <summary>
/// Internal Refit interface for Test Snapshots API operations.
/// </summary>
internal interface ITestSnapshotsRefitApi
{
	/// <summary>
	/// Creates a test snapshot.
	/// </summary>
	/// <param name="testId">Test ID</param>
	/// <param name="request">Snapshot configuration</param>
	/// <param name="aid">Optional account group ID</param>
	/// <param name="cancellationToken">Cancellation token</param>
	[Post("/tests/{testId}/snapshot")]
	Task<SnapshotResponse> CreateAsync(string testId, [Body] SnapshotRequest request, [Query] string? aid, CancellationToken cancellationToken);
}
