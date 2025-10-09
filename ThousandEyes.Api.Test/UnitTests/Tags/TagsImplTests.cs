using AwesomeAssertions;
using Moq;
using ThousandEyes.Api.Implementations.Tags;
using ThousandEyes.Api.Models.Tags;
using ThousandEyes.Api.Refit.Tags;

namespace ThousandEyes.Api.Test.UnitTests.Tags;

public class TagsImplTests
{
	private readonly Mock<ITagsRefitApi> _refitApi;
	private readonly TagsImpl _sut;

	public TagsImplTests()
	{
		_refitApi = new Mock<ITagsRefitApi>();
		_sut = new TagsImpl(_refitApi.Object);
	}

	[Fact]
	public async Task GetAllAsync_CallsApi_AndReturnsData()
	{
		// Arrange
		var cancellationToken = new CancellationToken();
		var expectedResponse = new Models.Tags.Tags
		{
			Items = [
				new Tag { Id = "123", Key = "test", Value = "value" }
			]
		};
		_ = _refitApi.Setup(x => x.GetAllAsync(null, null, cancellationToken))
			.ReturnsAsync(expectedResponse);

		// Act
		var result = await _sut.GetAllAsync(null, null, cancellationToken);

		// Assert
		_ = result.Should().Be(expectedResponse);
		_refitApi.Verify(x => x.GetAllAsync(null, null, cancellationToken), Times.Once);
	}

	[Fact]
	public async Task GetByIdAsync_CallsApi_AndReturnsData()
	{
		// Arrange
		var id = "123";
		var cancellationToken = new CancellationToken();
		var expectedResponse = new Tag
		{
			Id = id,
			Key = "test",
			Value = "value"
		};
		_ = _refitApi.Setup(x => x.GetByIdAsync(id, null, null, cancellationToken))
			.ReturnsAsync(expectedResponse);

		// Act
		var result = await _sut.GetByIdAsync(id, null, null, cancellationToken);

		// Assert
		_ = result.Should().Be(expectedResponse);
		_refitApi.Verify(x => x.GetByIdAsync(id, null, null, cancellationToken), Times.Once);
	}

	[Fact]
	public async Task CreateAsync_CallsApi_AndReturnsData()
	{
		// Arrange
		var request = new TagInfo();
		var cancellationToken = new CancellationToken();
		var expectedResponse = new TagInfo();
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
		var id = "123";
		var request = new TagInfo();
		var cancellationToken = new CancellationToken();
		var expectedResponse = new TagInfo();
		_ = _refitApi.Setup(x => x.UpdateAsync(id, request, null, cancellationToken))
			.ReturnsAsync(expectedResponse);

		// Act
		var result = await _sut.UpdateAsync(id, request, null, cancellationToken);

		// Assert
		_ = result.Should().Be(expectedResponse);
		_refitApi.Verify(x => x.UpdateAsync(id, request, null, cancellationToken), Times.Once);
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

	[Fact]
	public async Task AssignAsync_CallsApi_AndReturnsData()
	{
		// Arrange
		var id = "123";
		var request = new TagAssignment();
		var cancellationToken = new CancellationToken();
		var expectedResponse = new BulkTagAssignment();
		_ = _refitApi.Setup(x => x.AssignAsync(id, request, null, cancellationToken))
			.ReturnsAsync(expectedResponse);

		// Act
		var result = await _sut.AssignAsync(id, request, null, cancellationToken);

		// Assert
		_ = result.Should().Be(expectedResponse);
		_refitApi.Verify(x => x.AssignAsync(id, request, null, cancellationToken), Times.Once);
	}

	[Fact]
	public async Task UnassignAsync_CallsApi()
	{
		// Arrange
		var id = "123";
		var request = new TagAssignment();
		var cancellationToken = new CancellationToken();
		_ = _refitApi.Setup(x => x.UnassignAsync(id, request, null, cancellationToken))
			.Returns(Task.CompletedTask);

		// Act
		await _sut.UnassignAsync(id, request, null, cancellationToken);

		// Assert
		_refitApi.Verify(x => x.UnassignAsync(id, request, null, cancellationToken), Times.Once);
	}

	[Fact]
	public async Task CreateBulkAsync_CallsApi_AndReturnsData()
	{
		// Arrange
		var request = new BulkTagResponse();
		var cancellationToken = new CancellationToken();
		var expectedResponse = new BulkTagResponse();
		_ = _refitApi.Setup(x => x.CreateBulkAsync(request, null, cancellationToken))
			.ReturnsAsync(expectedResponse);

		// Act
		var result = await _sut.CreateBulkAsync(request, null, cancellationToken);

		// Assert
		_ = result.Should().Be(expectedResponse);
		_refitApi.Verify(x => x.CreateBulkAsync(request, null, cancellationToken), Times.Once);
	}

	[Fact]
	public async Task AssignBulkAsync_CallsApi_AndReturnsData()
	{
		// Arrange
		var request = new BulkTagAssignments();
		var cancellationToken = new CancellationToken();
		var expectedResponse = new BulkTagAssignments();
		_ = _refitApi.Setup(x => x.AssignBulkAsync(request, null, cancellationToken))
			.ReturnsAsync(expectedResponse);

		// Act
		var result = await _sut.AssignBulkAsync(request, null, cancellationToken);

		// Assert
		_ = result.Should().Be(expectedResponse);
		_refitApi.Verify(x => x.AssignBulkAsync(request, null, cancellationToken), Times.Once);
	}

	[Fact]
	public async Task UnassignBulkAsync_CallsApi_AndReturnsData()
	{
		// Arrange
		var request = new BulkTagAssignments();
		var cancellationToken = new CancellationToken();
		var expectedResponse = new BulkTagAssignments();
		_ = _refitApi.Setup(x => x.UnassignBulkAsync(request, null, cancellationToken))
			.ReturnsAsync(expectedResponse);

		// Act
		var result = await _sut.UnassignBulkAsync(request, null, cancellationToken);

		// Assert
		_ = result.Should().Be(expectedResponse);
		_refitApi.Verify(x => x.UnassignBulkAsync(request, null, cancellationToken), Times.Once);
	}
}
