namespace ThousandEyes.Api.Models.InternetInsights;

/// <summary>
/// Test affected by an outage
/// </summary>
public class AffectedTest
{
	/// <summary>
	/// Test ID
	/// </summary>
	public long Id { get; set; }

	/// <summary>
	/// Test name
	/// </summary>
	public string? Name { get; set; }
}
