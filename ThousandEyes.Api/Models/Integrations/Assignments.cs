namespace ThousandEyes.Api.Models.Integrations;

/// <summary>
/// Response containing list of assigned items (connector IDs or operation IDs)
/// </summary>
public class Assignments : ApiResource
{
	/// <summary>
	/// List of assigned item IDs
	/// </summary>
	public string[] Items { get; set; } = [];
}
