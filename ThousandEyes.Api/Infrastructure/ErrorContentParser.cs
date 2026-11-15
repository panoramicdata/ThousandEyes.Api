using System.Text.Json;

namespace ThousandEyes.Api.Infrastructure;

/// <summary>
/// The parts of an API error body that the exceptions expose
/// </summary>
internal sealed record ParsedError(
	Dictionary<string, object?>? Details,
	IReadOnlyList<string>? ValidationErrors,
	string? Message,
	string? Code);

/// <summary>
/// Reads a ThousandEyes error body without ever throwing on malformed content
/// </summary>
internal static class ErrorContentParser
{
	private static readonly ParsedError Empty = new(null, null, null, null);

	public static ParsedError Parse(string? content)
	{
		if (string.IsNullOrEmpty(content))
		{
			return Empty;
		}

		try
		{
			using var document = JsonDocument.Parse(content);
			if (document.RootElement.ValueKind == JsonValueKind.Object)
			{
				return FromObject(document.RootElement);
			}
		}
		catch (JsonException)
		{
			// Fall through to the raw content below
		}

		return Empty with { Details = new Dictionary<string, object?> { ["rawContent"] = content } };
	}

	private static ParsedError FromObject(JsonElement root) => new(
		ExtractDetails(root),
		ExtractValidationErrors(root),
		GetString(root, "message"),
		GetString(root, "error"));

	private static string? GetString(JsonElement root, string name) =>
		root.TryGetProperty(name, out var element) && element.ValueKind == JsonValueKind.String
			? element.GetString()
			: null;

	private static List<string>? ExtractValidationErrors(JsonElement root)
	{
		if (!root.TryGetProperty("errors", out var errors))
		{
			return null;
		}

		return errors.ValueKind == JsonValueKind.Array
			? [.. errors.EnumerateArray().Where(e => e.ValueKind == JsonValueKind.String).Select(e => e.GetString() ?? string.Empty)]
			: [];
	}

	private static Dictionary<string, object?> ExtractDetails(JsonElement root)
	{
		var details = new Dictionary<string, object?>();
		foreach (var property in root.EnumerateObject())
		{
			details[property.Name] = ToValue(property.Value);
		}

		return details;
	}

	private static object? ToValue(JsonElement value) => value.ValueKind switch
	{
		JsonValueKind.String => value.GetString(),
		JsonValueKind.Number => value.TryGetInt32(out var intValue) ? intValue : value.GetDouble(),
		JsonValueKind.True => true,
		JsonValueKind.False => false,
		JsonValueKind.Null => null,
		_ => value.GetRawText()
	};
}
