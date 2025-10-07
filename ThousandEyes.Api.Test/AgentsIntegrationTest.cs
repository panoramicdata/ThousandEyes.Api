using AwesomeAssertions;

namespace ThousandEyes.Api.Test;

[Collection("Integration Tests")]
public class AgentsIntegrationTest(IntegrationTestFixture fixture) : TestBase(fixture)
{
	[Fact]
	public async Task GetAgents_WithValidRequest_ReturnsAgents()
	{
		// Act
		var result = await ThousandEyesClient.Agents.Agents.GetAllAsync(aid: null, cancellationToken: CancellationToken);

		// Assert
		_ = result.Should().NotBeNull();
		_ = result.AgentsList.Should().NotBeNull();

		// If there are agents, verify the structure
		if (result.AgentsList.Length > 0)
		{
			var firstAgent = result.AgentsList[0];
			_ = firstAgent.AgentId.Should().NotBeNullOrEmpty();
			_ = firstAgent.AgentName.Should().NotBeNullOrEmpty();
			_ = firstAgent.AgentType.Should().NotBeNullOrEmpty();
			_ = firstAgent.Location.Should().NotBeNullOrEmpty();
			_ = firstAgent.CountryId.Should().NotBeNullOrEmpty();
		}
	}

	[Fact]
	public async Task GetAgentById_WithValidAgentId_ReturnsAgentDetails()
	{
		// Arrange - First get list of agents to find a valid agent ID
		var agents = await ThousandEyesClient.Agents.Agents.GetAllAsync(aid: null, cancellationToken: CancellationToken);
		
		// Skip test if no agents are available
		if (agents.AgentsList.Length == 0)
		{
			return; // Skip test - no agents available
		}

		var agentId = agents.AgentsList[0].AgentId;

		// Act
		var result = await ThousandEyesClient.Agents.Agents.GetByIdAsync(agentId, aid: null, cancellationToken: CancellationToken);

		// Assert
		_ = result.Should().NotBeNull();
		_ = result.AgentId.Should().Be(agentId);
		_ = result.AgentName.Should().NotBeNullOrEmpty();
		_ = result.AgentType.Should().NotBeNullOrEmpty();
		_ = result.Location.Should().NotBeNullOrEmpty();
		_ = result.CountryId.Should().NotBeNullOrEmpty();
	}

	[Fact]
	public async Task GetSupportedTests_WithValidAgentId_ReturnsSupportedTestTypes()
	{
		// Arrange - First get list of agents to find a valid agent ID
		var agents = await ThousandEyesClient.Agents.Agents.GetAllAsync(aid: null, cancellationToken: CancellationToken);
		
		// Skip test if no agents are available
		if (agents.AgentsList.Length == 0)
		{
			return; // Skip test - no agents available
		}

		var agentId = agents.AgentsList[0].AgentId;

		// Act
		var result = await ThousandEyesClient.Agents.Agents.GetSupportedTestsAsync(agentId, aid: null, cancellationToken: CancellationToken);

		// Assert
		_ = result.Should().NotBeNull();
		_ = result.Should().NotBeEmpty();
		
		// Verify that common test types are supported
		var supportedTests = result.ToList();
		_ = supportedTests.Should().Contain(testType => 
			testType.Contains("http-server") || 
			testType.Contains("page-load") || 
			testType.Contains("agent-to-server"));
	}
}