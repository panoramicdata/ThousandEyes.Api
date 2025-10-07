namespace ThousandEyes.Api.Models.Tests;

/// <summary>
/// HTTP Server tests response wrapper
/// </summary>
public class HttpServerTests
{
	/// <summary>
	/// List of HTTP Server tests
	/// </summary>
	public HttpServerTest[] Tests { get; set; } = [];

	/// <summary>
	/// Navigation links
	/// </summary>
	public TestLinks? Links { get; set; }
}
