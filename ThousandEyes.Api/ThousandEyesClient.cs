using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging.Abstractions;
using Refit;
using ThousandEyes.Api.Infrastructure;
using ThousandEyes.Api.Interfaces;
using ThousandEyes.Api.Interfaces.Credentials;
using ThousandEyes.Api.Modules;

namespace ThousandEyes.Api;

/// <summary>
/// Client for interacting with the ThousandEyes API
/// </summary>
public class ThousandEyesClient : IThousandEyesClient, IDisposable
{
	private readonly ThousandEyesClientOptions _options;
	private readonly HttpClient _httpClient;
	private readonly RefitSettings _refitSettings;
	private readonly IServiceProvider _serviceProvider;
	private bool _disposed;

	/// <summary>
	/// Initializes a new instance of the ThousandEyesClient
	/// </summary>
	/// <param name="options">Configuration options for the client</param>
	public ThousandEyesClient(ThousandEyesClientOptions options)
	{
		ArgumentNullException.ThrowIfNull(options);
		options.Validate();

		_options = options;
		_httpClient = CreateHttpClient();

		// Create Refit settings for proper JSON serialization
		_refitSettings = new RefitSettings
		{
			ContentSerializer = new SystemTextJsonContentSerializer(new System.Text.Json.JsonSerializerOptions
			{
				PropertyNamingPolicy = System.Text.Json.JsonNamingPolicy.CamelCase,
				PropertyNameCaseInsensitive = true
			})
		};

		// Set up dependency injection for Credentials module
		var services = new ServiceCollection();
		services.AddSingleton(_httpClient);
		CredentialsModule.RegisterCredentialsServices(services, _refitSettings);
		_serviceProvider = services.BuildServiceProvider();

		// Initialize Phase 1, 2, 3, 4, 5 & 6 API modules
		AccountManagement = new AccountManagementModule(_httpClient, _refitSettings);
		Tests = new TestsModule(_httpClient, _refitSettings);
		Agents = new AgentsModule(_httpClient, _refitSettings);
		TestResults = new TestResultsModule(_httpClient, _refitSettings);
		Alerts = new AlertsModule(_httpClient, _refitSettings);
		Dashboards = new DashboardsModule(_httpClient, _refitSettings);
		BgpMonitors = new BgpMonitorsModule(_httpClient, _refitSettings);
		InternetInsights = new InternetInsightsModule(_httpClient, _refitSettings);
		EventDetection = new EventDetectionModule(_httpClient, _refitSettings);
		Integrations = new IntegrationsModule(_httpClient, _refitSettings);
		Credentials = _serviceProvider.GetRequiredService<ICredentials>();
		Tags = new TagsModule(_httpClient, _refitSettings);
		TestSnapshots = new TestSnapshotsModule(_httpClient, _refitSettings);
		Templates = new TemplatesModule(_httpClient, _refitSettings);
		Emulation = new EmulationModule(_httpClient, _refitSettings);

		// Future modules will be initialized when implemented
		// Phase 6.5+: Endpoint Agents, etc.
	}

	/// <summary>
	/// Gets the Account Management module for administrative operations
	/// </summary>
	public AccountManagementModule AccountManagement { get; private set; }

	/// <summary>
	/// Gets the Tests module for test configuration and management
	/// </summary>
	public TestsModule Tests { get; private set; }

	/// <summary>
	/// Gets the Agents module for managing Cloud and Enterprise agents
	/// </summary>
	public AgentsModule Agents { get; private set; }

	/// <summary>
	/// Gets the Test Results module for retrieving monitoring data
	/// </summary>
	public TestResultsModule TestResults { get; private set; }

	/// <summary>
	/// Gets the Alerts module for alert management and notifications
	/// </summary>
	public AlertsModule Alerts { get; private set; }

	/// <summary>
	/// Gets the Dashboards module for reporting and data visualization
	/// </summary>
	public DashboardsModule Dashboards { get; private set; }

