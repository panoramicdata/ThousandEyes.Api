namespace ThousandEyes.Api.Models.InternetInsights;

/// <summary>
/// Network outage affected location information
/// </summary>
public class NetworkAffectedLocation
{
	/// <summary>
	/// The affected location
	/// </summary>
	public string? Location { get; set; }

	/// <summary>
	/// The affected interfaces in this location
	/// </summary>
	public string[] AffectedInterfaces { get; set; } = [];
}
