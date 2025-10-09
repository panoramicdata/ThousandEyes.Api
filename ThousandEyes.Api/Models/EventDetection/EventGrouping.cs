namespace ThousandEyes.Api.Models.EventDetection;

/// <summary>
/// Event grouping information (varies by event type)
/// </summary>
public class EventGrouping
{
	/// <summary>
	/// Target name (for target events)
	/// </summary>
	public string? Target { get; set; }

	/// <summary>
	/// Prefix value (for target-network events)
	/// </summary>
	public string? Prefix { get; set; }

	/// <summary>
	/// Proxy name or IP (for proxy events)
	/// </summary>
	public string? Proxy { get; set; }

	/// <summary>
	/// Root domain name (for DNS events)
	/// </summary>
	public string? RootDomain { get; set; }

	/// <summary>
	/// Agent ID (for agent-local events)
	/// </summary>
	public string? AgentId { get; set; }

	/// <summary>
	/// Source AS number (for network events)
	/// </summary>
	public int? SourceAsn { get; set; }

	/// <summary>
	/// Destination AS number (for network events)
	/// </summary>
	public int? DestAsn { get; set; }

	/// <summary>
	/// Source country code (for network events)
	/// </summary>
	public string? SourceCountryCode { get; set; }
}
