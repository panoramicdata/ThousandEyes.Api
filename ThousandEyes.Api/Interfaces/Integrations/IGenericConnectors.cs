using ThousandEyes.Api.Models.Integrations;

namespace ThousandEyes.Api.Interfaces.Integrations;

/// <summary>
/// Public interface for Generic Connectors
/// </summary>
public interface IGenericConnectors
{
	/// <summary>
	/// Returns a list of connectors in the specified account group.
	/// </summary>
	/// <param name="aid">Optional account group ID</param>
	/// <param name="cancellationToken">Cancellation token</param>
	/// <returns>List of generic connectors</returns>
	Task<GenericConnectors> GetAllAsync(
		string? aid,
		CancellationToken cancellationToken);

	/// <summary>
	/// Creates a new connector.
	/// </summary>
	/// <param name="connector">Connector to create</param>
	/// <param name="aid">Optional account group ID</param>
	/// <param name="cancellationToken">Cancellation token</param>
	/// <returns>Created connector</returns>
	Task<GenericConnector> CreateAsync(
		GenericConnector connector,
		string? aid,
		CancellationToken cancellationToken);

	/// <summary>
	/// Retrieves details of a connector by its ID.
	/// </summary>
	/// <param name="id">Connector ID</param>
	/// <param name="aid">Optional account group ID</param>
	/// <param name="cancellationToken">Cancellation token</param>
	/// <returns>Connector details</returns>
	Task<GenericConnector> GetByIdAsync(
		string id,
		string? aid,
		CancellationToken cancellationToken);

	/// <summary>
	/// Updates the connector specified by ID.
	/// </summary>
	/// <param name="id">Connector ID</param>
	/// <param name="connector">Updated connector</param>
	/// <param name="aid">Optional account group ID</param>
	/// <param name="cancellationToken">Cancellation token</param>
	/// <returns>Updated connector</returns>
	Task<GenericConnector> UpdateAsync(
		string id,
		GenericConnector connector,
		string? aid,
		CancellationToken cancellationToken);

	/// <summary>
	/// Deletes the connector specified by ID.
	/// </summary>
	/// <param name="id">Connector ID</param>
	/// <param name="aid">Optional account group ID</param>
	/// <param name="cancellationToken">Cancellation token</param>
	Task DeleteAsync(
		string id,
		string? aid,
		CancellationToken cancellationToken);

	/// <summary>
	/// Returns a list of operation IDs assigned to a connector.
	/// </summary>
	/// <param name="id">Connector ID</param>
	/// <param name="aid">Optional account group ID</param>
	/// <param name="cancellationToken">Cancellation token</param>
	/// <returns>List of assigned operation IDs</returns>
	Task<Assignments> GetOperationsAsync(
		string id,
		string? aid,
		CancellationToken cancellationToken);

	/// <summary>
	/// Assigns operations to a connector. This replaces any existing assignments.
	/// </summary>
	/// <param name="id">Connector ID</param>
	/// <param name="operationIds">List of operation IDs to assign</param>
	/// <param name="aid">Optional account group ID</param>
	/// <param name="cancellationToken">Cancellation token</param>
	/// <returns>Updated assignments</returns>
	Task<Assignments> SetOperationsAsync(
		string id,
		string[] operationIds,
		string? aid,
		CancellationToken cancellationToken);
}
