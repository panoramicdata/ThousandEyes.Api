namespace ThousandEyes.Api.Models.TestResults;

/// <summary>
/// HTTP Server test results response wrapper
/// </summary>
public class HttpServerTestResults
{
	/// <summary>
	/// List of HTTP Server test results
	/// </summary>
	public HttpServerTestResult[] Results { get; set; } = [];

	/// <summary>
	/// Navigation links
	/// </summary>
	public TestResultLinks? Links { get; set; }
}