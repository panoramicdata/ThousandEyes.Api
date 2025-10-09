using System.Text.Json;
using System.Text.Json.Serialization;

namespace ThousandEyes.Api.Models.Templates;

/// <summary>
/// The template configuration.
/// Templates provide a streamlined approach to creating multiple tests, alert rules, dashboards, and other assets
/// within ThousandEyes from a single configuration file.
/// </summary>
public class Template
{
	/// <summary>
	/// The name of the template
	/// </summary>
	[JsonPropertyName("name")]
	public required string Name { get; set; }

	/// <summary>
	/// Text that describes the template
	/// </summary>
	[JsonPropertyName("description")]
	public string? Description { get; set; }

	/// <summary>
	/// Icon that will be used when displaying the template in UI.
	/// If none supplied, will display thousandeyes icon.
	/// Supported values: api, custom, server, user-template, thousandeyes
	/// </summary>
	[JsonPropertyName("icon")]
	public string? Icon { get; set; }

	/// <summary>
	/// Indicates whether the template is a built-in template.
	/// This field is read-only for normal users.
	/// </summary>
	[JsonPropertyName("isBuiltIn")]
	public bool? IsBuiltIn { get; set; }

	/// <summary>
	/// The certification level of the template
	/// </summary>
	[JsonPropertyName("certificationLevel")]
	public CertificationLevel? CertificationLevel { get; set; }

	/// <summary>
	/// A map of user input definitions.
	/// User Inputs are values that the user must fill in order for the Template to deploy.
	/// They can be referenced using Handlebars template substitution notation.
	/// </summary>
	[JsonPropertyName("userInputs")]
	public Dictionary<string, UserInput> UserInputs { get; set; } = [];

	/// <summary>
	/// An ordered list of groupings that organize template objects, such as user inputs or tests
	/// </summary>
	[JsonPropertyName("groupings")]
	public List<TemplateGrouping> Groupings { get; set; } = [];

	/// <summary>
	/// A map of Label configurations.
	/// These are ThousandEyes Labels that will be created when the template is deployed.
	/// Keys are user-defined asset keys, values are label configurations.
	/// </summary>
	[JsonPropertyName("labels")]
	public Dictionary<string, JsonElement> Labels { get; set; } = [];

	/// <summary>
	/// A map of Test configurations.
	/// These are the ThousandEyes CEA/Synthetic tests that will be created when the template is deployed.
	/// Keys are user-defined asset keys, values are test configurations.
	/// </summary>
	[JsonPropertyName("tests")]
	public Dictionary<string, JsonElement> Tests { get; set; } = [];

	/// <summary>
	/// A map of Endpoint Test configurations.
	/// These are the Endpoint tests that will be created when the template is deployed.
	/// Keys are user-defined asset keys, values are endpoint test configurations.
	/// </summary>
	[JsonPropertyName("endpointTests")]
	public Dictionary<string, JsonElement> EndpointTests { get; set; } = [];

	/// <summary>
	/// A map of Alert Rule configurations.
	/// These are the set of Alert Rules that will be created when the template is deployed.
	/// Keys are user-defined asset keys, values are alert rule configurations.
	/// </summary>
	[JsonPropertyName("alertRules")]
	public Dictionary<string, JsonElement> AlertRules { get; set; } = [];

	/// <summary>
	/// A map of Dashboard configurations.
	/// These are the set of dashboards that will be created when the template is deployed.
	/// Keys are user-defined asset keys, values are dashboard configurations.
	/// </summary>
	[JsonPropertyName("dashboards")]
	public Dictionary<string, JsonElement> Dashboards { get; set; } = [];

	/// <summary>
	/// A map of Dashboard Filter configurations.
	/// These will be the dashboard filters created when the template is deployed.
	/// Keys are user-defined asset keys, values are dashboard filter configurations.
	/// </summary>
	[JsonPropertyName("dashboardFilters")]
	public Dictionary<string, JsonElement> DashboardFilters { get; set; } = [];

	/// <summary>
	/// A map of deployment strategies.
	/// Defines how the system behaves when an asset defined in the template already exists.
	/// Keys are asset keys, values are deployment strategies (create, update, ignore).
	/// </summary>
	[JsonPropertyName("deploymentStrategy")]
	public Dictionary<string, DeploymentStrategy> DeploymentStrategy { get; set; } = [];

	/// <summary>
	/// A map of resource inclusion settings.
	/// Specifies whether an asset should be included in a deployment.
	/// Keys are asset keys, values are inclusion settings (included, skipped).
	/// </summary>
	[JsonPropertyName("resourceInclusion")]
	public Dictionary<string, ResourceInclusion> ResourceInclusion { get; set; } = [];

	/// <summary>
	/// ThousandEyes modules this template belongs to.
	/// Regular users can only set this to default.
	/// </summary>
	[JsonPropertyName("modules")]
	public List<TemplateModule> Modules { get; set; } = [];
}
