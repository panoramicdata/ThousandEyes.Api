using AwesomeAssertions;
using ThousandEyes.Api.Models.Tags;
using ObjectType = ThousandEyes.Api.Models.Tags.ObjectType;

namespace ThousandEyes.Api.Test;

/// <summary>
/// Integration tests for the Tags API module
/// </summary>
[Collection("Integration Tests")]
public class TagsModuleTests(IntegrationTestFixture fixture) : TestBase(fixture)
{
	[Fact]
	public async Task GetTags_WithValidRequest_ReturnsTags()
	{
		// Act
		var result = await ThousandEyesClient.Tags.GetAllAsync(aid: null, expand: null, CancellationToken);

		// Assert
		result.Should().NotBeNull();
		result.Items.Should().NotBeNull();
	}

	[Fact]
	public async Task CreateTag_WithValidRequest_CreatesTag()
	{
		// Arrange
		var timestamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
		var request = new TagInfo
		{
			Key = "test-key",
			Value = $"test-value-{timestamp}",
			Color = "#FF0000",
			Description = $"Test Tag - {timestamp}",
			ObjectType = ObjectType.Test
		};

		try
		{
			// Act
			var result = await ThousandEyesClient.Tags.CreateAsync(request, aid: null, CancellationToken);

			// Assert
			result.Should().NotBeNull();
			result.Id.Should().NotBeNullOrEmpty();
			result.Key.Should().Be(request.Key);
			result.Value.Should().Be(request.Value);

			// Cleanup
			await ThousandEyesClient.Tags.DeleteAsync(result.Id!, aid: null, CancellationToken);
		}
		catch (Exception)
		{
			// Test failed - exception will be captured by test framework
			throw;
		}
	}

	[Fact]
	public async Task GetTag_WithValidId_ReturnsTag()
	{
		// Arrange
		var timestamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
		var request = new TagInfo
		{
			Key = "test-key",
			Value = $"test-value-{timestamp}",
			Color = "#00FF00",
			Description = $"Test Tag - {timestamp}",
			ObjectType = ObjectType.Test
		};

		// Create a tag first
		var created = await ThousandEyesClient.Tags.CreateAsync(request, aid: null, CancellationToken);

		try
		{
			// Act
			var result = await ThousandEyesClient.Tags.GetByIdAsync(created.Id!, aid: null, expand: null, CancellationToken);

			// Assert
			result.Should().NotBeNull();
			result.Id.Should().Be(created.Id);
			result.Key.Should().Be(request.Key);
			result.Value.Should().Be(request.Value);
		}
		finally
		{
			// Cleanup
			await ThousandEyesClient.Tags.DeleteAsync(created.Id!, aid: null, CancellationToken);
		}
	}

	[Fact]
	public async Task UpdateTag_WithValidRequest_UpdatesTag()
	{
		// Arrange
		var timestamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
		var createRequest = new TagInfo
		{
			Key = "test-key",
			Value = $"test-value-{timestamp}",
			Color = "#0000FF",
			Description = $"Test Tag - {timestamp}",
			ObjectType = ObjectType.Test
		};

		// Create a tag first
		var created = await ThousandEyesClient.Tags.CreateAsync(createRequest, aid: null, CancellationToken);

		try
		{
			// Act
			var updateRequest = new TagInfo
			{
				Key = "test-key",
				Value = $"updated-value-{timestamp}",
				Color = "#FFFF00",
				Description = $"Updated Test Tag - {timestamp}",
				ObjectType = ObjectType.Test
			};
			var result = await ThousandEyesClient.Tags.UpdateAsync(created.Id!, updateRequest, aid: null, CancellationToken);

			// Assert
			result.Should().NotBeNull();
			result.Id.Should().Be(created.Id);
			result.Value.Should().Be(updateRequest.Value);
			result.Color.Should().Be(updateRequest.Color);

			// Verify update by getting the tag
			var retrieved = await ThousandEyesClient.Tags.GetByIdAsync(created.Id!, aid: null, expand: null, CancellationToken);
			retrieved.Value.Should().Be(updateRequest.Value);
		}
		finally
		{
			// Cleanup
			await ThousandEyesClient.Tags.DeleteAsync(created.Id!, aid: null, CancellationToken);
		}
	}

