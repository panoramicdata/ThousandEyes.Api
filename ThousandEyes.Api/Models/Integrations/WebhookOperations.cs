using System.Text.Json.Serialization;

namespace ThousandEyes.Api.Models.Integrations;

/// <summary>
/// Response containing list of webhook operations
/// </summary>
public class WebhookOperations : ApiResource
{
	/// <summary>
	/// List of webhook operations
	/// </summary>
	[JsonPropertyName("items")]
	public WebhookOperation[] OperationsList { get; set; } = [];
}
