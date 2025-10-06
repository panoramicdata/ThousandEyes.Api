using ThousandEyes.Api.Modules;

namespace ThousandEyes.Api.Interfaces;

/// <summary>
/// Main interface for the ThousandEyes API client providing access to all API modules
/// </summary>
public interface IThousandEyesClient
{
	/// <summary>
	/// Gets the Account Management module for administrative operations
	/// </summary>
	/// <remarks>
	/// ✅ Phase 1 - COMPLETED: Full administrative API coverage including:
	/// - Account Groups management
	/// - Users management and current user operations
	/// - Roles and permissions management
	/// - User Events (audit logs) with advanced filtering
	/// </remarks>
	AccountManagementModule AccountManagement { get; }

	/// <summary>
	/// Gets the Tests module for test configuration and management
	/// </summary>
	/// <remarks>
	/// 🚧 Phase 2 - PLANNED: Core monitoring APIs including:
	/// - All test types (HTTP, Page Load, Web Transaction, etc.)
	/// - Test creation, update, and deletion
	/// - Test configuration and settings
	/// </remarks>
	TestsModule Tests { get; }

	/// <summary>
	/// Gets the Agents module for managing Cloud and Enterprise agents
	/// </summary>
	/// <remarks>
	/// 🚧 Phase 2 - PLANNED: Agent management including:
	/// - Cloud and Enterprise agent operations
	/// - Agent capabilities and supported tests
	/// - Agent location and network information
	/// </remarks>
	AgentsModule Agents { get; }

	/// <summary>
	/// Gets the Test Results module for retrieving monitoring data
	/// </summary>
	/// <remarks>
	/// 🚧 Phase 2 - PLANNED: Monitoring data retrieval including:
	/// - Network, HTTP, Page Load, and Web Transaction results
	/// - Path visualization data
	/// - Real-time and historical metrics
	/// </remarks>
	TestResultsModule TestResults { get; }

	/// <summary>
	/// Gets the Alerts module for alert management and notifications
	/// </summary>
	/// <remarks>
	/// 🚧 Phase 3 - PLANNED: Alert functionality including:
	/// - Alert rules and conditions
	/// - Notification management (email, webhook, integrations)
	/// - Alert history and clearing
	/// </remarks>
	AlertsModule Alerts { get; }

	/// <summary>
	/// Gets the Dashboards module for reporting and data visualization
	/// </summary>
	/// <remarks>
	/// 🚧 Phase 3 - PLANNED: Reporting functionality including:
	/// - Custom dashboard creation
	/// - Report generation and scheduling
	/// - Data visualization and filtering
	/// </remarks>
	DashboardsModule Dashboards { get; }

	/// <summary>
	/// Gets the Snapshots module for data preservation and sharing
	/// </summary>
	/// <remarks>
	/// 🚧 Phase 3 - PLANNED: Data preservation including:
	/// - Snapshot creation and management
	/// - Data sharing capabilities
	/// - Snapshot configuration and cleanup
	/// </remarks>
	SnapshotsModule Snapshots { get; }

	/// <summary>
	/// Gets the BGP Monitors module for network infrastructure monitoring
	/// </summary>
	/// <remarks>
	/// 🚧 Phase 4 - PLANNED: BGP monitoring including:
	/// - BGP monitor management
	/// - BGP route and path analysis
	/// - AS path and community information
	/// </remarks>
	BgpMonitorsModule BgpMonitors { get; }

	// Additional modules will be added in future phases:
	// Phase 4: InternetInsights, EventDetection, EndpointAgents
	// Phase 5: Integrations, Credentials, Usage
	// Phase 6: Emulation, Tags, Templates
	// Phase 7: OpenTelemetry (future)
}
