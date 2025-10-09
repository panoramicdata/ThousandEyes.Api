using System.Text.Json.Serialization;

namespace ThousandEyes.Api.Models.Credentials;

/// <summary>
/// Represents a credential without the encrypted value.
/// Returned from list, create, and update operations.
/// </summary>
public class CredentialWithoutValue : ApiResource
{
	/// <summary>
	/// Unique ID of the credential.
	/// </summary>
	[JsonPropertyName("id")]
	public string? Id { get; set; }

	/// <summary>
	/// The name of the credential.
	/// </summary>
	[JsonPropertyName("name")]
	public string? Name { get; set; }
}
