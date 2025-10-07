namespace ThousandEyes.Api.Models.Tests;

/// <summary>
/// Test version history entry
/// </summary>
public class TestVersionHistory
{
	/// <summary>
	/// Version ID
	/// </summary>
	public required string VersionId { get; set; }

	/// <summary>
	/// Test ID
	/// </summary>
	public required string TestId { get; set; }

	/// <summary>
	/// Version timestamp
	/// </summary>
	public required DateTime VersionTimestamp { get; set; }

	/// <summary>
	/// User who created this version
	/// </summary>
	public required string CreatedBy { get; set; }
}