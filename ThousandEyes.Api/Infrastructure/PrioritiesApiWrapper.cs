using ThousandEyes.Api.Interfaces;
using ThousandEyes.Api.Models.Priorities;

namespace ThousandEyes.Api.Infrastructure;

/// <summary>
/// Wrapper for the Priorities API that provides convenience methods
/// </summary>
/// <param name="prioritiesApi">The underlying Priorities API</param>
public class PrioritiesApiWrapper(IPrioritiesApi prioritiesApi) : IPrioritiesApi
{
	/// <summary>
	/// Gets all priorities as a convenient list
	/// </summary>
	/// <param name="cancellationToken">Cancellation token</param>
	/// <returns>List of priorities</returns>
	public async Task<IReadOnlyList<Priority>> GetAllAsync(CancellationToken cancellationToken)
	{
		var response = await prioritiesApi.GetAllResponseAsync(cancellationToken);
		return response.Priorities;
	}

	/// <inheritdoc />
	public Task<PrioritiesResponse> GetAllResponseAsync(CancellationToken cancellationToken)
		=> prioritiesApi.GetAllResponseAsync(cancellationToken);

	/// <inheritdoc />
	public Task<Priority> GetByIdAsync(int id, bool includeDetails, CancellationToken cancellationToken)
		=> prioritiesApi.GetByIdAsync(id, includeDetails, cancellationToken);

	/// <summary>
	/// Gets a specific priority by ID with default parameters
	/// </summary>
	/// <param name="id">The priority ID</param>
	/// <param name="cancellationToken">Cancellation token</param>
	/// <returns>The priority with the specified ID</returns>
	public Task<Priority> GetByIdAsync(int id, CancellationToken cancellationToken)
		=> GetByIdAsync(id, includeDetails: false, cancellationToken);
}