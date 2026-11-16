using AwesomeAssertions;
using ThousandEyes.Api.Models.Tags;
using ObjectType = ThousandEyes.Api.Models.Tags.ObjectType;

namespace ThousandEyes.Api.Test;

/// <summary>
/// Integration tests for the Tags API module
/// </summary>
[Collection("Integration Tests")]
[Trait("Category", "Integration")]
public class TagsModuleTests(IntegrationTestFixture fixture) : TestBase(fixture)
{
	private static TagInfo NewTag(string color, string key = "test-key", string valuePrefix = "test-value", ObjectType objectType = ObjectType.Test) => new()
	{
		Key = key,
		Value = $"{valuePrefix}-{Timestamp}",
		Color = color,
		Description = $"Test Tag - {Timestamp}",
		ObjectType = objectType
	};

	private static Tag NewBulkTag(int index, string color, long timestamp) => new()
	{
		Key = $"test-key-{index}",
		Value = $"test-value-{index}-{timestamp}",
		Color = color,
		Description = $"Test Tag {index} - {timestamp}",
		ObjectType = ObjectType.Test
	};

	private Task<Tag> GetAsync(string id, string[]? expand = null) =>
		ThousandEyesClient.Tags.GetByIdAsync(id, aid: null, expand: expand, CancellationToken);

	private Task DeleteAsync(string id) => ThousandEyesClient.Tags.DeleteAsync(id, aid: null, CancellationToken);

	/// <summary>
	/// Creates a tag, runs the body against it and always deletes it afterwards
	/// </summary>
	private Task WithTagAsync(TagInfo request, Func<TagInfo, Task> body) =>
		WithCreatedAsync(ThousandEyesClient.Tags.CreateAsync(request, aid: null, CancellationToken), t => t.Id!, DeleteAsync, body);

	[Fact]
	public async Task GetTags_WithValidRequest_ReturnsTags()
	{
		var result = await ThousandEyesClient.Tags.GetAllAsync(aid: null, expand: null, CancellationToken);

		_ = result.Items.Should().NotBeNull();
	}

	[Fact]
	public async Task CreateTag_WithValidRequest_CreatesTag()
	{
		var request = NewTag("#FF0000");

		await WithTagAsync(request, created =>
		{
			_ = created.Id.Should().NotBeNullOrEmpty();
			_ = created.Key.Should().Be(request.Key);
			_ = created.Value.Should().Be(request.Value);
			return Task.CompletedTask;
		});
	}

	[Fact]
	public async Task GetTag_WithValidId_ReturnsTag()
	{
		var request = NewTag("#00FF00");

		await WithTagAsync(request, async created =>
		{
			var result = await GetAsync(created.Id!);

			_ = result.Id.Should().Be(created.Id);
			_ = result.Key.Should().Be(request.Key);
			_ = result.Value.Should().Be(request.Value);
		});
	}

	[Fact]
	public async Task UpdateTag_WithValidRequest_UpdatesTag()
	{
		await WithTagAsync(NewTag("#0000FF"), async created =>
		{
			var updateRequest = NewTag("#FFFF00", valuePrefix: "updated-value");

			var result = await ThousandEyesClient.Tags.UpdateAsync(created.Id!, updateRequest, aid: null, CancellationToken);

			_ = result.Id.Should().Be(created.Id);
			_ = result.Value.Should().Be(updateRequest.Value);
			_ = result.Color.Should().Be(updateRequest.Color);
			_ = (await GetAsync(created.Id!)).Value.Should().Be(updateRequest.Value);
		});
	}

	[Fact]
	public async Task DeleteTag_WithValidId_DeletesTag()
	{
		var created = await ThousandEyesClient.Tags.CreateAsync(NewTag("#FF00FF"), aid: null, CancellationToken);

		await DeleteAsync(created.Id!);

		await AssertGoneAsync(() => GetAsync(created.Id!));

	}

	[Fact]
	public async Task CreateTags_Bulk_CreatesMultipleTags()
	{
		var timestamp = Timestamp;
		var request = new BulkTagResponse { Tags = [NewBulkTag(1, "#FF0000", timestamp), NewBulkTag(2, "#00FF00", timestamp)] };
		var createdIds = new List<string>();

		try
		{
			var result = await ThousandEyesClient.Tags.CreateBulkAsync(request, aid: null, CancellationToken);

			_ = result.Tags.Should().HaveCount(2);
			createdIds.AddRange(result.Tags.Select(t => t.Id!).Where(id => id != null));
		}
		finally
		{
			foreach (var id in createdIds)
			{
				await IgnoreErrorsAsync(() => DeleteAsync(id));
			}
		}
	}

	[Fact]
	public async Task GetTags_WithExpandAssignments_ReturnsTagsWithAssignments()
	{
		await WithTagAsync(NewTag("#00FFFF"), async created =>
		{
			var result = await GetAsync(created.Id!, ["assignments"]);

			_ = result.Id.Should().Be(created.Id);
			_ = result.Assignments.Should().NotBeNull();
		});
	}

	[Fact]
	public async Task TagOperations_WithColorAndIcon_HandlesCustomization()
	{
		var request = NewTag("#ABCDEF", key: "custom-key", valuePrefix: "custom-value", objectType: ObjectType.Dashboard);
		request.Icon = "star";

		await WithTagAsync(request, created =>
		{
			_ = created.Color.Should().Be(request.Color);
			_ = created.Icon.Should().Be(request.Icon);
			_ = created.ObjectType.Should().Be(ObjectType.Dashboard);
			return Task.CompletedTask;
		});
	}
}
