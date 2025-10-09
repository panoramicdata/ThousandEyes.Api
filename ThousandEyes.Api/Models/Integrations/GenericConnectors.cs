using System.Text.Json.Serialization;

namespace ThousandEyes.Api.Models.Integrations;

/// <summary>
/// Response containing list of generic connectors
/// </summary>
public class GenericConnectors : ApiResource
{
	/// <summary>
	/// List of generic connectors
	/// </summary>
	[JsonPropertyName("items")]
	public GenericConnector[] ConnectorsList { get; set; } = [];
}
