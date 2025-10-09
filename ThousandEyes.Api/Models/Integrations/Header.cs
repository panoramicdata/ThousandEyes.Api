namespace ThousandEyes.Api.Models.Integrations;

/// <summary>
/// HTTP header configuration
/// </summary>
public class Header
{
	/// <summary>
	/// Header name
	/// </summary>
	public required string Name { get; set; }

	/// <summary>
	/// Header value
	/// </summary>
	public required string Value { get; set; }
}
