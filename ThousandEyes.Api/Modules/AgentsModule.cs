using Refit;
using ThousandEyes.Api.Interfaces;

namespace ThousandEyes.Api.Modules;

/// <summary>
/// Agents API module for managing Cloud and Enterprise agents
/// </summary>
/// <remarks>
/// Phase 2 implementation - Complete agent management functionality
/// Essential for test configuration and agent management
/// </remarks>
public class AgentsModule
{
	/// <summary>
	/// Gets the Agents API for managing Cloud and Enterprise agents
	/// </summary>
	public IAgentsApi Agents { get; }

	/// <summary>
	/// Initializes a new instance of the AgentsModule
	/// </summary>
	/// <param name="httpClient">The HTTP client to use for API calls</param>
	/// <param name="refitSettings">Refit settings for JSON serialization</param>
	public AgentsModule(HttpClient httpClient, RefitSettings refitSettings)
	{
		// Create Refit API interfaces
		var agentsRefitApi = RestService.For<IAgentsRefitApi>(httpClient, refitSettings);

		// Initialize API implementations
		Agents = new AgentsApi(agentsRefitApi);
	}
}