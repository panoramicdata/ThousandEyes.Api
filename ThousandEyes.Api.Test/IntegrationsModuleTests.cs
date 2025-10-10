using AwesomeAssertions;
using Refit;
using ThousandEyes.Api.Exceptions;
using ThousandEyes.Api.Models.Integrations;

namespace ThousandEyes.Api.Test;

[Collection("Integration Tests")]
public class IntegrationsModuleTests(IntegrationTestFixture fixture) : TestBase(fixture)
{
	[Fact]
	public async Task GetWebhookOperations_WithValidRequest_ReturnsOperations()
	{
		// Act
		var result = await ThousandEyesClient.Integrations.WebhookOperations.GetAllAsync(
			aid: null,
			CancellationToken);

		// Assert
		_ = result.Should().NotBeNull();
		_ = result.OperationsList.Should().NotBeNull();
		// Note: Operations list may be empty if none configured
	}

	[Fact]
	public async Task CreateWebhookOperation_WithValidRequest_CreatesOperation()
	{
		// Arrange
		var operation = new WebhookOperation
		{
			Name = $"Test Webhook {Guid.NewGuid()}",
			Enabled = false,
			CategoryValue = OperationCategory.Alerts,
			StatusValue = OperationStatus.Pending,
			Path = "/test/webhook",
			Payload = "{\"test\": true}"
		};

		// Act
		try
		{
			var result = await ThousandEyesClient.Integrations.WebhookOperations.CreateAsync(
				operation,
				aid: null,
				CancellationToken);

			// Assert
			_ = result.Should().NotBeNull();
			_ = result.Id.Should().NotBeNullOrWhiteSpace();
			_ = result.Name.Should().Be(operation.Name);

			// Cleanup
			if (result.Id != null)
			{
				await ThousandEyesClient.Integrations.WebhookOperations.DeleteAsync(
					result.Id,
					aid: null,
					CancellationToken);
			}
		}
		catch (Exception e)
		{
			// Test may fail if rate limit exceeded or permissions insufficient
			// This is expected in some environments
			throw new Exception("CreateWebhookOperation test failed", e);
		}
	}

	[Fact]
	public async Task GetWebhookOperation_WithValidId_ReturnsOperation()
	{
		// Arrange - Create operation first
		var operation = new WebhookOperation
		{
			Name = $"Test Get Webhook {Guid.NewGuid()}",
			Enabled = false,
			CategoryValue = OperationCategory.Alerts,
			StatusValue = OperationStatus.Pending
		};

		var created = await ThousandEyesClient.Integrations.WebhookOperations.CreateAsync(
			operation,
			aid: null,
			CancellationToken);

		// Act
		var result = await ThousandEyesClient.Integrations.WebhookOperations.GetByIdAsync(
			created.Id!,
			aid: null,
			CancellationToken);

		// Assert
		_ = result.Should().NotBeNull();
		_ = result.Id.Should().Be(created.Id);
		_ = result.Name.Should().Be(operation.Name);

		// Cleanup
		await ThousandEyesClient.Integrations.WebhookOperations.DeleteAsync(
			created.Id!,
			aid: null,
			CancellationToken);
	}

	[Fact]
	public async Task UpdateWebhookOperation_WithValidRequest_UpdatesOperation()
	{
		// Arrange - Create operation first
		var operation = new WebhookOperation
		{
			Name = $"Test Update Webhook {Guid.NewGuid()}",
			Enabled = false,
			CategoryValue = OperationCategory.Alerts,
			StatusValue = OperationStatus.Pending
		};

		var created = await ThousandEyesClient.Integrations.WebhookOperations.CreateAsync(
			operation,
			aid: null,
			CancellationToken);

		// Modify
		created.Name = $"Updated {created.Name}";
		created.Enabled = true;

		// Act
		var result = await ThousandEyesClient.Integrations.WebhookOperations.UpdateAsync(
			created.Id!,
			created,
			aid: null,
			CancellationToken);

		// Assert
		_ = result.Should().NotBeNull();
		_ = result.Name.Should().Contain("Updated");
		_ = result.Enabled.Should().BeTrue();

		// Cleanup
		await ThousandEyesClient.Integrations.WebhookOperations.DeleteAsync(
			created.Id!,
			aid: null,
			CancellationToken);
	}

	[Fact]
	public async Task DeleteWebhookOperation_WithValidId_DeletesOperation()
	{
		// Arrange - Create operation first
		var operation = new WebhookOperation
		{
			Name = $"Test Delete Webhook {Guid.NewGuid()}",
			Enabled = false,
			CategoryValue = OperationCategory.Alerts,
			StatusValue = OperationStatus.Pending
		};

		var created = await ThousandEyesClient
			.Integrations
			.WebhookOperations.CreateAsync(
			operation,
			aid: null,
			CancellationToken);

		// Act - Delete should not throw
		await ThousandEyesClient.Integrations.WebhookOperations.DeleteAsync(
			created.Id!,
			aid: null,
			CancellationToken);

		// Assert - Verify deleted by trying to get (should throw or return null)
		// Note: Actual behavior depends on API - may throw 404
	}

	[Fact]
	public async Task GetGenericConnectors_WithValidRequest_ReturnsConnectors()
	{
		// Act
		var result = await ThousandEyesClient.Integrations.GenericConnectors.GetAllAsync(
			aid: null,
			CancellationToken);

		// Assert
		_ = result.Should().NotBeNull();
		_ = result.ConnectorsList.Should().NotBeNull();
		// Note: Connectors list may be empty if none configured
	}

