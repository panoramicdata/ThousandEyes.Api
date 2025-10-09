using Refit;
using ThousandEyes.Api.Models.Templates;

namespace ThousandEyes.Api.Interfaces;

/// <summary>
/// Internal Refit interface for Templates API
/// </summary>
internal interface ITemplatesRefitApi
{
	/// <summary>
	/// Get all templates
	/// </summary>
	[Get("/templates")]
	Task<Templates> GetAllAsync(
		[Query] string? aid, 
		[Query] CertificationLevel? certificationLevel, 
		[Query("module")] TemplateModule? templateModule, 
		[Query] string? name, 
		CancellationToken cancellationToken);

	/// <summary>
	/// Get template by ID
	/// </summary>
	[Get("/templates/{id}")]
	Task<TemplateResponse> GetByIdAsync(string id, [Query] string? aid, CancellationToken cancellationToken);

	/// <summary>
	/// Create template
	/// </summary>
	[Post("/templates")]
	Task<TemplateResponse> CreateAsync([Body] Template request, [Query] string? aid, CancellationToken cancellationToken);

	/// <summary>
	/// Update template
	/// </summary>
	[Put("/templates/{id}")]
	Task<TemplateResponse> UpdateAsync(string id, [Body] Template request, [Query] string? aid, CancellationToken cancellationToken);

	/// <summary>
	/// Delete template
	/// </summary>
	[Delete("/templates/{id}")]
	Task DeleteAsync(string id, [Query] string? aid, CancellationToken cancellationToken);

	/// <summary>
	/// Deploy template
	/// </summary>
	[Post("/templates/{id}/deploy")]
	Task<TemplateResponse> DeployAsync(string id, [Body] DeployTemplate request, [Query] string? aid, CancellationToken cancellationToken);

	/// <summary>
	/// Get sharing settings
	/// </summary>
	[Get("/templates/{id}/sharing-settings")]
	Task<SharingSettingsResponse> GetSharingSettingsAsync(string id, [Query] string? aid, CancellationToken cancellationToken);

	/// <summary>
	/// Update sharing settings
	/// </summary>
	[Put("/templates/{id}/sharing-settings")]
	Task<SharingSettingsResponse> UpdateSharingSettingsAsync(string id, [Body] SharingSettings request, [Query] string? aid, CancellationToken cancellationToken);
}