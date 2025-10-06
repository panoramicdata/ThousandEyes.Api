using Microsoft.Extensions.Logging;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ThousandEyes.Api.Infrastructure;

/// <summary>
/// HTTP message handler that manages OAuth2 authentication for Halo API requests using client credentials flow
/// </summary>
internal sealed class AuthenticationHandler : DelegatingHandler
{
	private static readonly JsonSerializerOptions _jsonSerializerOptions = new()
	{
		PropertyNameCaseInsensitive = true
	};

	private readonly ThousandEyesClientOptions _options;
	private readonly SemaphoreSlim _tokenSemaphore = new(1, 1);
	private readonly HttpClient _authHttpClient; // Separate client for auth requests
	private string? _accessToken;
	private DateTime _tokenExpiry = DateTime.MinValue;

	public AuthenticationHandler(ThousandEyesClientOptions options)
	{
		_options = options ?? throw new ArgumentNullException(nameof(options));

		// Create a separate HttpClient for authentication requests
		// This avoids circular dependencies with the main client
		_authHttpClient = new HttpClient
		{
			BaseAddress = new Uri(_options.EffectiveBaseUrl),
			Timeout = _options.RequestTimeout
		};
	}

	protected override async Task<HttpResponseMessage> SendAsync(
		HttpRequestMessage request,
		CancellationToken cancellationToken)
	{
		// Don't intercept auth requests to avoid infinite loops
		if (request.RequestUri?.PathAndQuery.StartsWith("/auth", StringComparison.OrdinalIgnoreCase) == true)
		{
			return await base.SendAsync(request, cancellationToken).ConfigureAwait(false);
		}

		// Ensure we have a valid access token
		await EnsureValidTokenAsync(cancellationToken).ConfigureAwait(false);

		// Add the authorization header
		if (!string.IsNullOrEmpty(_accessToken))
		{
			request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", _accessToken);
		}

		var response = await base.SendAsync(request, cancellationToken).ConfigureAwait(false);

		// If we get a 401, try to refresh the token once
		if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized && !string.IsNullOrEmpty(_accessToken))
		{
			_options.Logger?.LogWarning("Received 401 Unauthorized, attempting to refresh token");

			// Clear the current token and get a new one
			_accessToken = null;
			_tokenExpiry = DateTime.MinValue;

			await EnsureValidTokenAsync(cancellationToken).ConfigureAwait(false);

			// Create a new request message for retry (HttpRequestMessage can only be sent once)
			if (!string.IsNullOrEmpty(_accessToken))
			{
				var retryRequest = await CloneHttpRequestMessageAsync(request);
				retryRequest.Headers.Authorization = new AuthenticationHeaderValue("Bearer", _accessToken);

				response.Dispose(); // Dispose the 401 response
				response = await base.SendAsync(retryRequest, cancellationToken).ConfigureAwait(false);
			}
		}

