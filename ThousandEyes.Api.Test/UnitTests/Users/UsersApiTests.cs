using AwesomeAssertions;
using Moq;
using ThousandEyes.Api.Interfaces;
using ThousandEyes.Api.Models.Users;
using UsersCollection = ThousandEyes.Api.Models.Users.Users;

namespace ThousandEyes.Api.Test.UnitTests.Users;

public class UsersApiTests
{
	private readonly Mock<IUsersRefitApi> _refitApi;
	private readonly UsersApi _sut;

	public UsersApiTests()
	{
		_refitApi = new Mock<IUsersRefitApi>();
		_sut = new UsersApi(_refitApi.Object);
	}

	[Fact]
	public async Task GetAllAsync_CallsApi_AndReturnsData()
	{
		// Arrange
		var cancellationToken = new CancellationToken();
		var expectedResponse = new UsersCollection
		{
			UsersList =
			[
				new User { Name = "Test User", Uid = "123" }
			]
		};
		_ = _refitApi.Setup(x => x.GetAllAsync(null, cancellationToken))
			.ReturnsAsync(expectedResponse);

		// Act
		var result = await _sut.GetAllAsync(null, cancellationToken);

		// Assert
		_ = result.Should().Be(expectedResponse);
		_refitApi.Verify(x => x.GetAllAsync(null, cancellationToken), Times.Once);
	}

	[Fact]
	public async Task GetByIdAsync_CallsApi_AndReturnsData()
	{
		// Arrange
		var uid = "123";
		var cancellationToken = new CancellationToken();
		var expectedResponse = new UserDetail
		{
			Name = "Test User",
			Uid = uid
		};
		_ = _refitApi.Setup(x => x.GetByIdAsync(uid, null, cancellationToken))
			.ReturnsAsync(expectedResponse);

		// Act
		var result = await _sut.GetByIdAsync(uid, null, cancellationToken);

		// Assert
		_ = result.Should().Be(expectedResponse);
		_refitApi.Verify(x => x.GetByIdAsync(uid, null, cancellationToken), Times.Once);
	}

	[Fact]
	public async Task CreateAsync_CallsApi_AndReturnsData()
	{
		// Arrange
		var cancellationToken = new CancellationToken();
		var request = new UserRequest { Name = "Test User", Email = "test@example.com" };
		var expectedResponse = new CreatedUser();
		_ = _refitApi.Setup(x => x.CreateAsync(request, null, cancellationToken))
			.ReturnsAsync(expectedResponse);

		// Act
		var result = await _sut.CreateAsync(request, null, cancellationToken);

		// Assert
		_ = result.Should().Be(expectedResponse);
		_refitApi.Verify(x => x.CreateAsync(request, null, cancellationToken), Times.Once);
	}

	[Fact]
	public async Task DeleteAsync_CallsApi()
	{
		// Arrange
		var uid = "123";
		var cancellationToken = new CancellationToken();
		_ = _refitApi.Setup(x => x.DeleteAsync(uid, null, cancellationToken))
			.Returns(Task.CompletedTask);

		// Act
		await _sut.DeleteAsync(uid, null, cancellationToken);

		// Assert
		_refitApi.Verify(x => x.DeleteAsync(uid, null, cancellationToken), Times.Once);
	}

	[Fact]
	public async Task UpdateAsync_CallsApi_AndReturnsData()
	{
		// Arrange
		var uid = "123";
		var request = new UserRequest { Name = "Updated User", Email = "updated@example.com" };
		var cancellationToken = new CancellationToken();
		var expectedResponse = new UserDetail();
		_ = _refitApi.Setup(x => x.UpdateAsync(uid, request, null, cancellationToken))
			.ReturnsAsync(expectedResponse);

		// Act
		var result = await _sut.UpdateAsync(uid, request, null, cancellationToken);

		// Assert
		_ = result.Should().Be(expectedResponse);
		_refitApi.Verify(x => x.UpdateAsync(uid, request, null, cancellationToken), Times.Once);
	}
}
