using AwesomeAssertions;
using Refit;
using ThousandEyes.Api.Models.EndpointAgents;

namespace ThousandEyes.Api.Test;

[Collection("Integration Tests")]
public class EndpointAgentsIntegrationTest(IntegrationTestFixture fixture) : TestBase(fixture)
{
	[Fact]
	public async Task GetEndpointAgents_WithValidRequest_ReturnsAgents()
	{
		try
		{
			// Act
			var result = await ThousandEyesClient.EndpointAgents.EndpointAgents.GetAllAsync(
				aid: null,
				max: null,
				cursor: null,
				expand: null,
				includeDeleted: null,
				useAllPermittedAids: null,
				agentName: null,
				computerName: null,
				cancellationToken: CancellationToken);

			// Assert
			_ = result.Should().NotBeNull();
			_ = result.AgentsList.Should().NotBeNull();

			// If there are agents, verify the structure
			if (result.AgentsList.Length > 0)
			{
				var firstAgent = result.AgentsList[0];
				_ = firstAgent.Id.Should().NotBeNullOrEmpty();
				_ = firstAgent.Name.Should().NotBeNullOrEmpty();
				_ = firstAgent.Platform.Should().BeDefined();
			}
		}
		catch (ValidationApiException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
		{
			// Skip test if Endpoint Agents API is not available
			return;
		}
	}

	[Fact]
	public async Task GetEndpointAgents_WithExpandClients_ReturnsAgentsWithClients()
	{
		try
		{
			// Act
			var result = await ThousandEyesClient.EndpointAgents.EndpointAgents.GetAllAsync(
				aid: null,
				max: 5,
				cursor: null,
				expand: [ExpandEndpointAgentOptions.Clients],
				includeDeleted: false,
				useAllPermittedAids: null,
				agentName: null,
				computerName: null,
				cancellationToken: CancellationToken);

			// Assert
			_ = result.Should().NotBeNull();
			_ = result.AgentsList.Should().NotBeNull();

			// When expand=clients is used, agents with clients should have client data
			var agentsWithClients = result.AgentsList.Where(a => a.NumberOfClients > 0).ToArray();
			if (agentsWithClients.Length > 0)
			{
				var agentWithClients = agentsWithClients[0];
				_ = agentWithClients.Clients.Should().NotBeNull();
			}
		}
		catch (ValidationApiException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
		{
			// Skip test if Endpoint Agents API is not available
			return;
		}
	}

	[Fact]
	public async Task GetEndpointAgent_WithValidId_ReturnsAgentDetails()
	{
		try
		{
			// Arrange - Get list of agents first
			var agents = await ThousandEyesClient.EndpointAgents.EndpointAgents.GetAllAsync(
				aid: null,
				max: 1,
				cursor: null,
				expand: null,
				includeDeleted: false,
				useAllPermittedAids: null,
				agentName: null,
				computerName: null,
				cancellationToken: CancellationToken);

			if (agents.AgentsList.Length == 0)
			{
				// Skip if no agents available
				return;
			}

			var agentId = agents.AgentsList[0].Id!;

			// Act
			var result = await ThousandEyesClient.EndpointAgents.EndpointAgents.GetByIdAsync(
				agentId: agentId,
				aid: null,
				expand: null,
				includeDeleted: false,
				cancellationToken: CancellationToken);

			// Assert
			_ = result.Should().NotBeNull();
			_ = result.Id.Should().Be(agentId);
			_ = result.Name.Should().NotBeNullOrEmpty();
			_ = result.Platform.Should().BeDefined();
		}
		catch (ValidationApiException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
		{
			// Skip test if Endpoint Agents API is not available
			return;
		}
	}

	[Fact]
	public async Task FilterEndpointAgents_WithPlatformFilter_ReturnsFilteredAgents()
	{
		try
		{
			// Arrange
			var filterRequest = new AgentSearchRequest
			{
				SearchFilters = new AgentSearchFilters
				{
					Platform = [Models.EndpointAgents.Platform.Windows, Models.EndpointAgents.Platform.Mac]
				}
			};

			// Act
			var result = await ThousandEyesClient.EndpointAgents.EndpointAgents.FilterAsync(
				request: filterRequest,
				aid: null,
				max: null,
				cursor: null,
				expand: null,
				includeDeleted: false,
				cancellationToken: CancellationToken);

			// Assert
			_ = result.Should().NotBeNull();
			_ = result.AgentsList.Should().NotBeNull();

			// All returned agents should be Windows or Mac
			if (result.AgentsList.Length > 0)
			{
				_ = result.AgentsList.Should().OnlyContain(a =>
					a.Platform == Models.EndpointAgents.Platform.Windows ||
					a.Platform == Models.EndpointAgents.Platform.Mac);
			}
		}
		catch (ValidationApiException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
		{
			// Skip test if Endpoint Agents API is not available
			return;
		}
	}

	[Fact]
	public async Task GetConnectionString_WithValidRequest_ReturnsConnectionString()
	{
		try
		{
			// Act
			var result = await ThousandEyesClient.EndpointAgents.EndpointAgents.GetConnectionStringAsync(
				aid: null,
				cancellationToken: CancellationToken);

			// Assert
			_ = result.Should().NotBeNull();
			_ = result.ConnectionStringValue.Should().NotBeNullOrEmpty();
		}
		catch (ValidationApiException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
		{
			// Skip test if Endpoint Agents API is not available
			return;
		}
	}

	[Fact]
	public async Task UpdateEndpointAgent_WithValidRequest_UpdatesAgent()
	{
		try
		{
			// Arrange - Get list of agents first
			var agents = await ThousandEyesClient.EndpointAgents.EndpointAgents.GetAllAsync(
				aid: null,
				max: 1,
				cursor: null,
				expand: null,
				includeDeleted: false,
				useAllPermittedAids: null,
				agentName: null,
				computerName: null,
				cancellationToken: CancellationToken);

			if (agents.AgentsList.Length == 0)
			{
				// Skip if no agents available
				return;
			}

			var agent = agents.AgentsList[0];
			var originalName = agent.Name;

			var updateRequest = new EndpointAgentUpdate
			{
				Name = $"{originalName}-Updated-{DateTime.UtcNow:yyyyMMddHHmmss}"
			};

			// Act
			var result = await ThousandEyesClient.EndpointAgents.EndpointAgents.UpdateAsync(
				agentId: agent.Id!,
				request: updateRequest,
				aid: null,
				expand: null,
				cancellationToken: CancellationToken);

			// Assert
			_ = result.Should().NotBeNull();
			_ = result.Id.Should().Be(agent.Id);
			_ = result.Name.Should().Be(updateRequest.Name);

			// Cleanup - Restore original name
			try
			{
				var restoreRequest = new EndpointAgentUpdate { Name = originalName };
				_ = await ThousandEyesClient.EndpointAgents.EndpointAgents.UpdateAsync(
					agentId: agent.Id!,
					request: restoreRequest,
					aid: null,
					expand: null,
					cancellationToken: CancellationToken);
			}
			catch
			{
				// Ignore cleanup errors
			}
		}
		catch (ValidationApiException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
		{
			// Skip test if Endpoint Agents API is not available
			return;
		}
	}

	[Fact]
	public async Task EnableDisableEndpointAgent_WithValidId_TogglesAgentStatus()
	{
		try
		{
			// Arrange - Get list of agents first
			var agents = await ThousandEyesClient.EndpointAgents.EndpointAgents.GetAllAsync(
				aid: null,
				max: 1,
				cursor: null,
				expand: null,
				includeDeleted: false,
				useAllPermittedAids: null,
				agentName: null,
				computerName: null,
				cancellationToken: CancellationToken);

			if (agents.AgentsList.Length == 0)
			{
				// Skip if no agents available
				return;
			}

			var agent = agents.AgentsList[0];
			var originalStatus = agent.Status;

			// Act - Disable if enabled, enable if disabled
			EndpointAgent result;
			if (originalStatus == AgentStatus.Enabled)
			{
				result = await ThousandEyesClient.EndpointAgents.EndpointAgents.DisableAsync(
					agentId: agent.Id!,
					aid: null,
					cancellationToken: CancellationToken);

				// Assert
				_ = result.Should().NotBeNull();
				_ = result.Status.Should().Be(AgentStatus.Disabled);

				// Restore - Re-enable the agent
				_ = await ThousandEyesClient.EndpointAgents.EndpointAgents.EnableAsync(
					agentId: agent.Id!,
					aid: null,
					cancellationToken: CancellationToken);
			}
			else
			{
				result = await ThousandEyesClient.EndpointAgents.EndpointAgents.EnableAsync(
					agentId: agent.Id!,
					aid: null,
					cancellationToken: CancellationToken);

				// Assert
				_ = result.Should().NotBeNull();
				_ = result.Status.Should().Be(AgentStatus.Enabled);

				// Restore - Disable the agent
				_ = await ThousandEyesClient.EndpointAgents.EndpointAgents.DisableAsync(
					agentId: agent.Id!,
					aid: null,
					cancellationToken: CancellationToken);
			}
		}
		catch (ValidationApiException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
		{
			// Skip test if Endpoint Agents API is not available
			return;
		}
	}
}