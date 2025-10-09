using AwesomeAssertions;
using Moq;
using ThousandEyes.Api.Interfaces;
using ThousandEyes.Api.Models.Templates;
using TemplatesCollection = ThousandEyes.Api.Models.Templates.Templates;

namespace ThousandEyes.Api.Test.UnitTests.Templates;

public class TemplatesApiTests
{
	private readonly Mock<ITemplatesRefitApi> _refitApi;
	private readonly TemplatesApi _sut;

	public TemplatesApiTests()
	{
		_refitApi = new Mock<ITemplatesRefitApi>();
		_sut = new TemplatesApi(_refitApi.Object);
	}

	[Fact]
	public async Task GetAllAsync_CallsApi_AndReturnsData()
	{
		// Arrange
		var cancellationToken = new CancellationToken();
		var expectedResponse = new TemplatesCollection
		{
			Items = [
				new TemplateResponse { Id = "123", Name = "Test Template" }
			]
		};
		_ = _refitApi.Setup(x => x.GetAllAsync(null, null, null, null, cancellationToken))
			.ReturnsAsync(expectedResponse);

		// Act
		var result = await _sut.GetAllAsync(null, null, null, null, cancellationToken);

		// Assert
		_ = result.Should().Be(expectedResponse);
		_refitApi.Verify(x => x.GetAllAsync(null, null, null, null, cancellationToken), Times.Once);
	}

	[Fact]
	public async Task GetByIdAsync_CallsApi_AndReturnsData()
	{
		// Arrange
		var id = "123";
		var cancellationToken = new CancellationToken();
		var expectedResponse = new TemplateResponse
		{
			Id = id,
			Name = "Test Template"
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
		var cancellationToken = new CancellationToken();
		var request = new Template { Name = "New Template" };
		var expectedResponse = new TemplateResponse { Name = "New Template" };
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
		var request = new Template { Name = "Updated Template" };
		var cancellationToken = new CancellationToken();
		var expectedResponse = new TemplateResponse { Name = "Updated Template" };
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

	[Fact]
	public async Task DeployAsync_CallsApi_AndReturnsData()
	{
		// Arrange
		var id = "123";
		var request = new DeployTemplate();
		var cancellationToken = new CancellationToken();
		var expectedResponse = new TemplateResponse { Name = "Deployed Template" };
		_ = _refitApi.Setup(x => x.DeployAsync(id, request, null, cancellationToken))
			.ReturnsAsync(expectedResponse);

		// Act
		var result = await _sut.DeployAsync(id, request, null, cancellationToken);

		// Assert
		_ = result.Should().Be(expectedResponse);
		_refitApi.Verify(x => x.DeployAsync(id, request, null, cancellationToken), Times.Once);
	}

	[Fact]
	public async Task GetSharingSettingsAsync_CallsApi_AndReturnsData()
	{
		// Arrange
		var id = "123";
		var cancellationToken = new CancellationToken();
		var expectedResponse = new SharingSettingsResponse { Scope = SharingScope.Default };
		_ = _refitApi.Setup(x => x.GetSharingSettingsAsync(id, null, cancellationToken))
			.ReturnsAsync(expectedResponse);

		// Act
		var result = await _sut.GetSharingSettingsAsync(id, null, cancellationToken);

		// Assert
		_ = result.Should().Be(expectedResponse);
		_refitApi.Verify(x => x.GetSharingSettingsAsync(id, null, cancellationToken), Times.Once);
	}

	[Fact]
	public async Task UpdateSharingSettingsAsync_CallsApi_AndReturnsData()
	{
		// Arrange
		var id = "123";
		var request = new SharingSettings { Scope = SharingScope.Organization };
		var cancellationToken = new CancellationToken();
		var expectedResponse = new SharingSettingsResponse { Scope = SharingScope.Organization };
		_ = _refitApi.Setup(x => x.UpdateSharingSettingsAsync(id, request, null, cancellationToken))
			.ReturnsAsync(expectedResponse);

		// Act
		var result = await _sut.UpdateSharingSettingsAsync(id, request, null, cancellationToken);

		// Assert
		_ = result.Should().Be(expectedResponse);
		_refitApi.Verify(x => x.UpdateSharingSettingsAsync(id, request, null, cancellationToken), Times.Once);
	}
}
