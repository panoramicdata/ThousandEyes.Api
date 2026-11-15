using System.Net;
using System.Text;

namespace ThousandEyes.Api.Test.Infrastructure;

/// <summary>
/// Inner handler that answers from a delegate and counts the calls it receives
/// </summary>
internal sealed class StubHandler(Func<HttpRequestMessage, int, HttpResponseMessage> respond) : HttpMessageHandler
{
	public int Calls { get; private set; }

	protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
	{
		Calls++;
		return Task.FromResult(respond(request, Calls));
	}

	public static StubHandler Always(HttpStatusCode status, string? body = null) =>
		new((_, _) => Response(status, body));

	public static HttpResponseMessage Response(HttpStatusCode status, string? body = null, string? reason = null) =>
		new(status)
		{
			Content = new StringContent(body ?? string.Empty, Encoding.UTF8, "application/json"),
			ReasonPhrase = reason
		};
}

internal static class HandlerTestExtensions
{
	/// <summary>
	/// Sends a GET through the handler chain ending in the stub, returning the response
	/// </summary>
	public static async Task<HttpResponseMessage> GetAsync(this DelegatingHandler handler, StubHandler inner, string url = "https://api.thousandeyes.com/v7/users/123")
	{
		handler.InnerHandler = inner;
		using var invoker = new HttpMessageInvoker(handler, disposeHandler: false);
		using var request = new HttpRequestMessage(HttpMethod.Get, url);
		return await invoker.SendAsync(request, TestContext.Current.CancellationToken);
	}
}
