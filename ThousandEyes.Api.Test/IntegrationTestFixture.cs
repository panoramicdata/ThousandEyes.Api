using ThousandEyes.Api.Interfaces;
using Microsoft.Extensions.Configuration;

namespace ThousandEyes.Api.Test;

public class IntegrationTestFixture : IDisposable
{
	public IConfiguration Configuration { get; }
	public ILogger Logger { get; }

	private IThousandEyesClient? _ThousandEyesClient;

	public IntegrationTestFixture()
	{
		// Build configuration to read from user secrets
		Configuration = new ConfigurationBuilder()
			.AddUserSecrets<IntegrationTestFixture>()
			.Build();

		// Create logger factory and logger
		var loggerFactory = LoggerFactory.Create(builder =>
			builder
				.SetMinimumLevel(LogLevel.Information)
		);

		Logger = loggerFactory.CreateLogger<IntegrationTestFixture>();

		// Validate user secrets are present immediately
		ValidateUserSecrets();
	}

	private void ValidateUserSecrets()
	{
		var haloAccount = Configuration["HaloApi:Account"];
		var ThousandEyesClientId = Configuration["HaloApi:ClientId"];
		var ThousandEyesClientSecret = Configuration["HaloApi:ClientSecret"];

		if (string.IsNullOrWhiteSpace(haloAccount))
		{
			throw new InvalidOperationException(
				"HaloApi:Account not found in user secrets. " +
				"Please run: dotnet user-secrets set \"HaloApi:Account\" \"your-account-name\" --project Halo.Api.Test");
		}

		if (string.IsNullOrWhiteSpace(ThousandEyesClientId))
		{
			throw new InvalidOperationException(
				"HaloApi:ClientId not found in user secrets. " +
				"Please run: dotnet user-secrets set \"HaloApi:ClientId\" \"your-client-id\" --project Halo.Api.Test");
		}

		if (string.IsNullOrWhiteSpace(ThousandEyesClientSecret))
		{
			throw new InvalidOperationException(
				"HaloApi:ClientSecret not found in user secrets. " +
				"Please run: dotnet user-secrets set \"HaloApi:ClientSecret\" \"your-client-secret\" --project Halo.Api.Test");
		}

		Logger.LogInformation("User secrets validation passed for account: {Account}", haloAccount);
	}

	public IThousandEyesClient GetThousandEyesClient()
	{
		if (_ThousandEyesClient == null)
		{
			var options = new ThousandEyesClientOptions
			{
				Account = Configuration["HaloApi:Account"]!,
				ClientId = Configuration["HaloApi:ClientId"]!,
				ClientSecret = Configuration["HaloApi:ClientSecret"]!,
				Logger = Logger,
				EnableRequestLogging = true,
				EnableResponseLogging = true
			};

			_ThousandEyesClient = new ThousandEyesClient(options);
			Logger.LogInformation("Successfully created ThousandEyesClient for account: {Account}", options.Account);
		}

		return _ThousandEyesClient;
	}

	public void Dispose()
	{
		// Cleanup if needed
		if (_ThousandEyesClient is IDisposable disposableClient)
		{
			disposableClient.Dispose();
		}

		_ThousandEyesClient = null;
		Logger.LogInformation("IntegrationTestFixture disposed");
		GC.SuppressFinalize(this);
	}
}