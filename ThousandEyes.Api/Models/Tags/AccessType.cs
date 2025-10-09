using System.Text.Json.Serialization;

namespace ThousandEyes.Api.Models.Tags;

/// <summary>
/// The access level of the tag. The access level impacts the visibility
/// of the tag in UI and the permissions to modify the tag.
/// </summary>
[JsonConverter(typeof(JsonStringEnumConverter))]
public enum AccessType
{
	/// <summary>
	/// All users can view and modify
	/// </summary>
	All,

	/// <summary>
	/// Partner level access
	/// </summary>
	Partner,

	/// <summary>
	/// System level access
	/// </summary>
	System
}
