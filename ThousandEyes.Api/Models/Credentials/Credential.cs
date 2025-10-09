using System.Text.Json.Serialization;

namespace ThousandEyes.Api.Models.Credentials;

/// <summary>
/// Represents a credential with the encrypted value.
/// Returned from get by ID operation when user has "View sensitive data in web transaction scripts" permission.
/// </summary>
public class Credential : CredentialWithoutValue
{
	/// <summary>
	/// The encrypted value of the credential.
	/// Requires "View sensitive data in web transaction scripts" permission to view.
	/// </summary>
	[JsonPropertyName("value")]
	public string? Value { get; set; }
}
