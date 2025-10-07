using System.Text.Json.Serialization;

namespace ThousandEyes.Api.Models.Tests;

/// <summary>
/// BGP test configuration
/// </summary>
public class BgpTest : SimpleTest
{
	/// <summary>
	/// Prefix to monitor (IP address/CIDR notation)
	/// </summary>
	public required string Prefix { get; set; }

	/// <summary>
	/// Whether to include covered prefixes in the test
	/// </summary>
	public bool IncludeCoveredPrefixes { get; set; }

	/// <summary>
	/// Use public BGP monitors only
	/// </summary>
	public bool UsePublicBgp { get; set; } = true;

	/// <summary>
	/// List of BGP monitors assigned to this test
	/// </summary>
	[JsonPropertyName("bgpMonitors")]
	public TestAgent[] BgpMonitors { get; set; } = [];
}