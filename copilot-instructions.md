# GitHub Copilot Instructions

## Code Style Guidelines

### Zero Warnings Policy
- **Always aim for zero warnings and messages on every build**
- Address all compiler warnings before considering code complete
- Use `#pragma warning disable` only in exceptional cases with clear justification
- Enable "Treat warnings as errors" where appropriate

### Modern C# Practices (.NET 9)

#### Primary Constructors
- **Use primary constructors where possible** for cleaner, more concise code:
```csharp
// Preferred
public class ThousandEyesClient(ThousandEyesClientOptions options) : IThousandEyesClient
{
    public string BearerToken => options.BearerToken;
}

// Instead of
public class ThousandEyesClient : IThousandEyesClient
{
    private readonly ThousandEyesClientOptions _options;
    
    public ThousandEyesClient(ThousandEyesClientOptions options)
    {
        _options = options;
    }
    
    public string BearerToken => _options.BearerToken;
}
```

#### Collection Initialization
- **Use collection expressions `[]` instead of `new List<T>()`**:
```csharp
// Preferred
var items = [item1, item2, item3];
var emptyList = <string>[];

// Instead of
var items = new List<string> { item1, item2, item3 };
var emptyList = new List<string>();
```

#### Required Properties
- Use `required` keyword for mandatory properties:
```csharp
public class ThousandEyesClientOptions
{
    public required string BearerToken { get; init; }
}
```

#### File-Scoped Namespaces
- Always use file-scoped namespaces:
```csharp
// Preferred
namespace ThousandEyes.Api;

public class ThousandEyesClient
{
}

// Instead of
namespace ThousandEyes.Api
{
    public class ThousandEyesClient
    {
    }
}
```

#### String Interpolation
- Use string interpolation over concatenation:
```csharp
// Preferred
var message = $"Error in {methodName}: {errorDetails}";

// Instead of
var message = "Error in " + methodName + ": " + errorDetails;
```

#### Pattern Matching
- Use modern pattern matching features:
```csharp
// Preferred
var result = value switch
{
    null => "null value",
    string s when s.Length > 0 => $"String: {s}",
    _ => "other"
};

// Use is patterns for type checks
if (obj is ThousandEyesClient client && client.IsValid)
{
    // Process client
}
```

#### Null Handling
- Use null-conditional operators and null-coalescing:
```csharp
// Preferred
var result = client?.GetData()?.FirstOrDefault() ?? defaultValue;

// Use ArgumentNullException.ThrowIfNull
ArgumentNullException.ThrowIfNull(parameter);
```

#### Expression-Bodied Members
- Use expression-bodied members for simple operations:
```csharp
// Preferred
public string FullName => $"{FirstName} {LastName}";
public void LogError(string message) => _logger.LogError(message);
```

#### Records
- Use records for immutable data structures:
```csharp
public record ThousandEyesApiResponse(string Data, int StatusCode, DateTime Timestamp);
```

#### Async/Await and CancellationTokens

##### Mandatory CancellationTokens
- **ALWAYS include CancellationToken parameters** in async methods - no exceptions
- **Never use optional CancellationToken parameters** - this avoids developer confusion at the expense of verbosity
- **Always pass CancellationTokens through the call chain**

```csharp
// Preferred - Explicit CancellationToken required
public async Task<UserDetail> GetByIdAsync(string id, CancellationToken cancellationToken)
{
    // Implementation
}

// Call site - forces developer to be explicit
var user = await client.Users.GetByIdAsync("123", cancellationToken);

// Instead of - confusing optional parameter
public async Task<UserDetail> GetByIdAsync(string id, CancellationToken cancellationToken = default)
{
    // Implementation
}
```

##### Query Parameters
- **Avoid optional query parameters in API methods** - be explicit about all parameters
- **Use overloads instead of optional parameters** when you need simplified versions

```csharp
// Preferred - All parameters explicit
[Get("/users/{id}")]
Task<UserDetail> GetByIdAsync(string id, [Query] string? aid, CancellationToken cancellationToken);

// If you need a simpler version, use overloads
public async Task<UserDetail> GetByIdAsync(string id, CancellationToken cancellationToken)
{
    return await GetByIdAsync(id, aid: null, cancellationToken);
}

// Instead of - confusing optional behavior
[Get("/users/{id}")]
Task<UserDetail> GetByIdAsync(string id, [Query] string? aid = null, CancellationToken cancellationToken = default);
```

### Code Organization

#### Using Statements
- Place using statements outside namespace (file-scoped)
- Group and sort using statements
- Remove unused using statements

