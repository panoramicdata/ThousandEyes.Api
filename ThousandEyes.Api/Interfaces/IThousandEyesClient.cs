namespace ThousandEyes.Api.Interfaces;

/// <summary>
/// Main interface for the ThousandEyes API client providing access to all API modules
/// </summary>
public interface IThousandEyesClient
{
	/// <summary>
	/// Gets the PSA (Professional Services Automation) API module
	/// </summary>
	IPsaApi Psa { get; }

	/// <summary>
	/// Gets the ServiceDesk API module
	/// </summary>
	IServiceDeskApi ServiceDesk { get; }

	/// <summary>
	/// Gets the System API module for configuration and administration
	/// </summary>
	ISystemApi System { get; }
}
