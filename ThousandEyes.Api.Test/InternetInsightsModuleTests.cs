using AwesomeAssertions;
using ThousandEyes.Api.Models.InternetInsights;

namespace ThousandEyes.Api.Test;

[Collection("Integration Tests")]
[Trait("Category", "Integration")]
public class InternetInsightsModuleTests(IntegrationTestFixture fixture) : TestBase(fixture)
{
	private Task<CatalogProviderResponse> LicensedProvidersAsync() =>
		ThousandEyesClient.InternetInsights.CatalogProviders.FilterAsync(new CatalogProviderFilter { Included = true }, aid: null, CancellationToken);

	private Task<OutagesResponse> OutagesAsync(OutageFilter filter) =>
		ThousandEyesClient.InternetInsights.Outages.FilterAsync(filter, aid: null, CancellationToken);

	private async Task WithOutageOfTypeAsync(string type, Func<string, Task> body)
	{
		var outages = await OutagesAsync(new OutageFilter { Window = "30d" });
		var outage = outages.OutagesList.FirstOrDefault(o => o.Type == type);
		if (outage != null)
		{
			await body(outage.Id);
		}
	}

	[Fact]
	public async Task FilterCatalogProviders_WithValidFilter_ReturnsProviders()
	{
		var result = await LicensedProvidersAsync();

		_ = result.ProvidersList.Should().NotBeEmpty();
		_ = result.ProvidersList[0].Id.Should().NotBeNullOrWhiteSpace();
		_ = result.ProvidersList[0].ProviderName.Should().NotBeNullOrWhiteSpace();
	}

	[Fact]
	public async Task GetCatalogProvider_WithValidId_ReturnsProviderDetails()
	{
		var providers = await LicensedProvidersAsync();
		_ = providers.ProvidersList.Should().NotBeEmpty();
		var providerId = providers.ProvidersList[0].Id;

		var result = await ThousandEyesClient.InternetInsights.CatalogProviders.GetByIdAsync(providerId, aid: null, CancellationToken);

		_ = result.Id.Should().Be(providerId);
		_ = result.ProviderName.Should().NotBeNullOrWhiteSpace();
	}

	[Fact]
	public async Task FilterOutages_WithTimeWindow_ReturnsOutages()
	{
		var result = await OutagesAsync(new OutageFilter { Window = "7d" });

		_ = result.OutagesList.Should().NotBeNull();
	}

	[Fact]
	public async Task FilterOutages_WithDateRange_ReturnsOutages()
	{
		var result = await OutagesAsync(new OutageFilter { StartDate = DateTime.UtcNow.AddDays(-7), EndDate = DateTime.UtcNow });

		_ = result.OutagesList.Should().NotBeNull();
	}

	[Fact]
	public Task GetNetworkOutage_WithValidId_ReturnsOutageDetails() => WithOutageOfTypeAsync("net", async id =>
	{
		var result = await ThousandEyesClient.InternetInsights.Outages.GetNetworkOutageAsync(id, aid: null, CancellationToken);

		_ = result.Id.Should().Be(id);
	});

	[Fact]
	public Task GetApplicationOutage_WithValidId_ReturnsOutageDetails() => WithOutageOfTypeAsync("app", async id =>
	{
		var result = await ThousandEyesClient.InternetInsights.Outages.GetApplicationOutageAsync(id, aid: null, CancellationToken);

		_ = result.Id.Should().Be(id);
	});
}
