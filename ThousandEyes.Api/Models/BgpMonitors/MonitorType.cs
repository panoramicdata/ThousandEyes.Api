namespace ThousandEyes.Api.Models.BgpMonitors;

/// <summary>
/// Type of BGP monitor
/// </summary>
public enum MonitorType
{
	/// <summary>
	/// Public BGP monitor (available to all accounts)
	/// </summary>
	Public,

	/// <summary>
	/// Private BGP monitor (custom/internal)
	/// </summary>
	Private
}
