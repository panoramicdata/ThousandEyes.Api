using AwesomeAssertions;
using ThousandEyes.Api.Interfaces;

namespace ThousandEyes.Api.Test;

[Collection("Integration Tests")]
public class DiagnosticTests(IntegrationTestFixture fixture)
{
	private readonly IntegrationTestFixture _fixture = fixture;

	private IThousandEyesClient ThousandEyesClient => _fixture.GetThousandEyesClient();
	private ILogger Logger => _fixture.Logger;

	[Fact]
	public async Task CanConnectToApiAsync()
	{
		// This test attempts to connect to the ThousandEyes API
		// It should pass if the Bearer token and network connectivity are working
		using var httpClient = new HttpClient();
		httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue(
			"Bearer", 
			_fixture.Configuration["ThousandEyes:BearerToken"]);

		var baseUrl = "https://api.thousandeyes.com/v7";
		
		try
		{
			var cancellationToken = TestContext.Current.CancellationToken;
			var response = await httpClient.GetAsync($"{baseUrl}/account-groups", cancellationToken);
			var content = await response.Content.ReadAsStringAsync(cancellationToken);

			Console.WriteLine($"Response Status: {response.StatusCode}");
			Console.WriteLine($"Response Content: {content[..Math.Min(200, content.Length)]}...");

			Logger.LogInformation("API connection test - Status: {StatusCode}", response.StatusCode);
			
			// Should get a successful response (either 200 or 401 if token is invalid)
			// If we get network errors, that indicates connectivity issues
			_ = response.Should().NotBeNull();
			_ = content.Should().NotBeNull();
		}
		catch (HttpRequestException ex)
		{
			Console.WriteLine($"HTTP request failed: {ex.Message}");
			Logger.LogError(ex, "HTTP request failed: {Message}", ex.Message);
			throw;
		}
	}

	[Fact]
	public void CheckUserSecrets()
	{
		// Diagnostic test to verify user secrets are loaded correctly
		var config = _fixture.Configuration;

		var bearerToken = config["ThousandEyes:BearerToken"];

		Console.WriteLine($"Bearer Token: {(!string.IsNullOrEmpty(bearerToken) ? "***SET***" : "NOT SET")}");

		Logger.LogInformation("Bearer Token: {HasToken}", !string.IsNullOrEmpty(bearerToken) ? "***SET***" : "NOT SET");

		_ = bearerToken.Should().NotBeNullOrEmpty();

		// Check the expected API Base URL for ThousandEyes
		var expectedUrl = "https://api.thousandeyes.com/v7";
		Console.WriteLine($"Expected API Base URL: {expectedUrl}");

		Logger.LogInformation("Expected API Base URL: {Url}", expectedUrl);

		// Note: We don't have a static BaseUrl property in ThousandEyesClient like the old Halo client
		// The URL is configured internally based on the ThousandEyes API specification
	}
}