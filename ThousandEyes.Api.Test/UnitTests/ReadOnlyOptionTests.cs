using System.Net;
using AwesomeAssertions;
using ThousandEyes.Api.Infrastructure;

namespace ThousandEyes.Api.Test.UnitTests;

/// <summary>
/// Credential-free: a refused write throws before any request is sent.
/// </summary>
public class ReadOnlyOptionTests
{
	private const string FakeToken = "fake-token";

	private sealed class StubHandler : HttpMessageHandler
	{
		public List<HttpRequestMessage> Requests { get; } = [];

		protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
		{
			Requests.Add(request);
			return Task.FromResult(new HttpResponseMessage(HttpStatusCode.OK));
		}
	}

	private static (HttpClient Client, StubHandler Stub) CreateClient(bool isReadOnly)
	{
		var stub = new StubHandler();
		var options = new ThousandEyesClientOptions { BearerToken = FakeToken, IsReadOnly = isReadOnly };
		return (new HttpClient(new ReadOnlyHandler(options) { InnerHandler = stub }), stub);
	}

	[Fact]
	public void IsReadOnly_DefaultsToFalse()
		=> new ThousandEyesClientOptions { BearerToken = FakeToken }.IsReadOnly.Should().BeFalse();

	[Theory]
	[InlineData("POST")]
	[InlineData("PUT")]
	[InlineData("PATCH")]
	[InlineData("DELETE")]
	public async Task Write_IsRefused_WhenIsReadOnly(string method)
	{
		var (client, stub) = CreateClient(isReadOnly: true);
		using var _ = client;

		var act = () => client.SendAsync(new HttpRequestMessage(new HttpMethod(method), "https://example.com/agents/1"), TestContext.Current.CancellationToken);

		await act.Should().ThrowAsync<InvalidOperationException>();
		stub.Requests.Should().BeEmpty();
	}

	[Fact]
	public async Task Write_IsAllowed_WhenNotReadOnly()
	{
		var (client, stub) = CreateClient(isReadOnly: false);
		using var _ = client;

		using var response = await client.DeleteAsync("https://example.com/agents/1", TestContext.Current.CancellationToken);

		stub.Requests.Should().ContainSingle();
	}

	[Theory]
	[InlineData("GET")]
	[InlineData("HEAD")]
	[InlineData("OPTIONS")]
	public async Task Read_IsAllowed_WhenIsReadOnly(string method)
	{
		var (client, stub) = CreateClient(isReadOnly: true);
		using var _ = client;

		using var response = await client.SendAsync(new HttpRequestMessage(new HttpMethod(method), "https://example.com/agents"), TestContext.Current.CancellationToken);

		stub.Requests.Should().ContainSingle();
	}

	[Fact]
	public async Task EndpointAgentFilterPost_IsAllowed_WhenIsReadOnly()
	{
		var (client, stub) = CreateClient(isReadOnly: true);
		using var _ = client;

		using var response = await client.PostAsync("https://api.thousandeyes.com/v7/endpoint/agents/filter", new StringContent("{}"), TestContext.Current.CancellationToken);

		stub.Requests.Should().ContainSingle();
	}
}
