using ThousandEyes.Api.Interfaces;
using ThousandEyes.Api.Models.TestResults;

namespace ThousandEyes.Api;

/// <summary>
/// Implementation of Test Results API using Refit
/// </summary>
internal class TestResultsApi(ITestResultsRefitApi refitApi) : ITestResultsApi
{
	private readonly ITestResultsRefitApi _refitApi = refitApi;

	/// <inheritdoc />
	public Task<NetworkTestResults> GetNetworkResultsAsync(string testId, DateTime? fromDate, DateTime? toDate, string? aid, CancellationToken cancellationToken) =>
		_refitApi.GetNetworkResultsAsync(testId, fromDate, toDate, aid, cancellationToken);

	/// <inheritdoc />
	public Task<HttpServerTestResults> GetHttpServerResultsAsync(string testId, DateTime? fromDate, DateTime? toDate, string? aid, CancellationToken cancellationToken) =>
		_refitApi.GetHttpServerResultsAsync(testId, fromDate, toDate, aid, cancellationToken);

	/// <inheritdoc />
	public Task<NetworkTestResults> GetPathVisualizationResultsAsync(string testId, DateTime? fromDate, DateTime? toDate, string? aid, CancellationToken cancellationToken) =>
		_refitApi.GetPathVisualizationResultsAsync(testId, fromDate, toDate, aid, cancellationToken);
}