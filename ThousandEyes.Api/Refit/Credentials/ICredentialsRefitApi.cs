using Refit;
using ThousandEyes.Api.Models.Credentials;

namespace ThousandEyes.Api.Refit.Credentials;

/// <summary>
/// Internal Refit interface for Credentials API operations.
/// </summary>
internal interface ICredentialsRefitApi
{
	/// <summary>
	/// Retrieves a list of credentials configured in ThousandEyes.
	/// </summary>
	/// <param name="aid">Optional account group ID</param>
	/// <param name="cancellationToken">Cancellation token</param>
	[Get("/credentials")]
	Task<Models.Credentials.Credentials> GetAllAsync([Query] string? aid, CancellationToken cancellationToken);

	/// <summary>
	/// Creates a new credential for ThousandEyes transaction tests.
	/// </summary>
	/// <param name="request">The credential request</param>
	/// <param name="aid">Optional account group ID</param>
	/// <param name="cancellationToken">Cancellation token</param>
	[Post("/credentials")]
	Task<CredentialWithoutValue> CreateAsync([Body] CredentialRequest request, [Query] string? aid, CancellationToken cancellationToken);

	/// <summary>
	/// Retrieves detailed information about a credential.
	/// </summary>
	/// <param name="id">Credential ID</param>
	/// <param name="aid">Optional account group ID</param>
	/// <param name="cancellationToken">Cancellation token</param>
	[Get("/credentials/{id}")]
	Task<Credential> GetByIdAsync(string id, [Query] string? aid, CancellationToken cancellationToken);

	/// <summary>
	/// Updates the credential for ThousandEyes transaction tests.
	/// </summary>
	/// <param name="id">Credential ID</param>
	/// <param name="request">The credential request</param>
	/// <param name="aid">Optional account group ID</param>
	/// <param name="cancellationToken">Cancellation token</param>
	[Put("/credentials/{id}")]
	Task<CredentialWithoutValue> UpdateAsync(string id, [Body] CredentialRequest request, [Query] string? aid, CancellationToken cancellationToken);

	/// <summary>
	/// Deletes a ThousandEyes transaction test credential.
	/// </summary>
	/// <param name="id">Credential ID</param>
	/// <param name="aid">Optional account group ID</param>
	/// <param name="cancellationToken">Cancellation token</param>
	[Delete("/credentials/{id}")]
	Task DeleteAsync(string id, [Query] string? aid, CancellationToken cancellationToken);
}
