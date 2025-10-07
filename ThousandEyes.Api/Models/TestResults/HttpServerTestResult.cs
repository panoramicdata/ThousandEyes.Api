using System.Text.Json.Serialization;

namespace ThousandEyes.Api.Models.TestResults;

/// <summary>
/// HTTP Server test result data
/// </summary>
public class HttpServerTestResult
{
	/// <summary>
	/// Test ID
	/// </summary>
	public required string TestId { get; set; }

	/// <summary>
	/// Test name
	/// </summary>
	public required string TestName { get; set; }

	/// <summary>
	/// Agent ID that performed the test
	/// </summary>
	public required string AgentId { get; set; }

	/// <summary>
	/// Agent name
	/// </summary>
	public required string AgentName { get; set; }

	/// <summary>
	/// Round ID for this specific test execution
	/// </summary>
	public required string RoundId { get; set; }

	/// <summary>
	/// Date and time when the test was performed
	/// </summary>
	public required DateTime Date { get; set; }

	/// <summary>
	/// HTTP response code
	/// </summary>
	public int ResponseCode { get; set; }

	/// <summary>
	/// Total response time in milliseconds
	/// </summary>
	public double? ResponseTime { get; set; }

	/// <summary>
	/// DNS resolution time in milliseconds
	/// </summary>
	public double? DnsTime { get; set; }

	/// <summary>
	/// Connection establishment time in milliseconds
	/// </summary>
	public double? ConnectTime { get; set; }

	/// <summary>
	/// SSL handshake time in milliseconds
	/// </summary>
	public double? SslTime { get; set; }

	/// <summary>
	/// Time to first byte in milliseconds
	/// </summary>
	public double? WaitTime { get; set; }

	/// <summary>
	/// Content download time in milliseconds
	/// </summary>
	public double? ReceiveTime { get; set; }

	/// <summary>
	/// Total bytes downloaded
	/// </summary>
	public long? TotalSize { get; set; }

	/// <summary>
	/// Number of redirects followed
	/// </summary>
	public int? RedirectCount { get; set; }

	/// <summary>
	/// Wire size of the response
	/// </summary>
	public long? WireSize { get; set; }

	/// <summary>
	/// Response headers
	/// </summary>
	[JsonPropertyName("headers")]
	public Dictionary<string, string>? Headers { get; set; }

	/// <summary>
	/// Error details if test failed
	/// </summary>
	public string? ErrorDetails { get; set; }

	/// <summary>
	/// Navigation links
	/// </summary>
	public TestResultLinks? Links { get; set; }
}