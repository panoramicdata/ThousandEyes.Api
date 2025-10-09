using System.Text.Json.Serialization;

namespace ThousandEyes.Api.Models.Emulation;

/// <summary>
/// A user-agent string for HTTP, pageload, and transaction tests
/// </summary>
public class UserAgent
{
	/// <summary>
	/// The name of the web browser
	/// </summary>
	[JsonPropertyName("browser")]
	public string? Browser { get; set; }

	/// <summary>
	/// The operating system for the user-agent HTTP header
	/// </summary>
	[JsonPropertyName("os")]
	public string? Os { get; set; }

	/// <summary>
	/// The text of the user-agent header
	/// </summary>
	[JsonPropertyName("value")]
	public string? Value { get; set; }
}