using ThousandEyes.Api.Infrastructure;

namespace ThousandEyes.Api.Interfaces;

/// <summary>
/// Interface for PSA (Professional Services Automation) operations in Halo
/// </summary>
public interface IPsaApi
{
	/// <summary>
	/// Gets the Tickets API for ticket management operations
	/// </summary>
	TicketsApiWrapper Tickets { get; }

	/// <summary>
	/// Gets the TicketTypes API for ticket type operations
	/// </summary>
	TicketTypesApiWrapper TicketTypes { get; }

	/// <summary>
	/// Gets the Users API for user management operations
	/// </summary>
	UsersApiWrapper Users { get; }

	/// <summary>
	/// Gets the Clients API for client management operations
	/// </summary>
	ClientsApiWrapper Clients { get; }

	/// <summary>
	/// Gets the Assets API for asset management operations
	/// </summary>
	AssetsApiWrapper Assets { get; }

	/// <summary>
	/// Gets the Projects API for project management operations
	/// </summary>
	ProjectsApiWrapper Projects { get; }
}