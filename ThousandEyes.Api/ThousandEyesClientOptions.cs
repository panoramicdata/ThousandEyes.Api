﻿using Microsoft.Extensions.Logging;

namespace ThousandEyes.Api;

/// <summary>
/// Configuration options for the ThousandEyes API client
/// </summary>
public partial class ThousandEyesClientOptions
{
	/// <summary>
	/// Gets or sets the ThousandEyes API bearer token
	/// </summary>
	public required string BearerToken { get; init; }

	/// <summary>
	/// Gets or sets the HTTP request timeout. Default is 30 seconds
	/// </summary>
	public TimeSpan RequestTimeout { get; init; } = TimeSpan.FromSeconds(30);

	/// <summary>
	/// Gets or sets the maximum number of retry attempts for failed requests. Default is 3
	/// </summary>
	public int MaxRetryAttempts { get; init; } = 3;

	/// <summary>
	/// Gets or sets the initial delay between retry attempts. Default is 1 second
	/// </summary>
	public TimeSpan RetryDelay { get; init; } = TimeSpan.FromSeconds(1);

	/// <summary>
	/// Gets or sets the logger instance for HTTP operations. If null, no logging is performed
	/// </summary>
	public ILogger? Logger { get; init; }

	/// <summary>
	/// Gets or sets whether to log HTTP requests
	/// </summary>
	public bool EnableRequestLogging { get; init; }

	/// <summary>
	/// Gets or sets whether to log HTTP responses
	/// </summary>
	public bool EnableResponseLogging { get; init; }

	/// <summary>
	/// Gets or sets additional default headers to include with all requests
	/// </summary>
	public IReadOnlyDictionary<string, string> DefaultHeaders { get; init; } = new Dictionary<string, string>();

	/// <summary>
	/// Gets or sets whether to use exponential back-off for retries. Default is true
	/// </summary>
	public bool UseExponentialBackoff { get; init; } = true;

	/// <summary>
	/// Gets or sets the maximum retry delay when using exponential back-off. Default is 30 seconds
	/// </summary>
	public TimeSpan MaxRetryDelay { get; init; } = TimeSpan.FromSeconds(30);

	internal void Validate()
	{
		if (string.IsNullOrWhiteSpace(BearerToken))
		{
			throw new FormatException("BearerToken must be set.");
		}

		if (RequestTimeout <= TimeSpan.Zero)
		{
			throw new ArgumentException("RequestTimeout must be greater than zero.", nameof(RequestTimeout));
		}

		if (MaxRetryAttempts < 0)
		{
			throw new ArgumentException("MaxRetryAttempts cannot be negative.", nameof(MaxRetryAttempts));
		}

		if (RetryDelay < TimeSpan.Zero)
		{
			throw new ArgumentException("RetryDelay cannot be negative.", nameof(RetryDelay));
		}

		if (MaxRetryDelay < RetryDelay)
		{
			throw new ArgumentException("MaxRetryDelay must be greater than or equal to RetryDelay.", nameof(MaxRetryDelay));
		}
	}
}