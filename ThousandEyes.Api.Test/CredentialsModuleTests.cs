using AwesomeAssertions;
using ThousandEyes.Api.Models.Credentials;

namespace ThousandEyes.Api.Test;

/// <summary>
/// Integration tests for the Credentials API module
/// </summary>
[Collection("Integration Tests")]
public class CredentialsModuleTests(IntegrationTestFixture fixture) : TestBase(fixture)
{
	[Fact]
	public async Task GetCredentials_WithValidRequest_ReturnsCredentials()
	{
		// Act
		var result = await ThousandEyesClient.Credentials.GetAllAsync(aid: null, CancellationToken);

		// Assert
		result.Should().NotBeNull();
		result.Items.Should().NotBeNull();
	}

	[Fact]
	public async Task CreateCredential_WithValidRequest_CreatesCredential()
	{
		// Arrange
		var timestamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
		var request = new CredentialRequest(
			Name: $"Test Credential - {timestamp}",
			Value: $"test-password-{timestamp}"
		);

		try
		{
			// Act
			var result = await ThousandEyesClient.Credentials.CreateAsync(request, aid: null, CancellationToken);

			// Assert
			result.Should().NotBeNull();
			result.Id.Should().NotBeNullOrEmpty();
			result.Name.Should().Be(request.Name);

			// Cleanup
			await ThousandEyesClient.Credentials.DeleteAsync(result.Id!, aid: null, CancellationToken);
		}
		catch (Exception)
		{
			// Test failed - exception will be captured by test framework
			throw;
		}
	}

	[Fact]
	public async Task GetCredential_WithValidId_ReturnsCredentialWithValue()
	{
		// Arrange
		var timestamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
		var request = new CredentialRequest(
			Name: $"Test Credential - {timestamp}",
			Value: $"test-password-{timestamp}"
		);

		// Create a credential first
		var created = await ThousandEyesClient.Credentials.CreateAsync(request, aid: null, CancellationToken);

		try
		{
			// Act
			var result = await ThousandEyesClient.Credentials.GetByIdAsync(created.Id!, aid: null, CancellationToken);

			// Assert
			result.Should().NotBeNull();
			result.Id.Should().Be(created.Id);
			result.Name.Should().Be(request.Name);
			result.Value.Should().NotBeNullOrEmpty();
			result.Value.Should().NotBe(request.Value); // Value should be encrypted
		}
		finally
		{
			// Cleanup
			await ThousandEyesClient.Credentials.DeleteAsync(created.Id!, aid: null, CancellationToken);
		}
	}

	[Fact]
	public async Task UpdateCredential_WithValidRequest_UpdatesCredential()
	{
		// Arrange
		var timestamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
		var createRequest = new CredentialRequest(
			Name: $"Test Credential - {timestamp}",
			Value: $"test-password-{timestamp}"
		);

		// Create a credential first
		var created = await ThousandEyesClient.Credentials.CreateAsync(createRequest, aid: null, CancellationToken);

		try
		{
			// Act
			var updateRequest = new CredentialRequest(
				Name: $"Updated Test Credential - {timestamp}",
				Value: $"updated-password-{timestamp}"
			);
			var result = await ThousandEyesClient.Credentials.UpdateAsync(created.Id!, updateRequest, aid: null, CancellationToken);

			// Assert
			result.Should().NotBeNull();
			result.Id.Should().Be(created.Id);
			result.Name.Should().Be(updateRequest.Name);

			// Verify update by getting the credential
			var retrieved = await ThousandEyesClient.Credentials.GetByIdAsync(created.Id!, aid: null, CancellationToken);
			retrieved.Name.Should().Be(updateRequest.Name);
			retrieved.Value.Should().NotBe(createRequest.Value); // Value should be different
		}
		finally
		{
			// Cleanup
			await ThousandEyesClient.Credentials.DeleteAsync(created.Id!, aid: null, CancellationToken);
		}
	}

	[Fact]
	public async Task DeleteCredential_WithValidId_DeletesCredential()
	{
		// Arrange
		var timestamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
		var request = new CredentialRequest(
			Name: $"Test Credential - {timestamp}",
			Value: $"test-password-{timestamp}"
		);

		// Create a credential first
		var created = await ThousandEyesClient.Credentials.CreateAsync(request, aid: null, CancellationToken);

		// Act
		await ThousandEyesClient.Credentials.DeleteAsync(created.Id!, aid: null, CancellationToken);

		// Assert - verify deletion by attempting to get the credential
		// This should throw an exception (404 Not Found)
		var act = async () => await ThousandEyesClient.Credentials.GetByIdAsync(created.Id!, aid: null, CancellationToken);
		await act.Should().ThrowAsync<Exception>(); // Refit will throw an exception for 404
	}

	[Fact]
	public async Task CreateCredential_WithSensitiveValue_EncryptsValue()
	{
		// Arrange
		var timestamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
		var plainTextPassword = $"SuperSecretPassword123-{timestamp}";
		var request = new CredentialRequest(
			Name: $"Test Credential - {timestamp}",
			Value: plainTextPassword
		);

		// Create a credential
		var created = await ThousandEyesClient.Credentials.CreateAsync(request, aid: null, CancellationToken);

		try
		{
			// Act - retrieve the credential to get the encrypted value
			var retrieved = await ThousandEyesClient.Credentials.GetByIdAsync(created.Id!, aid: null, CancellationToken);

			// Assert
			retrieved.Value.Should().NotBeNullOrEmpty();
			retrieved.Value.Should().NotBe(plainTextPassword); // Encrypted value should differ from plain text
			retrieved.Value!.Length.Should().BeGreaterThan(plainTextPassword.Length); // Encrypted values are typically longer
		}
		finally
		{
			// Cleanup
			await ThousandEyesClient.Credentials.DeleteAsync(created.Id!, aid: null, CancellationToken);
		}
	}

	[Fact]
	public async Task GetAllCredentials_ReturnsCredentialsWithValues()
	{
		// Arrange
		var timestamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
		var request = new CredentialRequest(
			Name: $"Test Credential - {timestamp}",
			Value: $"test-password-{timestamp}"
		);

		// Create a credential
		var created = await ThousandEyesClient.Credentials.CreateAsync(request, aid: null, CancellationToken);

		try
		{
			// Act
			var result = await ThousandEyesClient.Credentials.GetAllAsync(aid: null, CancellationToken);

			// Assert
			result.Should().NotBeNull();
			result.Items.Should().NotBeEmpty();
			
			// Find our test credential
			var testCredential = result.Items.FirstOrDefault(c => c.Id == created.Id);
			testCredential.Should().NotBeNull();
			testCredential!.Name.Should().Be(request.Name);
			testCredential.Value.Should().NotBeNullOrEmpty(); // API returns values in list endpoint
		}
		finally
		{
			// Cleanup
			await ThousandEyesClient.Credentials.DeleteAsync(created.Id!, aid: null, CancellationToken);
		}
	}
}