	[Fact]
	public async Task DeleteTag_WithValidId_DeletesTag()
	{
		// Arrange
		var timestamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
		var request = new TagInfo
		{
			Key = "test-key",
			Value = $"test-value-{timestamp}",
			Color = "#FF00FF",
			Description = $"Test Tag - {timestamp}",
			ObjectType = ObjectType.Test
		};

		// Create a tag first
		var created = await ThousandEyesClient.Tags.CreateAsync(request, aid: null, CancellationToken);

		// Act
		await ThousandEyesClient.Tags.DeleteAsync(created.Id!, aid: null, CancellationToken);

		// Assert - verify deletion by attempting to get the tag
		// This should throw an exception (404 Not Found)
		var act = async () => await ThousandEyesClient.Tags.GetByIdAsync(created.Id!, aid: null, expand: null, CancellationToken);
		await act.Should().ThrowAsync<Exception>(); // Refit will throw an exception for 404
	}

	[Fact]
	public async Task CreateTags_Bulk_CreatesMultipleTags()
	{
		// Arrange
		var timestamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
		var request = new BulkTagResponse
		{
			Tags =
			[
				new Tag
				{
					Key = "test-key-1",
					Value = $"test-value-1-{timestamp}",
					Color = "#FF0000",
					Description = $"Test Tag 1 - {timestamp}",
					ObjectType = ObjectType.Test
				},
				new Tag
				{
					Key = "test-key-2",
					Value = $"test-value-2-{timestamp}",
					Color = "#00FF00",
					Description = $"Test Tag 2 - {timestamp}",
					ObjectType = ObjectType.Test
				}
			]
		};

		var createdIds = new List<string>();

		try
		{
			// Act
			var result = await ThousandEyesClient.Tags.CreateBulkAsync(request, aid: null, CancellationToken);

			// Assert
			result.Should().NotBeNull();
			result.Tags.Should().NotBeEmpty();
			result.Tags.Should().HaveCount(2);
			
			// Track IDs for cleanup
			createdIds.AddRange(result.Tags.Select(t => t.Id!).Where(id => id != null));
		}
		finally
		{
			// Cleanup all created tags
			foreach (var id in createdIds)
			{
				try
				{
					await ThousandEyesClient.Tags.DeleteAsync(id, aid: null, CancellationToken);
				}
				catch
				{
					// Ignore cleanup errors
				}
			}
		}
	}

	[Fact]
	public async Task GetTags_WithExpandAssignments_ReturnsTagsWithAssignments()
	{
		// Arrange
		var timestamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
		var request = new TagInfo
		{
			Key = "test-key",
			Value = $"test-value-{timestamp}",
			Color = "#00FFFF",
			Description = $"Test Tag - {timestamp}",
			ObjectType = ObjectType.Test
		};

		// Create a tag first
		var created = await ThousandEyesClient.Tags.CreateAsync(request, aid: null, CancellationToken);

		try
		{
			// Act
			var result = await ThousandEyesClient.Tags.GetByIdAsync(
				created.Id!, 
				aid: null, 
				expand: ["assignments"], 
				CancellationToken
			);

			// Assert
			result.Should().NotBeNull();
			result.Id.Should().Be(created.Id);
			result.Assignments.Should().NotBeNull(); // Should be populated with expand
		}
		finally
		{
			// Cleanup
			await ThousandEyesClient.Tags.DeleteAsync(created.Id!, aid: null, CancellationToken);
		}
	}

	[Fact]
	public async Task TagOperations_WithColorAndIcon_HandlesCustomization()
	{
		// Arrange
		var timestamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
		var request = new TagInfo
		{
			Key = "custom-key",
			Value = $"custom-value-{timestamp}",
			Color = "#ABCDEF",
			Icon = "star",
			Description = $"Customized Test Tag - {timestamp}",
			ObjectType = ObjectType.Dashboard
		};

		try
		{
			// Act - Create
			var created = await ThousandEyesClient.Tags.CreateAsync(request, aid: null, CancellationToken);

			// Assert - Create
			created.Should().NotBeNull();
			created.Color.Should().Be(request.Color);
			created.Icon.Should().Be(request.Icon);
			created.ObjectType.Should().Be(ObjectType.Dashboard);

			// Cleanup
			await ThousandEyesClient.Tags.DeleteAsync(created.Id!, aid: null, CancellationToken);
		}
		catch (Exception)
		{
			// Test failed - exception will be captured by test framework
			throw;
		}
	}
}
