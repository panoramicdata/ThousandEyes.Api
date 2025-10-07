namespace ThousandEyes.Api.Models.Tests;

/// <summary>
/// BGP tests response wrapper
/// </summary>
public class BgpTests
{
	/// <summary>
	/// List of BGP tests
	/// </summary>
	public BgpTest[] Tests { get; set; } = [];

	/// <summary>
	/// Navigation links
	/// </summary>
	public TestLinks? Links { get; set; }
}