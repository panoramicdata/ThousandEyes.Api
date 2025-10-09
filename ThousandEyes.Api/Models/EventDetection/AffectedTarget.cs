namespace ThousandEyes.Api.Models.EventDetection;

/// <summary>
/// Target affected by an event
/// </summary>
public class AffectedTarget
{
	/// <summary>
	/// Server ID of the target
	/// </summary>
	public string? ServerId { get; set; }

	/// <summary>
	/// Target name
	/// </summary>
	public string? Name { get; set; }

	/// <summary>
	/// Target IP address
	/// </summary>
	public string? Ip { get; set; }

	/// <summary>
	/// Test IDs that contributed to the event signal
	/// </summary>
	public string[] AffectedTestIds { get; set; } = [];

	/// <summary>
	/// Agent IDs that contributed to the event signal
	/// </summary>
	public string[] AffectedAgentIds { get; set; } = [];
}
