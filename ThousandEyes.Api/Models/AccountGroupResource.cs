namespace ThousandEyes.Api.Models;

/// <summary>
/// Base class for resources that belong to an account group
/// </summary>
public abstract class AccountGroupResource : ApiResource
{
	/// <summary>
	/// Account group ID
	/// </summary>
	public string? Aid { get; set; }
}
