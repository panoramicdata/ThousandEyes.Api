namespace ThousandEyes.Api.Models.Tests;

/// <summary>
/// HTTP Server test configuration
/// </summary>
public class HttpServerTest : SimpleTest
{
	/// <summary>
	/// Target URL for the test
	/// </summary>
	public required string Url { get; set; }

	/// <summary>
	/// HTTP request method
	/// </summary>
	public string RequestMethod { get; set; } = "GET";

	/// <summary>
	/// Expected HTTP status code
	/// </summary>
	public string DesiredStatusCode { get; set; } = "default";

	/// <summary>
	/// HTTP timeout in seconds
	/// </summary>
	public int HttpTimeLimit { get; set; } = 5;

	/// <summary>
	/// Target time for HTTP server completion in milliseconds
	/// </summary>
	public int? HttpTargetTime { get; set; }

	/// <summary>
	/// HTTP version preference (1 or 2)
	/// </summary>
	public int HttpVersion { get; set; } = 2;

	/// <summary>
	/// Follow HTTP redirects
	/// </summary>
	public bool FollowRedirects { get; set; } = true;

	/// <summary>
	/// Include response headers in results
	/// </summary>
	public bool IncludeHeaders { get; set; } = true;

	/// <summary>
	/// User agent string
	/// </summary>
	public string? UserAgent { get; set; }

	/// <summary>
	/// Authentication type
	/// </summary>
	public string AuthType { get; set; } = "none";

	/// <summary>
	/// Username for authentication
	/// </summary>
	public string? Username { get; set; }

	/// <summary>
	/// Password for authentication (write-only)
	/// </summary>
	public string? Password { get; set; }

	/// <summary>
	/// Content regex for validation
	/// </summary>
	public string? ContentRegex { get; set; }

	/// <summary>
	/// Verify SSL certificates
	/// </summary>
	public bool VerifyCertificate { get; set; }

	/// <summary>
	/// SSL version ID
	/// </summary>
	public string SslVersionId { get; set; } = "0";

	/// <summary>
	/// DNS override IP address
	/// </summary>
	public string? DnsOverride { get; set; }

	/// <summary>
	/// Custom HTTP headers
	/// </summary>
	public string[]? Headers { get; set; }

	/// <summary>
	/// POST request body
	/// </summary>
	public string? PostBody { get; set; }

	/// <summary>
	/// Enable bandwidth measurements
	/// </summary>
	public bool BandwidthMeasurements { get; set; }

	/// <summary>
	/// Enable MTU measurements
	/// </summary>
	public bool MtuMeasurements { get; set; }

	/// <summary>
	/// Enable network measurements
	/// </summary>
	public bool NetworkMeasurements { get; set; } = true;

	/// <summary>
	/// Number of path traces to execute
	/// </summary>
	public int NumPathTraces { get; set; } = 3;

	/// <summary>
	/// Path trace mode
	/// </summary>
	public string PathTraceMode { get; set; } = "classic";

	/// <summary>
	/// Network protocol for measurements
	/// </summary>
	public string Protocol { get; set; } = "tcp";

	/// <summary>
	/// Probe mode for TCP tests
	/// </summary>
	public string ProbeMode { get; set; } = "auto";

	/// <summary>
	/// Fixed packet rate for network measurements
	/// </summary>
	public int? FixedPacketRate { get; set; }

	/// <summary>
	/// IPv6 policy
	/// </summary>
	public string Ipv6Policy { get; set; } = "use-agent-policy";

	/// <summary>
	/// DSCP ID for QoS marking
	/// </summary>
	public string DscpId { get; set; } = "0";

	/// <summary>
	/// Enable BGP measurements
	/// </summary>
	public bool BgpMeasurements { get; set; } = true;

	/// <summary>
	/// Use public BGP monitors
	/// </summary>
	public bool UsePublicBgp { get; set; } = true;

	/// <summary>
	/// List of assigned agents
	/// </summary>
	public TestAgent[]? Agents { get; set; }
}