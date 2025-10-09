using ThousandEyes.Api.Interfaces;
using ThousandEyes.Api.Models.Templates;

namespace ThousandEyes.Api;

/// <summary>
/// Implementation of Templates API using Refit
/// </summary>
internal class TemplatesApi(ITemplatesRefitApi refitApi) : ITemplatesApi
{
	private readonly ITemplatesRefitApi _refitApi = refitApi;

	/// <inheritdoc />
	public Task<Templates> GetAllAsync(
		string? aid, 
		CertificationLevel? certificationLevel, 
		TemplateModule? templateModule, 
		string? name, 
		CancellationToken cancellationToken) =>
		_refitApi.GetAllAsync(aid, certificationLevel, templateModule, name, cancellationToken);

	/// <inheritdoc />
	public Task<TemplateResponse> GetByIdAsync(string id, string? aid, CancellationToken cancellationToken) =>
		_refitApi.GetByIdAsync(id, aid, cancellationToken);

	/// <inheritdoc />
	public Task<TemplateResponse> CreateAsync(Template request, string? aid, CancellationToken cancellationToken) =>
		_refitApi.CreateAsync(request, aid, cancellationToken);

	/// <inheritdoc />
	public Task<TemplateResponse> UpdateAsync(string id, Template request, string? aid, CancellationToken cancellationToken) =>
		_refitApi.UpdateAsync(id, request, aid, cancellationToken);

	/// <inheritdoc />
	public Task DeleteAsync(string id, string? aid, CancellationToken cancellationToken) =>
		_refitApi.DeleteAsync(id, aid, cancellationToken);

	/// <inheritdoc />
	public Task<TemplateResponse> DeployAsync(string id, DeployTemplate request, string? aid, CancellationToken cancellationToken) =>
		_refitApi.DeployAsync(id, request, aid, cancellationToken);

	/// <inheritdoc />
	public Task<SharingSettingsResponse> GetSharingSettingsAsync(string id, string? aid, CancellationToken cancellationToken) =>
		_refitApi.GetSharingSettingsAsync(id, aid, cancellationToken);

	/// <inheritdoc />
	public Task<SharingSettingsResponse> UpdateSharingSettingsAsync(string id, SharingSettings request, string? aid, CancellationToken cancellationToken) =>
		_refitApi.UpdateSharingSettingsAsync(id, request, aid, cancellationToken);
}