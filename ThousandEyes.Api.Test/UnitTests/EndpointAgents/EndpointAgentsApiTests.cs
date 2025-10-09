using AwesomeAssertions;
using Moq;
using ThousandEyes.Api.Interfaces;
using ThousandEyes.Api.Models.EndpointAgents;
using EndpointAgentsCollection = ThousandEyes.Api.Models.EndpointAgents.EndpointAgents;

namespace ThousandEyes.Api.Test.UnitTests.EndpointAgents;

public class EndpointAgentsApiTests
{
	private readonly Mock<IEndpointAgentsRefitApi> _refitApi;
	private readonly EndpointAgentsApi _sut;

	public EndpointAgentsApiTests()
	{
		_refitApi = new Mock<IEndpointAgentsRefitApi>();
		_sut = new EndpointAgentsApi(_refitApi.Object);
	}

	[Fact]
	public async Task GetAllAsync_CallsApi_AndReturnsData()
	{
		// Arrange
		var cancellationToken = new CancellationToken();
		var expectedResponse = new EndpointAgentsCollection
		{
			AgentsList = [
				new EndpointAgent { Id = "123", Name = "Test Agent" }
			]
		};
		_ = _refitApi.Setup(x => x.GetAllAsync(null, null, null, null, null, null, null, null, cancellationToken))
			.ReturnsAsync(expectedResponse);

		// Act
		var result = await _sut.GetAllAsync(null, null, null, null, null, null, null, null, cancellationToken);

		// Assert
		_ = result.Should().Be(expectedResponse);
		_refitApi.Verify(x => x.GetAllAsync(null, null, null, null, null, null, null, null, cancellationToken), Times.Once);
	}

	[Fact]
	public async Task GetByIdAsync_CallsApi_AndReturnsData()
	{
		// Arrange
		var agentId = "123";
		var cancellationToken = new CancellationToken();
		var expectedResponse = new EndpointAgent
		{
			Id = agentId,
			Name = "Test Agent"
		};
		_ = _refitApi.Setup(x => x.GetByIdAsync(agentId, null, null, null, cancellationToken))
			.ReturnsAsync(expectedResponse);

		// Act
		var result = await _sut.GetByIdAsync(agentId, null, null, null, cancellationToken);

		// Assert
		_ = result.Should().Be(expectedResponse);
		_refitApi.Verify(x => x.GetByIdAsync(agentId, null, null, null, cancellationToken), Times.Once);
	}

	[Fact]
	public async Task UpdateAsync_CallsApi_AndReturnsData()
	{
		// Arrange
		var agentId = "123";
		var request = new EndpointAgentUpdate();
		var cancellationToken = new CancellationToken();
		var expectedResponse = new EndpointAgent();
		_ = _refitApi.Setup(x => x.UpdateAsync(agentId, request, null, null, cancellationToken))
			.ReturnsAsync(expectedResponse);

		// Act
		var result = await _sut.UpdateAsync(agentId, request, null, null, cancellationToken);

		// Assert
		_ = result.Should().Be(expectedResponse);
		_refitApi.Verify(x => x.UpdateAsync(agentId, request, null, null, cancellationToken), Times.Once);
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
	public async Task FilterAsync_CallsApi_AndReturnsData()
	{
		// Arrange
		var request = new AgentSearchRequest();
		var cancellationToken = new CancellationToken();
		var expectedResponse = new EndpointAgentsCollection
		{
			AgentsList = [
				new EndpointAgent { Id = "123", Name = "Test Agent" }
			]
		};
		_ = _refitApi.Setup(x => x.FilterAsync(request, null, null, null, null, null, cancellationToken))
			.ReturnsAsync(expectedResponse);

		// Act
		var result = await _sut.FilterAsync(request, null, null, null, null, null, cancellationToken);

		// Assert
		_ = result.Should().Be(expectedResponse);
		_refitApi.Verify(x => x.FilterAsync(request, null, null, null, null, null, cancellationToken), Times.Once);
	}

	[Fact]
	public async Task GetConnectionStringAsync_CallsApi_AndReturnsData()
	{
		// Arrange
		var cancellationToken = new CancellationToken();
		var expectedResponse = new ConnectionString();
		_ = _refitApi.Setup(x => x.GetConnectionStringAsync(null, cancellationToken))
			.ReturnsAsync(expectedResponse);

		// Act
		var result = await _sut.GetConnectionStringAsync(null, cancellationToken);

		// Assert
		_ = result.Should().Be(expectedResponse);
		_refitApi.Verify(x => x.GetConnectionStringAsync(null, cancellationToken), Times.Once);
	}

	[Fact]
	public async Task EnableAsync_CallsApi_AndReturnsData()
	{
		// Arrange
		var agentId = "123";
		var cancellationToken = new CancellationToken();
		var expectedResponse = new EndpointAgent();
		_ = _refitApi.Setup(x => x.EnableAsync(agentId, null, cancellationToken))
			.ReturnsAsync(expectedResponse);

		// Act
		var result = await _sut.EnableAsync(agentId, null, cancellationToken);

		// Assert
		_ = result.Should().Be(expectedResponse);
		_refitApi.Verify(x => x.EnableAsync(agentId, null, cancellationToken), Times.Once);
	}

	[Fact]
	public async Task DisableAsync_CallsApi_AndReturnsData()
	{
		// Arrange
		var agentId = "123";
		var cancellationToken = new CancellationToken();
		var expectedResponse = new EndpointAgent();
		_ = _refitApi.Setup(x => x.DisableAsync(agentId, null, cancellationToken))
			.ReturnsAsync(expectedResponse);

		// Act
		var result = await _sut.DisableAsync(agentId, null, cancellationToken);

		// Assert
		_ = result.Should().Be(expectedResponse);
		_refitApi.Verify(x => x.DisableAsync(agentId, null, cancellationToken), Times.Once);
	}

	[Fact]
	public async Task TransferAsync_CallsApi()
	{
		// Arrange
		var agentId = "123";
		var request = new AgentTransferRequest { ToAid = "456" };
		var cancellationToken = new CancellationToken();
		_ = _refitApi.Setup(x => x.TransferAsync(agentId, request, null, cancellationToken))
			.Returns(Task.CompletedTask);

		// Act
		await _sut.TransferAsync(agentId, request, null, cancellationToken);

		// Assert
		_refitApi.Verify(x => x.TransferAsync(agentId, request, null, cancellationToken), Times.Once);
	}
}
