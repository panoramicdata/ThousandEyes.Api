using Refit;
using ThousandEyes.Api.Interfaces;

namespace ThousandEyes.Api.Modules;

/// <summary>
/// Endpoint Agents API module for endpoint agent management
/// </summary>
/// <remarks>
/// Phase 6.5 implementation - Complete endpoint agent functionality
/// Manage ThousandEyes Endpoint Agents including lifecycle management,
/// filtering, monitoring, and account transfers.
/// </remarks>
public class EndpointAgentsModule
{
	/// <summary>
	/// Gets the Endpoint Agents API for agent management operations
	/// </summary>
	public IEndpointAgentsApi EndpointAgents { get; }

	/// <summary>
	/// Initializes a new instance of the EndpointAgentsModule
	/// </summary>
	/// <param name="httpClient">The HTTP client to use for API calls</param>
	/// <param name="refitSettings">Refit settings for JSON serialization</param>
	public EndpointAgentsModule(HttpClient httpClient, RefitSettings refitSettings)
	{
		// Create Refit API interface
		var endpointAgentsRefitApi = RestService.For<IEndpointAgentsRefitApi>(httpClient, refitSettings);

		// Initialize API implementation
		EndpointAgents = new EndpointAgentsApi(endpointAgentsRefitApi);
	}
}