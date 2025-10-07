using ThousandEyes.Api.Interfaces;
using ThousandEyes.Api.Models.Tests;

namespace ThousandEyes.Api;

/// <summary>
/// Implementation of Tests API for general test operations
/// </summary>
internal class TestsApi(ITestsRefitApi refitApi) : ITestsApi
{
	private readonly ITestsRefitApi _refitApi = refitApi;

	/// <inheritdoc />
	public Task<Tests> GetAllAsync(string? aid, CancellationToken cancellationToken) =>
		_refitApi.GetAllAsync(aid, cancellationToken);

	/// <inheritdoc />
	public Task<TestVersionHistoryResponse> GetVersionHistoryAsync(string testId, string? aid, int? limit, CancellationToken cancellationToken) =>
		_refitApi.GetVersionHistoryAsync(testId, aid, limit, cancellationToken);
}