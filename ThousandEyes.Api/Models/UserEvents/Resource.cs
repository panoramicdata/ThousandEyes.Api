namespace ThousandEyes.Api.Models.UserEvents;

/// <summary>
/// Resource affected by the event
/// </summary>
public class Resource
{
	/// <summary>
	/// Resource type
	/// </summary>
	public string Type { get; set; } = "";

	/// <summary>
	/// Resource name
	/// </summary>
	public string Name { get; set; } = "";
}
