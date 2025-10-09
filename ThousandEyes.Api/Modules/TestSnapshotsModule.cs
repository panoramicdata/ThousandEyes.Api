using Refit;
using ThousandEyes.Api.Implementations.TestSnapshots;
using ThousandEyes.Api.Interfaces.TestSnapshots;
using ThousandEyes.Api.Models.TestSnapshots;
using ThousandEyes.Api.Refit.TestSnapshots;

namespace ThousandEyes.Api.Modules;

/// <summary>
/// Module for Test Snapshots API operations.
/// </summary>
public class TestSnapshotsModule(HttpClient httpClient, RefitSettings refitSettings) : ITestSnapshots
{
	private readonly TestSnapshotsImpl _implementation = new(RestService.For<ITestSnapshotsRefitApi>(httpClient, refitSettings));

	/// <inheritdoc/>
	public async Task<SnapshotResponse> CreateAsync(string testId, SnapshotRequest request, string? aid, CancellationToken cancellationToken)
		=> await _implementation.CreateAsync(testId, request, aid, cancellationToken).ConfigureAwait(false);
}
