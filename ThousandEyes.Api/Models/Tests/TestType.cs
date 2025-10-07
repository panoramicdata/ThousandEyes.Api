namespace ThousandEyes.Api.Models.Tests;

/// <summary>
/// Test types supported by ThousandEyes
/// </summary>
public enum TestType
{
	/// <summary>
	/// HTTP Server test
	/// </summary>
	HttpServer,

	/// <summary>
	/// Page Load test
	/// </summary>
	PageLoad,

	/// <summary>
	/// Web Transaction test
	/// </summary>
	WebTransaction,

	/// <summary>
	/// Agent to Server test
	/// </summary>
	AgentToServer,

	/// <summary>
	/// Agent to Agent test
	/// </summary>
	AgentToAgent,

	/// <summary>
	/// DNS Server test
	/// </summary>
	DnsServer,

	/// <summary>
	/// DNS Trace test
	/// </summary>
	DnsTrace,

	/// <summary>
	/// DNSSEC test
	/// </summary>
	DnsSec,

	/// <summary>
	/// BGP test
	/// </summary>
	Bgp,

	/// <summary>
	/// API test
	/// </summary>
	Api,

	/// <summary>
	/// FTP Server test
	/// </summary>
	FtpServer,

	/// <summary>
	/// SIP Server test
	/// </summary>
	SipServer,

	/// <summary>
	/// Voice test
	/// </summary>
	Voice
}