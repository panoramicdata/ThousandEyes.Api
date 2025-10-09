using System.Text.Json.Serialization;

namespace ThousandEyes.Api.Models.Credentials;

/// <summary>
/// Represents a list of credentials.
/// </summary>
public class Credentials : ApiResource
{
	/// <summary>
	/// List of credentials.
	/// </summary>
	[JsonPropertyName("credentials")]
	public List<Credential> Items { get; set; } = [];
}
