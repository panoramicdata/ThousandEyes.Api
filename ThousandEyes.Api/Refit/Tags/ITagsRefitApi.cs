using Refit;
using ThousandEyes.Api.Models.Tags;

namespace ThousandEyes.Api.Refit.Tags;

/// <summary>
/// Internal Refit interface for Tags API operations.
/// </summary>
internal interface ITagsRefitApi
{
	/// <summary>
	/// Retrieves a list of tags in the specified account group.
	/// </summary>
	/// <param name="aid">Optional account group ID</param>
	/// <param name="expand">Optional expand options</param>
	/// <param name="cancellationToken">Cancellation token</param>
	[Get("/tags")]
	Task<Models.Tags.Tags> GetAllAsync([Query] string? aid, [Query(CollectionFormat.Multi)] string[]? expand, CancellationToken cancellationToken);

	/// <summary>
	/// Creates a new tag.
	/// </summary>
	/// <param name="request">Tag information</param>
	/// <param name="aid">Optional account group ID</param>
	/// <param name="cancellationToken">Cancellation token</param>
	[Post("/tags")]
	Task<TagInfo> CreateAsync([Body] TagInfo request, [Query] string? aid, CancellationToken cancellationToken);

	/// <summary>
	/// Creates multiple tags in bulk.
	/// </summary>
	/// <param name="request">Bulk tag request</param>
	/// <param name="aid">Optional account group ID</param>
	/// <param name="cancellationToken">Cancellation token</param>
	[Post("/tags/bulk")]
	Task<BulkTagResponse> CreateBulkAsync([Body] BulkTagResponse request, [Query] string? aid, CancellationToken cancellationToken);

	/// <summary>
	/// Retrieves a tag using its ID.
	/// </summary>
	/// <param name="id">Tag ID</param>
	/// <param name="aid">Optional account group ID</param>
	/// <param name="expand">Optional expand options</param>
	/// <param name="cancellationToken">Cancellation token</param>
	[Get("/tags/{id}")]
	Task<Tag> GetByIdAsync(string id, [Query] string? aid, [Query(CollectionFormat.Multi)] string[]? expand, CancellationToken cancellationToken);

	/// <summary>
	/// Updates a tag.
	/// </summary>
	/// <param name="id">Tag ID</param>
	/// <param name="request">Updated tag information</param>
	/// <param name="aid">Optional account group ID</param>
	/// <param name="cancellationToken">Cancellation token</param>
	[Put("/tags/{id}")]
	Task<TagInfo> UpdateAsync(string id, [Body] TagInfo request, [Query] string? aid, CancellationToken cancellationToken);

	/// <summary>
	/// Deletes a tag.
	/// </summary>
	/// <param name="id">Tag ID</param>
	/// <param name="aid">Optional account group ID</param>
	/// <param name="cancellationToken">Cancellation token</param>
	[Delete("/tags/{id}")]
	Task DeleteAsync(string id, [Query] string? aid, CancellationToken cancellationToken);

	/// <summary>
	/// Assigns a tag to one or more objects.
	/// </summary>
	/// <param name="id">Tag ID</param>
	/// <param name="request">Assignment details</param>
	/// <param name="aid">Optional account group ID</param>
	/// <param name="cancellationToken">Cancellation token</param>
	[Post("/tags/{id}/assign")]
	Task<BulkTagAssignment> AssignAsync(string id, [Body] TagAssignment request, [Query] string? aid, CancellationToken cancellationToken);

	/// <summary>
	/// Removes a tag from one or more objects.
	/// </summary>
	/// <param name="id">Tag ID</param>
	/// <param name="request">Assignment details</param>
	/// <param name="aid">Optional account group ID</param>
	/// <param name="cancellationToken">Cancellation token</param>
	[Post("/tags/{id}/unassign")]
	Task UnassignAsync(string id, [Body] TagAssignment request, [Query] string? aid, CancellationToken cancellationToken);

	/// <summary>
	/// Assigns multiple tags to multiple objects in bulk.
	/// </summary>
	/// <param name="request">Bulk assignments</param>
	/// <param name="aid">Optional account group ID</param>
	/// <param name="cancellationToken">Cancellation token</param>
	[Post("/tags/assign")]
	Task<BulkTagAssignments> AssignBulkAsync([Body] BulkTagAssignments request, [Query] string? aid, CancellationToken cancellationToken);

	/// <summary>
	/// Removes multiple tags from multiple objects in bulk.
	/// </summary>
	/// <param name="request">Bulk assignments</param>
	/// <param name="aid">Optional account group ID</param>
	/// <param name="cancellationToken">Cancellation token</param>
	[Post("/tags/unassign")]
	Task<BulkTagAssignments> UnassignBulkAsync([Body] BulkTagAssignments request, [Query] string? aid, CancellationToken cancellationToken);
}
