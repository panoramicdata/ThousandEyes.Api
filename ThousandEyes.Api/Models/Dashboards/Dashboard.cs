using System.Text.Json.Serialization;

namespace ThousandEyes.Api.Models.Dashboards;

/// <summary>
/// Dashboard configuration and metadata
/// </summary>
public class Dashboard
{
	/// <summary>
	/// Unique dashboard ID
	/// </summary>
	public required string DashboardId { get; set; }

	/// <summary>
	/// Dashboard name
	/// </summary>
	public required string DashboardName { get; set; }

	/// <summary>
	/// Dashboard description
	/// </summary>
	public string? Description { get; set; }

	/// <summary>
	/// Dashboard type (global, personal, shared)
	/// </summary>
	public required string DashboardType { get; set; }

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
	public bool IsDefault { get; set; }

	/// <summary>
	/// Dashboard owner user ID
	/// </summary>
	public string? OwnerId { get; set; }

	/// <summary>
	/// Dashboard owner name
	/// </summary>
	public string? OwnerName { get; set; }

	/// <summary>
	/// Account group ID this dashboard belongs to
	/// </summary>
	public string? AccountGroupId { get; set; }

	/// <summary>
	/// Dashboard widgets configuration
	/// </summary>
	[JsonPropertyName("widgets")]
	public DashboardWidget[] Widgets { get; set; } = [];

	/// <summary>
	/// Dashboard layout configuration
	/// </summary>
	public DashboardLayout? Layout { get; set; }

	/// <summary>
	/// Dashboard filters
	/// </summary>
	[JsonPropertyName("filters")]
	public DashboardFilter[] Filters { get; set; } = [];

	/// <summary>
	/// Dashboard time span configuration
	/// </summary>
	public DashboardTimeSpan? TimeSpan { get; set; }

	/// <summary>
	/// When this dashboard was created
	/// </summary>
	public DateTime? CreatedDate { get; set; }

	/// <summary>
	/// When this dashboard was last modified
	/// </summary>
	public DateTime? ModifiedDate { get; set; }

	/// <summary>
	/// User who created this dashboard
	/// </summary>
	public string? CreatedBy { get; set; }

	/// <summary>
	/// User who last modified this dashboard
	/// </summary>
	public string? ModifiedBy { get; set; }

	/// <summary>
	/// Navigation links
	/// </summary>
	public DashboardLinks? Links { get; set; }
}