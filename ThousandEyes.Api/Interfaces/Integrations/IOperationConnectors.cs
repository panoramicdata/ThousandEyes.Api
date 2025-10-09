using ThousandEyes.Api.Models.Integrations;

namespace ThousandEyes.Api.Interfaces.Integrations;

/// <summary>
/// Public interface for Operation-Connector assignments
/// </summary>
public interface IOperationConnectors
{
	/// <summary>
	/// Returns a list of connectors assigned to a specific operation.
	/// </summary>
	/// <param name="type">Operation type (e.g., "webhooks")</param>
	/// <param name="id">Operation ID</param>
	/// <param name="aid">Optional account group ID</param>
	/// <param name="cancellationToken">Cancellation token</param>
	/// <returns>List of assigned connector IDs</returns>
	Task<Assignments> GetConnectorsAsync(
		string type,
		string id,
		string? aid,
		CancellationToken cancellationToken);

	/// <summary>
	/// Assigns one or more connectors to an operation. This replaces any existing assignments.
	/// </summary>
	/// <param name="type">Operation type (e.g., "webhooks")</param>
	/// <param name="id">Operation ID</param>
	/// <param name="connectorIds">List of connector IDs to assign (max 1)</param>
	/// <param name="aid">Optional account group ID</param>
	/// <param name="cancellationToken">Cancellation token</param>
	/// <returns>Updated assignments</returns>
	Task<Assignments> SetConnectorsAsync(
		string type,
		string id,
		string[] connectorIds,
		string? aid,
		CancellationToken cancellationToken);
}
