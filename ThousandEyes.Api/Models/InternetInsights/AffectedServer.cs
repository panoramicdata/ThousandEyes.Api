namespace ThousandEyes.Api.Models.InternetInsights;

/// <summary>
/// Server affected by an application outage
/// </summary>
public class AffectedServer
{
	/// <summary>
	/// Domain name of the affected server
	/// </summary>
	public string? Domain { get; set; }

	/// <summary>
	/// IP prefix of the affected server
	/// </summary>
	public string? Prefix { get; set; }
}
