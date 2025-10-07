using Refit;
using ThousandEyes.Api.Models.Alerts;

namespace ThousandEyes.Api.Interfaces;

/// <summary>
/// Internal Refit interface for Alert Rules API
/// </summary>
internal interface IAlertRulesRefitApi
{
	/// <summary>
	/// Get all alert rules
	/// </summary>
	[Get("/alert-rules")]
	Task<AlertRules> GetAllAsync([Query] string? aid, CancellationToken cancellationToken);

	/// <summary>
	/// Get alert rule by ID
	/// </summary>
	[Get("/alert-rules/{ruleId}")]
	Task<AlertRule> GetByIdAsync(string ruleId, [Query] string? aid, CancellationToken cancellationToken);

	/// <summary>
	/// Create alert rule
	/// </summary>
	[Post("/alert-rules")]
	Task<AlertRule> CreateAsync([Body] AlertRuleRequest request, [Query] string? aid, CancellationToken cancellationToken);

	/// <summary>
	/// Update alert rule
	/// </summary>
	[Put("/alert-rules/{ruleId}")]
	Task<AlertRule> UpdateAsync(string ruleId, [Body] AlertRuleRequest request, [Query] string? aid, CancellationToken cancellationToken);

	/// <summary>
	/// Delete alert rule
	/// </summary>
	[Delete("/alert-rules/{ruleId}")]
	Task DeleteAsync(string ruleId, [Query] string? aid, CancellationToken cancellationToken);
}