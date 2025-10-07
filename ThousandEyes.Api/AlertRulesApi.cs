using ThousandEyes.Api.Interfaces;
using ThousandEyes.Api.Models.Alerts;

namespace ThousandEyes.Api;

/// <summary>
/// Implementation of Alert Rules API using Refit
/// </summary>
internal class AlertRulesApi(IAlertRulesRefitApi refitApi) : IAlertRulesApi
{
	private readonly IAlertRulesRefitApi _refitApi = refitApi;

	/// <inheritdoc />
	public Task<AlertRules> GetAllAsync(string? aid, CancellationToken cancellationToken) =>
		_refitApi.GetAllAsync(aid, cancellationToken);

	/// <inheritdoc />
	public Task<AlertRule> GetByIdAsync(string ruleId, string? aid, CancellationToken cancellationToken) =>
		_refitApi.GetByIdAsync(ruleId, aid, cancellationToken);

	/// <inheritdoc />
	public Task<AlertRule> CreateAsync(AlertRuleRequest request, string? aid, CancellationToken cancellationToken) =>
		_refitApi.CreateAsync(request, aid, cancellationToken);

	/// <inheritdoc />
	public Task<AlertRule> UpdateAsync(string ruleId, AlertRuleRequest request, string? aid, CancellationToken cancellationToken) =>
		_refitApi.UpdateAsync(ruleId, request, aid, cancellationToken);

	/// <inheritdoc />
	public Task DeleteAsync(string ruleId, string? aid, CancellationToken cancellationToken) =>
		_refitApi.DeleteAsync(ruleId, aid, cancellationToken);
}