namespace ThousandEyes.Api.Models.TestResults;

/// <summary>
/// Path visualization hop information
/// </summary>
public class PathVisualization
{
	/// <summary>
	/// Hop number in the path
	/// </summary>
	public required int HopNumber { get; set; }

	/// <summary>
	/// IP address of the hop
	/// </summary>
	public required string IpAddress { get; set; }

	/// <summary>
	/// Hostname of the hop (if resolved)
	/// </summary>
	public string? Hostname { get; set; }

	/// <summary>
	/// Network information for this hop
	/// </summary>
	public string? Network { get; set; }

	/// <summary>
	/// AS (Autonomous System) number
	/// </summary>
	public int? AsNumber { get; set; }

	/// <summary>
	/// Response time to this hop in milliseconds
	/// </summary>
	public double? ResponseTime { get; set; }

	/// <summary>
	/// MPLS label information
	/// </summary>
	public string[]? MplsLabels { get; set; }
}