using Refit;
using ThousandEyes.Api.Models.Integrations;

namespace ThousandEyes.Api.Refit.Integrations;

/// <summary>
/// Internal Refit interface for Generic Connectors API
/// </summary>
internal interface IGenericConnectorsRefitApi
{
	/// <summary>
	/// List connectors
	/// </summary>
	[Get("/connectors/generic")]
	Task<GenericConnectors> GetAllAsync(
		[Query] string? aid,
		CancellationToken ct);

	/// <summary>
	/// Create connector
	/// </summary>
	[Post("/connectors/generic")]
	Task<GenericConnector> CreateAsync(
		[Body] GenericConnector connector,
		[Query] string? aid,
		CancellationToken ct);

	/// <summary>
	/// Get connector by ID
	/// </summary>
	[Get("/connectors/generic/{id}")]
	Task<GenericConnector> GetByIdAsync(
		string id,
		[Query] string? aid,
		CancellationToken ct);

	/// <summary>
	/// Update connector
	/// </summary>
	[Put("/connectors/generic/{id}")]
	Task<GenericConnector> UpdateAsync(
		string id,
		[Body] GenericConnector connector,
		[Query] string? aid,
		CancellationToken ct);

	/// <summary>
	/// Delete connector
	/// </summary>
	[Delete("/connectors/generic/{id}")]
	Task DeleteAsync(
		string id,
		[Query] string? aid,
		CancellationToken ct);

	/// <summary>
	/// List operations assigned to connector
	/// </summary>
	[Get("/connectors/generic/{id}/operations")]
	Task<Assignments> GetOperationsAsync(
		string id,
		[Query] string? aid,
		CancellationToken ct);

	/// <summary>
	/// Set operations for connector
	/// </summary>
	[Put("/connectors/generic/{id}/operations")]
	Task<Assignments> SetOperationsAsync(
		string id,
		[Body] string[] operationIds,
		[Query] string? aid,
		CancellationToken ct);
}
