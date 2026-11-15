namespace ThousandEyes.Api.Models.InternetInsights;

/// <summary>
/// Outage summary information
/// </summary>
public class Outage : OutageBase
{
	/// <summary>
	/// The type of outage (e.g., "app" or "net")
	/// </summary>
	public string? Type { get; set; }

	/// <summary>
	/// The name of the affected application or network
	/// </summary>
	public string? Name { get; set; }

	/// <summary>
	/// The number of affected tests
	/// </summary>
	public int AffectedTestsCount { get; set; }

	/// <summary>
	/// The number of affected servers
	/// </summary>
	public int AffectedServersCount { get; set; }

	/// <summary>
	/// The number of affected locations
	/// </summary>
	public int AffectedLocationsCount { get; set; }

	/// <summary>
	/// The number of affected interfaces
	/// </summary>
	public int AffectedInterfacesCount { get; set; }

	/// <summary>
	/// ASN number
	/// </summary>
	public int? Asn { get; set; }
}
