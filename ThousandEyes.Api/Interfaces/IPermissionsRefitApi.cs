using Refit;
using ThousandEyes.Api.Models.Permissions;

namespace ThousandEyes.Api.Interfaces;

/// <summary>Internal Refit interface for Permissions API</summary>
internal interface IPermissionsRefitApi
{
	/// <summary>Get all assignable permissions</summary>
	[Get("/permissions")]
	Task<Permissions> GetAllAsync([Query] string? aid, CancellationToken cancellationToken);
}