using System.Text.Json.Serialization;

namespace ThousandEyes.Api.Models.InternetInsights;

/// <summary>
/// Detailed catalog provider information
/// </summary>
public class CatalogProviderDetails
{
	/// <summary>
	/// The catalog provider ID
	/// </summary>
	public required string Id { get; set; }

	/// <summary>
	/// The name of the catalog provider
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
	/// The type of data produced by the provider
	/// </summary>
	[JsonPropertyName("dataType")]
	public DataType? DataTypeValue { get; set; }

	/// <summary>
	/// List of ASN's covered by the provider
	/// </summary>
	public Asn[] Asns { get; set; } = [];

	/// <summary>
	/// List of locations covered by the provider
	/// </summary>
	public ProviderLocation[] Locations { get; set; } = [];
}
