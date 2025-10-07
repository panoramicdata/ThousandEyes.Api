using System.Text.Json.Serialization;

namespace ThousandEyes.Api.Models.Tests;

/// <summary>
/// DNS Server test configuration
/// </summary>
public class DnsServerTest : SimpleTest
{
	/// <summary>
	/// Domain name to query
	/// </summary>
	public required string Domain { get; set; }

	/// <summary>
	/// DNS record type to query (A, AAAA, CNAME, MX, etc.)
	/// </summary>
	public required string RecordType { get; set; }

	/// <summary>
	/// DNS server to query (if not specified, uses default)
	/// </summary>
	public string? DnsServer { get; set; }

	/// <summary>
	/// Expected response value (optional)
	/// </summary>
	public string? ExpectedResponse { get; set; }

	/// <summary>
	/// DNS transport protocol (UDP or TCP)
	/// </summary>
	public string DnsTransportProtocol { get; set; } = "UDP";

	/// <summary>
	/// Recursive query flag
	/// </summary>
	public bool RecursiveQueries { get; set; } = true;

	/// <summary>
	/// Enable network layer measurements
	/// </summary>
	public bool NetworkMeasurements { get; set; } = true;

	/// <summary>
	/// Enable BGP measurements
	/// </summary>
	public bool BgpMeasurements { get; set; }

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