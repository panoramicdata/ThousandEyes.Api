namespace ThousandEyes.Api.Models.Tests;

/// <summary>
/// Test version history response
/// </summary>
public class TestVersionHistoryResponse
{
	/// <summary>
	/// List of test version history entries
	/// </summary>
	public TestVersionHistory[] TestVersionHistory { get; set; } = [];

	/// <summary>
	/// Navigation links
	/// </summary>
	public TestLinks? Links { get; set; }
}
