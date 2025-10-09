using System.Text.Json.Serialization;

namespace ThousandEyes.Api.Models.InternetInsights;

/// <summary>
/// Response containing list of catalog providers
/// </summary>
public class CatalogProviderResponse : ApiResource
{
	/// <summary>
	/// List of catalog providers
	/// </summary>
	[JsonPropertyName("providers")]
	public CatalogProvider[] ProvidersList { get; set; } = [];
}
