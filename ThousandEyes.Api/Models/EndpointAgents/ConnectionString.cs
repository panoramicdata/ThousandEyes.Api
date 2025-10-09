using System.Text.Json.Serialization;

namespace ThousandEyes.Api.Models.EndpointAgents;

/// <summary>
/// Connection string for endpoint agent installation
/// </summary>
public class ConnectionString : ApiResource
{
	/// <summary>
	/// The connection string used for integrations and client types
	/// </summary>
	[JsonPropertyName("connectionString")]
	public string? ConnectionStringValue { get; set; }
}