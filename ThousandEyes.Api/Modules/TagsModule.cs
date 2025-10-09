using Refit;
using ThousandEyes.Api.Implementations.Tags;
using ThousandEyes.Api.Interfaces.Tags;
using ThousandEyes.Api.Refit.Tags;

namespace ThousandEyes.Api.Modules;

/// <summary>
/// Module for Tags API operations including tag management and assignments.
/// </summary>
public class TagsModule(HttpClient httpClient, RefitSettings refitSettings) : ITags
{
	private readonly TagsImpl _implementation = new(RestService.For<ITagsRefitApi>(httpClient, refitSettings));

	/// <inheritdoc/>
	public async Task<Models.Tags.Tags> GetAllAsync(string? aid, string[]? expand, CancellationToken cancellationToken)
		=> await _implementation.GetAllAsync(aid, expand, cancellationToken).ConfigureAwait(false);

	/// <inheritdoc/>
	public async Task<Models.Tags.TagInfo> CreateAsync(Models.Tags.TagInfo request, string? aid, CancellationToken cancellationToken)
		=> await _implementation.CreateAsync(request, aid, cancellationToken).ConfigureAwait(false);

	/// <inheritdoc/>
	public async Task<Models.Tags.BulkTagResponse> CreateBulkAsync(Models.Tags.BulkTagResponse request, string? aid, CancellationToken cancellationToken)
		=> await _implementation.CreateBulkAsync(request, aid, cancellationToken).ConfigureAwait(false);

	/// <inheritdoc/>
	public async Task<Models.Tags.Tag> GetByIdAsync(string id, string? aid, string[]? expand, CancellationToken cancellationToken)
		=> await _implementation.GetByIdAsync(id, aid, expand, cancellationToken).ConfigureAwait(false);

	/// <inheritdoc/>
	public async Task<Models.Tags.TagInfo> UpdateAsync(string id, Models.Tags.TagInfo request, string? aid, CancellationToken cancellationToken)
		=> await _implementation.UpdateAsync(id, request, aid, cancellationToken).ConfigureAwait(false);

	/// <inheritdoc/>
	public async Task DeleteAsync(string id, string? aid, CancellationToken cancellationToken)
		=> await _implementation.DeleteAsync(id, aid, cancellationToken).ConfigureAwait(false);

	/// <inheritdoc/>
	public async Task<Models.Tags.BulkTagAssignment> AssignAsync(string id, Models.Tags.TagAssignment request, string? aid, CancellationToken cancellationToken)
		=> await _implementation.AssignAsync(id, request, aid, cancellationToken).ConfigureAwait(false);

	/// <inheritdoc/>
	public async Task UnassignAsync(string id, Models.Tags.TagAssignment request, string? aid, CancellationToken cancellationToken)
		=> await _implementation.UnassignAsync(id, request, aid, cancellationToken).ConfigureAwait(false);

	/// <inheritdoc/>
	public async Task<Models.Tags.BulkTagAssignments> AssignBulkAsync(Models.Tags.BulkTagAssignments request, string? aid, CancellationToken cancellationToken)
		=> await _implementation.AssignBulkAsync(request, aid, cancellationToken).ConfigureAwait(false);

	/// <inheritdoc/>
	public async Task<Models.Tags.BulkTagAssignments> UnassignBulkAsync(Models.Tags.BulkTagAssignments request, string? aid, CancellationToken cancellationToken)
		=> await _implementation.UnassignBulkAsync(request, aid, cancellationToken).ConfigureAwait(false);
}
