using Microsoft.Extensions.Logging.Abstractions;
using Refit;
using ThousandEyes.Api.Interfaces;

namespace ThousandEyes.Api.Modules;

/// <summary>
/// Account Management API module providing access to administrative functionality
/// </summary>
public class AccountManagementModule
{
	/// <summary>
	/// Initializes a new instance of the AccountManagementModule
	/// </summary>
	/// <param name="httpClient">The HTTP client to use for API calls</param>
	/// <param name="refitSettings">Refit settings for JSON serialization</param>
	public AccountManagementModule(HttpClient httpClient, RefitSettings refitSettings)
	{
		// Initialize Account Management APIs using Refit
		var accountGroupsRefitApi = RestService.For<IAccountGroupsRefitApi>(httpClient, refitSettings);
		AccountGroups = new AccountGroupsApi(accountGroupsRefitApi);

		var usersRefitApi = RestService.For<IUsersRefitApi>(httpClient, refitSettings);
		Users = new UsersApi(usersRefitApi);

		var rolesRefitApi = RestService.For<IRolesRefitApi>(httpClient, refitSettings);
		Roles = new RolesApi(rolesRefitApi);

		var permissionsRefitApi = RestService.For<IPermissionsRefitApi>(httpClient, refitSettings);
		Permissions = new PermissionsApi(permissionsRefitApi);

		var userEventsRefitApi = RestService.For<IUserEventsRefitApi>(httpClient, refitSettings);
		UserEvents = new UserEventsApi(userEventsRefitApi);
	}

	/// <summary>
	/// Gets the Account Groups API for managing organizational structure
	/// </summary>
	public IAccountGroupsApi AccountGroups { get; }

	/// <summary>
	/// Gets the Users API for user management
	/// </summary>
	public IUsersApi Users { get; }

	/// <summary>
	/// Gets the Roles API for role and permission management
	/// </summary>
	public IRolesApi Roles { get; }

	/// <summary>
	/// Gets the Permissions API for retrieving assignable permissions
	/// </summary>
	public IPermissionsApi Permissions { get; }

	/// <summary>
	/// Gets the User Events API for audit logs and activity tracking
	/// </summary>
	public IUserEventsApi UserEvents { get; }
}