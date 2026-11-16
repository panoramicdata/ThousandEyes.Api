using AwesomeAssertions;
using ThousandEyes.Api.Models.Credentials;

namespace ThousandEyes.Api.Test;

/// <summary>
/// Integration tests for the Credentials API module
/// </summary>
[Collection("Integration Tests")]
[Trait("Category", "Integration")]
public class CredentialsModuleTests(IntegrationTestFixture fixture) : TestBase(fixture)
{
	private static long Timestamp => DateTimeOffset.UtcNow.ToUnixTimeSeconds();

	private static CredentialRequest NewRequest(string namePrefix = "Test Credential", string valuePrefix = "test-password") =>
		new(Name: $"{namePrefix} - {Timestamp}", Value: $"{valuePrefix}-{Timestamp}");

	private Task RunWhenAvailableAsync(Func<Task> body) => RunWhenAvailableAsync(
		"Credentials",
		() => ThousandEyesClient.Credentials.GetAllAsync(aid: null, CancellationToken),
		body);

	private Task<Credential> GetAsync(string id) =>
		ThousandEyesClient.Credentials.GetByIdAsync(id, aid: null, CancellationToken);

	private Task DeleteAsync(string id) =>
		ThousandEyesClient.Credentials.DeleteAsync(id, aid: null, CancellationToken);

	/// <summary>
	/// Creates a credential, runs the body against it and always deletes it afterwards
	/// </summary>
	private Task WithCredentialAsync(CredentialRequest request, Func<CredentialWithoutValue, Task> body) => RunWhenAvailableAsync(async () =>
	{
		var created = await ThousandEyesClient.Credentials.CreateAsync(request, aid: null, CancellationToken);
		try
		{
			await body(created);
		}
		finally
		{
			await DeleteAsync(created.Id!);
		}
	});

	[Fact]
	public Task GetCredentials_WithValidRequest_ReturnsCredentials() => RunWhenAvailableAsync(async () =>
	{
		var result = await ThousandEyesClient.Credentials.GetAllAsync(aid: null, CancellationToken);

		_ = result.Items.Should().NotBeNull();
	});

	[Fact]
	public Task CreateCredential_WithValidRequest_CreatesCredential()
	{
		var request = NewRequest();
		return WithCredentialAsync(request, created =>
		{
			_ = created.Id.Should().NotBeNullOrEmpty();
			_ = created.Name.Should().Be(request.Name);
			return Task.CompletedTask;
		});
	}

	[Fact]
	public Task GetCredential_WithValidId_ReturnsCredentialWithValue()
	{
		var request = NewRequest();
		return WithCredentialAsync(request, async created =>
		{
			var result = await GetAsync(created.Id!);

			_ = result.Id.Should().Be(created.Id);
			_ = result.Name.Should().Be(request.Name);
			_ = result.Value.Should().NotBeNullOrEmpty().And.NotBe(request.Value);
		});
	}

	[Fact]
	public Task UpdateCredential_WithValidRequest_UpdatesCredential()
	{
		var createRequest = NewRequest();
		var updateRequest = NewRequest("Updated Test Credential", "updated-password");
		return WithCredentialAsync(createRequest, async created =>
		{
			var result = await ThousandEyesClient.Credentials.UpdateAsync(created.Id!, updateRequest, aid: null, CancellationToken);

			_ = result.Id.Should().Be(created.Id);
			_ = result.Name.Should().Be(updateRequest.Name);

			var retrieved = await GetAsync(created.Id!);
			_ = retrieved.Name.Should().Be(updateRequest.Name);
			_ = retrieved.Value.Should().NotBe(createRequest.Value);
		});
	}

	[Fact]
	public Task DeleteCredential_WithValidId_DeletesCredential() => RunWhenAvailableAsync(async () =>
	{
		var created = await ThousandEyesClient.Credentials.CreateAsync(NewRequest(), aid: null, CancellationToken);

		await DeleteAsync(created.Id!);

		// Refit throws for the 404 that follows a successful delete
		var act = () => GetAsync(created.Id!);
		_ = await act.Should().ThrowAsync<Exception>();
	});

	[Fact]
	public Task CreateCredential_WithSensitiveValue_EncryptsValue()
	{
		var plainTextPassword = $"SuperSecretPassword123-{Timestamp}";
		return WithCredentialAsync(new CredentialRequest(Name: $"Test Credential - {Timestamp}", Value: plainTextPassword), async created =>
		{
			var retrieved = await GetAsync(created.Id!);

			_ = retrieved.Value.Should().NotBeNullOrEmpty().And.NotBe(plainTextPassword);
			_ = retrieved.Value!.Length.Should().BeGreaterThan(plainTextPassword.Length);
		});
	}

	[Fact]
	public Task GetAllCredentials_ReturnsCredentialsWithValues()
	{
		var request = NewRequest();
		return WithCredentialAsync(request, async created =>
		{
			var result = await ThousandEyesClient.Credentials.GetAllAsync(aid: null, CancellationToken);

			var testCredential = result.Items.FirstOrDefault(c => c.Id == created.Id);
			_ = testCredential.Should().NotBeNull();
			_ = testCredential!.Name.Should().Be(request.Name);
			_ = testCredential.Value.Should().NotBeNullOrEmpty();
		});
	}
}
