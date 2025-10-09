using System.Text.Json.Serialization;

namespace ThousandEyes.Api.Models.Integrations;

/// <summary>
/// Webhook operation configuration
/// </summary>
public class WebhookOperation
{
	/// <summary>
	/// Operation ID
	/// </summary>
	public string? Id { get; set; }

	/// <summary>
	/// Operation name
	/// </summary>
	public required string Name { get; set; }

	/// <summary>
	/// Whether the operation is enabled
	/// </summary>
	public bool Enabled { get; set; }

	/// <summary>
	/// Operation category
	/// </summary>
	[JsonPropertyName("category")]
	public required OperationCategory CategoryValue { get; set; }

	/// <summary>
	/// Operation status
	/// </summary>
	[JsonPropertyName("status")]
	public required OperationStatus StatusValue { get; set; }

	/// <summary>
	/// Custom path for the webhook
	/// </summary>
	public string? Path { get; set; }

	/// <summary>
	/// Handlebars template for the payload
	/// </summary>
	public string? Payload { get; set; }

	/// <summary>
	/// Custom HTTP headers
	/// </summary>
	public Header[] Headers { get; set; } = [];

	/// <summary>
	/// Handlebars template for query parameters (must be valid JSON)
	/// </summary>
	public string? QueryParams { get; set; }

	/// <summary>
	/// Operation type
	/// </summary>
	[JsonPropertyName("type")]
	public OperationType? TypeValue { get; set; }
}
