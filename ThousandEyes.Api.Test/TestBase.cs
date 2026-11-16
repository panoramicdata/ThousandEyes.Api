using AwesomeAssertions;
using Refit;
using ThousandEyes.Api.Exceptions;
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

	/// <summary>
	/// Runs a test body, treating a 404 from the API as "this API is not available in the account"
	/// </summary>
	protected async Task RunIfAvailableAsync(Func<Task> body)
	{
		try
		{
			await body();
		}
		catch (ValidationApiException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
		{
			Logger.LogWarning("API not available in this account (404); test body abandoned");
		}
	}

	/// <summary>
	/// Runs a cleanup action and logs, rather than propagates, any failure
	/// </summary>
	protected async Task IgnoreErrorsAsync(Func<Task> cleanup)
	{
		try
		{
			await cleanup();
		}
		catch (Exception ex)
		{
			Logger.LogDebug(ex, "Ignoring cleanup error");
		}
	}

	/// <summary>
	/// Runs the body only when the probe shows the API is available in the account, otherwise logs and returns
	/// </summary>
	protected async Task RunWhenAvailableAsync(string apiName, Func<Task> probe, Func<Task> body)
	{
		try
		{
			await probe();
		}
		catch (Exception ex) when (ex is ThousandEyesBadRequestException or ThousandEyesAuthorizationException
			|| ex is ValidationApiException { StatusCode: System.Net.HttpStatusCode.NotFound })
		{
			Logger.LogWarning(
				"{Api} API not available in this account - this may require specific permissions, a licence or a newer API version. Test skipped.",
				apiName);
			return;
		}

		await body();
	}

	/// <summary>
	/// Seconds since the Unix epoch, for unique test resource names
	/// </summary>
	protected static long Timestamp => DateTimeOffset.UtcNow.ToUnixTimeSeconds();

	/// <summary>
	/// Creates a resource, runs the body against it and always deletes it afterwards
	/// </summary>
	protected static async Task WithCreatedAsync<T>(Task<T> create, Func<T, string> idOf, Func<string, Task> delete, Func<T, Task> body)
	{
		var created = await create;
		try
		{
			await body(created);
		}
		finally
		{
			await delete(idOf(created));
		}
	}

	/// <summary>
	/// Asserts that fetching a deleted resource fails
	/// </summary>
	protected static async Task AssertGoneAsync(Func<Task> fetch)
	{
		_ = await AwesomeAssertions.FluentActions.Awaiting(fetch).Should().ThrowAsync<Exception>();
	}
}
