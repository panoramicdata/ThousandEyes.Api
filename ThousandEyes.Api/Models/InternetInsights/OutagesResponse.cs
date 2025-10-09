using System.Text.Json.Serialization;

namespace ThousandEyes.Api.Models.InternetInsights;

/// <summary>
/// Response containing list of outages
/// </summary>
public class OutagesResponse : ApiResource
{
	/// <summary>
	/// List of outages
	/// </summary>
	[JsonPropertyName("outages")]
	public Outage[] OutagesList { get; set; } = [];
}
