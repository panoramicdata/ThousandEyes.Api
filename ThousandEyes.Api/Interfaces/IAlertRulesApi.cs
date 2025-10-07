using ThousandEyes.Api.Models.Alerts;

namespace ThousandEyes.Api.Interfaces;

/// <summary>
/// Interface for Alert Rules API operations
/// </summary>
/// <remarks>
/// Phase 3 implementation - Alert rule management and configuration
/// </remarks>
public interface IAlertRulesApi
{
	/// <summary>
	/// Get all alert rules
	/// </summary>
	/// <param name="aid">Account group ID (optional)</param>
	/// <param name="cancellationToken">Cancellation token</param>
	/// <returns>List of alert rules</returns>
	Task<AlertRules> GetAllAsync(string? aid, CancellationToken cancellationToken);

	/// <summary>
	/// Get a specific alert rule by ID
	/// </summary>
	/// <param name="ruleId">Alert rule ID</param>
	/// <param name="aid">Account group ID (optional)</param>
	/// <param name="cancellationToken">Cancellation token</param>
	/// <returns>Alert rule details</returns>
	Task<AlertRule> GetByIdAsync(string ruleId, string? aid, CancellationToken cancellationToken);

	/// <summary>
	/// Create a new alert rule
	/// </summary>
	/// <param name="request">Alert rule configuration</param>
	/// <param name="aid">Account group ID (optional)</param>
	/// <param name="cancellationToken">Cancellation token</param>
	/// <returns>Created alert rule</returns>
	Task<AlertRule> CreateAsync(AlertRuleRequest request, string? aid, CancellationToken cancellationToken);

	/// <summary>
	/// Update an existing alert rule
	/// </summary>
	/// <param name="ruleId">Alert rule ID</param>
	/// <param name="request">Updated alert rule configuration</param>
	/// <param name="aid">Account group ID (optional)</param>
	/// <param name="cancellationToken">Cancellation token</param>
	/// <returns>Updated alert rule</returns>
	Task<AlertRule> UpdateAsync(string ruleId, AlertRuleRequest request, string? aid, CancellationToken cancellationToken);

	/// <summary>
	/// Delete an alert rule
	/// </summary>
	/// <param name="ruleId">Alert rule ID</param>
	/// <param name="aid">Account group ID (optional)</param>
	/// <param name="cancellationToken">Cancellation token</param>
	Task DeleteAsync(string ruleId, string? aid, CancellationToken cancellationToken);
}