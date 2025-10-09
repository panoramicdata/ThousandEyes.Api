using System.Text.Json.Serialization;

namespace ThousandEyes.Api.Models.InternetInsights;

/// <summary>
/// Catalog provider summary information
/// </summary>
public class CatalogProvider
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
	/// The number of ASN's covered by the provider
	/// </summary>
	public int AsnsCount { get; set; }

	/// <summary>
	/// The number of countries covered by the provider
	/// </summary>
	public int CountriesCount { get; set; }

	/// <summary>
	/// The number of locations covered by the provider
	/// </summary>
	public int LocationsCount { get; set; }

	/// <summary>
	/// The number of interfaces covered by the provider
	/// </summary>
	public int InterfacesCount { get; set; }

	/// <summary>
	/// Indicates whether the catalog provider is included in the licensed packages
	/// </summary>
	public bool Included { get; set; }
}
