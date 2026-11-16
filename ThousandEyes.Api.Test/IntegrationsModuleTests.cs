using AwesomeAssertions;
using ThousandEyes.Api.Exceptions;
using ThousandEyes.Api.Interfaces.Integrations;
using ThousandEyes.Api.Models.Integrations;

namespace ThousandEyes.Api.Test;

[Collection("Integration Tests")]
[Trait("Category", "Integration")]
public class IntegrationsModuleTests(IntegrationTestFixture fixture) : TestBase(fixture)
{
	private IWebhookOperations Webhooks => ThousandEyesClient.Integrations.WebhookOperations;

	private IGenericConnectors Connectors => ThousandEyesClient.Integrations.GenericConnectors;

	private static WebhookOperation NewOperation(string label) => new()
	{
		Name = $"Test {label} {Guid.NewGuid()}",
		Enabled = false,
		CategoryValue = OperationCategory.Alerts,
		StatusValue = OperationStatus.Pending
	};

	private static GenericConnector NewConnector(string label) => new()
	{
		TypeValue = ConnectorType.Generic,
		Name = $"Test {label} {Guid.NewGuid()}",
		Target = "https://hooks.slack.com/test"
	};

	private Task DeleteOperationAsync(string id) => Webhooks.DeleteAsync(id, aid: null, CancellationToken);

	/// <summary>
	/// Creates a webhook operation, runs the body against it and always deletes it afterwards
	/// </summary>
	private async Task WithOperationAsync(string label, Func<WebhookOperation, Task> body)
	{
		var created = await Webhooks.CreateAsync(NewOperation(label), aid: null, CancellationToken);
		try
		{
			await body(created);
		}
		finally
		{
			await DeleteOperationAsync(created.Id!);
		}
	}

	[Fact]
	public async Task GetWebhookOperations_WithValidRequest_ReturnsOperations()
	{
		var result = await Webhooks.GetAllAsync(aid: null, CancellationToken);

		_ = result.OperationsList.Should().NotBeNull();
	}

	[Fact]
	public async Task CreateWebhookOperation_WithValidRequest_CreatesOperation()
	{
		var operation = NewOperation("Webhook");
		operation.Path = "/test/webhook";
		operation.Payload = "{\"test\": true}";

		var result = await Webhooks.CreateAsync(operation, aid: null, CancellationToken);

		_ = result.Id.Should().NotBeNullOrWhiteSpace();
		_ = result.Name.Should().Be(operation.Name);
		await DeleteOperationAsync(result.Id!);
	}

	[Fact]
	public Task GetWebhookOperation_WithValidId_ReturnsOperation() => WithOperationAsync("Get Webhook", async created =>
	{
		var result = await Webhooks.GetByIdAsync(created.Id!, aid: null, CancellationToken);

		_ = result.Id.Should().Be(created.Id);
		_ = result.Name.Should().Be(created.Name);
	});

	[Fact]
	public Task UpdateWebhookOperation_WithValidRequest_UpdatesOperation() => WithOperationAsync("Update Webhook", async created =>
	{
		created.Name = $"Updated {created.Name}";
		created.Enabled = true;

		var result = await Webhooks.UpdateAsync(created.Id!, created, aid: null, CancellationToken);

		_ = result.Name.Should().Contain("Updated");
		_ = result.Enabled.Should().BeTrue();
	});

	[Fact]
	public async Task DeleteWebhookOperation_WithValidId_DeletesOperation()
	{
		var created = await Webhooks.CreateAsync(NewOperation("Delete Webhook"), aid: null, CancellationToken);

		var act = () => DeleteOperationAsync(created.Id!);

		_ = await act.Should().NotThrowAsync();
	}

	[Fact]
	public async Task GetGenericConnectors_WithValidRequest_ReturnsConnectors()
	{
		var result = await Connectors.GetAllAsync(aid: null, CancellationToken);

		_ = result.ConnectorsList.Should().NotBeNull();
	}

	[Fact]
	public async Task CreateGenericConnector_WithValidRequest_CreatesConnector()
	{
		var connector = NewConnector("Connector");
		connector.Authentication = new BearerTokenAuthentication
		{
			AuthenticationTypeValue = AuthenticationType.BearerToken,
			Token = "test-token-123"
		};

		var result = await Connectors.CreateAsync(connector, aid: null, CancellationToken);

		_ = result.Id.Should().NotBeNullOrWhiteSpace();
		_ = result.Name.Should().Be(connector.Name);
		await Connectors.DeleteAsync(result.Id!, aid: null, CancellationToken);
	}

	[Fact]
	public Task GetOperationConnectors_WithValidOperationId_ReturnsConnectors() => WithOperationAsync("Op Connectors", async created =>
	{
		var result = await ThousandEyesClient.Integrations.OperationConnectors.GetConnectorsAsync(
			type: "webhooks",
			id: created.Id!,
			aid: null,
			CancellationToken);

		_ = result.Items.Should().NotBeNull();
	});

	[Fact]
	public Task SetOperationConnectors_WithValidRequest_AssignsConnectors() => WithOperationAsync("Set Connectors", async createdOp =>
	{
		var createdConn = await Connectors.CreateAsync(NewConnector("Assign Connector"), aid: null, CancellationToken);
		try
		{
			var result = await ThousandEyesClient.Integrations.OperationConnectors.SetConnectorsAsync(
				type: "webhooks",
				id: createdOp.Id!,
				connectorIds: [createdConn.Id!],
				aid: null,
				CancellationToken);

			_ = result.Items.Should().Contain(createdConn.Id!);
		}
		finally
		{
			await Connectors.DeleteAsync(createdConn.Id!, aid: null, CancellationToken);
		}
	});

	[Fact]
	public async Task CreateWebhookOperation_WithMissingRequiredFields_ThrowsThousandEyesBadRequestException()
	{
		var operation = NewOperation("Invalid");
		operation.Name = "";

		var act = () => Webhooks.CreateAsync(operation, aid: null, CancellationToken);

		var exception = (await act.Should().ThrowAsync<ThousandEyesBadRequestException>()).Which;

		_ = exception.StatusCode.Should().Be(400);
		_ = exception.Message.Should().NotBeNullOrWhiteSpace();
		_ = exception.Details.Should().ContainKeys("status", "message", "path");
		_ = exception.Details!["path"].Should().Be("/v7/operations/webhooks");
		_ = exception.Details["status"].Should().Be(400);
		_ = exception.ErrorCode.Should().NotBeNullOrWhiteSpace();
		_ = exception.RequestUrl.Should().Contain("/v7/operations/webhooks");
		_ = exception.RequestMethod.Should().Be("POST");
	}
}
