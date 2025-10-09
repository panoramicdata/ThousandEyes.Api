using AwesomeAssertions;
using Moq;
using ThousandEyes.Api.Interfaces;
using ThousandEyes.Api.Models.AccountGroups;

namespace ThousandEyes.Api.Test.UnitTests.Accounts;

public class AccountGroupsApiTests
{
	private readonly Mock<IAccountGroupsRefitApi> _refitApi;
	private readonly AccountGroupsApi _sut;

	public AccountGroupsApiTests()
	{
		_refitApi = new Mock<IAccountGroupsRefitApi>();
		_sut = new AccountGroupsApi(_refitApi.Object);
	}

	[Fact]
	public async Task GetAllAsync_CallsApi_AndReturnsData()
	{
		// Arrange
		var cancellationToken = new CancellationToken();
		var expectedResponse = new AccountGroups
		{
			AccountGroupsList =
			[
				new AccountGroupInfo { AccountGroupName = "Test Group", Aid = "123" }
			]
		};
		_ = _refitApi.Setup(x => x.GetAllAsync(cancellationToken))
			.ReturnsAsync(expectedResponse);

		// Act
		var result = await _sut.GetAllAsync(cancellationToken);

		// Assert
		_ = result.Should().Be(expectedResponse);
		_refitApi.Verify(x => x.GetAllAsync(cancellationToken), Times.Once);
	}

	[Fact]
	public async Task GetAsync_CallsApi_AndReturnsData()
	{
		// Arrange
		var aid = "123";
		var cancellationToken = new CancellationToken();
		var expectedResponse = new AccountGroupDetail
		{
			AccountGroupName = "Test Group",
			Aid = aid
		};
		_ = _refitApi.Setup(x => x.GetByIdAsync(aid, null, cancellationToken))
			.ReturnsAsync(expectedResponse);

		// Act
		var result = await _sut.GetByIdAsync(aid, null, cancellationToken);

		// Assert
		_ = result.Should().Be(expectedResponse);
		_refitApi.Verify(x => x.GetByIdAsync(aid, null, cancellationToken), Times.Once);
	}
}
