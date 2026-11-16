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
	public async Task<Models.Credentials.Credentials> GetAllAsync(string? aid, CancellationToken cancel)
		=> await refitApi.GetAllAsync(aid, cancel).ConfigureAwait(false);

	/// <inheritdoc/>
	public async Task<CredentialWithoutValue> CreateAsync(CredentialRequest request, string? aid, CancellationToken cancel)
		=> await refitApi.CreateAsync(request, aid, cancel).ConfigureAwait(false);

	/// <inheritdoc/>
	public async Task<Credential> GetByIdAsync(string id, string? aid, CancellationToken cancel)
		=> await refitApi.GetByIdAsync(id, aid, cancel).ConfigureAwait(false);

	/// <inheritdoc/>
	public async Task<CredentialWithoutValue> UpdateAsync(string id, CredentialRequest request, string? aid, CancellationToken cancel)
		=> await refitApi.UpdateAsync(id, request, aid, cancel).ConfigureAwait(false);

	/// <inheritdoc/>
	public async Task DeleteAsync(string id, string? aid, CancellationToken cancel)
		=> await refitApi.DeleteAsync(id, aid, cancel).ConfigureAwait(false);
}
