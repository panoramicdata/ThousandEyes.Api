namespace ThousandEyes.Api.Models.InternetInsights;

/// <summary>
/// Type of catalog provider
/// </summary>
public enum ProviderType
{
	/// <summary>
	/// Infrastructure as a Service provider
	/// </summary>
	Iaas,

	/// <summary>
	/// Software as a Service provider
	/// </summary>
	Saas,

	/// <summary>
	/// Content Delivery Network provider
	/// </summary>
	Cdn,

	/// <summary>
	/// DNS provider
	/// </summary>
	Dns
}
