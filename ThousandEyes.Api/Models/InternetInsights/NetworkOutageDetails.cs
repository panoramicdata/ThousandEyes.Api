namespace ThousandEyes.Api.Models.InternetInsights;

/// <summary>
/// Detailed network outage information
/// </summary>
public class NetworkOutageDetails : OutageBase
{
	/// <summary>
	/// The affected network name
	/// </summary>
	public string? NetworkName { get; set; }

	/// <summary>
	/// ASN number
	/// </summary>
	public int? Asn { get; set; }

	/// <summary>
	/// List of affected tests
	/// </summary>
	public AffectedTest[] AffectedTests { get; set; } = [];

	/// <summary>
	/// List of affected domains
	/// </summary>
	public string[] AffectedDomains { get; set; } = [];

	/// <summary>
	/// List of affected agents
	/// </summary>
	public AffectedAgent[] AffectedAgents { get; set; } = [];

	/// <summary>
	/// List of affected locations
	/// </summary>
	public NetworkAffectedLocation[] AffectedLocations { get; set; } = [];
}