#### Access Modifiers
- Always be explicit about access modifiers
- Use `internal` for implementation details not meant for public API

#### Naming Conventions
- Use PascalCase for public members
- Use camelCase with underscore prefix for private fields: `_fieldName`
- Use meaningful, descriptive names

### Testing Standards & Test Success Rate

#### Comprehensive Testing Requirements
- **Maintain 100% test success rate** for all code
- **Write tests FIRST or alongside implementation** - never leave code untested
- **Update existing tests** when modifying functionality
- **All tests must pass** before considering any task complete
- **Zero failing tests policy** - no exceptions, all tests must be green

#### Testing Patterns
- Use AwesomeAssertions for fluent test assertions (note: uses `.Should()` syntax like FluentAssertions)
- Follow AAA pattern (Arrange, Act, Assert)
- Use descriptive test method names that explain the scenario
- Group related tests using `[Collection("Integration Tests")]` for integration tests

#### Test Categories
- **Unit Tests**: Test individual methods/classes in isolation
- **Integration Tests**: Test API endpoints, HTTP handlers, authentication flows
- **Infrastructure Tests**: Test client instantiation, configuration, disposal
- **Error Handling Tests**: Test exception scenarios and edge cases

#### Test Maintenance Strategy
- **For each new feature**: Create corresponding unit and integration tests
- **For each bug fix**: Add regression tests to prevent reoccurrence
- **For each refactor**: Update tests to match new implementation while preserving behavior verification
- **Regular test reviews**: Ensure tests remain valuable and maintainable

#### Test Success Expectations by Component
- **Public API Methods**: 100% test success rate (all paths, success and error cases)
- **HTTP Infrastructure**: 100% test success rate (handlers, retry logic, authentication)
- **Configuration/Options**: 100% test success rate (validation, edge cases)
- **Internal Utilities**: 100% test success rate
- **Exception Scenarios**: All custom exceptions must have passing tests

#### Test Quality Standards
```csharp
[Fact]
public async Task GetUsers_WithValidFilter_ReturnsFilteredResults()
{
    // Arrange - Set up test data and dependencies
    var client = _fixture.GetThousandEyesClient();
    var cancellationToken = new CancellationToken();
    
    // Act - Execute the operation being tested
    var result = await client.Users.GetAllAsync(cancellationToken);
    
    // Assert - Verify behavior and state
    result.Should().NotBeNull();
    result.Users.Should().NotBeEmpty();
    result.Users.Should().OnlyContain(u => !string.IsNullOrEmpty(u.Name));
}
```

### EditorConfig Compliance
- Follow the .editorconfig settings in the workspace
- Use tabs for indentation as configured
- Maintain consistent formatting across all files

### Performance Considerations
- Use `ConfigureAwait(false)` for library code
- Prefer `StringBuilder` for multiple string concatenations
- Use `span` and `memory` types where appropriate for performance-critical code

### Documentation
- Use XML documentation comments for public APIs
- Include `<summary>`, `<param>`, and `<returns>` tags
- Document any non-obvious behavior or assumptions

### Error Handling
- Use specific exception types (FormatException, ArgumentException, etc.)
- Provide meaningful error messages
- Use structured logging where applicable

## Project-Specific Guidelines

### ThousandEyes API Library Structure

#### Project Organization
- **Main Library**: `ThousandEyes.Api` (targets .NET 9)
  - Core client classes and interfaces
  - API models and DTOs (based on OpenAPI spec v7.0.63)
  - Authentication and configuration
  
- **Test Project**: `ThousandEyes.Api.Test` (targets .NET 9)
  - Unit tests with AwesomeAssertions
  - Integration tests using Microsoft Testing Platform
  - Uses User Secrets for sensitive configuration

#### Authentication & Configuration
```csharp
// ThousandEyesClientOptions pattern - always use required properties
public class ThousandEyesClientOptions
{
    public required string BearerToken { get; init; }
}
```

#### Validation Patterns
- Use proper validation for Bearer tokens:
```csharp
internal void Validate()
{
    if (string.IsNullOrWhiteSpace(BearerToken))
        throw new ArgumentException("BearerToken cannot be null or empty.", nameof(BearerToken));
}
```

#### Interface Design
- All public client functionality should be exposed through interfaces
- Use `IThousandEyesClient` for main client operations
- Create specialized interfaces for different API areas (e.g., `IAccountGroupsApi`, `IUsersApi`)

#### OpenAPI Integration
- The project includes a comprehensive `administrative_api_7_0_63.yaml` specification
- Generate API models and clients from this specification
- API supports extensive filtering, pagination, and field selection
- Common patterns:
  - List endpoints: Support filtering, sorting, pagination
  - Single endpoints: Support optional parameters like `expand`
  - POST/PUT: Accept structured request bodies
  - DELETE: Standard resource deletion

