using System.Text.Json.Serialization;

namespace ThousandEyes.Api.Models.BgpMonitors;

/// <summary>
/// Response containing list of BGP monitors
/// </summary>
public class Monitors : ApiResource
{
	/// <summary>
	/// List of BGP monitors
	/// </summary>
	[JsonPropertyName("monitors")]
	public Monitor[] MonitorsList { get; set; } = [];
}
