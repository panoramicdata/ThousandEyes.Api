using ThousandEyes.Api.Interfaces.Integrations;
using ThousandEyes.Api.Models.Integrations;
using ThousandEyes.Api.Refit.Integrations;

namespace ThousandEyes.Api.Implementations.Integrations;

/// <summary>
/// Implementation of Operation-Connector assignments
/// </summary>
internal class OperationConnectorsImpl(IOperationConnectorsRefitApi refitApi) : IOperationConnectors
{
	private readonly IOperationConnectorsRefitApi _refitApi = refitApi;

	public async Task<Assignments> GetConnectorsAsync(
		string type,
		string id,
		string? aid,
		CancellationToken cancellationToken)
		=> await _refitApi.GetConnectorsAsync(type, id, aid, cancellationToken);

	public async Task<Assignments> SetConnectorsAsync(
		string type,
		string id,
		string[] connectorIds,
		string? aid,
		CancellationToken cancellationToken)
		=> await _refitApi.SetConnectorsAsync(type, id, connectorIds, aid, cancellationToken);
}
