using ThousandEyes.Api.Interfaces.TestSnapshots;
using ThousandEyes.Api.Models.TestSnapshots;
using ThousandEyes.Api.Refit.TestSnapshots;

namespace ThousandEyes.Api.Implementations.TestSnapshots;

/// <summary>
/// Implementation of <see cref="ITestSnapshots"/> for managing test snapshots.
/// </summary>
internal class TestSnapshotsImpl(ITestSnapshotsRefitApi refitApi) : ITestSnapshots
{
	/// <inheritdoc/>
	public async Task<SnapshotResponse> CreateAsync(string testId, SnapshotRequest request, string? aid, CancellationToken cancellationToken)
		=> await refitApi.CreateAsync(testId, request, aid, cancellationToken).ConfigureAwait(false);
}
