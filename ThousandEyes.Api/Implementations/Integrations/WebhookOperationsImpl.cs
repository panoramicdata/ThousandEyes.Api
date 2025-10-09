using ThousandEyes.Api.Interfaces.Integrations;
using ThousandEyes.Api.Models.Integrations;
using ThousandEyes.Api.Refit.Integrations;

namespace ThousandEyes.Api.Implementations.Integrations;

/// <summary>
/// Implementation of Webhook Operations
/// </summary>
internal class WebhookOperationsImpl(IWebhookOperationsRefitApi refitApi) : IWebhookOperations
{
	private readonly IWebhookOperationsRefitApi _refitApi = refitApi;

	public async Task<WebhookOperations> GetAllAsync(
		string? aid,
		CancellationToken cancellationToken)
		=> await _refitApi.GetAllAsync(aid, cancellationToken);

	public async Task<WebhookOperation> CreateAsync(
		WebhookOperation operation,
		string? aid,
		CancellationToken cancellationToken)
		=> await _refitApi.CreateAsync(operation, aid, cancellationToken);

	public async Task<WebhookOperation> GetByIdAsync(
		string id,
		string? aid,
		CancellationToken cancellationToken)
		=> await _refitApi.GetByIdAsync(id, aid, cancellationToken);

	public async Task<WebhookOperation> UpdateAsync(
		string id,
		WebhookOperation operation,
		string? aid,
		CancellationToken cancellationToken)
		=> await _refitApi.UpdateAsync(id, operation, aid, cancellationToken);

	public async Task DeleteAsync(
		string id,
		string? aid,
		CancellationToken cancellationToken)
		=> await _refitApi.DeleteAsync(id, aid, cancellationToken);
}
