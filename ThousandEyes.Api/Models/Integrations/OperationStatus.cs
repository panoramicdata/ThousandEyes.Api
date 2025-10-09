namespace ThousandEyes.Api.Models.Integrations;

/// <summary>
/// Operation status
/// </summary>
public enum OperationStatus
{
	/// <summary>
	/// Operation is pending configuration
	/// </summary>
	Pending,

	/// <summary>
	/// Operation is successfully connected
	/// </summary>
	Connected,

	/// <summary>
	/// Operation is failing
	/// </summary>
	Failing,

	/// <summary>
	/// Operation is unverified
	/// </summary>
	Unverified
}
