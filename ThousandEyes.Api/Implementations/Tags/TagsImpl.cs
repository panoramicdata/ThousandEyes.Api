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
	public async Task<Models.Tags.Tags> GetAllAsync(string? aid, string[]? expand, CancellationToken ct)
		=> await refitApi.GetAllAsync(aid, expand, ct).ConfigureAwait(false);

	/// <inheritdoc/>
	public async Task<TagInfo> CreateAsync(TagInfo request, string? aid, CancellationToken ct)
		=> await refitApi.CreateAsync(request, aid, ct).ConfigureAwait(false);

	/// <inheritdoc/>
	public async Task<BulkTagResponse> CreateBulkAsync(BulkTagResponse request, string? aid, CancellationToken ct)
		=> await refitApi.CreateBulkAsync(request, aid, ct).ConfigureAwait(false);

	/// <inheritdoc/>
	public async Task<Tag> GetByIdAsync(string id, string? aid, string[]? expand, CancellationToken ct)
		=> await refitApi.GetByIdAsync(id, aid, expand, ct).ConfigureAwait(false);

	/// <inheritdoc/>
	public async Task<TagInfo> UpdateAsync(string id, TagInfo request, string? aid, CancellationToken ct)
		=> await refitApi.UpdateAsync(id, request, aid, ct).ConfigureAwait(false);

	/// <inheritdoc/>
	public async Task DeleteAsync(string id, string? aid, CancellationToken ct)
		=> await refitApi.DeleteAsync(id, aid, ct).ConfigureAwait(false);

	/// <inheritdoc/>
	public async Task<BulkTagAssignment> AssignAsync(string id, TagAssignment request, string? aid, CancellationToken ct)
		=> await refitApi.AssignAsync(id, request, aid, ct).ConfigureAwait(false);

	/// <inheritdoc/>
	public async Task UnassignAsync(string id, TagAssignment request, string? aid, CancellationToken ct)
		=> await refitApi.UnassignAsync(id, request, aid, ct).ConfigureAwait(false);

	/// <inheritdoc/>
	public async Task<BulkTagAssignments> AssignBulkAsync(BulkTagAssignments request, string? aid, CancellationToken ct)
		=> await refitApi.AssignBulkAsync(request, aid, ct).ConfigureAwait(false);

	/// <inheritdoc/>
	public async Task<BulkTagAssignments> UnassignBulkAsync(BulkTagAssignments request, string? aid, CancellationToken ct)
		=> await refitApi.UnassignBulkAsync(request, aid, ct).ConfigureAwait(false);
}
