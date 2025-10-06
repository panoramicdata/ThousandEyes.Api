using Refit;

namespace ThousandEyes.Api.Modules;

/// <summary>
/// Agents API module for managing Cloud and Enterprise agents
/// </summary>
/// <remarks>
/// Planned for Phase 2 implementation
/// Essential for test configuration and agent management
/// </remarks>
public class AgentsModule
{
	/// <summary>
	/// Initializes a new instance of the AgentsModule
	/// </summary>
	/// <param name="httpClient">The HTTP client to use for API calls</param>
	/// <param name="refitSettings">Refit settings for JSON serialization</param>
	public AgentsModule(HttpClient httpClient, RefitSettings refitSettings)
	{
		// TODO: Phase 2 - Implement Agents API
		// Will include:
		// - Cloud Agents
		// - Enterprise Agents
		// - Enterprise Agent Clusters
		// - Agent capabilities and supported tests
		// - Agent location and network information
		throw new NotImplementedException("Agents API will be implemented in Phase 2");
	}
}