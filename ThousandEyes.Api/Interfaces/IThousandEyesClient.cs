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
	/// ✅ Phase 2 - IMPLEMENTED: Core monitoring APIs including:
	/// - General Tests API (list all tests, version history)
	/// - HTTP Server Tests (full CRUD operations)
	/// - Page Load Tests (basic implementation)
	/// - Web Transaction Tests (basic implementation)
	/// - Agent to Server Tests (basic implementation)
	/// - Agent to Agent Tests (basic implementation)
	/// - DNS Server Tests (basic implementation)
	/// - BGP Tests (basic implementation)
	/// 🚧 Remaining Phase 2: Agents API, Test Results API
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
	/// ✅ Phase 4.1 - IMPLEMENTED: BGP monitoring including:
	/// - BGP monitor management
	/// - BGP route and path analysis
	/// - AS path and community information
	/// </remarks>
	BgpMonitorsModule BgpMonitors { get; }

	/// <summary>
	/// Gets the Internet Insights module for global internet health monitoring
	/// </summary>
	/// <remarks>
	/// ✅ Phase 4.2 - IMPLEMENTED: Internet health monitoring including:
	/// - Catalog provider discovery and management
	/// - Network and application outage tracking
	/// - Provider location and ASN information
	/// - Outage impact analysis
	/// </remarks>
	InternetInsightsModule InternetInsights { get; }

	// Additional modules will be added in future phases:
	// Phase 4.3: EventDetection (if available)
	// Phase 5: Integrations, Credentials, Usage
	// Phase 6: Emulation, Tags, Templates
	// Phase 7: OpenTelemetry (future)
}
