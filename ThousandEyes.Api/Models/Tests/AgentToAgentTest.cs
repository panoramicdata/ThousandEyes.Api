namespace ThousandEyes.Api.Models.Tests;

/// <summary>
/// Agent to Agent test configuration
/// </summary>
public class AgentToAgentTest : SimpleTest
{
	/// <summary>
	/// Target agent ID
	/// </summary>
	public required string TargetAgentId { get; set; }

	/// <summary>
	/// Test protocol (TCP, UDP, ICMP)
	/// </summary>
	public string Protocol { get; set; } = "TCP";

	/// <summary>
	/// Target port for TCP/UDP tests
	/// </summary>
	public int Port { get; set; } = 49153;

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
	/// Throughput measurements duration in milliseconds
	/// </summary>
	public int ThroughputMeasurements { get; set; } = 10000;

	/// <summary>
	/// Throughput test rate in kilobits per second
	/// </summary>
	public int ThroughputRate { get; set; } = 1000;

	/// <summary>
	/// List of source agents assigned to this test
	/// </summary>
	public TestAgent[] Agents { get; set; } = [];
}