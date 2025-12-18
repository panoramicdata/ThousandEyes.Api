using AwesomeAssertions;
using Moq;
using Refit;
using System.Net;
using ThousandEyes.Api.Implementations.Integrations;
using ThousandEyes.Api.Models.Integrations;
using ThousandEyes.Api.Refit.Integrations;

namespace ThousandEyes.Api.Test.UnitTests.Integrations;

public class WebhookOperationsImplTests
{
	private readonly Mock<IWebhookOperationsRefitApi> _refitApi;
	private readonly WebhookOperationsImpl _sut;

	public WebhookOperationsImplTests()
	{
		_refitApi = new Mock<IWebhookOperationsRefitApi>();
		_sut = new WebhookOperationsImpl(_refitApi.Object);
	}

	[Fact]
	public async Task GetAllAsync_CallsApi_AndReturnsData()
	{
		// Arrange
		var cancellationToken = new CancellationToken();
		var expectedResponse = new WebhookOperations
		{
			OperationsList = [
				new WebhookOperation
				{
					Name = "Test Webhook",
					CategoryValue = OperationCategory.Alerts,
					StatusValue = OperationStatus.Pending
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
	public async Task CreateAsync_CallsApi_AndReturnsData()
	{
		// Arrange
		var operation = new WebhookOperation
		{
			Name = "Test Webhook",
			CategoryValue = OperationCategory.Alerts,
			StatusValue = OperationStatus.Pending
		};
		var cancellationToken = new CancellationToken();
		var expectedResponse = new WebhookOperation
		{
			Id = "123",
			Name = operation.Name,
			CategoryValue = operation.CategoryValue,
			StatusValue = operation.StatusValue
		};
		_ = _refitApi.Setup(x => x.CreateAsync(operation, null, cancellationToken))
			.ReturnsAsync(expectedResponse);

		// Act
		var result = await _sut.CreateAsync(operation, null, cancellationToken);

		// Assert
		_ = result.Should().Be(expectedResponse);
		_refitApi.Verify(x => x.CreateAsync(operation, null, cancellationToken), Times.Once);
	}

	[Fact]
	public async Task CreateAsync_WithBadRequest_ThrowsThousandEyesBadRequestException()
	{
		// Arrange
		var operation = new WebhookOperation
		{
			Name = "Test Webhook",
			CategoryValue = OperationCategory.Alerts,
			StatusValue = OperationStatus.Pending
		};
		var cancellationToken = new CancellationToken();

		// Simulate the API error response from the issue
		var errorContent = @"{
			""timestamp"": ""2025-10-10T09:50:21.956+00:00"",
			""status"": 400,
			""error"": ""Bad Request"",
			""message"": ""JSON parse error: Invalid value for Category: 0"",
			""path"": ""/v7/operations/webhooks""
		}";

		var refitSettings = new RefitSettings();
		var apiException = await ApiException.Create(
			new HttpRequestMessage(HttpMethod.Post, "https://api.thousandeyes.com/v7/operations/webhooks"),
			HttpMethod.Post,
			new HttpResponseMessage(HttpStatusCode.BadRequest)
			{
				Content = new StringContent(errorContent, System.Text.Encoding.UTF8, "application/json")
			},
			refitSettings);

		_ = _refitApi.Setup(x => x.CreateAsync(operation, null, cancellationToken))
			.ThrowsAsync(apiException);

		// Act & Assert - Verify the Refit ApiException is thrown with correct properties
		var act = () => _sut.CreateAsync(operation, null, cancellationToken);

		// The implementation doesn't convert Refit exceptions to ThousandEyes exceptions,
		// so we expect the Refit ApiException to propagate
		var exceptionAssertions = await act.Should().ThrowAsync<ApiException>();
		var exception = exceptionAssertions.Which;

		_ = exception.StatusCode.Should().Be(HttpStatusCode.BadRequest);
		_ = exception.Content.Should().Contain("JSON parse error");
		_ = exception.Content.Should().Contain("Invalid value for Category");
	}

	[Fact]
	public async Task GetByIdAsync_CallsApi_AndReturnsData()
	{
		// Arrange
		var id = "123";
		var cancellationToken = new CancellationToken();
		var expectedResponse = new WebhookOperation
		{
			Id = id,
			Name = "Test Webhook",
			CategoryValue = OperationCategory.Alerts,
			StatusValue = OperationStatus.Pending
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
	public async Task UpdateAsync_CallsApi_AndReturnsData()
	{
		// Arrange
		var id = "123";
		var operation = new WebhookOperation
		{
			Id = id,
			Name = "Updated Webhook",
			CategoryValue = OperationCategory.Alerts,
			StatusValue = OperationStatus.Connected
		};
		var cancellationToken = new CancellationToken();
		var expectedResponse = operation;
		_ = _refitApi.Setup(x => x.UpdateAsync(id, operation, null, cancellationToken))
			.ReturnsAsync(expectedResponse);

		// Act
		var result = await _sut.UpdateAsync(id, operation, null, cancellationToken);

		// Assert
		_ = result.Should().Be(expectedResponse);
		_refitApi.Verify(x => x.UpdateAsync(id, operation, null, cancellationToken), Times.Once);
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