		return response;
	}

	/// <summary>
	/// Clones an HttpRequestMessage for retry purposes since HttpRequestMessage can only be sent once
	/// </summary>
	private static async Task<HttpRequestMessage> CloneHttpRequestMessageAsync(HttpRequestMessage original)
	{
		var clone = new HttpRequestMessage(original.Method, original.RequestUri);

		// Copy headers
		foreach (var header in original.Headers)
		{
			_ = clone.Headers.TryAddWithoutValidation(header.Key, header.Value);
		}

		// Copy content if present
		if (original.Content != null)
		{
			var contentBytes = await original.Content.ReadAsByteArrayAsync();
			clone.Content = new ByteArrayContent(contentBytes);

			// Copy content headers
			foreach (var header in original.Content.Headers)
			{
				_ = clone.Content.Headers.TryAddWithoutValidation(header.Key, header.Value);
			}
		}

		// Copy other properties
		clone.Version = original.Version;
		foreach (var property in original.Options)
		{
			clone.Options.Set(new HttpRequestOptionsKey<object?>(property.Key), property.Value);
		}

		return clone;
	}

	private async Task EnsureValidTokenAsync(CancellationToken cancellationToken)
	{
		// Check if we need a new token (with 5-minute buffer)
		if (!string.IsNullOrEmpty(_accessToken) && _tokenExpiry > DateTime.UtcNow.AddMinutes(5))
		{
			return;
		}

		await _tokenSemaphore.WaitAsync(cancellationToken).ConfigureAwait(false);
		try
		{
			// Double-check after acquiring the lock
			if (!string.IsNullOrEmpty(_accessToken) && _tokenExpiry > DateTime.UtcNow.AddMinutes(5))
			{
				return;
			}

			await RefreshTokenAsync(cancellationToken).ConfigureAwait(false);
		}
		finally
		{
			_ = _tokenSemaphore.Release();
		}
	}

	private async Task RefreshTokenAsync(CancellationToken cancellationToken)
	{
		_options.Logger?.LogDebug("Refreshing Halo API access token using client credentials");
		try
		{
			var formData = new FormUrlEncodedContent(new Dictionary<string, string>
			{
				["grant_type"] = "client_credentials",
				["client_id"] = _options.ClientId,
				["client_secret"] = _options.ClientSecret,
				["scope"] = "all"
			});

			// Use the separate auth client to avoid circular dependencies
			var response = await _authHttpClient.PostAsync("/auth/token", formData, cancellationToken).ConfigureAwait(false);

			if (!response.IsSuccessStatusCode)
			{
				var errorContent = await response.Content.ReadAsStringAsync(cancellationToken).ConfigureAwait(false);
				_options.Logger?.LogError("Authentication failed: {StatusCode} - {Content}", response.StatusCode, errorContent);
				throw new AuthenticationException(
					$"Failed to obtain access token. Status: {response.StatusCode}, Content: {errorContent}");
			}

			var responseContent = await response.Content.ReadAsStringAsync(cancellationToken).ConfigureAwait(false);

			// Log the raw response for debugging
			_options.Logger?.LogDebug("Token response: {Response}", responseContent);

			var tokenResponse = JsonSerializer.Deserialize<TokenResponse>(responseContent, _jsonSerializerOptions);

			if (tokenResponse == null || string.IsNullOrEmpty(tokenResponse.AccessToken))
			{
				throw new AuthenticationException($"Invalid token response received from Halo API. Response: {responseContent}");
			}

			_accessToken = tokenResponse.AccessToken;
			_tokenExpiry = DateTime.UtcNow.AddSeconds(tokenResponse.ExpiresIn - 60); // 60-second buffer

			_options.Logger?.LogDebug("Successfully refreshed Halo API access token, expires at {Expiry}", _tokenExpiry);
		}
		catch (Exception ex) when (ex is not AuthenticationException)
		{
			_options.Logger?.LogError(ex, "Failed to refresh Halo API access token");
			throw new AuthenticationException("Failed to obtain access token from Halo API", ex);
		}
	}

	protected override void Dispose(bool disposing)
	{
		if (disposing)
		{
			_tokenSemaphore.Dispose();
			_authHttpClient.Dispose();
		}

		base.Dispose(disposing);
	}

	private sealed record TokenResponse
	{
		[JsonPropertyName("access_token")]
		public string AccessToken { get; init; } = "";

		[JsonPropertyName("token_type")]
		public string TokenType { get; init; } = "";

		[JsonPropertyName("expires_in")]
		public int ExpiresIn { get; init; }
	}
}

/// <summary>
/// Exception thrown when authentication with the Halo API fails
/// </summary>
public sealed class AuthenticationException : Exception
{
	/// <summary>
	/// Initializes a new instance of the AuthenticationException class with a specified error message
	/// </summary>
	/// <param name="message">The message that describes the error</param>
	public AuthenticationException(string message) : base(message) { }

	/// <summary>
	/// Initializes a new instance of the AuthenticationException class with a specified error message and a reference to the inner exception
	/// </summary>
	/// <param name="message">The message that describes the error</param>
	/// <param name="innerException">The exception that is the cause of the current exception</param>
	public AuthenticationException(string message, Exception innerException) : base(message, innerException) { }
}