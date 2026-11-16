using AwesomeAssertions;
using ThousandEyes.Api.Interfaces;
using ThousandEyes.Api.Models.EndpointAgents;

namespace ThousandEyes.Api.Test;

[Collection("Integration Tests")]
[Trait("Category", "Integration")]
public class EndpointAgentsIntegrationTest(IntegrationTestFixture fixture) : TestBase(fixture)
{
	private IEndpointAgentsApi Api => ThousandEyesClient.EndpointAgents.EndpointAgents;

	private async Task<EndpointAgent[]> ListAgentsAsync(
		int? max = null,
		ExpandEndpointAgentOptions[]? expand = null,
		bool? includeDeleted = null) =>
		(await Api.GetAllAsync(
			new EndpointAgentListOptions { Max = max, Expand = expand, IncludeDeleted = includeDeleted },
			cancellationToken: CancellationToken)).AgentsList;

	/// <summary>
	/// Runs the body against the first endpoint agent, or returns when the account has none
	/// </summary>
	private Task WithFirstAgentAsync(Func<EndpointAgent, Task> body) => RunIfAvailableAsync(async () =>
	{
		var agents = await ListAgentsAsync(max: 1, includeDeleted: false);
		if (agents.Length > 0)
		{
			await body(agents[0]);
		}
	});

	private Task<EndpointAgent> SetEnabledAsync(string agentId, bool enabled) => enabled
		? Api.EnableAsync(agentId: agentId, aid: null, cancellationToken: CancellationToken)
		: Api.DisableAsync(agentId: agentId, aid: null, cancellationToken: CancellationToken);

	[Fact]
	public Task GetEndpointAgents_WithValidRequest_ReturnsAgents() => RunIfAvailableAsync(async () =>
	{
		var agents = await ListAgentsAsync();

		_ = agents.Should().NotBeNull();
		if (agents.Length > 0)
		{
			_ = agents[0].Id.Should().NotBeNullOrEmpty();
			_ = agents[0].Name.Should().NotBeNullOrEmpty();
			_ = agents[0].Platform.Should().BeDefined();
		}
	});

	[Fact]
	public Task GetEndpointAgents_WithExpandClients_ReturnsAgentsWithClients() => RunIfAvailableAsync(async () =>
	{
		var agents = await ListAgentsAsync(max: 5, expand: [ExpandEndpointAgentOptions.Clients], includeDeleted: false);

		_ = agents.Should().NotBeNull();
		var agentsWithClients = agents.Where(a => a.NumberOfClients > 0).ToArray();
		if (agentsWithClients.Length > 0)
		{
			_ = agentsWithClients[0].Clients.Should().NotBeNull();
		}
	});

	[Fact]
	public Task GetEndpointAgent_WithValidId_ReturnsAgentDetails() => WithFirstAgentAsync(async agent =>
	{
		var result = await Api.GetByIdAsync(
			agentId: agent.Id!,
			aid: null,
			expand: null,
			includeDeleted: false,
			cancellationToken: CancellationToken);

		_ = result.Should().NotBeNull();
		_ = result.Id.Should().Be(agent.Id);
		_ = result.Name.Should().NotBeNullOrEmpty();
		_ = result.Platform.Should().BeDefined();
	});

	[Fact]
	public Task FilterEndpointAgents_WithPlatformFilter_ReturnsFilteredAgents() => RunIfAvailableAsync(async () =>
	{
		var filterRequest = new AgentSearchRequest
		{
			SearchFilters = new AgentSearchFilters
			{
				Platform = [Platform.Windows, Platform.Mac]
			}
		};

		var result = await Api.FilterAsync(
			request: filterRequest,
			aid: null,
			max: null,
			cursor: null,
			expand: null,
			includeDeleted: false,
			cancellationToken: CancellationToken);

		_ = result.Should().NotBeNull();
		_ = result.AgentsList.Should().NotBeNull();
		if (result.AgentsList.Length > 0)
		{
			_ = result.AgentsList.Should().OnlyContain(a => a.Platform == Platform.Windows || a.Platform == Platform.Mac);
		}
	});

	[Fact]
	public Task GetConnectionString_WithValidRequest_ReturnsConnectionString() => RunIfAvailableAsync(async () =>
	{
		var result = await Api.GetConnectionStringAsync(aid: null, cancellationToken: CancellationToken);

		_ = result.Should().NotBeNull();
		_ = result.ConnectionStringValue.Should().NotBeNullOrEmpty();
	});

	[Fact]
	public Task UpdateEndpointAgent_WithValidRequest_UpdatesAgent() => WithFirstAgentAsync(async agent =>
	{
		var originalName = agent.Name;
		var updateRequest = new EndpointAgentUpdate
		{
			Name = $"{originalName}-Updated-{DateTime.UtcNow:yyyyMMddHHmmss}"
		};

		var result = await Api.UpdateAsync(
			agentId: agent.Id!,
			request: updateRequest,
			aid: null,
			expand: null,
			cancellationToken: CancellationToken);

		_ = result.Should().NotBeNull();
		_ = result.Id.Should().Be(agent.Id);
		_ = result.Name.Should().Be(updateRequest.Name);

		await IgnoreErrorsAsync(() => Api.UpdateAsync(
			agentId: agent.Id!,
			request: new EndpointAgentUpdate { Name = originalName },
			aid: null,
			expand: null,
			cancellationToken: CancellationToken));
	});

	[Fact]
	public Task EnableDisableEndpointAgent_WithValidId_TogglesAgentStatus() => WithFirstAgentAsync(async agent =>
	{
		var wasEnabled = agent.Status == AgentStatus.Enabled;

		var result = await SetEnabledAsync(agent.Id!, enabled: !wasEnabled);

		_ = result.Should().NotBeNull();
		_ = result.Status.Should().Be(wasEnabled ? AgentStatus.Disabled : AgentStatus.Enabled);

		_ = await SetEnabledAsync(agent.Id!, enabled: wasEnabled);
	});
}
