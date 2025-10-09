using ThousandEyes.Api.Interfaces.Credentials;
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

	/// <summary>
	/// Gets the Event Detection module for automated anomaly detection
	/// </summary>
	/// <remarks>
	/// ✅ Phase 4.3 - IMPLEMENTED: Event detection including:
	/// - Automated anomaly detection
	/// - Event list retrieval with filtering
	/// - Detailed event information
	/// - Affected tests, targets, and agents tracking
	/// </remarks>
	EventDetectionModule EventDetection { get; }

	/// <summary>
	/// Gets the Integrations module for webhook and third-party service integrations
	/// </summary>
	/// <remarks>
	/// ✅ Phase 5.1 - IMPLEMENTED: Integrations including:
	/// - Webhook operation management
	/// - Generic connector configuration (Slack, PagerDuty, ServiceNow, etc.)
	/// - Operation-connector assignments
	/// - Multiple authentication types (Basic, Bearer, OAuth)
	/// </remarks>
	IntegrationsModule Integrations { get; }

	/// <summary>
	/// Gets the Credentials interface for managing transaction test credentials
	/// </summary>
	/// <remarks>
	/// ✅ Phase 5.2 - IMPLEMENTED: Credentials management including:
	/// - Create, read, update, delete credentials
	/// - Encrypted credential storage
	/// - Account group context support
	/// - Secure credential value handling
	/// </remarks>
	ICredentials Credentials { get; }

	/// <summary>
	/// Gets the Tags module for managing asset tags
	/// </summary>
	/// <remarks>
	/// ✅ Phase 6.1 - IMPLEMENTED: Tag management including:
	/// - Tag CRUD operations (create, read, update, delete)
	/// - Bulk tag creation operations
	/// - Tag assignment to tests, agents, dashboards, endpoint tests
	/// - Bulk assignment and unassignment operations
	/// - Optional expand parameter for assignments
	/// </remarks>
	TagsModule Tags { get; }

	/// <summary>
	/// Gets the Test Snapshots module for snapshot creation
	/// </summary>
	/// <remarks>
	/// ✅ Phase 6.2 - IMPLEMENTED: Test snapshot management including:
	/// - Create test snapshots for data preservation
	/// - Time range specification (1-48 hours)
	/// - Public and private snapshot support
	/// - 30-day expiration period
	/// </remarks>
	TestSnapshotsModule TestSnapshots { get; }

	/// <summary>
	/// Gets the Templates module for template management and deployment
	/// </summary>
	/// <remarks>
	/// ✅ Phase 6.3 - IMPLEMENTED: Template management including:
	/// - Template CRUD operations (create, read, update, delete)
	/// - Template deployment with user inputs
	/// - Sharing settings management
	/// - Infrastructure as code capabilities
	/// - Support for tests, alert rules, dashboards, and filters
	/// </remarks>
	TemplatesModule Templates { get; }

	/// <summary>
	/// Gets the Emulation module for device emulation and user-agent management
	/// </summary>
	/// <remarks>
	/// ✅ Phase 6.4 - IMPLEMENTED: Emulation functionality including:
	/// - User-agent string retrieval for HTTP, pageload, and transaction tests
	/// - Emulated device management for pageload and transaction tests
	/// - Device creation with display specifications
	/// - Support for desktop, laptop, phone, and tablet emulation
	/// </remarks>
	EmulationModule Emulation { get; }
}
