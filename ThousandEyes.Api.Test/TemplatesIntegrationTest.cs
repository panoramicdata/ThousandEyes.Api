using AwesomeAssertions;
using Refit;
using System.Text.Json;
using ThousandEyes.Api.Exceptions;
using ThousandEyes.Api.Models.Templates;

namespace ThousandEyes.Api.Test;

[Collection("Integration Tests")]
public class TemplatesIntegrationTest(IntegrationTestFixture fixture) : TestBase(fixture)
{
	/// <summary>
	/// Checks if the Templates API is available in the current account
	/// </summary>
	private async Task<bool> IsTemplatesApiAvailableAsync()
	{
		try
		{
			await ThousandEyesClient.Templates.Templates.GetAllAsync(
				aid: null,
				certificationLevel: null,
				templateModule: null,
				name: null,
				cancellationToken: CancellationToken);
			return true;
		}
		catch (ThousandEyesBadRequestException)
		{
			return false;
		}
		catch (ThousandEyesAuthorizationException)
		{
			return false;
		}
		catch (ValidationApiException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
		{
			return false;
		}
	}

	[Fact]
	public async Task GetTemplates_WithValidRequest_ReturnsTemplates()
	{
		// Check if API is available
		if (!await IsTemplatesApiAvailableAsync())
		{
			Logger.LogWarning(
				"Templates API not available in this account - " +
				"this may require specific permissions or API version. Test skipped."
			);
			return;
		}

		// Act
		var result = await ThousandEyesClient.Templates.Templates.GetAllAsync(
			aid: null,
			certificationLevel: null,
			templateModule: null,
			name: null,
			cancellationToken: CancellationToken);

		// Assert
		_ = result.Should().NotBeNull();
		_ = result.Items.Should().NotBeNull();

		// If there are templates, verify the structure
		if (result.Items.Count > 0)
		{
			var firstTemplate = result.Items[0];
			_ = firstTemplate.Id.Should().NotBeNullOrEmpty();
			_ = firstTemplate.Name.Should().NotBeNullOrEmpty();
		}
	}

	[Fact]
	public async Task GetTemplateById_WithValidTemplateId_ReturnsTemplateDetails()
	{
		// Check if API is available
		if (!await IsTemplatesApiAvailableAsync())
		{
			Logger.LogWarning(
				"Templates API not available in this account - " +
				"this may require specific permissions or API version. Test skipped."
			);
			return;
		}

		// Arrange - First get list of templates to find a valid template ID
		var templates = await ThousandEyesClient.Templates.Templates.GetAllAsync(
			aid: null,
			certificationLevel: null,
			templateModule: null,
			name: null,
			cancellationToken: CancellationToken);

		// Skip test if no templates are available
		if (templates.Items.Count == 0)
		{
			Logger.LogInformation("No templates exist in account - test skipped.");
			return;
		}

		var templateId = templates.Items[0].Id!;

		// Act
		var result = await ThousandEyesClient.Templates.Templates.GetByIdAsync(
			templateId,
			aid: null,
			cancellationToken: CancellationToken);

		// Assert
		_ = result.Should().NotBeNull();
		_ = result.Id.Should().Be(templateId);
		_ = result.Name.Should().NotBeNullOrEmpty();
	}

	[Fact]
	public async Task CreateTemplate_WithValidRequest_CreatesTemplate()
	{
		// Check if API is available
		if (!await IsTemplatesApiAvailableAsync())
		{
			Logger.LogWarning(
				"Templates API not available in this account - " +
				"this may require specific permissions or API version. Test skipped."
			);
			return;
		}

		// Arrange
		var templateRequest = new Template
		{
			Name = $"Test Template - {DateTime.UtcNow:yyyyMMdd-HHmmss}",
			Description = "Integration test template",
			Icon = "user-template",
			CertificationLevel = CertificationLevel.User,
			UserInputs = new Dictionary<string, UserInput>
			{
				["testName"] = new UserInput
				{
					Type = UserInputType.String,
					Name = "Test Name",
					Description = "Name for the HTTP test",
					DefaultValue = new UserInputValue { Value = JsonSerializer.SerializeToElement("My HTTP Test") }
				},
				["targetUrl"] = new UserInput
				{
					Type = UserInputType.String,
					Name = "Target URL",
					Description = "URL to monitor",
					DefaultValue = new UserInputValue { Value = JsonSerializer.SerializeToElement("https://example.com") }
				}
			},
			Modules = [TemplateModule.Default],
			Groupings = [
				new TemplateGrouping
				{
					Name = "Basic Configuration",
					Title = "Basic Configuration",
					Description = "Basic test configuration",
					Type = TemplateGroupingType.UserInput,
					Items = ["testName", "targetUrl"]
				}
			]
		};

		// Act
		var result = await ThousandEyesClient.Templates.Templates.CreateAsync(
			templateRequest,
			aid: null,
			cancellationToken: CancellationToken);

		// Assert
		_ = result.Should().NotBeNull();
		_ = result.Id.Should().NotBeNullOrEmpty();
		_ = result.Name.Should().Be(templateRequest.Name);
		_ = result.CertificationLevel.Should().Be(templateRequest.CertificationLevel);

		// Cleanup - Delete the created template
		try
		{
			await ThousandEyesClient.Templates.Templates.DeleteAsync(
				result.Id!,
				aid: null,
				cancellationToken: CancellationToken);
		}
		catch
		{
			// Ignore cleanup errors
		}
	}

	[Fact]
	public async Task DeleteTemplate_WithValidId_DeletesTemplate()
	{
		// Check if API is available
		if (!await IsTemplatesApiAvailableAsync())
		{
			Logger.LogWarning(
				"Templates API not available in this account - " +
				"this may require specific permissions or API version. Test skipped."
			);
			return;
		}

		// Arrange - Create a template first
		var templateRequest = new Template
		{
			Name = $"Test Template for Deletion - {DateTime.UtcNow:yyyyMMdd-HHmmss}",
			Description = "Template for deletion testing",
			Icon = "user-template",
			CertificationLevel = CertificationLevel.User,
			Modules = [TemplateModule.Default]
		};

		var createdTemplate = await ThousandEyesClient.Templates.Templates.CreateAsync(
			templateRequest,
			aid: null,
			cancellationToken: CancellationToken);

		// Act & Assert - Delete should not throw
		await ThousandEyesClient.Templates.Templates.DeleteAsync(
			createdTemplate.Id!,
			aid: null,
			cancellationToken: CancellationToken);

		// Verify deletion by trying to get the template (should throw 404)
		try
		{
			_ = await ThousandEyesClient.Templates.Templates.GetByIdAsync(
				createdTemplate.Id!,
				aid: null,
				cancellationToken: CancellationToken);

			// If we get here, the template wasn't deleted
			throw new InvalidOperationException("Template should have been deleted");
		}
		catch (ValidationApiException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
		{
			// This is expected - template was successfully deleted
		}
	}

	[Fact]
	public async Task GetSharingSettings_WithValidId_ReturnsSettings()
	{
		// Check if API is available
		if (!await IsTemplatesApiAvailableAsync())
		{
			Logger.LogWarning(
				"Templates API not available in this account - " +
				"this may require specific permissions or API version. Test skipped."
			);
			return;
		}

		// Arrange - First get list of templates to find a valid template ID
		var templates = await ThousandEyesClient.Templates.Templates.GetAllAsync(
			aid: null,
			certificationLevel: null,
			templateModule: null,
			name: null,
			cancellationToken: CancellationToken);

		// Skip test if no templates are available
		if (templates.Items.Count == 0)
		{
			Logger.LogInformation("No templates exist in account - test skipped.");
			return;
		}

		var templateId = templates.Items[0].Id!;

		// Act
		var result = await ThousandEyesClient.Templates.Templates.GetSharingSettingsAsync(
			templateId,
			aid: null,
			cancellationToken: CancellationToken);

		// Assert
		_ = result.Should().NotBeNull();
		_ = result.Scope.Should().BeDefined();
	}
}