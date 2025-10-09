using System.Text.Json.Serialization;

namespace ThousandEyes.Api.Models.InternetInsights;

/// <summary>
/// Filter request for outages
/// </summary>
public class OutageFilter
{
	/// <summary>
	/// Start of the time range (must be paired with EndDate)
	/// </summary>
	public DateTime? StartDate { get; set; }

	/// <summary>
	/// End of the time range (must be paired with StartDate)
	/// </summary>
	public DateTime? EndDate { get; set; }

	/// <summary>
	/// Time window for the past (alternative to StartDate/EndDate).
	/// Format: [0-9]+[smhdw] (e.g., "1d" for 1 day, "24h" for 24 hours)
	/// </summary>
	public string? Window { get; set; }

	/// <summary>
	/// Scope of the outage (all or with-affected-test)
	/// </summary>
	[JsonPropertyName("outageScope")]
	public OutageScope? OutageScopeValue { get; set; }

	/// <summary>
	/// Filter by provider names
	/// </summary>
	public string[] ProviderName { get; set; } = [];

	/// <summary>
	/// Filter by application names
	/// </summary>
	public string[] ApplicationName { get; set; } = [];

	/// <summary>
	/// Filter by interface network (ASN names)
	/// </summary>
	public string[] InterfaceNetwork { get; set; } = [];
}
