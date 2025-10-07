namespace ThousandEyes.Api.Models.Tests;

/// <summary>
/// Tests response wrapper
/// </summary>
public class Tests
{
	/// <summary>
	/// List of tests
	/// </summary>
	public SimpleTest[] TestsList { get; set; } = [];

	/// <summary>
	/// Navigation links
	/// </summary>
	public TestLinks? Links { get; set; }
}