using AwesomeAssertions;
using Moq;
using ThousandEyes.Api.Implementations.InternetInsights;
using ThousandEyes.Api.Models.InternetInsights;
using ThousandEyes.Api.Refit.InternetInsights;

namespace ThousandEyes.Api.Test.UnitTests.InternetInsights;

public class CatalogProvidersImplTests
{
	private readonly Mock<ICatalogProvidersRefitApi> _refitApi;
	private readonly CatalogProvidersImpl _sut;

	public CatalogProvidersImplTests()
	{
		_refitApi = new Mock<ICatalogProvidersRefitApi>();
		_sut = new CatalogProvidersImpl(_refitApi.Object);
	}

	[Fact]
	public async Task FilterAsync_CallsApi_AndReturnsData()
	{
		// Arrange
		var filter = new CatalogProviderFilter();
		var cancellationToken = new CancellationToken();
		var expectedResponse = new CatalogProviderResponse
		{
			ProvidersList = [
				new CatalogProvider { Id = "123", ProviderName = "Test Provider" }
			]
		};
		_ = _refitApi.Setup(x => x.FilterAsync(filter, null, cancellationToken))
			.ReturnsAsync(expectedResponse);

		// Act
		var result = await _sut.FilterAsync(filter, null, cancellationToken);

		// Assert
		_ = result.Should().Be(expectedResponse);
		_refitApi.Verify(x => x.FilterAsync(filter, null, cancellationToken), Times.Once);
	}

	[Fact]
	public async Task GetByIdAsync_CallsApi_AndReturnsData()
	{
		// Arrange
		var providerId = "123";
		var cancellationToken = new CancellationToken();
		var expectedResponse = new CatalogProviderDetails
		{
			Id = providerId,
			ProviderName = "Test Provider"
		};
		_ = _refitApi.Setup(x => x.GetByIdAsync(providerId, null, cancellationToken))
			.ReturnsAsync(expectedResponse);

		// Act
		var result = await _sut.GetByIdAsync(providerId, null, cancellationToken);

		// Assert
		_ = result.Should().Be(expectedResponse);
		_refitApi.Verify(x => x.GetByIdAsync(providerId, null, cancellationToken), Times.Once);
	}
}
