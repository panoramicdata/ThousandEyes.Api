using ThousandEyes.Api.Models.Templates;

namespace ThousandEyes.Api.Interfaces;

/// <summary>
/// Interface for Templates API operations
/// </summary>
/// <remarks>
/// Phase 6.3 implementation - Template management and deployment
/// Templates provide a streamlined approach to creating multiple tests, alert rules, 
/// dashboards, and other assets within ThousandEyes from a single configuration file.
/// </remarks>
public interface ITemplatesApi
{
	/// <summary>
	/// Get all templates with optional filtering
	/// </summary>
	/// <param name="aid">Account group ID (optional)</param>
	/// <param name="certificationLevel">Filter by certification level (optional)</param>
	/// <param name="templateModule">Filter by module (optional)</param>
	/// <param name="name">Filter by template name (optional)</param>
	/// <param name="cancellationToken">Cancellation token</param>
	/// <returns>List of templates</returns>
	Task<Templates> GetAllAsync(
		string? aid, 
		CertificationLevel? certificationLevel, 
		TemplateModule? templateModule, 
		string? name, 
		CancellationToken cancellationToken);

	/// <summary>
	/// Get a specific template by ID
	/// </summary>
	/// <param name="id">Template ID</param>
	/// <param name="aid">Account group ID (optional)</param>
	/// <param name="cancellationToken">Cancellation token</param>
	/// <returns>Template details</returns>
	Task<TemplateResponse> GetByIdAsync(string id, string? aid, CancellationToken cancellationToken);

	/// <summary>
	/// Create a new template
	/// </summary>
	/// <param name="request">Template configuration</param>
	/// <param name="aid">Account group ID (optional)</param>
	/// <param name="cancellationToken">Cancellation token</param>
	/// <returns>Created template</returns>
	Task<TemplateResponse> CreateAsync(Template request, string? aid, CancellationToken cancellationToken);

	/// <summary>
	/// Update an existing template
	/// </summary>
	/// <param name="id">Template ID</param>
	/// <param name="request">Updated template configuration</param>
	/// <param name="aid">Account group ID (optional)</param>
	/// <param name="cancellationToken">Cancellation token</param>
	/// <returns>Updated template</returns>
	Task<TemplateResponse> UpdateAsync(string id, Template request, string? aid, CancellationToken cancellationToken);

	/// <summary>
	/// Delete a template
	/// </summary>
	/// <param name="id">Template ID</param>
	/// <param name="aid">Account group ID (optional)</param>
	/// <param name="cancellationToken">Cancellation token</param>
	Task DeleteAsync(string id, string? aid, CancellationToken cancellationToken);

	/// <summary>
	/// Deploy a template (creates assets from template)
	/// </summary>
	/// <param name="id">Template ID</param>
	/// <param name="request">Deploy configuration with user inputs</param>
	/// <param name="aid">Account group ID (optional)</param>
	/// <param name="cancellationToken">Cancellation token</param>
	/// <returns>Deployment response</returns>
	Task<TemplateResponse> DeployAsync(string id, DeployTemplate request, string? aid, CancellationToken cancellationToken);

	/// <summary>
	/// Get sharing settings for a template
	/// </summary>
	/// <param name="id">Template ID</param>
	/// <param name="aid">Account group ID (optional)</param>
	/// <param name="cancellationToken">Cancellation token</param>
	/// <returns>Sharing settings</returns>
	Task<SharingSettingsResponse> GetSharingSettingsAsync(string id, string? aid, CancellationToken cancellationToken);

	/// <summary>
	/// Update sharing settings for a template
	/// </summary>
	/// <param name="id">Template ID</param>
	/// <param name="request">Updated sharing settings</param>
	/// <param name="aid">Account group ID (optional)</param>
	/// <param name="cancellationToken">Cancellation token</param>
	/// <returns>Updated sharing settings</returns>
	Task<SharingSettingsResponse> UpdateSharingSettingsAsync(string id, SharingSettings request, string? aid, CancellationToken cancellationToken);
}