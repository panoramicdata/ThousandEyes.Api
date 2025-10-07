namespace ThousandEyes.Api.Models.Tests;

/// <summary>
/// HTTP Server test creation/update request
/// </summary>
public class HttpServerTestRequest : HttpServerTest
{
	/// <summary>
	/// List of agent assignments for the test
	/// </summary>
	public new required TestAgentRequest[] Agents { get; set; }

	/// <summary>
	/// List of alert rule IDs to apply to the test
	/// </summary>
	public string[]? AlertRules { get; set; }

	/// <summary>
	/// List of test label IDs to assign to the test
	/// </summary>
	public string[]? Labels { get; set; }

	/// <summary>
	/// List of account group IDs to share the test with
	/// </summary>
	public string[]? SharedWithAccounts { get; set; }

	/// <summary>
	/// List of BGP monitor IDs for BGP measurements
	/// </summary>
	public string[]? Monitors { get; set; }
}
