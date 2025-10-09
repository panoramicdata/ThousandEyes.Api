using System.Text.Json.Serialization;

namespace ThousandEyes.Api.Models.Credentials;

/// <summary>
/// Request model for creating or updating a credential.
/// </summary>
/// <param name="Name">The name of the credential.</param>
/// <param name="Value">The value of the credential that will be encrypted.</param>
public record CredentialRequest(
	[property: JsonPropertyName("name")] string Name,
	[property: JsonPropertyName("value")] string Value
);
