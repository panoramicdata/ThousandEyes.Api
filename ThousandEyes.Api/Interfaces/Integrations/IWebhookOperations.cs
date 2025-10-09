using ThousandEyes.Api.Models.Integrations;

namespace ThousandEyes.Api.Interfaces.Integrations;

/// <summary>
/// Public interface for Webhook Operations
/// </summary>
public interface IWebhookOperations
{
	/// <summary>
	/// Returns a list of webhook operations in the specified account group.
	/// </summary>
	/// <param name="aid">Optional account group ID</param>
	/// <param name="cancellationToken">Cancellation token</param>
	/// <returns>List of webhook operations</returns>
	Task<WebhookOperations> GetAllAsync(
		string? aid,
		CancellationToken cancellationToken);

	/// <summary>
	/// Creates a new webhook operation.
	/// </summary>
	/// <param name="operation">Webhook operation to create</param>
	/// <param name="aid">Optional account group ID</param>
	/// <param name="cancellationToken">Cancellation token</param>
	/// <returns>Created webhook operation</returns>
	Task<WebhookOperation> CreateAsync(
		WebhookOperation operation,
		string? aid,
		CancellationToken cancellationToken);

	/// <summary>
	/// Retrieves details of a webhook operation by its ID.
	/// </summary>
	/// <param name="id">Operation ID</param>
	/// <param name="aid">Optional account group ID</param>
	/// <param name="cancellationToken">Cancellation token</param>
	/// <returns>Webhook operation details</returns>
	Task<WebhookOperation> GetByIdAsync(
		string id,
		string? aid,
		CancellationToken cancellationToken);

	/// <summary>
	/// Updates the webhook operation specified by ID.
	/// </summary>
	/// <param name="id">Operation ID</param>
	/// <param name="operation">Updated webhook operation</param>
	/// <param name="aid">Optional account group ID</param>
	/// <param name="cancellationToken">Cancellation token</param>
	/// <returns>Updated webhook operation</returns>
	Task<WebhookOperation> UpdateAsync(
		string id,
		WebhookOperation operation,
		string? aid,
		CancellationToken cancellationToken);

	/// <summary>
	/// Deletes the webhook operation specified by ID.
	/// </summary>
	/// <param name="id">Operation ID</param>
	/// <param name="aid">Optional account group ID</param>
	/// <param name="cancellationToken">Cancellation token</param>
	Task DeleteAsync(
		string id,
		string? aid,
		CancellationToken cancellationToken);
}
