using ThousandEyes.Api.Interfaces;
using Microsoft.Extensions.Configuration;

namespace ThousandEyes.Api.Test;

public class IntegrationTestFixture : IDisposable
{
	public IConfiguration Configuration { get; }
	public ILogger Logger { get; }

	private IThousandEyesClient? _thousandEyesClient;

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
		var bearerToken = Configuration["ThousandEyes:BearerToken"];

		if (string.IsNullOrWhiteSpace(bearerToken))
		{
			throw new InvalidOperationException(
				"ThousandEyes:BearerToken not found in user secrets. " +
				"Please run: dotnet user-secrets set \"ThousandEyes:BearerToken\" \"your-bearer-token\" --project ThousandEyes.Api.Test");
		}

		Logger.LogInformation("User secrets validation passed - Bearer token is configured");
	}

	public IThousandEyesClient GetThousandEyesClient()
	{
		if (_thousandEyesClient == null)
		{
			var options = new ThousandEyesClientOptions
			{
				BearerToken = Configuration["ThousandEyes:BearerToken"]!,
				Logger = Logger,
				EnableRequestLogging = true,
				EnableResponseLogging = true
			};

			_thousandEyesClient = new ThousandEyesClient(options);
			Logger.LogInformation("Successfully created ThousandEyesClient with Bearer token authentication");
		}

		return _thousandEyesClient;
	}

	public void Dispose()
	{
		// Cleanup if needed
		if (_thousandEyesClient is IDisposable disposableClient)
		{
			disposableClient.Dispose();
		}

		_thousandEyesClient = null;
		Logger.LogInformation("IntegrationTestFixture disposed");
		GC.SuppressFinalize(this);
	}
}