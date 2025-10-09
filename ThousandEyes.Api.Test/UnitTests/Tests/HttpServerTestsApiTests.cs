using AwesomeAssertions;
using Moq;
using ThousandEyes.Api.Interfaces;
using ThousandEyes.Api.Models.Tests;

namespace ThousandEyes.Api.Test.UnitTests.Tests;

public class HttpServerTestsApiTests
{
	private readonly Mock<IHttpServerTestsRefitApi> _refitApi;
	private readonly HttpServerTestsApi _sut;

	public HttpServerTestsApiTests()
	{
		_refitApi = new Mock<IHttpServerTestsRefitApi>();
		_sut = new HttpServerTestsApi(_refitApi.Object);
	}

	[Fact]
	public async Task GetAllAsync_CallsApi_AndReturnsData()
	{
		// Arrange
		var cancellationToken = new CancellationToken();
		var expectedResponse = new HttpServerTests
		{
			Tests = [
				new HttpServerTest
				{
					TestId = "123",
					TestName = "HTTP Test",
					Type = "http-server",
					Interval = 300,
					Url = "https://example.com"
				}
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
		var testId = "123";
		var cancellationToken = new CancellationToken();
		var expectedResponse = new HttpServerTest
		{
			TestId = testId,
			TestName = "HTTP Test",
			Type = "http-server",
			Interval = 300,
			Url = "https://example.com"
		};
		_ = _refitApi.Setup(x => x.GetByIdAsync(testId, null, null, null, cancellationToken))
			.ReturnsAsync(expectedResponse);

		// Act
		var result = await _sut.GetByIdAsync(testId, null, null, null, cancellationToken);

		// Assert
		_ = result.Should().Be(expectedResponse);
		_refitApi.Verify(x => x.GetByIdAsync(testId, null, null, null, cancellationToken), Times.Once);
	}

	[Fact]
	public async Task CreateAsync_CallsApi_AndReturnsData()
	{
		// Arrange
		var cancellationToken = new CancellationToken();
		var request = new HttpServerTestRequest
		{
			TestId = "123",
			TestName = "New Test",
			Type = "http-server",
			Interval = 300,
			Url = "https://example.com",
			Agents = []
		};
		var expectedResponse = new HttpServerTest
		{
			TestId = "123",
			TestName = "New Test",
			Type = "http-server",
			Interval = 300,
			Url = "https://example.com"
		};
		_ = _refitApi.Setup(x => x.CreateAsync(request, null, null, cancellationToken))
			.ReturnsAsync(expectedResponse);

		// Act
		var result = await _sut.CreateAsync(request, null, null, cancellationToken);

		// Assert
		_ = result.Should().Be(expectedResponse);
		_refitApi.Verify(x => x.CreateAsync(request, null, null, cancellationToken), Times.Once);
	}

	[Fact]
	public async Task UpdateAsync_CallsApi_AndReturnsData()
	{
		// Arrange
		var testId = "123";
		var request = new HttpServerTestRequest
		{
			TestId = testId,
			TestName = "Updated Test",
			Type = "http-server",
			Interval = 300,
			Url = "https://example.com",
			Agents = []
		};
		var cancellationToken = new CancellationToken();
		var expectedResponse = new HttpServerTest
		{
			TestId = testId,
			TestName = "Updated Test",
			Type = "http-server",
			Interval = 300,
			Url = "https://example.com"
		};
		_ = _refitApi.Setup(x => x.UpdateAsync(testId, request, null, null, cancellationToken))
			.ReturnsAsync(expectedResponse);

		// Act
		var result = await _sut.UpdateAsync(testId, request, null, null, cancellationToken);

		// Assert
		_ = result.Should().Be(expectedResponse);
		_refitApi.Verify(x => x.UpdateAsync(testId, request, null, null, cancellationToken), Times.Once);
	}

	[Fact]
	public async Task DeleteAsync_CallsApi()
	{
		// Arrange
		var testId = "123";
		var cancellationToken = new CancellationToken();
		_ = _refitApi.Setup(x => x.DeleteAsync(testId, null, cancellationToken))
			.Returns(Task.CompletedTask);

		// Act
		await _sut.DeleteAsync(testId, null, cancellationToken);

		// Assert
		_refitApi.Verify(x => x.DeleteAsync(testId, null, cancellationToken), Times.Once);
	}
}
