using System.Text.Json.Serialization;

namespace ThousandEyes.Api.Models.TestSnapshots;

/// <summary>
/// Response after creating a test snapshot.
/// </summary>
public class SnapshotResponse : ApiResource
{
	/// <summary>
	/// Snapshot ID.
	/// </summary>
	[JsonPropertyName("id")]
	public string? Id { get; set; }

	/// <summary>
	/// The start time of the test snapshot, represented in epoch time format (in seconds).
	/// </summary>
	[JsonPropertyName("startRoundId")]
	public long? StartRoundId { get; set; }

	/// <summary>
	/// The end time of the test snapshot, represented in epoch time format (in seconds).
	/// </summary>
	[JsonPropertyName("endRoundId")]
	public long? EndRoundId { get; set; }

	/// <summary>
	/// The selected time of the test snapshot, represented in epoch time format (in seconds).
	/// </summary>
	[JsonPropertyName("roundId")]
	public long? RoundId { get; set; }

	/// <summary>
	/// The date when the test snapshot was created in UTC time, formatted in ISO date-time.
	/// </summary>
	[JsonPropertyName("shareDate")]
	public DateTime? ShareDate { get; set; }

	/// <summary>
	/// Source test ID (original test).
	/// </summary>
	[JsonPropertyName("sourceTestId")]
	public string? SourceTestId { get; set; }

	/// <summary>
	/// Snapshot test ID.
	/// </summary>
	[JsonPropertyName("testId")]
	public string? TestId { get; set; }

	/// <summary>
	/// User ID who created the snapshot.
	/// </summary>
	[JsonPropertyName("uid")]
	public string? Uid { get; set; }

	/// <summary>
	/// Snapshot title.
	/// </summary>
	[JsonPropertyName("displayName")]
	public string? DisplayName { get; set; }

	/// <summary>
	/// Extra parameters.
	/// </summary>
	[JsonPropertyName("extraParams")]
	public string? ExtraParams { get; set; }
}
