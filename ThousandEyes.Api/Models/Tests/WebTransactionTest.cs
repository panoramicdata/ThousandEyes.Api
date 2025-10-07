using System.Text.Json.Serialization;

namespace ThousandEyes.Api.Models.Tests;

/// <summary>
/// Web Transaction test configuration
/// </summary>
public class WebTransactionTest : SimpleTest
{
	/// <summary>
	/// URL to start the transaction
	/// </summary>
	public required string Url { get; set; }

	/// <summary>
	/// Transaction script content
	/// </summary>
	public required string TransactionScript { get; set; }

	/// <summary>
	/// Transaction timeout in seconds (5-180)
	/// </summary>
	public int TransactionTimeLimit { get; set; } = 60;

	/// <summary>
	/// HTTP request timeout in seconds (5-180)
	/// </summary>
	public int HttpTimeLimit { get; set; } = 30;

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