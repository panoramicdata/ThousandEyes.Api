using Microsoft.Extensions.Logging.Abstractions;
using Refit;
using ThousandEyes.Api.Infrastructure;
using ThousandEyes.Api.Interfaces;
using ThousandEyes.Api.Modules;

namespace ThousandEyes.Api;

/// <summary>
/// Client for interacting with the ThousandEyes API
/// </summary>
public class ThousandEyesClient : IThousandEyesClient, IDisposable
{
	private readonly ThousandEyesClientOptions _options;
	private readonly HttpClient _httpClient;
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
		var refitSettings = new RefitSettings
		{
			ContentSerializer = new SystemTextJsonContentSerializer(new System.Text.Json.JsonSerializerOptions
			{
				PropertyNamingPolicy = System.Text.Json.JsonNamingPolicy.CamelCase,
				PropertyNameCaseInsensitive = true
			})
		};

		// Initialize only the implemented API modules (Phase 1)
		AccountManagement = new AccountManagementModule(_httpClient, refitSettings);
		
		// Future modules will be initialized when implemented
		// Phase 2: Tests, Agents, TestResults
		// Phase 3: Alerts, Dashboards, Snapshots  
		// Phase 4+: BgpMonitors, etc.
	}

	/// <summary>
	/// Gets the Account Management module for administrative operations
	/// </summary>
	public AccountManagementModule AccountManagement { get; private set; }

	/// <summary>
	/// Gets the Tests module for test configuration and management
	/// </summary>
	/// <remarks>
	/// 🚧 Phase 2 - PLANNED: Will be implemented in Phase 2
	/// </remarks>
	public TestsModule Tests => throw new NotImplementedException("Tests API will be implemented in Phase 2. Track progress at: https://github.com/panoramicdata/ThousandEyes.Api/issues");

	/// <summary>
	/// Gets the Agents module for managing Cloud and Enterprise agents
	/// </summary>
	/// <remarks>
	/// 🚧 Phase 2 - PLANNED: Will be implemented in Phase 2
	/// </remarks>
	public AgentsModule Agents => throw new NotImplementedException("Agents API will be implemented in Phase 2. Track progress at: https://github.com/panoramicdata/ThousandEyes.Api/issues");

	/// <summary>
	/// Gets the Test Results module for retrieving monitoring data
	/// </summary>
	/// <remarks>
	/// 🚧 Phase 2 - PLANNED: Will be implemented in Phase 2
	/// </remarks>
	public TestResultsModule TestResults => throw new NotImplementedException("Test Results API will be implemented in Phase 2. Track progress at: https://github.com/panoramicdata/ThousandEyes.Api/issues");

	/// <summary>
	/// Gets the Alerts module for alert management and notifications
	/// </summary>
	/// <remarks>
	/// 🚧 Phase 3 - PLANNED: Will be implemented in Phase 3
	/// </remarks>
	public AlertsModule Alerts => throw new NotImplementedException("Alerts API will be implemented in Phase 3. Track progress at: https://github.com/panoramicdata/ThousandEyes.Api/issues");

	/// <summary>
	/// Gets the Dashboards module for reporting and data visualization
	/// </summary>
	/// <remarks>
	/// 🚧 Phase 3 - PLANNED: Will be implemented in Phase 3
	/// </remarks>
	public DashboardsModule Dashboards => throw new NotImplementedException("Dashboards API will be implemented in Phase 3. Track progress at: https://github.com/panoramicdata/ThousandEyes.Api/issues");

	/// <summary>
	/// Gets the Snapshots module for data preservation and sharing
	/// </summary>
	/// <remarks>
	/// 🚧 Phase 3 - PLANNED: Will be implemented in Phase 3
	/// </remarks>
	public SnapshotsModule Snapshots => throw new NotImplementedException("Snapshots API will be implemented in Phase 3. Track progress at: https://github.com/panoramicdata/ThousandEyes.Api/issues");

	/// <summary>
	/// Gets the BGP Monitors module for network infrastructure monitoring
	/// </summary>
	/// <remarks>
	/// 🚧 Phase 4 - PLANNED: Will be implemented in Phase 4
	/// </remarks>
	public BgpMonitorsModule BgpMonitors => throw new NotImplementedException("BGP Monitors API will be implemented in Phase 4. Track progress at: https://github.com/panoramicdata/ThousandEyes.Api/issues");

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
			_disposed = true;
		}
	}
}
