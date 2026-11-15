using System.Runtime.InteropServices;
using ThousandEyes.Api.Models.Permissions;

namespace ThousandEyes.Api.Interfaces;

/// <summary>Interface for Permissions API operations</summary>
public interface IPermissionsApi
{
	/// <summary>Get all assignable permissions</summary>
	Task<Permissions> GetAllAsync([Optional] string? aid, [Optional] CancellationToken cancellationToken);
}
