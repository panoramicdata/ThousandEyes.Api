using System.Text.Json.Serialization;

namespace ThousandEyes.Api.Models.Dashboards;

/// <summary>
/// Report configuration and metadata
/// </summary>
public class Report
{
	/// <summary>
	/// Unique report ID
	/// </summary>
	public required string ReportId { get; set; }

	/// <summary>
	/// Report name
	/// </summary>
	public required string ReportName { get; set; }

	/// <summary>
	/// Report description
	/// </summary>
	public string? Description { get; set; }

	/// <summary>
	/// Report type (scheduled, on-demand)
	/// </summary>
	public required string ReportType { get; set; }

	/// <summary>
	/// Whether the report is built-in
	/// </summary>
	public bool IsBuiltIn { get; set; }

	/// <summary>
	/// Report owner user ID
	/// </summary>
	public string? OwnerId { get; set; }

	/// <summary>
	/// Report owner name
	/// </summary>
	public string? OwnerName { get; set; }

	/// <summary>
	/// Account group ID this report belongs to
	/// </summary>
	public string? AccountGroupId { get; set; }

	/// <summary>
	/// Report dashboard ID (if based on a dashboard)
	/// </summary>
	public string? DashboardId { get; set; }

	/// <summary>
	/// Report format (pdf, csv, json)
	/// </summary>
	public string? Format { get; set; }

	/// <summary>
	/// Report schedule configuration
	/// </summary>
	public ReportSchedule? Schedule { get; set; }

	/// <summary>
	/// Report data configuration
	/// </summary>
	public ReportDataConfig? DataConfig { get; set; }

	/// <summary>
	/// Report email configuration
	/// </summary>
	public ReportEmailConfig? EmailConfig { get; set; }

	/// <summary>
	/// When this report was created
	/// </summary>
	public DateTime? CreatedDate { get; set; }

	/// <summary>
	/// When this report was last modified
	/// </summary>
	public DateTime? ModifiedDate { get; set; }

	/// <summary>
	/// When this report was last run
	/// </summary>
	public DateTime? LastRunDate { get; set; }

	/// <summary>
	/// Next scheduled run date
	/// </summary>
	public DateTime? NextRunDate { get; set; }

	/// <summary>
	/// Report status (active, paused, error)
	/// </summary>
	public string? Status { get; set; }

	/// <summary>
	/// User who created this report
	/// </summary>
	public string? CreatedBy { get; set; }

	/// <summary>
	/// User who last modified this report
	/// </summary>
	public string? ModifiedBy { get; set; }

	/// <summary>
	/// Navigation links
	/// </summary>
	public DashboardLinks? Links { get; set; }
}