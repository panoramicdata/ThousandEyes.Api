using System.Text.Json.Serialization;
using ThousandEyes.Api.Models;

namespace ThousandEyes.Api.Models.Dashboards;

/// <summary>
/// Dashboard configuration and metadata
/// </summary>
public class Dashboard : AccountGroupResource
{
	// Inherited from AccountGroupResource: Aid, Links

	/// <summary>
	/// Unique dashboard ID
	/// </summary>
	public required string DashboardId { get; set; }

	/// <summary>
	/// Dashboard title
	/// </summary>
	public required string Title { get; set; }

	/// <summary>
	/// Dashboard description
	/// </summary>
	public string? Description { get; set; }

	/// <summary>
	/// Whether the dashboard is built-in
	/// </summary>
	public bool IsBuiltIn { get; set; }

	/// <summary>
	/// Whether the dashboard is private
	/// </summary>
	public bool IsPrivate { get; set; }

	/// <summary>
	/// Whether the dashboard is default for the user
	/// </summary>
	public bool IsDefaultForUser { get; set; }

	/// <summary>
	/// Whether the dashboard is default for the account
	/// </summary>
	public bool IsDefaultForAccount { get; set; }

	/// <summary>
	/// User ID who created this dashboard
	/// </summary>
	public string? DashboardCreatedBy { get; set; }

	/// <summary>
	/// User ID who last modified this dashboard
	/// </summary>
	public string? DashboardModifiedBy { get; set; }

	/// <summary>
	/// When this dashboard was last modified (ISO 8601 format)
	/// </summary>
	public string? DashboardModifiedDate { get; set; }

	/// <summary>
	/// Dashboard widgets configuration
	/// </summary>
	public DashboardWidget[] Widgets { get; set; } = [];

	/// <summary>
	/// Default time span for the dashboard
	/// </summary>
	public DashboardTimeSpan? DefaultTimespan { get; set; }

	/// <summary>
	/// Whether global override is enabled
	/// </summary>
	public bool IsGlobalOverride { get; set; }
}