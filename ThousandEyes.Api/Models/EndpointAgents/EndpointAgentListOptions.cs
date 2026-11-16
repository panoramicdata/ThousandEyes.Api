namespace ThousandEyes.Api.Models.EndpointAgents;

/// <summary>
/// Optional filters, paging and expansion for listing endpoint agents
/// </summary>
public sealed record EndpointAgentListOptions
{
	/// <summary>
	/// Account group ID
	/// </summary>
	public string? Aid { get; init; }

	/// <summary>
	/// Maximum number of objects to return
	/// </summary>
	public int? Max { get; init; }

	/// <summary>
	/// Pagination cursor
	/// </summary>
	public string? Cursor { get; init; }

	/// <summary>
	/// Expansion parameters
	/// </summary>
	public ExpandEndpointAgentOptions[]? Expand { get; init; }

	/// <summary>
	/// Include deleted agents
	/// </summary>
	public bool? IncludeDeleted { get; init; }

	/// <summary>
	/// Load data from all permitted accounts
	/// </summary>
	public bool? UseAllPermittedAids { get; init; }

	/// <summary>
	/// Filter by agent name
	/// </summary>
	public string? AgentName { get; init; }

	/// <summary>
	/// Filter by computer name
	/// </summary>
	public string? ComputerName { get; init; }
}
