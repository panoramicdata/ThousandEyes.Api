using System.Text.Json;
using System.Text.Json.Serialization;

namespace ThousandEyes.Api.Models.Templates;

/// <summary>
/// Template response with ID, creation date, and hypermedia links.
/// </summary>
public class TemplateResponse : ApiResource
{
	/// <summary>
	/// The ID of the template
	/// </summary>
	[JsonPropertyName("id")]
	public string? Id { get; set; }

	/// <summary>
	/// The date and time the template was created
	/// </summary>
	[JsonPropertyName("dateCreated")]
	public DateTime? DateCreated { get; set; }

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
	/// Icon that will be used when displaying the template in UI
	/// </summary>
	[JsonPropertyName("icon")]
	public string? Icon { get; set; }

	/// <summary>
	/// Indicates whether the template is a built-in template
	/// </summary>
	[JsonPropertyName("isBuiltIn")]
	public bool? IsBuiltIn { get; set; }

	/// <summary>
	/// The certification level of the template
	/// </summary>
	[JsonPropertyName("certificationLevel")]
	public CertificationLevel? CertificationLevel { get; set; }

	/// <summary>
	/// A map of user input definitions
	/// </summary>
	[JsonPropertyName("userInputs")]
	public Dictionary<string, UserInput> UserInputs { get; set; } = [];

	/// <summary>
	/// An ordered list of groupings that organize template objects
	/// </summary>
	[JsonPropertyName("groupings")]
	public List<TemplateGrouping> Groupings { get; set; } = [];

	/// <summary>
	/// A map of Label configurations
	/// </summary>
	[JsonPropertyName("labels")]
	public Dictionary<string, JsonElement> Labels { get; set; } = [];

	/// <summary>
	/// A map of Test configurations
	/// </summary>
	[JsonPropertyName("tests")]
	public Dictionary<string, JsonElement> Tests { get; set; } = [];

	/// <summary>
	/// A map of Endpoint Test configurations
	/// </summary>
	[JsonPropertyName("endpointTests")]
	public Dictionary<string, JsonElement> EndpointTests { get; set; } = [];

	/// <summary>
	/// A map of Alert Rule configurations
	/// </summary>
	[JsonPropertyName("alertRules")]
	public Dictionary<string, JsonElement> AlertRules { get; set; } = [];

	/// <summary>
	/// A map of Dashboard configurations
	/// </summary>
	[JsonPropertyName("dashboards")]
	public Dictionary<string, JsonElement> Dashboards { get; set; } = [];

	/// <summary>
	/// A map of Dashboard Filter configurations
	/// </summary>
	[JsonPropertyName("dashboardFilters")]
	public Dictionary<string, JsonElement> DashboardFilters { get; set; } = [];

	/// <summary>
	/// A map of deployment strategies
	/// </summary>
	[JsonPropertyName("deploymentStrategy")]
	public Dictionary<string, DeploymentStrategy> DeploymentStrategy { get; set; } = [];

	/// <summary>
	/// A map of resource inclusion settings
	/// </summary>
	[JsonPropertyName("resourceInclusion")]
	public Dictionary<string, ResourceInclusion> ResourceInclusion { get; set; } = [];

	/// <summary>
	/// ThousandEyes modules this template belongs to
	/// </summary>
	[JsonPropertyName("modules")]
	public List<TemplateModule> Modules { get; set; } = [];
}
