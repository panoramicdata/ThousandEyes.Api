namespace ThousandEyes.Api.Models;

/// <summary>
/// Generic navigation links for HAL responses
/// </summary>
public class Links
{
	/// <summary>
	/// Self reference link
	/// </summary>
	public Link? Self { get; set; }

	/// <summary>
	/// Next page link (for paginated responses)
	/// </summary>
	public Link? Next { get; set; }

	/// <summary>
	/// Previous page link (for paginated responses)
	/// </summary>
	public Link? Previous { get; set; }
}
