using System.Text.Json.Serialization;

namespace ThousandEyes.Api.Models.Dashboards;

/// <summary>
/// Dashboard request for create/update operations
/// </summary>
public class DashboardRequest
{
	/// <summary>
	/// Dashboard title
	/// </summary>
	public required string Title { get; set; }

	/// <summary>
	/// Dashboard description
	/// </summary>
	public string? Description { get; set; }

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