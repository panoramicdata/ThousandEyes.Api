using System.Text.Json.Serialization;

namespace ThousandEyes.Api.Models.Teams;

/// <summary>
/// Response wrapper for team list operations
/// </summary>
public record TeamsResponse
{
	/// <summary>
	/// The list of teams
	/// </summary>
	[JsonPropertyName("teams")]
	public IReadOnlyList<Team> Teams { get; init; } = [];

	/// <summary>
	/// The total number of teams in the system
	/// </summary>
	[JsonPropertyName("record_count")]
	public int RecordCount { get; init; }
}