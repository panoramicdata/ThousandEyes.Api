using AwesomeAssertions;
using Moq;
using ThousandEyes.Api.Interfaces;
using ThousandEyes.Api.Models.Roles;

namespace ThousandEyes.Api.Test.UnitTests.Accounts;

public class RolesApiTests
{
	private readonly Mock<IRolesRefitApi> _refitApi;
	private readonly RolesApi _sut;

	public RolesApiTests()
	{
		_refitApi = new Mock<IRolesRefitApi>();
		_sut = new RolesApi(_refitApi.Object);
	}

	[Fact]
	public async Task GetAllAsync_CallsApi_AndReturnsData()
	{
		// Arrange
		var cancellationToken = new CancellationToken();
		var expectedResponse = new Roles
		{
			RolesList =
			[
				new Role { Name = "Test Role", RoleId = "123" }
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
	public async Task GetAsync_CallsApi_AndReturnsData()
	{
		// Arrange
		var roleId = "123";
		var cancellationToken = new CancellationToken();
		var expectedResponse = new RoleDetail
		{
			Name = "Test Role",
			RoleId = roleId
		};
		_ = _refitApi.Setup(x => x.GetByIdAsync(roleId, null, cancellationToken))
			.ReturnsAsync(expectedResponse);

		// Act
		var result = await _sut.GetByIdAsync(roleId, null, cancellationToken);

		// Assert
		_ = result.Should().Be(expectedResponse);
		_refitApi.Verify(x => x.GetByIdAsync(roleId, null, cancellationToken), Times.Once);
	}

	[Fact]
	public async Task CreateAsync_CallsApi_AndReturnsData()
	{
		// Arrange
		var cancellationToken = new CancellationToken();
		var request = new RoleRequestBody();
		var expectedResponse = new RoleDetail();
		_ = _refitApi.Setup(x => x.CreateAsync(request, null, cancellationToken))
			.ReturnsAsync(expectedResponse);

		// Act
		var result = await _sut.CreateAsync(request, null, cancellationToken);

		// Assert
		_ = result.Should().Be(expectedResponse);
		_refitApi.Verify(x => x.CreateAsync(request, null, cancellationToken), Times.Once);
	}

	[Fact]
	public async Task UpdateAsync_CallsApi_AndReturnsData()
	{
		// Arrange
		var roleId = "123";
		var request = new RoleRequestBody();
		var cancellationToken = new CancellationToken();
		var expectedResponse = new RoleDetail();
		_ = _refitApi.Setup(x => x.UpdateAsync(roleId, request, null, cancellationToken))
			.ReturnsAsync(expectedResponse);

		// Act
		var result = await _sut.UpdateAsync(roleId, request, null, cancellationToken);

		// Assert
		_ = result.Should().Be(expectedResponse);
		_refitApi.Verify(x => x.UpdateAsync(roleId, request, null, cancellationToken), Times.Once);
	}

	[Fact]
	public async Task DeleteAsync_CallsApi()
	{
		// Arrange
		var roleId = "123";
		var cancellationToken = new CancellationToken();
		_ = _refitApi.Setup(x => x.DeleteAsync(roleId, null, cancellationToken))
			.Returns(Task.CompletedTask);

		// Act
		await _sut.DeleteAsync(roleId, null, cancellationToken);

		// Assert
		_refitApi.Verify(x => x.DeleteAsync(roleId, null, cancellationToken), Times.Once);
	}
}
