using System.Runtime.CompilerServices;

// Allow test project to access internal types
[assembly: InternalsVisibleTo("ThousandEyes.Api.Test")]

// Allow Moq/Castle.Core to create proxies for internal interfaces
[assembly: InternalsVisibleTo("DynamicProxyGenAssembly2")]
