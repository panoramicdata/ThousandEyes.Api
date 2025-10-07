namespace ThousandEyes.Api.Models.Tests;

/// <summary>
/// Test navigation links
/// </summary>
public class TestLinks
{
	/// <summary>
	/// Self reference link
	/// </summary>
	public Link? Self { get; set; }

	/// <summary>
	/// Test results links
	/// </summary>
	public Link[]? TestResults { get; set; }
}