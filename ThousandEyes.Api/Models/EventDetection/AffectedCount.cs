namespace ThousandEyes.Api.Models.EventDetection;

/// <summary>
/// Count of affected items
/// </summary>
public class AffectedCount
{
	/// <summary>
	/// Total number affected
	/// </summary>
	public int Total { get; set; }

	/// <summary>
	/// Number affected in the account group
	/// </summary>
	public int InAccountGroup { get; set; }
}
