namespace ThousandEyes.Api.Models;

/// <summary>
/// Pagination links for navigation
/// </summary>
public class PaginationLinks
{
	/// <summary>
	/// Previous page link
	/// </summary>
	public Link? Previous { get; set; }

	/// <summary>
	/// Next page link
	/// </summary>
	public Link? Next { get; set; }

	/// <summary>
	/// Self link
	/// </summary>
	public Link? Self { get; set; }
}
