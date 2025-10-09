namespace ThousandEyes.Api.Models.EventDetection;

/// <summary>
/// Event type classification
/// </summary>
public enum EventType
{
	/// <summary>
	/// Local agent issue
	/// </summary>
	AgentLocal,

	/// <summary>
	/// Network point-of-presence issue
	/// </summary>
	NetworkPop,

	/// <summary>
	/// Network issue
	/// </summary>
	Network,

	/// <summary>
	/// DNS issue
	/// </summary>
	Dns,

	/// <summary>
	/// Target/server issue
	/// </summary>
	Target,

	/// <summary>
	/// Target network issue
	/// </summary>
	TargetNetwork,

	/// <summary>
	/// Proxy issue
	/// </summary>
	Proxy
}
