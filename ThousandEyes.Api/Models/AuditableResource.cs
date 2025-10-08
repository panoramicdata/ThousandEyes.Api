namespace ThousandEyes.Api.Models;

/// <summary>
/// Base class for resources that track creation and modification
/// </summary>
public abstract class AuditableResource : AccountGroupResource
{
	/// <summary>
	/// When the resource was created (ISO 8601 format)
	/// </summary>
	public DateTime? CreatedDate { get; set; }

	/// <summary>
	/// When the resource was last modified (ISO 8601 format)
	/// </summary>
	public DateTime? ModifiedDate { get; set; }

	/// <summary>
	/// User ID who created the resource (numeric ID)
	/// </summary>
	public long? CreatedBy { get; set; }

	/// <summary>
	/// User ID who last modified the resource (numeric ID)
	/// </summary>
	public long? ModifiedBy { get; set; }
}
