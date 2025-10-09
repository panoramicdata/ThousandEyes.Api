namespace ThousandEyes.Api.Models.InternetInsights;

/// <summary>
/// Application outage affected location information
/// </summary>
public class ApplicationAffectedLocation
{
	/// <summary>
	/// The affected location
	/// </summary>
	public string? Location { get; set; }

	/// <summary>
	/// The affected servers in this location
	/// </summary>
	public AffectedServer[] AffectedServers { get; set; } = [];
}
