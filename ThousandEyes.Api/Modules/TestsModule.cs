using Refit;
using ThousandEyes.Api.Interfaces;

namespace ThousandEyes.Api.Modules;

/// <summary>
/// Tests API module for managing ThousandEyes tests
/// </summary>
/// <remarks>
/// Phase 2 implementation - Core test management functionality
/// Provides comprehensive test management for all ThousandEyes test types
/// </remarks>
public class TestsModule
{
	/// <summary>
	/// Initializes a new instance of the TestsModule
	/// </summary>
	/// <param name="httpClient">The HTTP client to use for API calls</param>
	/// <param name="refitSettings">Refit settings for JSON serialization</param>
	public TestsModule(HttpClient httpClient, RefitSettings refitSettings)
	{
		// Initialize Tests APIs using Refit
		var testsRefitApi = RestService.For<ITestsRefitApi>(httpClient, refitSettings);
		Tests = new TestsApi(testsRefitApi);

		var httpServerTestsRefitApi = RestService.For<IHttpServerTestsRefitApi>(httpClient, refitSettings);
		HttpServerTests = new HttpServerTestsApi(httpServerTestsRefitApi);

		var pageLoadTestsRefitApi = RestService.For<IPageLoadTestsRefitApi>(httpClient, refitSettings);
		PageLoadTests = new PageLoadTestsApi(pageLoadTestsRefitApi);

		var webTransactionTestsRefitApi = RestService.For<IWebTransactionTestsRefitApi>(httpClient, refitSettings);
		WebTransactionTests = new WebTransactionTestsApi(webTransactionTestsRefitApi);

		var agentToServerTestsRefitApi = RestService.For<IAgentToServerTestsRefitApi>(httpClient, refitSettings);
		AgentToServerTests = new AgentToServerTestsApi(agentToServerTestsRefitApi);

		var agentToAgentTestsRefitApi = RestService.For<IAgentToAgentTestsRefitApi>(httpClient, refitSettings);
		AgentToAgentTests = new AgentToAgentTestsApi(agentToAgentTestsRefitApi);

		var dnsServerTestsRefitApi = RestService.For<IDnsServerTestsRefitApi>(httpClient, refitSettings);
		DnsServerTests = new DnsServerTestsApi(dnsServerTestsRefitApi);

		var bgpTestsRefitApi = RestService.For<IBgpTestsRefitApi>(httpClient, refitSettings);
		BgpTests = new BgpTestsApi(bgpTestsRefitApi);
	}

	/// <summary>
	/// Gets the general Tests API for listing all tests
	/// </summary>
	public ITestsApi Tests { get; }

	/// <summary>
	/// Gets the HTTP Server Tests API for managing HTTP server monitoring tests
	/// </summary>
	public IHttpServerTestsApi HttpServerTests { get; }

	/// <summary>
	/// Gets the Page Load Tests API for managing web page loading tests
	/// </summary>
	public IPageLoadTestsApi PageLoadTests { get; }

	/// <summary>
	/// Gets the Web Transaction Tests API for managing browser transaction tests
	/// </summary>
	public IWebTransactionTestsApi WebTransactionTests { get; }

	/// <summary>
	/// Gets the Agent to Server Tests API for managing network connectivity tests
	/// </summary>
	public IAgentToServerTestsApi AgentToServerTests { get; }

	/// <summary>
	/// Gets the Agent to Agent Tests API for managing point-to-point network tests
	/// </summary>
	public IAgentToAgentTestsApi AgentToAgentTests { get; }

	/// <summary>
	/// Gets the DNS Server Tests API for managing DNS resolution tests
	/// </summary>
	public IDnsServerTestsApi DnsServerTests { get; }

	/// <summary>
	/// Gets the BGP Tests API for managing BGP routing tests
	/// </summary>
	public IBgpTestsApi BgpTests { get; }
}