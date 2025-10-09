using System.Text.Json.Serialization;

namespace ThousandEyes.Api.Models.InternetInsights;

/// <summary>
/// Detailed application outage information
/// </summary>
public class ApplicationOutageDetails
{
	/// <summary>
	/// The ID of the outage
	/// </summary>
	public required string Id { get; set; }

	/// <summary>
	/// The name of the affected provider
	/// </summary>
	public string? ProviderName { get; set; }

	/// <summary>
	/// The type of the affected provider
	/// </summary>
	[JsonPropertyName("providerType")]
	public ProviderType? ProviderTypeValue { get; set; }

	/// <summary>
	/// The name of the affected application
	/// </summary>
	public string? ApplicationName { get; set; }

	/// <summary>
	/// Date and time when the outage started
	/// </summary>
	public DateTime? StartDate { get; set; }

	/// <summary>
	/// Epoch time (seconds) when the outage started
	/// </summary>
	public long? StartRoundId { get; set; }

	/// <summary>
	/// Date and time when the outage ended
	/// </summary>
	public DateTime? EndDate { get; set; }

	/// <summary>
	/// Epoch time (seconds) when the outage ended
	/// </summary>
	public long? EndRoundId { get; set; }

	/// <summary>
	/// Duration of the outage in seconds
	/// </summary>
	public long? Duration { get; set; }

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
