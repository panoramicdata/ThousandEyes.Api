namespace ThousandEyes.Api.Models.Alerts;

/// <summary>
/// Alert navigation links
/// </summary>
public class AlertLinks
{
	/// <summary>
	/// Self reference link
	/// </summary>
	public AlertLink? Self { get; set; }

	/// <summary>
	/// Next page link
	/// </summary>
	public AlertLink? Next { get; set; }

	/// <summary>
	/// Previous page link
	/// </summary>
	public AlertLink? Previous { get; set; }
}

/// <summary>
/// Alert navigation link
/// </summary>
public class AlertLink
{
	/// <summary>
	/// The href URL of the link
	/// </summary>
	public required string Href { get; set; }
}