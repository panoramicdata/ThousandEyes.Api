namespace ThousandEyes.Api.Models.Tests;

/// <summary>
/// Web Transaction tests response wrapper
/// </summary>
public class WebTransactionTests
{
	/// <summary>
	/// List of Web Transaction tests
	/// </summary>
	public WebTransactionTest[] Tests { get; set; } = [];

	/// <summary>
	/// Navigation links
	/// </summary>
	public TestLinks? Links { get; set; }
}