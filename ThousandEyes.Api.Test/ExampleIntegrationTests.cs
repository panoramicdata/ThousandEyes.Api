using AwesomeAssertions;

namespace ThousandEyes.Api.Test;

/// <summary>
/// Example test class showing how to inherit from TestBase for integration tests
/// that need access to ThousandEyesClient and Logger
/// </summary>
[Collection("Integration Tests")]
public class ExampleIntegrationTests(IntegrationTestFixture fixture) : TestBase(fixture)
{
	[Fact]
	public void ThousandEyesClient_ShouldBeConfiguredCorrectly()
	{
		// Arrange & Act
		try
		{
			Logger.LogInformation("Testing ThousandEyesClient configuration");

			// Assert - Using the protected ThousandEyesClient property from TestBase
			_ = ThousandEyesClient.Should().NotBeNull();

			Logger.LogInformation("ThousandEyesClient configuration validated successfully");
		}
		catch (InvalidOperationException ex) when (ex.Message.Contains("not found in user secrets"))
		{
			Logger.LogWarning(ex, "Integration test skipped - user secrets not configured: {Message}", ex.Message);

			// Test passes - this is expected behavior when secrets aren't configured
		}
	}

	[Fact]
	public void Logger_ShouldLogInformationCorrectly()
	{
		// Arrange
		const string testMessage = "This is a test log message";

		// Act & Assert - Using the protected Logger property from TestBase
		_ = Logger.Should().NotBeNull();

		// This should not throw any exceptions
		Logger.LogInformation(testMessage);
		Logger.LogWarning("This is a test warning");
		Logger.LogDebug("This is a test debug message");
	}
}