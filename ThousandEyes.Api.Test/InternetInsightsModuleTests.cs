using AwesomeAssertions;
using ThousandEyes.Api.Models.InternetInsights;

namespace ThousandEyes.Api.Test;

[Collection("Integration Tests")]
public class InternetInsightsModuleTests(IntegrationTestFixture fixture)
{
	private readonly IntegrationTestFixture _fixture = fixture;

	[Fact]
	public async Task FilterCatalogProviders_WithValidFilter_ReturnsProviders()
	{
		// Arrange
		var client = _fixture.GetThousandEyesClient();
		var cancellationToken = CancellationToken.None;
		var filter = new CatalogProviderFilter
		{
			Included = true // Only licensed providers
		};

		// Act
		var result = await client.InternetInsights.CatalogProviders.FilterAsync(
			filter,
			aid: null,
			cancellationToken);

		// Assert
		result.Should().NotBeNull();
		result.ProvidersList.Should().NotBeEmpty();

		// Validate first provider structure
		var firstProvider = result.ProvidersList[0];
		firstProvider.Id.Should().NotBeNullOrWhiteSpace();
		firstProvider.ProviderName.Should().NotBeNullOrWhiteSpace();
	}

	[Fact]
	public async Task GetCatalogProvider_WithValidId_ReturnsProviderDetails()
	{
		// Arrange
		var client = _fixture.GetThousandEyesClient();
		var cancellationToken = CancellationToken.None;

		// First, get a list of providers to get a valid ID
		var filter = new CatalogProviderFilter { Included = true };
		var providers = await client.InternetInsights.CatalogProviders.FilterAsync(
			filter,
			aid: null,
			cancellationToken);

		providers.ProvidersList.Should().NotBeEmpty();
		var testProviderId = providers.ProvidersList[0].Id;

		// Act
		var result = await client.InternetInsights.CatalogProviders.GetByIdAsync(
			testProviderId,
			aid: null,
			cancellationToken);

		// Assert
		result.Should().NotBeNull();
		result.Id.Should().Be(testProviderId);
		result.ProviderName.Should().NotBeNullOrWhiteSpace();
	}

	[Fact]
	public async Task FilterOutages_WithTimeWindow_ReturnsOutages()
	{
		// Arrange
		var client = _fixture.GetThousandEyesClient();
		var cancellationToken = CancellationToken.None;
		var filter = new OutageFilter
		{
			Window = "7d" // Last 7 days
		};

		// Act
		var result = await client.InternetInsights.Outages.FilterAsync(
			filter,
			aid: null,
			cancellationToken);

		// Assert
		result.Should().NotBeNull();
		result.OutagesList.Should().NotBeNull();
		// Note: Outages list may be empty if no outages in timeframe
	}

	[Fact]
	public async Task FilterOutages_WithDateRange_ReturnsOutages()
	{
		// Arrange
		var client = _fixture.GetThousandEyesClient();
		var cancellationToken = CancellationToken.None;
		var filter = new OutageFilter
		{
			StartDate = DateTime.UtcNow.AddDays(-7),
			EndDate = DateTime.UtcNow
		};

		// Act
		var result = await client.InternetInsights.Outages.FilterAsync(
			filter,
			aid: null,
			cancellationToken);

		// Assert
		result.Should().NotBeNull();
		result.OutagesList.Should().NotBeNull();
	}

	[Fact]
	public async Task GetNetworkOutage_WithValidId_ReturnsOutageDetails()
	{
		// Arrange
		var client = _fixture.GetThousandEyesClient();
		var cancellationToken = CancellationToken.None;

		// First, get a list of outages to find a network outage
		var filter = new OutageFilter { Window = "30d" };
		var outages = await client.InternetInsights.Outages.FilterAsync(
			filter,
			aid: null,
			cancellationToken);

		var networkOutage = outages.OutagesList.FirstOrDefault(o => o.Type == "net");
		if (networkOutage == null)
		{
			// Skip test if no network outages found
			return;
		}

		// Act
		var result = await client.InternetInsights.Outages.GetNetworkOutageAsync(
			networkOutage.Id,
			aid: null,
			cancellationToken);

		// Assert
		result.Should().NotBeNull();
		result.Id.Should().Be(networkOutage.Id);
	}

	[Fact]
	public async Task GetApplicationOutage_WithValidId_ReturnsOutageDetails()
	{
		// Arrange
		var client = _fixture.GetThousandEyesClient();
		var cancellationToken = CancellationToken.None;

		// First, get a list of outages to find an application outage
		var filter = new OutageFilter { Window = "30d" };
		var outages = await client.InternetInsights.Outages.FilterAsync(
			filter,
			aid: null,
			cancellationToken);

		var appOutage = outages.OutagesList.FirstOrDefault(o => o.Type == "app");
		if (appOutage == null)
		{
			// Skip test if no application outages found
			return;
		}

		// Act
		var result = await client.InternetInsights.Outages.GetApplicationOutageAsync(
			appOutage.Id,
			aid: null,
			cancellationToken);

		// Assert
		result.Should().NotBeNull();
		result.Id.Should().Be(appOutage.Id);
	}
}
