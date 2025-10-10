using System.Text.Json.Serialization;

namespace ThousandEyes.Api.Models.Integrations;

/// <summary>
/// Operation status
/// </summary>
[JsonConverter(typeof(JsonStringEnumConverter))]
public enum OperationStatus
{
	/// <summary>
	/// Operation is pending configuration
	/// </summary>
	[JsonPropertyName("pending")]
	Pending,

	/// <summary>
	/// Operation is successfully connected
	/// </summary>
	[JsonPropertyName("connected")]
	Connected,

	/// <summary>
	/// Operation is failing
	/// </summary>
	[JsonPropertyName("failing")]
	Failing,

	/// <summary>
	/// Operation is unverified
	/// </summary>
	[JsonPropertyName("unverified")]
	Unverified
}
