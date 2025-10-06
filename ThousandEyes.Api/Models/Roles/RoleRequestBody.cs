namespace ThousandEyes.Api.Models.Roles;

/// <summary>
/// Role request body for create/update operations
/// </summary>
public class RoleRequestBody
{
	/// <summary>
	/// Role name
	/// </summary>
	public string? Name { get; set; }

	/// <summary>
	/// Permission IDs
	/// </summary>
	public string[]? Permissions { get; set; }
}
