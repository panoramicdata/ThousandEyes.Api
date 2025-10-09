# GitHub Copilot Instructions

## Code Style Guidelines

### One File Per Type Policy ? **MANDATORY**
- **Each type (class, interface, enum, record) MUST be in its own separate file**
- **File name MUST match the type name exactly** (e.g., `UserDetail.cs` for `class UserDetail`)
- **No exceptions** - this rule applies to:
  - Classes (models, implementations, modules)
  - Interfaces (public and internal Refit interfaces)
  - Enums (all enumerations)
  - Records (all record types)
- **Nested types are the ONLY exception** - keep nested private types within their parent file
- **Benefits**:
  - ?? Easy navigation (type name = file name)
  - ?? Better source control (fewer merge conflicts)
  - ?? Clear responsibilities (one concept per file)
  - ?? Easier testing and maintenance

### Zero Tolerance Policy - NO Errors, Warnings, or Messages
- **ZERO TOLERANCE**: No errors, no warnings, no messages - EVER
- **Always check Roslyn diagnostics** before attempting any build
- Use Visual Studio's Roslyn analyzer to detect issues in real-time
- **Address ALL compiler diagnostics** before considering code complete
- Use `#pragma warning disable` only in exceptional cases with clear justification and documentation
- **Add new words to the custom dictionary file** (`ThousandEyes.Api.dic`) instead of suppressing spelling warnings
- Enable "Treat warnings as errors" where appropriate
- **Every commit must have zero diagnostics** - no exceptions

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
public async Task GetUsers_WithValidRequest_ReturnsUsers()
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

## Development Workflow

### Before Implementing New Features
1. **Review existing tests** to understand current patterns
2. **Plan test coverage** for the new feature
3. **Write failing tests first** (TDD approach when possible)
4. **Implement the feature** to make tests pass
5. **Verify 100% test success rate** on all tests
6. **Run full test suite** to ensure no regressions

### Before Submitting Code
1. **All tests must pass** (unit + integration) - **100% success rate required**
2. **Zero build warnings** policy enforced
3. **All functionality fully tested** with comprehensive test coverage
4. **Integration tests validate real-world scenarios**
5. **Documentation updated** for public API changes

### Testing Infrastructure Notes
- Integration tests use `IntegrationTestFixture` with User Secrets
- Test collection: `[Collection("Integration Tests")]`
- ThousandEyes test environment credentials for testing (safe to create/destroy test data)
- Always clean up test data to leave system in original state
- **Always pass CancellationToken.None or a real CancellationToken** in tests - never rely on default values

### ThousandEyes API Specific Guidelines
- **Administrative API v7.0.63**: Complete coverage of account management, user management, roles, permissions, and audit logs
- **Bearer Token Authentication**: Simple token-based authentication
- **Account Group Context**: Many operations support `aid` parameter for account group context
- **Expand Parameters**: Support for expanding related resources (users, agents)
- **Pagination**: Cursor-based pagination for large result sets
- **Time Filtering**: Advanced time-based filtering for audit events