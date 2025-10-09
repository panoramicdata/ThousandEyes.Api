using AwesomeAssertions;
using Moq;
using ThousandEyes.Api.Implementations.Credentials;
using ThousandEyes.Api.Models.Credentials;
using ThousandEyes.Api.Refit.Credentials;

namespace ThousandEyes.Api.Test.UnitTests.Credentials;

public class CredentialsImplTests
{
	private readonly Mock<ICredentialsRefitApi> _refitApi;
	private readonly CredentialsImpl _sut;

	public CredentialsImplTests()
	{
		_refitApi = new Mock<ICredentialsRefitApi>();
		_sut = new CredentialsImpl(_refitApi.Object);
	}

	[Fact]
	public async Task GetAllAsync_CallsApi_AndReturnsData()
	{
		// Arrange
		var cancellationToken = new CancellationToken();
		var expectedResponse = new Models.Credentials.Credentials
		{
			Items = [
				new Credential { Id = "123", Name = "Test Credential" }
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
		var id = "123";
		var cancellationToken = new CancellationToken();
		var expectedResponse = new Credential
		{
			Id = id,
			Name = "Test Credential"
		};
		_ = _refitApi.Setup(x => x.GetByIdAsync(id, null, cancellationToken))
			.ReturnsAsync(expectedResponse);

		// Act
		var result = await _sut.GetByIdAsync(id, null, cancellationToken);

		// Assert
		_ = result.Should().Be(expectedResponse);
		_refitApi.Verify(x => x.GetByIdAsync(id, null, cancellationToken), Times.Once);
	}

	[Fact]
	public async Task CreateAsync_CallsApi_AndReturnsData()
	{
		// Arrange
		var request = new CredentialRequest("Test", "password123");
		var cancellationToken = new CancellationToken();
		var expectedResponse = new CredentialWithoutValue();
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
		var id = "123";
		var request = new CredentialRequest("Updated", "newpassword");
		var cancellationToken = new CancellationToken();
		var expectedResponse = new CredentialWithoutValue();
		_ = _refitApi.Setup(x => x.UpdateAsync(id, request, null, cancellationToken))
			.ReturnsAsync(expectedResponse);

		// Act
		var result = await _sut.UpdateAsync(id, request, null, cancellationToken);

		// Assert
		_ = result.Should().Be(expectedResponse);
		_refitApi.Verify(x => x.UpdateAsync(id, request, null, cancellationToken), Times.Once);
	}

	[Fact]
	public async Task DeleteAsync_CallsApi()
	{
		// Arrange
		var id = "123";
		var cancellationToken = new CancellationToken();
		_ = _refitApi.Setup(x => x.DeleteAsync(id, null, cancellationToken))
			.Returns(Task.CompletedTask);

		// Act
		await _sut.DeleteAsync(id, null, cancellationToken);

		// Assert
		_refitApi.Verify(x => x.DeleteAsync(id, null, cancellationToken), Times.Once);
	}
}
