using ThousandEyes.Api.Interfaces.Integrations;
using ThousandEyes.Api.Models.Integrations;
using ThousandEyes.Api.Refit.Integrations;

namespace ThousandEyes.Api.Implementations.Integrations;

/// <summary>
/// Implementation of Generic Connectors
/// </summary>
internal class GenericConnectorsImpl(IGenericConnectorsRefitApi refitApi) : IGenericConnectors
{
	private readonly IGenericConnectorsRefitApi _refitApi = refitApi;

	public async Task<GenericConnectors> GetAllAsync(
		string? aid,
		CancellationToken cancellationToken)
		=> await _refitApi.GetAllAsync(aid, cancellationToken);

	public async Task<GenericConnector> CreateAsync(
		GenericConnector connector,
		string? aid,
		CancellationToken cancellationToken)
		=> await _refitApi.CreateAsync(connector, aid, cancellationToken);

	public async Task<GenericConnector> GetByIdAsync(
		string id,
		string? aid,
		CancellationToken cancellationToken)
		=> await _refitApi.GetByIdAsync(id, aid, cancellationToken);

	public async Task<GenericConnector> UpdateAsync(
		string id,
		GenericConnector connector,
		string? aid,
		CancellationToken cancellationToken)
		=> await _refitApi.UpdateAsync(id, connector, aid, cancellationToken);

	public async Task DeleteAsync(
		string id,
		string? aid,
		CancellationToken cancellationToken)
		=> await _refitApi.DeleteAsync(id, aid, cancellationToken);

	public async Task<Assignments> GetOperationsAsync(
		string id,
		string? aid,
		CancellationToken cancellationToken)
		=> await _refitApi.GetOperationsAsync(id, aid, cancellationToken);

	public async Task<Assignments> SetOperationsAsync(
		string id,
		string[] operationIds,
		string? aid,
		CancellationToken cancellationToken)
		=> await _refitApi.SetOperationsAsync(id, operationIds, aid, cancellationToken);
}
