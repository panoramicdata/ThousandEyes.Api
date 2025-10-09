using Refit;
using ThousandEyes.Api.Models.Integrations;

namespace ThousandEyes.Api.Refit.Integrations;

/// <summary>
/// Internal Refit interface for Webhook Operations API
/// </summary>
internal interface IWebhookOperationsRefitApi
{
	/// <summary>
	/// List webhook operations
	/// </summary>
	[Get("/operations/webhooks")]
	Task<WebhookOperations> GetAllAsync(
		[Query] string? aid,
		CancellationToken cancellationToken);

	/// <summary>
	/// Create webhook operation
	/// </summary>
	[Post("/operations/webhooks")]
	Task<WebhookOperation> CreateAsync(
		[Body] WebhookOperation operation,
		[Query] string? aid,
		CancellationToken cancellationToken);

	/// <summary>
	/// Get webhook operation by ID
	/// </summary>
	[Get("/operations/webhooks/{id}")]
	Task<WebhookOperation> GetByIdAsync(
		string id,
		[Query] string? aid,
		CancellationToken cancellationToken);

	/// <summary>
	/// Update webhook operation
	/// </summary>
	[Put("/operations/webhooks/{id}")]
	Task<WebhookOperation> UpdateAsync(
		string id,
		[Body] WebhookOperation operation,
		[Query] string? aid,
		CancellationToken cancellationToken);

	/// <summary>
	/// Delete webhook operation
	/// </summary>
	[Delete("/operations/webhooks/{id}")]
	Task DeleteAsync(
		string id,
		[Query] string? aid,
		CancellationToken cancellationToken);
}