	/// <summary>
	/// Gets the Snapshots module for data preservation and sharing
	/// </summary>
	/// <remarks>
	/// 🚧 Phase 3 - PLANNED: Will be implemented to complete Phase 3
	/// </remarks>
	public SnapshotsModule Snapshots => throw new NotImplementedException("Snapshots API will be implemented to complete Phase 3. Track progress at: https://github.com/panoramicdata/ThousandEyes.Api/issues");

	/// <summary>
	/// Gets the BGP Monitors module for network infrastructure monitoring
	/// </summary>
	public BgpMonitorsModule BgpMonitors { get; private set; }

	/// <summary>
	/// Gets the Internet Insights module for global internet health monitoring
	/// </summary>
	public InternetInsightsModule InternetInsights { get; private set; }

	/// <summary>
	/// Gets the Event Detection module for automated anomaly detection
	/// </summary>
	public EventDetectionModule EventDetection { get; private set; }

	/// <summary>
	/// Gets the Integrations module for webhook and third-party service integrations
	/// </summary>
	public IntegrationsModule Integrations { get; private set; }

	/// <summary>
	/// Gets the Credentials interface for managing transaction test credentials
	/// </summary>
	public ICredentials Credentials { get; private set; }

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
	public TagsModule Tags { get; private set; }

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
	public TestSnapshotsModule TestSnapshots { get; private set; }

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
	public TemplatesModule Templates { get; private set; }

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
	public EmulationModule Emulation { get; private set; }

	/// <summary>
	/// Gets the base URL for the ThousandEyes API
	/// </summary>
	public static string BaseUrl => "https://api.thousandeyes.com/v7";

	private HttpClient CreateHttpClient()
	{
		var handler = new HttpClientHandler();

		// Build the handler chain (order matters - authentication should be innermost, error handling outermost)
		DelegatingHandler chain = new AuthenticationHandler(_options)
		{
			// Set the innermost handler (AuthenticationHandler) to point to HttpClientHandler
			InnerHandler = handler
		};

		if (_options.MaxRetryAttempts > 0)
		{
			var retryHandler = new RetryHandler(
				_options.MaxRetryAttempts,
				_options.RetryDelay,
				_options.UseExponentialBackoff,
				_options.MaxRetryDelay,
				_options.Logger)
			{
				InnerHandler = chain
			};
			chain = retryHandler;
		}

		if (_options.EnableRequestLogging || _options.EnableResponseLogging)
		{
			var loggingHandler = new LoggingHandler(
				_options.Logger ?? NullLogger.Instance,
				_options.EnableRequestLogging,
				_options.EnableResponseLogging)
			{
				InnerHandler = chain
			};
			chain = loggingHandler;
		}

		// Add error handling as the outermost handler (wraps everything)
		var errorHandler = new ErrorHandler(_options.Logger)
		{
			InnerHandler = chain
		};

		var httpClient = new HttpClient(errorHandler)
		{
			BaseAddress = new Uri(BaseUrl),
			Timeout = _options.RequestTimeout
		};

		// Add default headers
		httpClient.DefaultRequestHeaders.Add("User-Agent", "ThousandEyes.Api/.NET");
		foreach (var header in _options.DefaultHeaders)
		{
			httpClient.DefaultRequestHeaders.Add(header.Key, header.Value);
		}

		return httpClient;
	}

	/// <summary>
	/// Disposes the HTTP client and releases resources
	/// </summary>
	public void Dispose()
	{
		Dispose(true);
		GC.SuppressFinalize(this);
	}

	/// <summary>
	/// Protected dispose method
	/// </summary>
	/// <param name="disposing">True if disposing managed resources</param>
	protected virtual void Dispose(bool disposing)
	{
		if (!_disposed && disposing)
		{
			_httpClient?.Dispose();
			if (_serviceProvider is IDisposable disposable)
			{
				disposable.Dispose();
			}

			_disposed = true;
		}
	}
}
