using System.Net.Http.Headers;
using Microsoft.Extensions.Configuration;

namespace ThousandEyes.Api.Test;

/// <summary>
/// Simple standalone test to verify Bearer Token authentication
/// </summary>
public class StandaloneBearerTokenTest
{
	public static async Task<int> TestBearerTokenAsync(string[] args)
	{
		Console.WriteLine("=== ThousandEyes Bearer Token Authentication Test ===");
		Console.WriteLine();

		// Build configuration to read from user secrets
		var configuration = new ConfigurationBuilder()
			.AddUserSecrets<StandaloneBearerTokenTest>()
			.Build();

		var bearerToken = configuration["ThousandEyes:BearerToken"];

		if (string.IsNullOrEmpty(bearerToken))
		{
			Console.WriteLine("? ERROR: ThousandEyes:BearerToken not found in user secrets.");
			Console.WriteLine("Please run: dotnet user-secrets set \"ThousandEyes:BearerToken\" \"your-bearer-token\"");
			return 1;
		}

		Console.WriteLine($"? Bearer Token loaded: {bearerToken[..10]}***");
		Console.WriteLine();

		// Test direct API call
		using var httpClient = new HttpClient();
		httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", bearerToken);

		var baseUrl = "https://api.thousandeyes.com/v7";
		var testUrl = $"{baseUrl}/account-groups";

		Console.WriteLine($"?? Testing API connection to: {testUrl}");
		Console.WriteLine();

		try
		{
			var response = await httpClient.GetAsync(testUrl);
			var content = await response.Content.ReadAsStringAsync();

			Console.WriteLine($"?? Response Status: {response.StatusCode} ({(int)response.StatusCode})");
			Console.WriteLine($"?? Content Length: {content.Length} characters");

			if (response.IsSuccessStatusCode)
			{
				Console.WriteLine("? SUCCESS: Bearer Token authentication is working!");
				Console.WriteLine("?? Response preview:");
				Console.WriteLine(content[..Math.Min(300, content.Length)]);
				if (content.Length > 300)
				{
					Console.WriteLine("...(truncated)");
				}
				return 0;
			}
			else
			{
				Console.WriteLine($"? FAILED: HTTP {response.StatusCode}");
				Console.WriteLine("?? Error response:");
				Console.WriteLine(content);
				return 1;
			}
		}
		catch (HttpRequestException ex)
		{
			Console.WriteLine($"? NETWORK ERROR: {ex.Message}");
			return 1;
		}
		catch (Exception ex)
		{
			Console.WriteLine($"? UNEXPECTED ERROR: {ex.Message}");
			return 1;
		}
	}
}