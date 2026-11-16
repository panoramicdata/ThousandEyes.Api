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
		CancellationToken token) =>
		_refitApi.GetAllAsync(aid, certificationLevel, templateModule, name, token);

	/// <inheritdoc />
	public Task<TemplateResponse> GetByIdAsync(string id, string? aid, CancellationToken token) =>
		_refitApi.GetByIdAsync(id, aid, token);

	/// <inheritdoc />
	public Task<TemplateResponse> CreateAsync(Template request, string? aid, CancellationToken token) =>
		_refitApi.CreateAsync(request, aid, token);

	/// <inheritdoc />
	public Task<TemplateResponse> UpdateAsync(string id, Template request, string? aid, CancellationToken token) =>
		_refitApi.UpdateAsync(id, request, aid, token);

	/// <inheritdoc />
	public Task DeleteAsync(string id, string? aid, CancellationToken token) =>
		_refitApi.DeleteAsync(id, aid, token);

	/// <inheritdoc />
	public Task<TemplateResponse> DeployAsync(string id, DeployTemplate request, string? aid, CancellationToken token) =>
		_refitApi.DeployAsync(id, request, aid, token);

	/// <inheritdoc />
	public Task<SharingSettingsResponse> GetSharingSettingsAsync(string id, string? aid, CancellationToken token) =>
		_refitApi.GetSharingSettingsAsync(id, aid, token);

	/// <inheritdoc />
	public Task<SharingSettingsResponse> UpdateSharingSettingsAsync(string id, SharingSettings request, string? aid, CancellationToken token) =>
		_refitApi.UpdateSharingSettingsAsync(id, request, aid, token);
}