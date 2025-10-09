using AwesomeAssertions;
using Moq;
using ThousandEyes.Api.Interfaces;
using ThousandEyes.Api.Models.Agents;
using AgentsCollection = ThousandEyes.Api.Models.Agents.Agents;

namespace ThousandEyes.Api.Test.UnitTests.Agents;

public class AgentsApiTests
{
	private readonly Mock<IAgentsRefitApi> _refitApi;
	private readonly AgentsApi _sut;

	public AgentsApiTests()
	{
		_refitApi = new Mock<IAgentsRefitApi>();
		_sut = new AgentsApi(_refitApi.Object);
	}

	[Fact]
	public async Task GetAllAsync_CallsApi_AndReturnsData()
	{
		// Arrange
		var cancellationToken = new CancellationToken();
		var expectedResponse = new AgentsCollection
		{
			AgentsList =
			[
				new Agent
				{
					AgentId = "123",
					AgentName = "Test Agent",
					AgentType = "cloud",
					Location = "San Francisco Bay Area",
					CountryId = "US"
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
		var agentId = "123";
		var cancellationToken = new CancellationToken();
		var expectedResponse = new Agent
		{
			AgentId = agentId,
			AgentName = "Test Agent",
			AgentType = "cloud",
			Location = "San Francisco Bay Area",
			CountryId = "US"
		};
		_ = _refitApi.Setup(x => x.GetByIdAsync(agentId, null, cancellationToken))
			.ReturnsAsync(expectedResponse);

		// Act
		var result = await _sut.GetByIdAsync(agentId, null, cancellationToken);

		// Assert
		_ = result.Should().Be(expectedResponse);
		_refitApi.Verify(x => x.GetByIdAsync(agentId, null, cancellationToken), Times.Once);
	}

	[Fact]
	public async Task CreateAsync_CallsApi_AndReturnsData()
	{
		// Arrange
		var cancellationToken = new CancellationToken();
		var request = new AgentRequest { AgentName = "Test Agent" };
		var expectedResponse = new Agent
		{
			AgentId = "123",
			AgentName = "Test Agent",
			AgentType = "enterprise",
			Location = "San Francisco Bay Area",
			CountryId = "US"
		};
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
		var agentId = "123";
		var request = new AgentRequest { AgentName = "Updated Agent" };
		var cancellationToken = new CancellationToken();
		var expectedResponse = new Agent
		{
			AgentId = agentId,
			AgentName = "Updated Agent",
			AgentType = "enterprise",
			Location = "San Francisco Bay Area",
			CountryId = "US"
		};
		_ = _refitApi.Setup(x => x.UpdateAsync(agentId, request, null, cancellationToken))
			.ReturnsAsync(expectedResponse);

		// Act
		var result = await _sut.UpdateAsync(agentId, request, null, cancellationToken);

		// Assert
		_ = result.Should().Be(expectedResponse);
		_refitApi.Verify(x => x.UpdateAsync(agentId, request, null, cancellationToken), Times.Once);
	}

	[Fact]
	public async Task DeleteAsync_CallsApi()
	{
		// Arrange
		var agentId = "123";
		var cancellationToken = new CancellationToken();
		_ = _refitApi.Setup(x => x.DeleteAsync(agentId, null, cancellationToken))
			.Returns(Task.CompletedTask);

		// Act
		await _sut.DeleteAsync(agentId, null, cancellationToken);

		// Assert
		_refitApi.Verify(x => x.DeleteAsync(agentId, null, cancellationToken), Times.Once);
	}

	[Fact]
	public async Task GetSupportedTestsAsync_CallsApi_AndReturnsData()
	{
		// Arrange
		var agentId = "123";
		var cancellationToken = new CancellationToken();
		var expectedResponse = new string[] { "http-server", "page-load" };
		_ = _refitApi.Setup(x => x.GetSupportedTestsAsync(agentId, null, cancellationToken))
			.ReturnsAsync(expectedResponse);

		// Act
		var result = await _sut.GetSupportedTestsAsync(agentId, null, cancellationToken);

		// Assert
		_ = result.Should().BeEquivalentTo(expectedResponse);
		_refitApi.Verify(x => x.GetSupportedTestsAsync(agentId, null, cancellationToken), Times.Once);
	}
}
