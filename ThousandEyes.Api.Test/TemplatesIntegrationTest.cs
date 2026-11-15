using AwesomeAssertions;
using System.Text.Json;
using ThousandEyes.Api.Models.Templates;

namespace ThousandEyes.Api.Test;

[Collection("Integration Tests")]
[Trait("Category", "Integration")]
public class TemplatesIntegrationTest(IntegrationTestFixture fixture) : TestBase(fixture)
{
	private Task<Models.Templates.Templates> ListAsync() =>
		ThousandEyesClient.Templates.Templates.GetAllAsync(
			aid: null,
			certificationLevel: null,
			templateModule: null,
			name: null,
			cancellationToken: CancellationToken);

	private Task RunWhenAvailableAsync(Func<Task> body) => RunWhenAvailableAsync("Templates", ListAsync, body);

	/// <summary>
	/// Runs the body against the first template in the account, or logs and returns when there is none
	/// </summary>
	private async Task WithFirstTemplateAsync(Func<string, Task> body)
	{
		var templates = await ListAsync();
		if (templates.Items.Count == 0)
		{
			Logger.LogInformation("No templates exist in account - test skipped.");
			return;
		}

		await body(templates.Items[0].Id!);
	}

	private static UserInput StringInput(string name, string description, string defaultValue) => new()
	{
		Type = UserInputType.String,
		Name = name,
		Description = description,
		DefaultValue = new UserInputValue { Value = JsonSerializer.SerializeToElement(defaultValue) }
	};

	private static Template NewTemplate(string name, string description) => new()
	{
		Name = $"{name} - {DateTime.UtcNow:yyyyMMdd-HHmmss}",
		Description = description,
		Icon = "user-template",
		CertificationLevel = CertificationLevel.User,
		Modules = [TemplateModule.Default]
	};

	[Fact]
	public Task GetTemplates_WithValidRequest_ReturnsTemplates() => RunWhenAvailableAsync(async () =>
	{
		var result = await ListAsync();

		_ = result.Should().NotBeNull();
		_ = result.Items.Should().NotBeNull();
		if (result.Items.Count > 0)
		{
			_ = result.Items[0].Id.Should().NotBeNullOrEmpty();
			_ = result.Items[0].Name.Should().NotBeNullOrEmpty();
		}
	});

	[Fact]
	public Task GetTemplateById_WithValidTemplateId_ReturnsTemplateDetails() => RunWhenAvailableAsync(() =>
		WithFirstTemplateAsync(async templateId =>
		{
			var result = await ThousandEyesClient.Templates.Templates.GetByIdAsync(templateId, aid: null, cancellationToken: CancellationToken);

			_ = result.Should().NotBeNull();
			_ = result.Id.Should().Be(templateId);
			_ = result.Name.Should().NotBeNullOrEmpty();
		}));

	[Fact]
	public Task CreateTemplate_WithValidRequest_CreatesTemplate() => RunWhenAvailableAsync(async () =>
	{
		var templateRequest = NewTemplate("Test Template", "Integration test template");
		templateRequest.UserInputs = new Dictionary<string, UserInput>
		{
			["testName"] = StringInput("Test Name", "Name for the HTTP test", "My HTTP Test"),
			["targetUrl"] = StringInput("Target URL", "URL to monitor", "https://example.com")
		};
		templateRequest.Groupings =
		[
			new TemplateGrouping
			{
				Name = "Basic Configuration",
				Title = "Basic Configuration",
				Description = "Basic test configuration",
				Type = TemplateGroupingType.UserInput,
				Items = ["testName", "targetUrl"]
			}
		];

		var result = await ThousandEyesClient.Templates.Templates.CreateAsync(templateRequest, aid: null, cancellationToken: CancellationToken);

		_ = result.Should().NotBeNull();
		_ = result.Id.Should().NotBeNullOrEmpty();
		_ = result.Name.Should().Be(templateRequest.Name);
		_ = result.CertificationLevel.Should().Be(templateRequest.CertificationLevel);

		await IgnoreErrorsAsync(() =>
			ThousandEyesClient.Templates.Templates.DeleteAsync(result.Id!, aid: null, cancellationToken: CancellationToken));
	});

	[Fact]
	public Task DeleteTemplate_WithValidId_DeletesTemplate() => RunWhenAvailableAsync(async () =>
	{
		var createdTemplate = await ThousandEyesClient.Templates.Templates.CreateAsync(
			NewTemplate("Test Template for Deletion", "Template for deletion testing"),
			aid: null,
			cancellationToken: CancellationToken);

		await ThousandEyesClient.Templates.Templates.DeleteAsync(createdTemplate.Id!, aid: null, cancellationToken: CancellationToken);

		await RunIfAvailableAsync(async () =>
		{
			_ = await ThousandEyesClient.Templates.Templates.GetByIdAsync(createdTemplate.Id!, aid: null, cancellationToken: CancellationToken);
			throw new InvalidOperationException("Template should have been deleted");
		});
	});

	[Fact]
	public Task GetSharingSettings_WithValidId_ReturnsSettings() => RunWhenAvailableAsync(() =>
		WithFirstTemplateAsync(async templateId =>
		{
			var result = await ThousandEyesClient.Templates.Templates.GetSharingSettingsAsync(templateId, aid: null, cancellationToken: CancellationToken);

			_ = result.Should().NotBeNull();
			_ = result.Scope.Should().BeDefined();
		}));
}
