using AwesomeAssertions;
using ThousandEyes.Api.Models.Agents;

namespace ThousandEyes.Api.Test;

[Collection("Integration Tests")]
[Trait("Category", "Integration")]
public class AgentsIntegrationTest(IntegrationTestFixture fixture) : TestBase(fixture)
{
	private static void CheckAgent(Agent agent)
	{
		_ = agent.AgentName.Should().NotBeNullOrEmpty();
		_ = agent.AgentType.Should().NotBeNullOrEmpty();
		_ = agent.Location.Should().NotBeNullOrEmpty();
		_ = agent.CountryId.Should().NotBeNullOrEmpty();
	}

	[Fact]
	public async Task GetAgents_WithValidRequest_ReturnsAgents()
	{
		var result = await ThousandEyesClient.Agents.Agents.GetAllAsync(aid: null, cancellationToken: CancellationToken);

		_ = result.AgentsList.Should().NotBeNull();
		if (result.AgentsList.Length > 0)
		{
			_ = result.AgentsList[0].AgentId.Should().NotBeNullOrEmpty();
			CheckAgent(result.AgentsList[0]);
		}
	}

	[Fact]
	public async Task GetAgentById_WithValidAgentId_ReturnsAgentDetails()
	{
		var agentId = await FirstAgentIdAsync();
		if (agentId is null)
		{
			return;
		}

		var result = await ThousandEyesClient.Agents.Agents.GetByIdAsync(agentId, aid: null, cancellationToken: CancellationToken);

		_ = result.AgentId.Should().Be(agentId);
		CheckAgent(result);
	}

	[Fact]
	public async Task GetSupportedTests_WithValidAgentId_ReturnsSupportedTestTypes()
	{
		var agentId = await FirstAgentIdAsync();
		if (agentId is null)
		{
			return;
		}

		var result = await ThousandEyesClient.Agents.Agents.GetSupportedTestsAsync(agentId, aid: null, cancellationToken: CancellationToken);

		_ = result.Should().NotBeEmpty();
		_ = result.Should().Contain(t => t.Contains("http-server") || t.Contains("page-load") || t.Contains("agent-to-server"));
	}

	private async Task<string?> FirstAgentIdAsync()
	{
		var agents = await ThousandEyesClient.Agents.Agents.GetAllAsync(aid: null, cancellationToken: CancellationToken);
		return agents.AgentsList.FirstOrDefault()?.AgentId;
	}
}
