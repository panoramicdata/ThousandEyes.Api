using System.Text.Json.Serialization;

namespace ThousandEyes.Api.Models.Tests;

/// <summary>
/// Page Load test configuration
/// </summary>
public class PageLoadTest : SimpleTest
{
	/// <summary>
	/// URL to load
	/// </summary>
	public required string Url { get; set; }

	/// <summary>
	/// HTTP request timeout in seconds (5-180)
	/// </summary>
	public int HttpTimeLimit { get; set; } = 30;

	/// <summary>
	/// Page load timeout in seconds (5-180)
	/// </summary>
	public int PageLoadTimeLimit { get; set; } = 30;

	/// <summary>
	/// Follow HTTP redirects
	/// </summary>
	public bool FollowRedirects { get; set; } = true;

	/// <summary>
	/// Verify SSL certificate
	/// </summary>
	public bool VerifyCertificate { get; set; } = true;

	/// <summary>
	/// Include headers in response
	/// </summary>
	public bool IncludeHeaders { get; set; } = true;

	/// <summary>
	/// Enable network layer measurements
	/// </summary>
	public bool NetworkMeasurements { get; set; } = true;

	/// <summary>
	/// Enable BGP measurements
	/// </summary>
	public bool BgpMeasurements { get; set; }

	/// <summary>
	/// Browser viewpoint width in pixels
	/// </summary>
	public int ViewportWidth { get; set; } = 1366;

	/// <summary>
	/// Browser viewpoint height in pixels
	/// </summary>
	public int ViewportHeight { get; set; } = 768;

	/// <summary>
	/// User Agent string override
	/// </summary>
	public string? UserAgent { get; set; }

	/// <summary>
	/// Custom HTTP headers
	/// </summary>
	public Dictionary<string, string> CustomHeaders { get; set; } = [];

	/// <summary>
	/// HTTP authentication username
	/// </summary>
	public string? AuthUser { get; set; }

	/// <summary>
	/// HTTP authentication password
	/// </summary>
	public string? AuthPassword { get; set; }

	/// <summary>
	/// List of agents assigned to this test
	/// </summary>
	public TestAgent[] Agents { get; set; } = [];

	/// <summary>
	/// List of BGP monitors for BGP measurements
	/// </summary>
	[JsonPropertyName("bgpMonitors")]
	public TestAgent[] BgpMonitors { get; set; } = [];
}