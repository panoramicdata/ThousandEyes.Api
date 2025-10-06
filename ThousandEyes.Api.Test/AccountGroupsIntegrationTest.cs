using AwesomeAssertions;

namespace ThousandEyes.Api.Test;

[Collection("Integration Tests")]
public class AccountGroupsIntegrationTest(IntegrationTestFixture fixture) : TestBase(fixture)
{
	[Fact]
	public async Task GetAccountGroups_WithValidRequest_ReturnsAccountGroups()
	{
		// Act
		var result = await ThousandEyesClient.AccountManagement.AccountGroups.GetAllAsync(CancellationToken);

		// Assert
		_ = result.Should().NotBeNull();
		_ = result.AccountGroupsList.Should().NotBeNull();

		// If there are account groups, verify the structure
		if (result.AccountGroupsList.Length > 0)
		{
			var firstGroup = result.AccountGroupsList[0];
			_ = firstGroup.Aid.Should().NotBeNullOrEmpty();
			_ = firstGroup.AccountGroupName.Should().NotBeNullOrEmpty();
		}
	}
}