using Microsoft.Extensions.DependencyInjection;
using Refit;
using ThousandEyes.Api.Implementations.Credentials;
using ThousandEyes.Api.Interfaces.Credentials;
using ThousandEyes.Api.Refit.Credentials;

namespace ThousandEyes.Api.Modules;

/// <summary>
/// Module for registering Credentials API services.
/// </summary>
internal static class CredentialsModule
{
	/// <summary>
	/// Registers all Credentials API services with the dependency injection container.
	/// </summary>
	/// <param name="services">The service collection</param>
	/// <param name="refitSettings">Refit settings to use for API clients</param>
	internal static void RegisterCredentialsServices(this IServiceCollection services, RefitSettings refitSettings)
	{
		// Register Refit API client
		services
			.AddRefitClient<ICredentialsRefitApi>(refitSettings)
			.ConfigureHttpClient(c => c.BaseAddress = new Uri("https://api.thousandeyes.com/v7"));

		// Register implementation
		services.AddScoped<ICredentials, CredentialsImpl>();
	}
}
