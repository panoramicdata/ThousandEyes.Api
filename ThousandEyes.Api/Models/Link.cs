namespace ThousandEyes.Api.Models;

/// <summary>
/// Hyperlink to a resource (HAL format)
/// </summary>
public class Link
{
	/// <summary>
	/// Link URI or URI template
	/// </summary>
	public required string Href { get; set; }

	/// <summary>
	/// Whether the link is templated
	/// </summary>
	public bool? Templated { get; set; }

	/// <summary>
	/// Media type hint
	/// </summary>
	public string? Type { get; set; }

	/// <summary>
	/// Link title
	/// </summary>
	public string? Title { get; set; }
}