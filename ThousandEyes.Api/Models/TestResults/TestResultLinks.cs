namespace ThousandEyes.Api.Models.TestResults;

/// <summary>
/// Test result navigation links
/// </summary>
public class TestResultLinks
{
	/// <summary>
	/// Self reference link
	/// </summary>
	public TestResultLink? Self { get; set; }

	/// <summary>
	/// Next page link
	/// </summary>
	public TestResultLink? Next { get; set; }

	/// <summary>
	/// Previous page link
	/// </summary>
	public TestResultLink? Previous { get; set; }
}

/// <summary>
/// Test result navigation link
/// </summary>
public class TestResultLink
{
	/// <summary>
	/// The href URL of the link
	/// </summary>
	public required string Href { get; set; }
}