namespace ThousandEyes.Api.Models.TestResults;

/// <summary>
/// Network test results response wrapper
/// </summary>
public class NetworkTestResults
{
	/// <summary>
	/// List of network test results
	/// </summary>
	public NetworkTestResult[] Results { get; set; } = [];

	/// <summary>
	/// Navigation links
	/// </summary>
	public TestResultLinks? Links { get; set; }
}