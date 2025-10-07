namespace ThousandEyes.Api.Models.Tests;

/// <summary>
/// Page Load tests response wrapper
/// </summary>
public class PageLoadTests
{
	/// <summary>
	/// List of Page Load tests
	/// </summary>
	public PageLoadTest[] Tests { get; set; } = [];

	/// <summary>
	/// Navigation links
	/// </summary>
	public TestLinks? Links { get; set; }
}