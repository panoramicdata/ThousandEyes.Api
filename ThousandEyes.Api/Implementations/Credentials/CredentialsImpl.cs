using ThousandEyes.Api.Interfaces.Credentials;
using ThousandEyes.Api.Models.Credentials;
using ThousandEyes.Api.Refit.Credentials;

namespace ThousandEyes.Api.Implementations.Credentials;

/// <summary>
/// Implementation of <see cref="ICredentials"/> for managing transaction test credentials.
/// </summary>
internal class CredentialsImpl(ICredentialsRefitApi refitApi) : ICredentials
{
	/// <inheritdoc/>
	public async Task<Models.Credentials.Credentials> GetAllAsync(string? aid, CancellationToken cancellationToken)
		=> await refitApi.GetAllAsync(aid, cancellationToken).ConfigureAwait(false);

	/// <inheritdoc/>
	public async Task<CredentialWithoutValue> CreateAsync(CredentialRequest request, string? aid, CancellationToken cancellationToken)
		=> await refitApi.CreateAsync(request, aid, cancellationToken).ConfigureAwait(false);

	/// <inheritdoc/>
	public async Task<Credential> GetByIdAsync(string id, string? aid, CancellationToken cancellationToken)
		=> await refitApi.GetByIdAsync(id, aid, cancellationToken).ConfigureAwait(false);

	/// <inheritdoc/>
	public async Task<CredentialWithoutValue> UpdateAsync(string id, CredentialRequest request, string? aid, CancellationToken cancellationToken)
		=> await refitApi.UpdateAsync(id, request, aid, cancellationToken).ConfigureAwait(false);

	/// <inheritdoc/>
	public async Task DeleteAsync(string id, string? aid, CancellationToken cancellationToken)
		=> await refitApi.DeleteAsync(id, aid, cancellationToken).ConfigureAwait(false);
}
