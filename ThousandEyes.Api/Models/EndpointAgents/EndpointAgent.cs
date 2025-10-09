using System.Text.Json.Serialization;

namespace ThousandEyes.Api.Models.EndpointAgents;

/// <summary>
/// Endpoint agent information
/// </summary>
public class EndpointAgent : ApiResource
{
	/// <summary>
	/// Unique ID of endpoint agent
	/// </summary>
	[JsonPropertyName("id")]
	public string? Id { get; set; }

	/// <summary>
	/// Account group ID
	/// </summary>
	[JsonPropertyName("aid")]
	public string? Aid { get; set; }

	/// <summary>
	/// Name of the agent
	/// </summary>
	[JsonPropertyName("name")]
	public string? Name { get; set; }

	/// <summary>
	/// Computer name
	/// </summary>
	[JsonPropertyName("computerName")]
	public string? ComputerName { get; set; }

	/// <summary>
	/// Operating system version
	/// </summary>
	[JsonPropertyName("osVersion")]
	public string? OsVersion { get; set; }

	/// <summary>
	/// Platform type
	/// </summary>
	[JsonPropertyName("platform")]
	public Platform? Platform { get; set; }

	/// <summary>
	/// Kernel version
	/// </summary>
	[JsonPropertyName("kernelVersion")]
	public string? KernelVersion { get; set; }

	/// <summary>
	/// Hardware manufacturer
	/// </summary>
	[JsonPropertyName("manufacturer")]
	public string? Manufacturer { get; set; }

	/// <summary>
	/// Hardware model
	/// </summary>
	[JsonPropertyName("model")]
	public string? Model { get; set; }

	/// <summary>
	/// Last time the agent checked in
	/// </summary>
	[JsonPropertyName("lastSeen")]
	public DateTime? LastSeen { get; set; }

	/// <summary>
	/// Status of the endpoint agent
	/// </summary>
	[JsonPropertyName("status")]
	public AgentStatus? Status { get; set; }

	/// <summary>
	/// Indicates if the agent is deleted
	/// </summary>
	[JsonPropertyName("deleted")]
	public bool? Deleted { get; set; }

	/// <summary>
	/// Version of the agent software
	/// </summary>
	[JsonPropertyName("version")]
	public string? Version { get; set; }

	/// <summary>
	/// Creation timestamp
	/// </summary>
	[JsonPropertyName("createdAt")]
	public DateTime? CreatedAt { get; set; }

	/// <summary>
	/// Number of clients (users) on this agent
	/// </summary>
	[JsonPropertyName("numberOfClients")]
	public int? NumberOfClients { get; set; }

	/// <summary>
	/// Public IP address
	/// </summary>
	[JsonPropertyName("publicIP")]
	public string? PublicIp { get; set; }

	/// <summary>
	/// Agent location information
	/// </summary>
	[JsonPropertyName("location")]
	public EndpointAgentLocation? Location { get; set; }

	/// <summary>
	/// List of clients (users) - populated when expand=clients
	/// </summary>
	[JsonPropertyName("clients")]
	public EndpointClient[] Clients { get; set; } = [];

	/// <summary>
	/// Total memory
	/// </summary>
	[JsonPropertyName("totalMemory")]
	public string? TotalMemory { get; set; }

	/// <summary>
	/// Agent type
	/// </summary>
	[JsonPropertyName("agentType")]
	public string? AgentType { get; set; }

	/// <summary>
	/// ASN details
	/// </summary>
	[JsonPropertyName("asnDetails")]
	public EndpointAsnDetails? AsnDetails { get; set; }

	/// <summary>
	/// License type
	/// </summary>
	[JsonPropertyName("licenseType")]
	public AgentLicenseType? LicenseType { get; set; }

	/// <summary>
	/// TCP driver availability status
	/// </summary>
	[JsonPropertyName("tcpDriverAvailable")]
	public bool? TcpDriverAvailable { get; set; }
}