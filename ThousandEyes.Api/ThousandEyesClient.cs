﻿using Microsoft.Extensions.Logging.Abstractions;
using ThousandEyes.Api.Infrastructure;
using ThousandEyes.Api.Interfaces;

namespace ThousandEyes.Api;

/// <summary>
/// Client for interacting with the Halo API
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

		// Initialize API modules lazily
		Psa = new Lazy<IPsaApi>(() => new PsaApi(_httpClient)).Value;
		ServiceDesk = new Lazy<IServiceDeskApi>(() => new ServiceDeskApi(_httpClient)).Value;
		System = new Lazy<ISystemApi>(() => new SystemApi(_httpClient)).Value;
	}

	/// <summary>
	/// Gets the PSA (Professional Services Automation) API module
	/// </summary>
	public IPsaApi Psa { get; private set; }

	/// <summary>
	/// Gets the ServiceDesk API module
	/// </summary>
	public IServiceDeskApi ServiceDesk { get; private set; }

	/// <summary>
	/// Gets the System API module for configuration and administration
	/// </summary>
	public ISystemApi System { get; private set; }

	/// <summary>
	/// Gets the base URL for the Halo API
	/// </summary>
	public const string BaseUrl = "api.thousandeyes.com";

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
