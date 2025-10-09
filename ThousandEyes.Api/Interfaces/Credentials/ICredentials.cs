using ThousandEyes.Api.Models.Credentials;

namespace ThousandEyes.Api.Interfaces.Credentials;

/// <summary>
/// Interface for managing credentials for transaction tests.
/// </summary>
public interface ICredentials
{
	/// <summary>
	/// Retrieves a list of credentials configured in ThousandEyes.
	/// Users have access to the list of credentials based on the default settings or the specified account ID.
	/// </summary>
	/// <param name="aid">A unique identifier associated with your account group. Optional.</param>
	/// <param name="cancellationToken">Cancellation token</param>
	/// <returns>A list of credentials</returns>
	Task<Models.Credentials.Credentials> GetAllAsync(string? aid, CancellationToken cancellationToken);

	/// <summary>
	/// Creates a new credential for ThousandEyes transaction tests.
	/// Requires "Settings Tests Update" and "Settings Tests Create Transaction (Tx) Tests" permissions.
	/// </summary>
	/// <param name="request">The credential request containing name and value</param>
	/// <param name="aid">A unique identifier associated with your account group. Optional.</param>
	/// <param name="cancellationToken">Cancellation token</param>
	/// <returns>The created credential (without the encrypted value)</returns>
	Task<CredentialWithoutValue> CreateAsync(CredentialRequest request, string? aid, CancellationToken cancellationToken);

	/// <summary>
	/// Retrieves detailed information about a ThousandEyes transaction test credential.
	/// Requires "View sensitive data in web transaction scripts" permission to view the encrypted value.
	/// </summary>
	/// <param name="id">The ID of the desired credential</param>
	/// <param name="aid">A unique identifier associated with your account group. Optional.</param>
	/// <param name="cancellationToken">Cancellation token</param>
	/// <returns>The credential with encrypted value (if permission granted)</returns>
	Task<Credential> GetByIdAsync(string id, string? aid, CancellationToken cancellationToken);

	/// <summary>
	/// Updates the credential for ThousandEyes transaction tests.
	/// Requires "Settings Tests Update" permission.
	/// </summary>
	/// <param name="id">The ID of the credential to update</param>
	/// <param name="request">The credential request containing updated name and value</param>
	/// <param name="aid">A unique identifier associated with your account group. Optional.</param>
	/// <param name="cancellationToken">Cancellation token</param>
	/// <returns>The updated credential (without the encrypted value)</returns>
	Task<CredentialWithoutValue> UpdateAsync(string id, CredentialRequest request, string? aid, CancellationToken cancellationToken);

	/// <summary>
	/// Deletes a ThousandEyes transaction test credential.
	/// Requires "Settings Tests Update" permission.
	/// </summary>
	/// <param name="id">The ID of the credential to delete</param>
	/// <param name="aid">A unique identifier associated with your account group. Optional.</param>
	/// <param name="cancellationToken">Cancellation token</param>
	Task DeleteAsync(string id, string? aid, CancellationToken cancellationToken);
}
