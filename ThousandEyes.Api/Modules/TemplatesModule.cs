using Refit;
using ThousandEyes.Api.Interfaces;

namespace ThousandEyes.Api.Modules;

/// <summary>
/// Templates API module for template management and deployment
/// </summary>
/// <remarks>
/// Phase 6.3 implementation - Complete template management functionality
/// Templates provide a streamlined approach to creating multiple tests, alert rules, 
/// dashboards, and other assets within ThousandEyes from a single configuration file.
/// Essential for infrastructure as code and automated monitoring setup.
/// </remarks>
public class TemplatesModule
{
	/// <summary>
	/// Gets the Templates API for template management and deployment
	/// </summary>
	public ITemplatesApi Templates { get; }

	/// <summary>
	/// Initializes a new instance of the TemplatesModule
	/// </summary>
	/// <param name="httpClient">The HTTP client to use for API calls</param>
	/// <param name="refitSettings">Refit settings for JSON serialization</param>
	public TemplatesModule(HttpClient httpClient, RefitSettings refitSettings)
	{
		// Create Refit API interface
		var templatesRefitApi = RestService.For<ITemplatesRefitApi>(httpClient, refitSettings);

		// Initialize API implementation
		Templates = new TemplatesApi(templatesRefitApi);
	}
}