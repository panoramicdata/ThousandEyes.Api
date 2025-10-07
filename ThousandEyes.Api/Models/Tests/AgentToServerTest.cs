using System.Text.Json.Serialization;

namespace ThousandEyes.Api.Models.Tests;

/// <summary>
/// Agent to Server test configuration
/// </summary>
public class AgentToServerTest : SimpleTest
{
	/// <summary>
	/// Target server hostname or IP address
	/// </summary>
	public required string Server { get; set; }

	/// <summary>
	/// Target server port
	/// </summary>
	public int Port { get; set; } = 80;

	/// <summary>
	/// Protocol to use (TCP, UDP, ICMP)
	/// </summary>
	public string Protocol { get; set; } = "TCP";

	/// <summary>
	/// Enable network layer measurements
	/// </summary>
	public bool NetworkMeasurements { get; set; } = true;

	/// <summary>
	/// Enable BGP measurements
	/// </summary>
	public bool BgpMeasurements { get; set; }

	/// <summary>
	/// Enable path trace
	/// </summary>
	public bool PathTraceEnabled { get; set; } = true;

	/// <summary>
	/// Path trace mode (classic or inSession)
	/// </summary>
	public string PathTraceMode { get; set; } = "classic";

	/// <summary>
	/// Maximum time to live (TTL) for path trace
	/// </summary>
	public int MaxPathTraceHops { get; set; } = 32;

	/// <summary>
	/// Enable MTU measurements
	/// </summary>
	public bool MtuMeasurements { get; set; }

	/// <summary>
	/// Bandwidth measurements probe mode
	/// </summary>
	public string? BandwidthMeasurements { get; set; }

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