namespace ThousandEyes.Api.Models.InternetInsights;

/// <summary>
/// Detailed application outage information
/// </summary>
public class ApplicationOutageDetails : OutageBase
{
	/// <summary>
	/// The name of the affected application
	/// </summary>
	public string? ApplicationName { get; set; }

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
	/// List of errors
	/// </summary>
	public string[] Errors { get; set; } = [];

	/// <summary>
	/// List of affected locations
	/// </summary>
	public ApplicationAffectedLocation[] AffectedLocations { get; set; } = [];
}
