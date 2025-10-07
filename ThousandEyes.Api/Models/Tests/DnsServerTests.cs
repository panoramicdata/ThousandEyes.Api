namespace ThousandEyes.Api.Models.Tests;

/// <summary>
/// DNS Server tests response wrapper
/// </summary>
public class DnsServerTests
{
	/// <summary>
	/// List of DNS Server tests
	/// </summary>
	public DnsServerTest[] Tests { get; set; } = [];

	/// <summary>
	/// Navigation links
	/// </summary>
	public TestLinks? Links { get; set; }
}