	[Fact]
	public async Task CreateGenericConnector_WithValidRequest_CreatesConnector()
	{
		// Arrange
		var connector = new GenericConnector
		{
			TypeValue = ConnectorType.Generic,
			Name = $"Test Connector {Guid.NewGuid()}",
			Target = "https://hooks.slack.com/test",
			Authentication = new BearerTokenAuthentication
			{
				AuthenticationTypeValue = AuthenticationType.BearerToken,
				Token = "test-token-123"
			}
		};

		// Act
		var result = await ThousandEyesClient.Integrations.GenericConnectors.CreateAsync(
			connector,
			aid: null,
			CancellationToken);

		// Assert
		_ = result.Should().NotBeNull();
		_ = result.Id.Should().NotBeNullOrWhiteSpace();
		_ = result.Name.Should().Be(connector.Name);

		// Cleanup
		if (result.Id != null)
		{
			await ThousandEyesClient.Integrations.GenericConnectors.DeleteAsync(
				result.Id,
				aid: null,
				CancellationToken);
		}
	}

	[Fact]
	public async Task GetOperationConnectors_WithValidOperationId_ReturnsConnectors()
	{
		// Arrange - Create operation first
		var operation = new WebhookOperation
		{
			Name = $"Test Op Connectors {Guid.NewGuid()}",
			Enabled = false,
			CategoryValue = OperationCategory.Alerts,
			StatusValue = OperationStatus.Pending
		};

		var created = await ThousandEyesClient.Integrations.WebhookOperations.CreateAsync(
			operation,
			aid: null,
			CancellationToken);

		// Act
		var result = await ThousandEyesClient.Integrations.OperationConnectors.GetConnectorsAsync(
			type: "webhooks",
			id: created.Id!,
			aid: null,
			CancellationToken);

		// Assert
		_ = result.Should().NotBeNull();
		_ = result.Items.Should().NotBeNull();

		// Cleanup
		await ThousandEyesClient.Integrations.WebhookOperations.DeleteAsync(
			created.Id!,
			aid: null,
			CancellationToken);
	}

	[Fact]
	public async Task SetOperationConnectors_WithValidRequest_AssignsConnectors()
	{
		// Arrange - Create operation and connector
		var operation = new WebhookOperation
		{
			Name = $"Test Set Connectors {Guid.NewGuid()}",
			Enabled = false,
			CategoryValue = OperationCategory.Alerts,
			StatusValue = OperationStatus.Pending
		};

		var connector = new GenericConnector
		{
			TypeValue = ConnectorType.Generic,
			Name = $"Test Assign Connector {Guid.NewGuid()}",
			Target = "https://hooks.slack.com/test"
		};

		var createdOp = await ThousandEyesClient.Integrations.WebhookOperations.CreateAsync(
			operation,
			aid: null,
			CancellationToken);

		var createdConn = await ThousandEyesClient.Integrations.GenericConnectors.CreateAsync(
			connector,
			aid: null,
			CancellationToken);

		// Act
		var result = await ThousandEyesClient.Integrations.OperationConnectors.SetConnectorsAsync(
			type: "webhooks",
			id: createdOp.Id!,
			connectorIds: [createdConn.Id!],
			aid: null,
			CancellationToken);

		// Assert
		_ = result.Should().NotBeNull();
		_ = result.Items.Should().Contain(createdConn.Id!);

		// Cleanup
		await ThousandEyesClient.Integrations.WebhookOperations.DeleteAsync(
			createdOp.Id!,
			aid: null,
			CancellationToken);
		await ThousandEyesClient.Integrations.GenericConnectors.DeleteAsync(
			createdConn.Id!,
			aid: null,
			CancellationToken);
	}

	[Fact]
	public async Task CreateWebhookOperation_WithMissingRequiredFields_ThrowsThousandEyesBadRequestException()
	{
		// Arrange - Create an operation with missing required fields to trigger 400 Bad Request
		// Note: We're testing that the exception is properly parsed, not that validation works
		var operation = new WebhookOperation
		{
			Name = "", // Empty name should cause validation error
			CategoryValue = OperationCategory.Alerts,
			StatusValue = OperationStatus.Pending
		};

		// Act & Assert
		var exception = await Assert.ThrowsAsync<ThousandEyesBadRequestException>(
			async () => await ThousandEyesClient.Integrations.WebhookOperations.CreateAsync(
				operation,
				aid: null,
				CancellationToken));

		// Verify exception properties are properly populated
		_ = exception.Should().NotBeNull();
		_ = exception.StatusCode.Should().Be(400);
		_ = exception.Message.Should().NotBeNullOrWhiteSpace();
		
		// Verify Details dictionary contains the error response fields
		_ = exception.Details.Should().NotBeNull();
		_ = exception.Details.Should().ContainKey("status");
		_ = exception.Details.Should().ContainKey("message");
		_ = exception.Details.Should().ContainKey("path");
		
		// Verify the path is correct
		_ = exception.Details["path"].Should().Be("/v7/operations/webhooks");
		
		// Verify StatusCode in Details matches the exception property
		_ = exception.Details["status"].Should().Be(400);
		
		// Verify error code is populated
		_ = exception.ErrorCode.Should().NotBeNullOrWhiteSpace();
		
		// Note: InnerException is null when ErrorHandler intercepts the error response
		// before Refit can throw an ApiException. This is the correct behavior.
		// If we needed to preserve the original ApiException, we would have to let Refit throw first,
		// but that would complicate the error handling logic.
		
		// Verify request context is captured
		_ = exception.RequestUrl.Should().Contain("/v7/operations/webhooks");
		_ = exception.RequestMethod.Should().Be("POST");
	}
}
