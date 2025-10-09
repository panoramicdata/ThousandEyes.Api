namespace ThousandEyes.Api.Models.BgpMonitors;

/// <summary>
/// BGP monitor information
/// </summary>
public class Monitor
{
	/// <summary>
	/// BGP monitor ID
	/// </summary>
	public required string MonitorId { get; set; }

	/// <summary>
	/// Display name of the BGP monitor
	/// </summary>
	public string? MonitorName { get; set; }

	/// <summary>
	/// IP address of the BGP monitor
	/// </summary>
	public string? IpAddress { get; set; }

	/// <summary>
	/// Name of the autonomous system in which the monitor is found
	/// </summary>
	public string? Network { get; set; }

	/// <summary>
	/// Country ID (ISO 3166-1 alpha-2)
	/// </summary>
	public string? CountryId { get; set; }

	/// <summary>
	/// Type of monitor (public or private)
	/// </summary>
	public MonitorType? MonitorType { get; set; }
}
