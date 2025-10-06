using Refit;

namespace ThousandEyes.Api.Modules;

/// <summary>
/// Snapshots API module for data preservation and sharing
/// </summary>
/// <remarks>
/// Planned for Phase 3 implementation
/// Enables data preservation and sharing capabilities
/// </remarks>
public class SnapshotsModule
{
	/// <summary>
	/// Initializes a new instance of the SnapshotsModule
	/// </summary>
	/// <param name="httpClient">The HTTP client to use for API calls</param>
	/// <param name="refitSettings">Refit settings for JSON serialization</param>
	public SnapshotsModule(HttpClient httpClient, RefitSettings refitSettings)
	{
		// TODO: Phase 3 - Implement Snapshots API
		// Will include:
		// - Snapshot creation and management
		// - Snapshot data preservation
		// - Snapshot sharing capabilities
		// - Snapshot settings and timespan configuration
		// - Snapshot deletion and cleanup
		throw new NotImplementedException("Snapshots API will be implemented in Phase 3");
	}
}