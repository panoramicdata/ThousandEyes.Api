namespace ThousandEyes.Api.Infrastructure;

/// <summary>
/// Refuses every request other than GET, HEAD, OPTIONS or the read-only endpoint agent filter POST while <see cref="ThousandEyesClientOptions.IsReadOnly"/> is set.
/// The option is read on every request.
/// </summary>
internal sealed class ReadOnlyHandler(ThousandEyesClientOptions options) : DelegatingHandler
{
	// POST /endpoint/agents/filter only searches.
	private const string EndpointAgentFilterPath = "/endpoint/agents/filter";

	protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
	{
		if (options.IsReadOnly && !IsRead(request))
		{
			throw new InvalidOperationException($"The client is read-only (IsReadOnly is true): {request.Method} {request.RequestUri?.AbsolutePath} is not allowed.");
		}

		return base.SendAsync(request, cancellationToken);
	}

	private static bool IsRead(HttpRequestMessage request)
		=> request.Method == HttpMethod.Get
			|| request.Method == HttpMethod.Head
			|| request.Method == HttpMethod.Options
			|| (request.Method == HttpMethod.Post
				&& request.RequestUri is { } uri
				&& uri.AbsolutePath.EndsWith(EndpointAgentFilterPath, StringComparison.OrdinalIgnoreCase));
}
