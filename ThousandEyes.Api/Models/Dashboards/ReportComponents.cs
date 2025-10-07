using System.Text.Json.Serialization;

namespace ThousandEyes.Api.Models.Dashboards;

/// <summary>
/// Report schedule configuration
/// </summary>
public class ReportSchedule
{
	/// <summary>
	/// Schedule frequency (daily, weekly, monthly)
	/// </summary>
	public required string Frequency { get; set; }

	/// <summary>
	/// Day of week for weekly reports (1-7, Monday=1)
	/// </summary>
	public int? DayOfWeek { get; set; }

	/// <summary>
	/// Day of month for monthly reports (1-31)
	/// </summary>
	public int? DayOfMonth { get; set; }

	/// <summary>
	/// Hour of day to run (0-23)
	/// </summary>
	public int Hour { get; set; } = 9;

	/// <summary>
	/// Minute of hour to run (0-59)
	/// </summary>
	public int Minute { get; set; }

	/// <summary>
	/// Timezone for scheduling
	/// </summary>
	public string TimeZone { get; set; } = "UTC";

	/// <summary>
	/// Whether the schedule is enabled
	/// </summary>
	public bool IsEnabled { get; set; } = true;
}

/// <summary>
/// Report data configuration
/// </summary>
public class ReportDataConfig
{
	/// <summary>
	/// Time range type (relative, absolute)
	/// </summary>
	public required string TimeRangeType { get; set; }

	/// <summary>
	/// Relative time range (7d, 30d, etc.) for relative type
	/// </summary>
	public string? RelativeTimeRange { get; set; }

	/// <summary>
	/// Number of days back for relative reports
	/// </summary>
	public int? DaysBack { get; set; }

	/// <summary>
	/// Custom start date for absolute type
	/// </summary>
	public DateTime? StartDate { get; set; }

	/// <summary>
	/// Custom end date for absolute type
	/// </summary>
	public DateTime? EndDate { get; set; }

	/// <summary>
	/// Data aggregation interval
	/// </summary>
	public string? Aggregation { get; set; }

	/// <summary>
	/// Include raw data
	/// </summary>
	public bool IncludeRawData { get; set; }

	/// <summary>
	/// Include summary statistics
	/// </summary>
	public bool IncludeSummary { get; set; } = true;
}

/// <summary>
/// Report email configuration
/// </summary>
public class ReportEmailConfig
{
	/// <summary>
	/// Email addresses to send report to
	/// </summary>
	[JsonPropertyName("recipients")]
	public string[] Recipients { get; set; } = [];

	/// <summary>
	/// Email subject template
	/// </summary>
	public string? Subject { get; set; }

	/// <summary>
	/// Email body template
	/// </summary>
	public string? Body { get; set; }

	/// <summary>
	/// Whether to attach the report file
	/// </summary>
	public bool AttachReport { get; set; } = true;

	/// <summary>
	/// Whether to embed charts in email
	/// </summary>
	public bool EmbedCharts { get; set; }
}

/// <summary>
/// Report request for create/update operations
/// </summary>
public class ReportRequest
{
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
	/// Report dashboard ID (if based on a dashboard)
	/// </summary>
	public string? DashboardId { get; set; }

	/// <summary>
	/// Report format (pdf, csv, json)
	/// </summary>
	public string Format { get; set; } = "pdf";

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
}