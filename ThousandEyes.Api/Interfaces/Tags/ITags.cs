using ThousandEyes.Api.Models.Tags;

namespace ThousandEyes.Api.Interfaces.Tags;

/// <summary>
/// Interface for managing tags for ThousandEyes assets.
/// </summary>
public interface ITags
{
	/// <summary>
	/// Retrieves a list of tags in the specified account group.
	/// </summary>
	/// <param name="aid">Optional account group ID</param>
	/// <param name="expand">Optional expand options to include assignments</param>
	/// <param name="cancellationToken">Cancellation token</param>
	/// <returns>A list of tags</returns>
	Task<Models.Tags.Tags> GetAllAsync(string? aid, string[]? expand, CancellationToken cancellationToken);

	/// <summary>
	/// Creates a new tag.
	/// </summary>
	/// <param name="request">Tag information</param>
	/// <param name="aid">Optional account group ID</param>
	/// <param name="cancellationToken">Cancellation token</param>
	/// <returns>The created tag information</returns>
	Task<TagInfo> CreateAsync(TagInfo request, string? aid, CancellationToken cancellationToken);

	/// <summary>
	/// Creates multiple tags in bulk.
	/// The response includes a statuses array indexed 1:1 with the tags array.
	/// </summary>
	/// <param name="request">Bulk tag request with tags to create</param>
	/// <param name="aid">Optional account group ID</param>
	/// <param name="cancellationToken">Cancellation token</param>
	/// <returns>Bulk response with created tags and any errors</returns>
	Task<BulkTagResponse> CreateBulkAsync(BulkTagResponse request, string? aid, CancellationToken cancellationToken);

	/// <summary>
	/// Retrieves a tag using its ID.
	/// </summary>
	/// <param name="id">Tag ID</param>
	/// <param name="aid">Optional account group ID</param>
	/// <param name="expand">Optional expand options to include assignments</param>
	/// <param name="cancellationToken">Cancellation token</param>
	/// <returns>The tag with details</returns>
	Task<Tag> GetByIdAsync(string id, string? aid, string[]? expand, CancellationToken cancellationToken);

	/// <summary>
	/// Updates a tag.
	/// </summary>
	/// <param name="id">Tag ID to update</param>
	/// <param name="request">Updated tag information</param>
	/// <param name="aid">Optional account group ID</param>
	/// <param name="cancellationToken">Cancellation token</param>
	/// <returns>The updated tag information</returns>
	Task<TagInfo> UpdateAsync(string id, TagInfo request, string? aid, CancellationToken cancellationToken);

	/// <summary>
	/// Deletes a tag.
	/// </summary>
	/// <param name="id">Tag ID to delete</param>
	/// <param name="aid">Optional account group ID</param>
	/// <param name="cancellationToken">Cancellation token</param>
	Task DeleteAsync(string id, string? aid, CancellationToken cancellationToken);

	/// <summary>
	/// Assigns a tag to one or more objects.
	/// This operation has cumulative behavior: The tag is assigned to the specified objects,
	/// and the previous assignments persist. No unassignment takes place.
	/// </summary>
	/// <param name="id">Tag ID</param>
	/// <param name="request">Assignment details</param>
	/// <param name="aid">Optional account group ID</param>
	/// <param name="cancellationToken">Cancellation token</param>
	/// <returns>Bulk assignment result</returns>
	Task<BulkTagAssignment> AssignAsync(string id, TagAssignment request, string? aid, CancellationToken cancellationToken);

	/// <summary>
	/// Removes a tag from one or more objects.
	/// </summary>
	/// <param name="id">Tag ID</param>
	/// <param name="request">Assignment details</param>
	/// <param name="aid">Optional account group ID</param>
	/// <param name="cancellationToken">Cancellation token</param>
	Task UnassignAsync(string id, TagAssignment request, string? aid, CancellationToken cancellationToken);

	/// <summary>
	/// Assigns multiple tags to multiple objects in bulk.
	/// This operation has cumulative behavior: The tags are assigned to the specified objects,
	/// and the previous assignments persist. No unassignment takes place.
	/// </summary>
	/// <param name="request">Bulk assignments</param>
	/// <param name="aid">Optional account group ID</param>
	/// <param name="cancellationToken">Cancellation token</param>
	/// <returns>Bulk assignment result</returns>
	Task<BulkTagAssignments> AssignBulkAsync(BulkTagAssignments request, string? aid, CancellationToken cancellationToken);

	/// <summary>
	/// Removes multiple tags from multiple objects in bulk.
	/// </summary>
	/// <param name="request">Bulk assignments</param>
	/// <param name="aid">Optional account group ID</param>
	/// <param name="cancellationToken">Cancellation token</param>
	/// <returns>Bulk assignment result</returns>
	Task<BulkTagAssignments> UnassignBulkAsync(BulkTagAssignments request, string? aid, CancellationToken cancellationToken);
}
