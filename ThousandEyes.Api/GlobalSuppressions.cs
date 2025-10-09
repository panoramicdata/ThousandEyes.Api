// This file is used by Code Analysis to maintain SuppressMessage
// attributes that are applied to this project.
// Project-level suppressions either have no target or are given
// a specific target and scoped to a namespace, type, member, etc.

using System.Diagnostics.CodeAnalysis;

[assembly: SuppressMessage(
	"Performance",
	"CA1848:Use the LoggerMessage delegates",
	Justification = "implementation cost not worth the development effort"
)]

[assembly: SuppressMessage(
	"Naming",
	"CA1711:Identifiers should not have incorrect suffix",
	Justification = "Stream is the actual API resource name from ThousandEyes OpenTelemetry API",
	Scope = "type",
	Target = "~T:ThousandEyes.Api.Models.OpenTelemetry.Stream"
)]

[assembly: SuppressMessage(
	"Naming",
	"CA1711:Identifiers should not have incorrect suffix",
	Justification = "PutStream is the actual API request name from ThousandEyes OpenTelemetry API",
	Scope = "type",
	Target = "~T:ThousandEyes.Api.Models.OpenTelemetry.PutStream"
)]

[assembly: SuppressMessage(
	"Naming",
	"CA1711:Identifiers should not have incorrect suffix",
	Justification = "StreamCollection is the actual API response wrapper from ThousandEyes OpenTelemetry API",
	Scope = "type",
	Target = "~T:ThousandEyes.Api.Models.OpenTelemetry.StreamCollection"
)]
