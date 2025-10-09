using System.Text.Json.Serialization;

namespace ThousandEyes.Api.Models.InternetInsights;

/// <summary>
/// Filter request for catalog providers
/// </summary>
public class CatalogProviderFilter
{
	/// <summary>
	/// The name of the catalog provider (partial match supported)
	/// </summary>
	public string? ProviderName { get; set; }

	/// <summary>
	/// The type of catalog provider
	/// </summary>
	[JsonPropertyName("providerType")]
	public ProviderType? ProviderTypeValue { get; set; }

	/// <summary>
	/// The catalog provider region
	/// </summary>
	public string? Region { get; set; }

	/// <summary>
	/// Location of the catalog provider (partial match supported)
	/// </summary>
	public string? Location { get; set; }

	/// <summary>
	/// Name of the ASN covered by providers (partial match supported)
	/// </summary>
	public string? Asn { get; set; }

	/// <summary>
	/// Indicates whether the catalog provider is included in the licensed packages.
	/// true returns providers covered by licensed packages,
	/// false returns providers not covered by licensed packages.
	/// </summary>
	public bool? Included { get; set; }
}
