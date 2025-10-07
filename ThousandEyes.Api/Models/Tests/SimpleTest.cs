namespace ThousandEyes.Api.Models.Tests;

/// <summary>
/// Base test information
/// </summary>
public class SimpleTest
{
	/// <summary>
	/// Unique ID of the test
	/// </summary>
	public required string TestId { get; set; }

	/// <summary>
	/// Name of the test
	/// </summary>
	public required string TestName { get; set; }

	/// <summary>
	/// Type of the test
	/// </summary>
	public required string Type { get; set; }

	/// <summary>
	/// Test interval in seconds
	/// </summary>
	public required int Interval { get; set; }

	/// <summary>
	/// Indicates if the test is enabled
	/// </summary>
	public bool Enabled { get; set; } = true;

	/// <summary>
	/// Indicates if alerts are enabled
	/// </summary>
	public bool AlertsEnabled { get; set; } = true;

	/// <summary>
	/// Description of the test
	/// </summary>
	public string? Description { get; set; }

	/// <summary>
	/// User that created the test
	/// </summary>
	public string? CreatedBy { get; set; }

	/// <summary>
	/// UTC created date
	/// </summary>
	public DateTime? CreatedDate { get; set; }

	/// <summary>
	/// User that modified the test
	/// </summary>
	public string? ModifiedBy { get; set; }

	/// <summary>
	/// UTC last modification date
	/// </summary>
	public DateTime? ModifiedDate { get; set; }

	/// <summary>
	/// Indicates if the test is shared with the account group
	/// </summary>
	public bool LiveShare { get; set; }

	/// <summary>
	/// Indicates if the test is a saved event (private snapshot)
	/// </summary>
	public bool SavedEvent { get; set; }

	/// <summary>
	/// Navigation links
	/// </summary>
	public TestLinks? Links { get; set; }
}