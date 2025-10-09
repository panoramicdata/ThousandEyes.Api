using System.Text.Json.Serialization;

namespace ThousandEyes.Api.Models.InternetInsights;

/// <summary>
/// Outage summary information
/// </summary>
public class Outage
{
	/// <summary>
	/// The ID of the outage
	/// </summary>
	public required string Id { get; set; }

	/// <summary>
	/// The type of outage (e.g., "app" or "net")
	/// </summary>
	public string? Type { get; set; }

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
	/// The name of the affected application or network
	/// </summary>
	public string? Name { get; set; }

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
	/// Duration of the outage (seconds)
	/// </summary>
	public long? Duration { get; set; }

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
