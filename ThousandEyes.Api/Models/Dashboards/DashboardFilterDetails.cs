using System.Text.Json.Serialization;
using ThousandEyes.Api.Models;

namespace ThousandEyes.Api.Models.Dashboards;

/// <summary>
/// Dashboard filter details
/// </summary>
public class DashboardFilterDetails : AuditableResource
{
	// Inherited from AuditableResource: Aid, Links, CreatedDate, ModifiedDate, CreatedBy, ModifiedBy

	/// <summary>
	/// Filter ID
	/// </summary>
	public required string Id { get; set; }

	/// <summary>
	/// Filter name (must be unique)
	/// </summary>
	public required string Name { get; set; }

	/// <summary>
	/// Filter description
	/// </summary>
	public string? Description { get; set; }

	/// <summary>
	/// Filter context (list of data source filters)
	/// </summary>
	public DataSourceFilter[] Context { get; set; } = [];
}
