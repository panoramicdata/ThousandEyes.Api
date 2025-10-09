using ThousandEyes.Api.Interfaces.Tags;
using ThousandEyes.Api.Models.Tags;
using ThousandEyes.Api.Refit.Tags;

namespace ThousandEyes.Api.Implementations.Tags;

/// <summary>
/// Implementation of <see cref="ITags"/> for managing ThousandEyes tags.
/// </summary>
internal class TagsImpl(ITagsRefitApi refitApi) : ITags
{
	/// <inheritdoc/>
	public async Task<Models.Tags.Tags> GetAllAsync(string? aid, string[]? expand, CancellationToken cancellationToken)
		=> await refitApi.GetAllAsync(aid, expand, cancellationToken).ConfigureAwait(false);

	/// <inheritdoc/>
	public async Task<TagInfo> CreateAsync(TagInfo request, string? aid, CancellationToken cancellationToken)
		=> await refitApi.CreateAsync(request, aid, cancellationToken).ConfigureAwait(false);

	/// <inheritdoc/>
	public async Task<BulkTagResponse> CreateBulkAsync(BulkTagResponse request, string? aid, CancellationToken cancellationToken)
		=> await refitApi.CreateBulkAsync(request, aid, cancellationToken).ConfigureAwait(false);

	/// <inheritdoc/>
	public async Task<Tag> GetByIdAsync(string id, string? aid, string[]? expand, CancellationToken cancellationToken)
		=> await refitApi.GetByIdAsync(id, aid, expand, cancellationToken).ConfigureAwait(false);

	/// <inheritdoc/>
	public async Task<TagInfo> UpdateAsync(string id, TagInfo request, string? aid, CancellationToken cancellationToken)
		=> await refitApi.UpdateAsync(id, request, aid, cancellationToken).ConfigureAwait(false);

	/// <inheritdoc/>
	public async Task DeleteAsync(string id, string? aid, CancellationToken cancellationToken)
		=> await refitApi.DeleteAsync(id, aid, cancellationToken).ConfigureAwait(false);

	/// <inheritdoc/>
	public async Task<BulkTagAssignment> AssignAsync(string id, TagAssignment request, string? aid, CancellationToken cancellationToken)
		=> await refitApi.AssignAsync(id, request, aid, cancellationToken).ConfigureAwait(false);

	/// <inheritdoc/>
	public async Task UnassignAsync(string id, TagAssignment request, string? aid, CancellationToken cancellationToken)
		=> await refitApi.UnassignAsync(id, request, aid, cancellationToken).ConfigureAwait(false);

	/// <inheritdoc/>
	public async Task<BulkTagAssignments> AssignBulkAsync(BulkTagAssignments request, string? aid, CancellationToken cancellationToken)
		=> await refitApi.AssignBulkAsync(request, aid, cancellationToken).ConfigureAwait(false);

	/// <inheritdoc/>
	public async Task<BulkTagAssignments> UnassignBulkAsync(BulkTagAssignments request, string? aid, CancellationToken cancellationToken)
		=> await refitApi.UnassignBulkAsync(request, aid, cancellationToken).ConfigureAwait(false);
}
