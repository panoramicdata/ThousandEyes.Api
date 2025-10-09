namespace ThousandEyes.Api.Models.InternetInsights;

/// <summary>
/// Provider location information
/// </summary>
public class ProviderLocation
{
	/// <summary>
	/// The location covered by the provider
	/// </summary>
	public string? Location { get; set; }

	/// <summary>
	/// The number of interfaces covered by the provider at this location
	/// </summary>
	public int InterfacesCount { get; set; }
}
