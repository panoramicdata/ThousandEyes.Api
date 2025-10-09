namespace ThousandEyes.Api.Models.InternetInsights;

/// <summary>
/// Agent affected by an outage
/// </summary>
public class AffectedAgent
{
	/// <summary>
	/// Agent ID
	/// </summary>
	public long Id { get; set; }

	/// <summary>
	/// Agent name/location
	/// </summary>
	public string? Name { get; set; }
}
