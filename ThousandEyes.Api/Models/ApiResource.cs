using System.Text.Json.Serialization;

namespace ThousandEyes.Api.Models;

/// <summary>
/// Base class for all API resources with HAL navigation links
/// </summary>
public abstract class ApiResource
{
	/// <summary>
	/// Navigation links (HAL format)
	/// </summary>
	[JsonPropertyName("_links")]
	public Links? Links { get; set; }
}
