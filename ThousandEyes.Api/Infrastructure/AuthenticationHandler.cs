using System.Net.Http.Headers;
using Microsoft.Extensions.Logging;

namespace ThousandEyes.Api.Infrastructure;

/// <summary>
/// HTTP handler that adds Bearer token authentication to requests
/// </summary>
internal class AuthenticationHandler(ThousandEyesClientOptions options) : DelegatingHandler
{
	private readonly ThousandEyesClientOptions _options = options;

	/// <summary>
	/// Processes HTTP requests and adds Bearer token authentication
	/// </summary>
	/// <param name="request">The HTTP request message</param>
	/// <param name="cancellationToken">Cancellation token</param>
	/// <returns>HTTP response message</returns>
	protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
	{
		// Add Bearer token authentication header
		if (!string.IsNullOrEmpty(_options.BearerToken))
		{
			request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", _options.BearerToken);
		}

		_options.Logger?.LogDebug("Added Bearer token authentication to request: {Method} {Uri}",
			request.Method, request.RequestUri);

		return await base.SendAsync(request, cancellationToken).ConfigureAwait(false);
	}
}