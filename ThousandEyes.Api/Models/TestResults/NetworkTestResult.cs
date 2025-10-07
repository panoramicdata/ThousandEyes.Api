using System.Text.Json.Serialization;

namespace ThousandEyes.Api.Models.TestResults;

/// <summary>
/// Network test result data
/// </summary>
public class NetworkTestResult
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
	/// Network loss percentage (0-100)
	/// </summary>
	public double? Loss { get; set; }

	/// <summary>
	/// Average latency in milliseconds
	/// </summary>
	public double? Latency { get; set; }

	/// <summary>
	/// Jitter in milliseconds
	/// </summary>
	public double? Jitter { get; set; }

	/// <summary>
	/// Available bandwidth in kbps
	/// </summary>
	public long? AvailableBandwidth { get; set; }

	/// <summary>
	/// Mean deviation in latency
	/// </summary>
	public double? MeanDeviation { get; set; }

	/// <summary>
	/// Maximum latency observed
	/// </summary>
	public double? MaxLatency { get; set; }

	/// <summary>
	/// Minimum latency observed
	/// </summary>
	public double? MinLatency { get; set; }

	/// <summary>
	/// Standard deviation of latency
	/// </summary>
	public double? StdDeviation { get; set; }

	/// <summary>
	/// Path trace information
	/// </summary>
	[JsonPropertyName("pathVis")]
	public PathVisualization[]? PathVis { get; set; }

	/// <summary>
	/// Error details if test failed
	/// </summary>
	public string? ErrorDetails { get; set; }

	/// <summary>
	/// Navigation links
	/// </summary>
	public TestResultLinks? Links { get; set; }
}