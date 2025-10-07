using Refit;
using ThousandEyes.Api.Models.Agents;

namespace ThousandEyes.Api.Interfaces;

/// <summary>
/// Internal Refit interface for Agents API
/// </summary>
internal interface IAgentsRefitApi
{
	/// <summary>
	/// Get all agents
	/// </summary>
	[Get("/agents")]
	Task<Agents> GetAllAsync([Query] string? aid, CancellationToken cancellationToken);

	/// <summary>
	/// Get agent by ID
	/// </summary>
	[Get("/agents/{agentId}")]
	Task<Agent> GetByIdAsync(string agentId, [Query] string? aid, CancellationToken cancellationToken);

	/// <summary>
	/// Update agent
	/// </summary>
	[Put("/agents/{agentId}")]
	Task<Agent> UpdateAsync(string agentId, [Body] AgentRequest request, [Query] string? aid, CancellationToken cancellationToken);

	/// <summary>
	/// Delete agent
	/// </summary>
	[Delete("/agents/{agentId}")]
	Task DeleteAsync(string agentId, [Query] string? aid, CancellationToken cancellationToken);

	/// <summary>
	/// Create agent
	/// </summary>
	[Post("/agents")]
	Task<Agent> CreateAsync([Body] AgentRequest request, [Query] string? aid, CancellationToken cancellationToken);

	/// <summary>
	/// Get supported test types
	/// </summary>
	[Get("/agents/{agentId}/supported-tests")]
	Task<string[]> GetSupportedTestsAsync(string agentId, [Query] string? aid, CancellationToken cancellationToken);
}