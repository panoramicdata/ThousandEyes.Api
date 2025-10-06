using AwesomeAssertions;
using ThousandEyes.Api.Models.Assets;

namespace ThousandEyes.Api.Test;

[Collection("Integration Tests")]
public class AssetsIntegrationTest(IntegrationTestFixture fixture) : TestBase(fixture)
{
	[Fact]
	public async Task GetAssets_ShouldReturnAssets()
	{
		// Act - Use the proper ThousandEyesClient API
		var result = await ThousandEyesClient.Psa.Assets.GetAllAsync(CancellationToken);

		// Assert
		_ = result.Should().NotBeNull();
		_ = result.Should().BeAssignableTo<IReadOnlyList<Asset>>();

		// If there are assets, verify the structure
		if (result.Count > 0)
		{
			var firstAsset = result[0];
			_ = firstAsset.Id.Should().BePositive();
			_ = firstAsset.Name.Should().NotBeNullOrEmpty();
		}
	}
}