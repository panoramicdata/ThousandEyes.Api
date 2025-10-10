using System.Text.Json;
using ThousandEyes.Api.Interfaces;

namespace ThousandEyes.Api.Test;

/// <summary>
/// Abstract base class for tests that provides common dependencies
/// </summary>
public abstract class TestBase(IntegrationTestFixture fixture)
{
	protected JsonSerializerOptions JsonSerializerOptions = new() { WriteIndented = true };

	/// <summary>
	/// Gets the test fixture for creating fresh client instances
	/// </summary>
	protected readonly IntegrationTestFixture _fixture = fixture;

	/// <summary>
	/// Gets the API client for testing
	/// </summary>
	protected IThousandEyesClient ThousandEyesClient { get; } = fixture.GetThousandEyesClient();

	/// <summary>
	/// Gets the logger for test output and diagnostics
	/// </summary>
	protected ILogger Logger { get; } = fixture.Logger;

	/// <summary>
	/// Gets a cancellation token for test operations with a reasonable timeout
	/// </summary>
	protected static CancellationToken CancellationToken { get; } = new CancellationTokenSource(TimeSpan.FromMinutes(2)).Token;
}