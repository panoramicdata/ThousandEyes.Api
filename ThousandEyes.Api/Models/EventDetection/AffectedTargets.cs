namespace ThousandEyes.Api.Models.EventDetection;

/// <summary>
/// Affected targets with details
/// </summary>
public class AffectedTargets : AffectedCount
{
	/// <summary>
	/// List of affected targets
	/// </summary>
	public AffectedTarget[] Targets { get; set; } = [];
}
