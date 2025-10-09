using Refit;
using ThousandEyes.Api.Implementations.Integrations;
using ThousandEyes.Api.Interfaces.Integrations;
using ThousandEyes.Api.Refit.Integrations;

namespace ThousandEyes.Api.Modules;

/// <summary>
/// Integrations API module for webhook and third-party service integrations
/// </summary>
/// <remarks>
/// ? Phase 5.1 - IMPLEMENTED
/// Integration capabilities including:
/// - Webhook operation management
/// - Generic connector configuration (Slack, PagerDuty, etc.)
/// - Operation-connector assignments
/// - Multiple authentication types (Basic, Bearer, OAuth)
/// </remarks>
public class IntegrationsModule
{
	/// <summary>
	/// Gets the Webhook Operations interface
	/// </summary>
	public IWebhookOperations WebhookOperations { get; }

	/// <summary>
	/// Gets the Generic Connectors interface
	/// </summary>
	public IGenericConnectors GenericConnectors { get; }

	/// <summary>
	/// Gets the Operation Connectors interface
	/// </summary>
	public IOperationConnectors OperationConnectors { get; }

	/// <summary>
	/// Initializes a new instance of the IntegrationsModule
	/// </summary>
	/// <param name="httpClient">The HTTP client to use for API calls</param>
	/// <param name="refitSettings">Refit settings for JSON serialization</param>
	public IntegrationsModule(HttpClient httpClient, RefitSettings refitSettings)
	{
		var webhookOpsApi = RestService.For<IWebhookOperationsRefitApi>(httpClient, refitSettings);
		WebhookOperations = new WebhookOperationsImpl(webhookOpsApi);

		var genericConnectorsApi = RestService.For<IGenericConnectorsRefitApi>(httpClient, refitSettings);
		GenericConnectors = new GenericConnectorsImpl(genericConnectorsApi);

		var operationConnectorsApi = RestService.For<IOperationConnectorsRefitApi>(httpClient, refitSettings);
		OperationConnectors = new OperationConnectorsImpl(operationConnectorsApi);
	}
}
