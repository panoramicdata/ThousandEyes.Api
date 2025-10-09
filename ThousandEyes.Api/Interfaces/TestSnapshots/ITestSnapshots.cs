using ThousandEyes.Api.Models.TestSnapshots;

namespace ThousandEyes.Api.Interfaces.TestSnapshots;

/// <summary>
/// Interface for managing test snapshots.
/// </summary>
public interface ITestSnapshots
{
	/// <summary>
	/// Creates a test snapshot based on the provided properties.
	/// Requires "Create snapshot shares" permission.
	/// </summary>
	/// <param name="testId">Test ID to create snapshot for</param>
	/// <param name="request">Snapshot configuration</param>
	/// <param name="aid">Optional account group ID</param>
	/// <param name="cancellationToken">Cancellation token</param>
	/// <returns>Created snapshot response</returns>
	/// <remarks>
	/// <para>Limitations:</para>
	/// <list type="bullet">
	/// <item>Maximum of 5 snapshots per organization within a 5-minute interval</item>
	/// <item>Snapshots have a 30-day expiration period</item>
	/// <item>Time range must be 1, 2, 4, 6, 12, 24, or 48 hours</item>
	/// <item>End date must be present or past</item>
	/// <item>Does not support Agent snapshots</item>
	/// <item>Some regions may not have public snapshots enabled</item>
	/// </list>
	/// </remarks>
	Task<SnapshotResponse> CreateAsync(string testId, SnapshotRequest request, string? aid, CancellationToken cancellationToken);
}
