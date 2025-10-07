namespace ThousandEyes.Api.Models.Alerts;

/// <summary>
/// Alerts response wrapper
/// </summary>
public class Alerts
{
	/// <summary>
	/// List of alerts
	/// </summary>
	public Alert[] AlertsList { get; set; } = [];

	/// <summary>
	/// Navigation links
	/// </summary>
	public AlertLinks? Links { get; set; }
}