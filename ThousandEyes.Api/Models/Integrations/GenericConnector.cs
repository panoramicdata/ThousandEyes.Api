using System.Text.Json.Serialization;

namespace ThousandEyes.Api.Models.Integrations;

/// <summary>
/// Generic connector configuration for third-party services
/// </summary>
public class GenericConnector
{
	/// <summary>
	/// Connector ID
	/// </summary>
	public string? Id { get; set; }

	/// <summary>
	/// Connector type
	/// </summary>
	[JsonPropertyName("type")]
	public required ConnectorType TypeValue { get; set; }

	/// <summary>
	/// Connector name
	/// </summary>
	public required string Name { get; set; }

	/// <summary>
	/// Target URL for the connector
	/// </summary>
	public required string Target { get; set; }

	/// <summary>
	/// Authentication configuration
	/// </summary>
	public ConnectorAuthentication? Authentication { get; set; }

	/// <summary>
	/// Date when the connector was last modified
	/// </summary>
	public DateTime? LastModifiedDate { get; set; }

	/// <summary>
	/// Custom HTTP headers
	/// </summary>
	public Header[] Headers { get; set; } = [];
}
