using System.Text.Json.Serialization;

namespace ThousandEyes.Api.Models.Templates;

/// <summary>
/// The certification level of the Template.
/// </summary>
[JsonConverter(typeof(JsonStringEnumConverter))]
public enum CertificationLevel
{
	/// <summary>
	/// User created template; not verified by ThousandEyes
	/// </summary>
	User,

	/// <summary>
	/// Created by ThousandEyes
	/// </summary>
	Thousandeyes,

	/// <summary>
	/// Created by ThousandEyes and approved by Partner/Vendor
	/// </summary>
	Partner,

	/// <summary>
	/// Created and certified by ThousandEyes using included best practice guide
	/// </summary>
	Certified
}
