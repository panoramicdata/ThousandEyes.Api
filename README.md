# ThousandEyes API .NET Library

[![NuGet Version](https://img.shields.io/nuget/v/ThousandEyes.Api)](https://www.nuget.org/packages/ThousandEyes.Api)
[![NuGet Downloads](https://img.shields.io/nuget/dt/ThousandEyes.Api)](https://www.nuget.org/packages/ThousandEyes.Api)
[![Build Status](https://img.shields.io/github/actions/workflow/status/panoramicdata/ThousandEyes.Api/publish-nuget.yml)](https://github.com/panoramicdata/ThousandEyes.Api/actions)
[![Codacy Badge](https://app.codacy.com/project/badge/Grade/a6c135d1c93d4d818e770f149385a149)](https://app.codacy.com/gh/panoramicdata/ThousandEyes.Api/dashboard?utm_source=gh&utm_medium=referral&utm_content=&utm_campaign=Badge_grade)
[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](https://opensource.org/licenses/MIT)

A comprehensive, modern .NET library for interacting with the [ThousandEyes Administrative API v7.0.63](https://api.thousandeyes.com/v7). This library provides full coverage of the ThousandEyes Administrative API with a clean, intuitive interface using modern C# patterns and best practices.

## 📚 Official Documentation

> **🎯 For the definitive and most up-to-date API documentation, always refer to:**  
> **[https://developer.cisco.com/docs/thousandeyes/overview/](https://developer.cisco.com/docs/thousandeyes/overview/)**

### Additional Resources
- **Administrative API Reference**: [ThousandEyes API v7](https://api.thousandeyes.com/v7)
- **Authentication Guide**: [Bearer Token Authentication](https://docs.thousandeyes.com/product-documentation/api/authentication)
- **ThousandEyes Official Site**: [thousandeyes.com](https://www.thousandeyes.com/)

## Features

- 🎯 **Complete API Coverage** - Full support for all ThousandEyes Administrative API v7.0.63 endpoints
- 🚀 **Modern .NET 9** - Built with primary constructors, collection expressions, and latest C# features
- 🏛️ **Refit-Powered** - Uses Refit for type-safe, declarative HTTP API definitions
- 🔒 **Type Safety** - Strongly typed models and responses with comprehensive validation
- 📝 **Comprehensive Logging** - Built-in logging and request/response interception
- 🔄 **Retry Logic** - Automatic retry with exponential backoff for resilient operations
- 📖 **Rich Documentation** - IntelliSense-friendly XML documentation
- ✅ **100% Test Coverage** - Comprehensive unit and integration tests with zero tolerance for failing tests
- ⚡ **High Performance** - Optimized for efficiency and low memory usage
- 🔐 **Bearer Token Authentication** - Simple, secure token-based authentication
- 🏗️ **Modular Architecture** - Organized API modules matching ThousandEyes structure

## Installation

Install the package via NuGet Package Manager:

```bash
dotnet add package ThousandEyes.Api
```

Or via Package Manager Console:

```powershell
Install-Package ThousandEyes.Api
```

## Quick Start

### 1. Authentication Setup

ThousandEyes API uses **Bearer Token authentication**. You'll need:

1. **Bearer Token** - Your API token from the ThousandEyes platform

#### Obtaining API Credentials in ThousandEyes
1. Log into your ThousandEyes platform
2. Navigate to **Account Settings** → **Users and Roles** → **API Tokens**
3. Click **Add New Token** to create a new API token
4. Configure the token settings:
   - **Token Name**: Your application name
   - **Permissions**: Select appropriate permissions for your use case (Account management, User management, etc.)
5. Save the token to generate your **Bearer Token**
6. **Important**: Store the token securely - it won't be displayed again

```csharp
using ThousandEyes.Api;

// Modern .NET 9 approach with primary constructor
var options = new ThousandEyesClientOptions
{
    BearerToken = "your-bearer-token"
};

using var client = new ThousandEyesClient(options);
```

### 2. Basic Usage Examples

#### Working with Account Groups

```csharp
// Use a CancellationToken for all async operations
using var cts = new CancellationTokenSource();
var cancellationToken = cts.Token;

// Get all account groups
var accountGroups = await client.AccountManagement.AccountGroups.GetAllAsync(cancellationToken);

foreach (var accountGroup in accountGroups.AccountGroupsList)
{
    Console.WriteLine($"Account Group: {accountGroup.AccountGroupName} (ID: {accountGroup.Aid})");
}

// Get a specific account group with details
var accountGroup = await client.AccountManagement.AccountGroups.GetByIdAsync("1234", cancellationToken);
Console.WriteLine($"Account Group: {accountGroup.AccountGroupName}");
Console.WriteLine($"Organization: {accountGroup.OrganizationName}");

// Create a new account group
var newAccountGroup = new AccountGroupRequest
{
    AccountGroupName = "New Account Group",
    Agents = ["105", "719"] // Enterprise agent IDs to assign
};

var createdAccountGroup = await client.AccountManagement.AccountGroups.CreateAsync(newAccountGroup, cancellationToken);
Console.WriteLine($"Created account group with ID: {createdAccountGroup.Aid}");
```

#### Working with Users

```csharp
// Get all users
var users = await client.AccountManagement.Users.GetAllAsync(cancellationToken);

foreach (var user in users.UsersList)
{
    Console.WriteLine($"User: {user.Name} ({user.Email})");
    Console.WriteLine($"Last Login: {user.LastLogin}");
}

// Get current user details (no ID required)
var currentUser = await client.AccountManagement.Users.GetCurrentAsync(cancellationToken);
Console.WriteLine($"Current User: {currentUser.Name} ({currentUser.Email})");

// Get user details by ID
var user = await client.AccountManagement.Users.GetByIdAsync("123", cancellationToken);
Console.WriteLine($"User: {user.Name} ({user.Email})");
Console.WriteLine($"Login Account Group: {user.LoginAccountGroup?.AccountGroupName}");

// Create a new user
var newUser = new UserRequest
{
    Name = "Jane Smith",
    Email = "jane.smith@example.com",
    LoginAccountGroupId = "691",
    AccountGroupRoles = 
    [
        new UserAccountGroupRole
        {
            AccountGroupId = "315",
            RoleIds = ["57", "1140"]
        }
    ]
};

var createdUser = await client.AccountManagement.Users.CreateAsync(newUser, cancellationToken);
Console.WriteLine($"Created user with ID: {createdUser.Uid}");
```

#### Working with Roles and Permissions

```csharp
// Get all available roles
var roles = await client.AccountManagement.Roles.GetAllAsync(cancellationToken);

foreach (var role in roles.RolesList)
{
    Console.WriteLine($"Role: {role.Name} (ID: {role.RoleId})");
    Console.WriteLine($"Built-in: {role.IsBuiltin}");
    Console.WriteLine($"Management Permissions: {role.HasManagementPermissions}");
}

// Get all assignable permissions
var permissions = await client.AccountManagement.Permissions.GetAllAsync(cancellationToken);

foreach (var permission in permissions.PermissionsList)
{
    Console.WriteLine($"Permission: {permission.Label}");
    Console.WriteLine($"Management Permission: {permission.IsManagementPermission}");
}

// Create a custom role
var newRole = new RoleRequestBody
{
    Name = "Custom API Role",
    Permissions = ["56", "315"] // Permission IDs from /permissions endpoint
};

var createdRole = await client.AccountManagement.Roles.CreateAsync(newRole, cancellationToken);
Console.WriteLine($"Created role with ID: {createdRole.RoleId}");
```

#### Working with User Events (Audit Logs)

```csharp
// Get recent user events with time window
var events = await client.AccountManagement.UserEvents.GetAllAsync(
    window: "24h", // Last 24 hours
    cancellationToken: cancellationToken);

foreach (var userEvent in events.AuditEvents)
{
    Console.WriteLine($"Event: {userEvent.Event}");
    Console.WriteLine($"User: {userEvent.User}");
    Console.WriteLine($"Date: {userEvent.Date}");
    Console.WriteLine($"IP Address: {userEvent.IpAddress}");
    
    if (userEvent.Resources?.Length > 0)
    {
        foreach (var resource in userEvent.Resources)
        {
            Console.WriteLine($"  Resource: {resource.Name} ({resource.Type})");
        }
    }
}

// Get events for a specific date range (method overload)
var startDate = DateTime.UtcNow.AddDays(-7);
var endDate = DateTime.UtcNow;

var dateRangeEvents = await client.AccountManagement.UserEvents.GetAllAsync(
    startDate,
    endDate,
    cancellationToken: cancellationToken);
```

### 3. API Module Overview

The ThousandEyes API is organized into logical modules that match the official ThousandEyes API structure:

#### ✅ Account Management (Phase 1 - COMPLETED)
```csharp
// Access administrative functionality
client.AccountManagement.AccountGroups   // Account group management
client.AccountManagement.Users          // User management
client.AccountManagement.Roles          // Role management
client.AccountManagement.Permissions    // Permission queries
client.AccountManagement.UserEvents     // Audit logs
```

#### 🚧 Core Monitoring (Phase 2 - PLANNED)
```csharp
// Test management and monitoring data (coming in Phase 2)
client.Tests         // Test configuration and management
client.Agents        // Agent management
client.TestResults   // Monitoring data retrieval
```

#### 🚧 Advanced Features (Phase 3+ - PLANNED)
```csharp
// Advanced monitoring capabilities (coming in future phases)
client.Alerts        // Alert management
client.Dashboards    // Reporting and visualization
client.Snapshots     // Data preservation
client.BgpMonitors   // BGP monitoring
// Additional modules: InternetInsights, EventDetection, etc.
```

### 4. Advanced Configuration

#### Custom HTTP Configuration

```csharp
var options = new ThousandEyesClientOptions
{
    BearerToken = "your-bearer-token",
    
    // Custom timeout
    RequestTimeout = TimeSpan.FromSeconds(30),
    
    // Custom retry policy with exponential backoff
    MaxRetryAttempts = 3,
    RetryDelay = TimeSpan.FromSeconds(1),
    UseExponentialBackoff = true,
    MaxRetryDelay = TimeSpan.FromSeconds(30)
};

using var client = new ThousandEyesClient(options);
```

#### Logging Configuration

```csharp
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;

// Create a service collection with logging
var services = new ServiceCollection();
services.AddLogging(builder => builder.AddConsole().SetMinimumLevel(LogLevel.Debug));

using var serviceProvider = services.BuildServiceProvider();
var logger = serviceProvider.GetRequiredService<ILogger<ThousandEyesClient>>();

var options = new ThousandEyesClientOptions
{
    BearerToken = "your-bearer-token",
    Logger = logger,
    EnableRequestLogging = true,
    EnableResponseLogging = true
};

using var client = new ThousandEyesClient(options);
```

### 5. Error Handling

```csharp
try
{
    var user = await client.AccountManagement.Users.GetByIdAsync("99999", cancellationToken);
}
catch (ThousandEyesNotFoundException ex)
{
    Console.WriteLine($"User not found: {ex.Message}");
    Console.WriteLine($"Resource Type: {ex.ResourceType}");
    Console.WriteLine($"Resource ID: {ex.ResourceId}");
}
catch (ThousandEyesAuthenticationException ex)
{
    Console.WriteLine($"Authentication failed: {ex.Message}");
    Console.WriteLine("Check your bearer token and permissions");
}
catch (ThousandEyesRateLimitException ex)
{
    Console.WriteLine($"Rate limit exceeded: {ex.Message}");
    Console.WriteLine($"Retry after: {ex.RetryAfterSeconds} seconds");
}
catch (ThousandEyesBadRequestException ex)
{
    Console.WriteLine($"Bad request: {ex.Message}");
    if (ex.ValidationErrors?.Count > 0)
    {
        Console.WriteLine("Validation errors:");
        foreach (var error in ex.ValidationErrors)
        {
            Console.WriteLine($"  - {error}");
        }
    }
}
catch (ThousandEyesApiException ex)
{
    Console.WriteLine($"API error: {ex.Message}");
    Console.WriteLine($"Status code: {ex.StatusCode}");
    Console.WriteLine($"Request URL: {ex.RequestUrl}");
}
```

## API Coverage

This library provides comprehensive coverage of the ThousandEyes API ecosystem, organized into logical modules. 

> **📖 For complete and up-to-date API endpoint documentation, always refer to:**  
> **[https://developer.cisco.com/docs/thousandeyes/overview/](https://developer.cisco.com/docs/thousandeyes/overview/)**

### Available API Modules

| Module | Status | Description |
|--------|--------|-------------|
| **AccountManagement** | ✅ **Completed** | Account groups, users, roles, permissions, audit logs |
| **Tests** | 🚧 Phase 2 | Test configuration and management for all test types |
| **Agents** | 🚧 Phase 2 | Cloud and Enterprise agent management |
| **TestResults** | 🚧 Phase 2 | Monitoring data retrieval and metrics |
| **Alerts** | 🚧 Phase 3 | Alert rules, notifications, and management |
| **Dashboards** | 🚧 Phase 3 | Reporting and data visualization |
| **Snapshots** | 🚧 Phase 3 | Data preservation and sharing |
| **BgpMonitors** | 🚧 Phase 4 | BGP monitoring and route analysis |

### Administrative Module (✅ Completed)

| Endpoint | Operations | Description |
|----------|------------|-------------|
| `/account-groups` | GET, POST | List and create account groups |
| `/account-groups/{id}` | GET, PUT, DELETE | Manage specific account groups |
| `/users` | GET, POST | List and create users |
| `/users/{id}` | GET, PUT, DELETE | Manage specific users |
| `/users/current` | GET | Get current user details |
| `/roles` | GET, POST | List and create roles |
| `/roles/{id}` | GET, PUT, DELETE | Manage specific roles |
| `/permissions` | GET | List assignable permissions |
| `/audit-user-events` | GET | Retrieve activity log events |

## Architecture

This library is built using modern .NET 9 patterns and follows industry best practices:

- **Modular Design** - API endpoints organized into logical modules matching ThousandEyes structure
- **Refit-Powered APIs** - Uses Refit for type-safe, declarative HTTP API definitions
- **Primary Constructors** - Modern C# syntax throughout
- **Collection Expressions** - Uses `[]` syntax for cleaner code
- **Required Properties** - Enforces mandatory configuration at compile time
- **Comprehensive Exception Handling** - Specific exception types for different API error scenarios
- **Handler Chain Pattern** - Composable HTTP handlers for authentication, retry, logging, and error handling
- **Zero Warnings Policy** - All code compiles without warnings
- **100% Test Success Rate** - Comprehensive testing with no tolerance for failing tests

## Development Roadmap

For a complete implementation roadmap covering all ThousandEyes API modules, see [Implementation Plan](Specification/ImplementationPlan.md).

### Current Status: Phase 1 ✅ COMPLETED
- **Account Management**: Full administrative API coverage

### Next: Phase 2 (3-4 weeks)
- **Tests API**: Complete test management functionality
- **Agents API**: Cloud and Enterprise agent operations  
- **Test Results API**: Monitoring data retrieval

### Future Phases (Phase 3+)
- Advanced monitoring APIs (Alerts, Dashboards, Snapshots)
- Specialized monitoring (BGP Monitors, Internet Insights, Event Detection)
- Integration APIs (Integrations, Credentials, Usage)
- Specialized features (Emulation, Endpoint Agents, Tags, Templates)

## Contributing

We welcome contributions from the community! Here's how you can help:

### Development Setup

1. **Clone the repository**:
   ```bash
   git clone https://github.com/panoramicdata/ThousandEyes.Api.git
   cd ThousandEyes.Api
   ```

2. **Install .NET 9 SDK**:
   Download from [dotnet.microsoft.com](https://dotnet.microsoft.com/download)

3. **Set up User Secrets for testing**:
   ```bash
   cd ThousandEyes.Api.Test
   dotnet user-secrets init
   dotnet user-secrets set "ThousandEyes:BearerToken" "your-test-bearer-token"
   ```

4. **Build and test**:
   ```bash
   dotnet build
   dotnet test
   ```

### Coding Standards

- **Follow the project's coding standards** as defined in `copilot-instructions.md`
- **Use modern C# patterns** (primary constructors, collection expressions, required properties)
- **Maintain zero warnings policy** - all code must compile without warnings
- **100% test success rate** - all tests must pass before code is considered complete
- **Write comprehensive tests** - both unit and integration tests required
- **Document public APIs** - use XML documentation comments
- **Always use explicit CancellationTokens** - no optional parameters for async methods

### Pull Request Process

1. **Fork the repository** and create a feature branch
2. **Follow the implementation plan** in `Specification/ImplementationPlan.md`
3. **Write tests first** - TDD approach preferred
4. **Ensure 100% test success rate** including integration tests
5. **Update documentation** as needed
6. **Submit a pull request** with a clear description of changes

### Issue Reporting

When reporting issues:

- **Use the issue templates** provided in the repository
- **Include minimal reproduction code** when possible
- **Specify the library version** and .NET version
- **Include relevant error messages** and stack traces

## Support

- **Official API Documentation**: [https://developer.cisco.com/docs/thousandeyes/overview/](https://developer.cisco.com/docs/thousandeyes/overview/)
- **GitHub Issues**: [Report Issues](https://github.com/panoramicdata/ThousandEyes.Api/issues)
- **GitHub Discussions**: [Community Support](https://github.com/panoramicdata/ThousandEyes.Api/discussions)
- **ThousandEyes Support**: Contact ThousandEyes support for API access and account issues

## License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

## Copyright

Copyright © 2025 Panoramic Data Limited. All rights reserved.

## Changelog

See [CHANGELOG.md](CHANGELOG.md) for a detailed history of changes and releases
