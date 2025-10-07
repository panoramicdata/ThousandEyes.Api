using AwesomeAssertions;

namespace ThousandEyes.Api.Test;

[Collection("Integration Tests")]
public class TestsIntegrationTest(IntegrationTestFixture fixture) : TestBase(fixture)
{
	[Fact]
	public async Task GetAllTests_WithValidRequest_ReturnsTests()
	{
		// Act
		var result = await ThousandEyesClient.Tests.Tests.GetAllAsync(null, CancellationToken);

		// Assert
		_ = result.Should().NotBeNull();
		_ = result.TestsList.Should().NotBeNull();

		// If there are tests, verify the structure
		if (result.TestsList.Length > 0)
		{
			var firstTest = result.TestsList[0];
			_ = firstTest.TestId.Should().NotBeNullOrEmpty();
			_ = firstTest.TestName.Should().NotBeNullOrEmpty();
			_ = firstTest.Type.Should().NotBeNullOrEmpty();
			_ = firstTest.Interval.Should().BeGreaterThan(0);
		}
	}

	[Fact]
	public async Task GetHttpServerTests_WithValidRequest_ReturnsHttpServerTests()
	{
		// Act
		var result = await ThousandEyesClient.Tests.HttpServerTests.GetAllAsync(null, CancellationToken);

		// Assert
		_ = result.Should().NotBeNull();
		// Note: Result might be empty if no HTTP Server tests exist, which is valid
	}
}