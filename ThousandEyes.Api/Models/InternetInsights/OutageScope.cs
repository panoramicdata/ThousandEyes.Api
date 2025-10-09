namespace ThousandEyes.Api.Models.InternetInsights;

/// <summary>
/// Scope of the outage filter
/// </summary>
public enum OutageScope
{
	/// <summary>
	/// All outages
	/// </summary>
	All,

	/// <summary>
	/// Only outages affecting tests
	/// </summary>
	WithAffectedTest
}
