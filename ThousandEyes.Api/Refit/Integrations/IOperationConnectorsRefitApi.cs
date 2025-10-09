using Refit;
using ThousandEyes.Api.Models.Integrations;

namespace ThousandEyes.Api.Refit.Integrations;

/// <summary>
/// Internal Refit interface for Operation-Connector assignments API
/// </summary>
internal interface IOperationConnectorsRefitApi
{
	/// <summary>
	/// Get connectors assigned to operation
	/// </summary>
	[Get("/operations/{type}/{id}/connectors")]
	Task<Assignments> GetConnectorsAsync(
		string type,
		string id,
		[Query] string? aid,
		CancellationToken cancellationToken);

	/// <summary>
	/// Set connectors for operation
	/// </summary>
	[Put("/operations/{type}/{id}/connectors")]
	Task<Assignments> SetConnectorsAsync(
		string type,
		string id,
		[Body] string[] connectorIds,
		[Query] string? aid,
		CancellationToken cancellationToken);
}
