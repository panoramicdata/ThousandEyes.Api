using AwesomeAssertions;

namespace ThousandEyes.Api.Test;

[Collection("Integration Tests")]
public class UsersIntegrationTest(IntegrationTestFixture fixture) : TestBase(fixture)
{
	[Fact]
	public async Task GetUsers_WithValidRequest_ReturnsUsers()
	{
		// Act
		var result = await ThousandEyesClient.AccountManagement.Users.GetAllAsync(aid: null, CancellationToken);

		// Assert
		_ = result.Should().NotBeNull();
		_ = result.UsersList.Should().NotBeNull();

		// If there are users, verify the structure
		if (result.UsersList.Length > 0)
		{
			var firstUser = result.UsersList[0];
			_ = firstUser.Uid.Should().NotBeNullOrEmpty();
			_ = firstUser.Name.Should().NotBeNullOrEmpty();
			_ = firstUser.Email.Should().NotBeNullOrEmpty();
		}
	}

	[Fact]
	public async Task GetCurrentUser_WithValidRequest_ReturnsCurrentUser()
	{
		// Act
		var result = await ThousandEyesClient.AccountManagement.Users.GetCurrentAsync(CancellationToken);

		// Assert
		_ = result.Should().NotBeNull();
		_ = result.Uid.Should().NotBeNullOrEmpty();
		_ = result.Name.Should().NotBeNullOrEmpty();
		_ = result.Email.Should().NotBeNullOrEmpty();
	}
}