#### Test Configuration
- Integration tests use User Secrets
- Test configuration structure:
```json
{
  "ThousandEyesApi": {
    "BearerToken": "your-test-bearer-token"
  }
}
```

#### Testing Framework Setup
- Uses Microsoft Testing Platform (not traditional xUnit runner)
- Code coverage with Microsoft.Testing.Extensions.CodeCoverage
- Global usings for `Xunit` and `Microsoft.Extensions.Logging`

#### API Endpoint Patterns
Based on the OpenAPI spec v7.0.63, follow these patterns:

1. **Resource Collections**: `/resource` (GET for list, POST for create)
2. **Single Resources**: `/resource/{id}` (GET for single, PUT for update, DELETE for removal)
3. **Special Operations**: `/resource/special` for specific operations (e.g., `/users/current`)
4. **Common Query Parameters**:
   - `aid` - Account group ID context
   - `expand` - Expand related resources
   - `window` - Time window for events
   - `startDate`, `endDate` - Date range filtering
   - `cursor` - Pagination cursor
   - `useAllPermittedAids` - Cross-account querying

#### Error Handling Patterns
```csharp
// Validation in client options
internal void Validate()
{
    if (string.IsNullOrWhiteSpace(BearerToken))
        throw new ArgumentException("BearerToken cannot be null or empty.", nameof(BearerToken));
}

// API exception hierarchy
public class ThousandEyesApiException : Exception
{
    public int StatusCode { get; }
    public string? ErrorDetails { get; }
}

public class ThousandEyesBadRequestException : ThousandEyesApiException
{
    public ValidationError? ValidationError { get; }
}
```

#### Logging Integration
- Use structured logging with Microsoft.Extensions.Logging
- Log important operations and errors
- Use appropriate log levels (Information for normal operations, Warning for issues)

### Build and Packaging
- Uses Nerdbank.GitVersioning for semantic versioning
- Source Link enabled for debugging support
- Package generation enabled with comprehensive metadata
- Zero warnings policy enforced at build time
- XML documentation generation required for all public APIs

### Development Tools
- Microsoft Testing Platform for test execution
- AwesomeAssertions for fluent test syntax
- User Secrets for local development configuration
- Git versioning with semantic release tags

## Ongoing Development Guidelines

### Implementation Plan Reference
- **📋 Implementation Plan**: See `Specification/ImplementationPlan.md` for the complete development approach
- **🎯 Current Status**: Complete implementation of Administrative API v7.0.63
- **📊 Progress Tracking**: Update the implementation plan as each phase/milestone is completed

### Development Workflow
1. **Always reference the implementation plan** before starting new work
2. **Update progress markers** in the implementation plan as work is completed
3. **Follow the phased approach** - complete current phase before moving to next
4. **Run integration tests** after each significant change
5. **Maintain zero warnings** policy throughout development

### Refit Integration Guidelines
- Use Refit for HTTP client generation from OpenAPI spec
- Follow established patterns for HTTP handlers and interceptors
- Implement custom `DelegatingHandler` for logging and retry logic
- Structure APIs by functional groups (e.g., `IAccountGroupsApi`, `IUsersApi`)

### API Client Structure
```csharp
// Target API structure
var client = new ThousandEyesClient(options);
await client.AccountGroups.GetAllAsync(cancellationToken);
await client.Users.GetByIdAsync("123", cancellationToken);
await client.Roles.CreateAsync(request, cancellationToken);
await client.UserEvents.GetAllAsync(window: "24h", cancellationToken: cancellationToken);
```

### Integration Testing Approach
- Use valid Bearer token in test environment
- Always restore system state after tests (create/cleanup pattern)
- Test each API group thoroughly
- Include both positive and negative test cases
- Test all HTTP status codes and error scenarios

### ThousandEyes API Specifics
- **Administrative API v7.0.63**: Complete coverage of account management, user management, roles, permissions, and audit logs
- **Bearer Token Authentication**: Simple token-based authentication
- **Account Group Context**: Many operations support `aid` parameter for account group context
- **Expand Parameters**: Support for expanding related resources (users, agents)
- **Pagination**: Cursor-based pagination for large result sets
- **Time Filtering**: Advanced time-based filtering for audit events

### Questions and Clarifications
- **When stuck**: Add questions to the "Clarifying Questions" section in the implementation plan
- **Before major decisions**: Consult the implementation plan and add questions if needed
- **During reviews**: Update completed sections and mark progress