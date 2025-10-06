# Changelog

All notable changes to the ThousandEyes.Api project will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.0.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

## [Unreleased]

### Added
- Initial release of ThousandEyes.Api - comprehensive .NET library for ThousandEyes Administrative API v7.0.63
- Complete implementation covering all administrative endpoints with full CRUD operations
- Bearer Token authentication for ThousandEyes API
- Modern .NET 9 implementation with latest C# features
- Comprehensive test suite with 100% success rate requirement
- Full API coverage for ThousandEyes Administrative API v7.0.63:
  - **Account Groups** - Complete CRUD operations for organizational structure
  - **Users** - User management with role and account group assignments
  - **Roles** - Custom role creation and permission management
  - **Permissions** - Comprehensive permission system integration
  - **User Events** - Activity logs and audit trail functionality
- Advanced HTTP client features:
  - Automatic retry logic with exponential backoff
  - Comprehensive request/response logging
  - Custom error handling with detailed exception hierarchy
  - Configurable timeouts and retry policies
- Robust error handling with custom exception types:
  - `ThousandEyesApiException` - Base exception for all API errors
  - `ThousandEyesBadRequestException` - 400 Bad Request errors with validation details
  - `ThousandEyesAuthenticationException` - 401 Authentication failures
  - `ThousandEyesAuthorizationException` - 403 Authorization failures  
  - `ThousandEyesNotFoundException` - 404 Resource not found errors
  - `ThousandEyesRateLimitException` - 429 Rate limit exceeded errors
  - `ThousandEyesServerException` - 5xx Server errors
- Comprehensive filtering and pagination support with cursor-based navigation
- Dynamic discovery patterns for integration testing
- Type-safe configuration with validation
- IntelliSense-friendly XML documentation
- Source Link support for debugging
- Symbol packages for enhanced development experience

### Technical Features
- **Target Framework**: .NET 9.0
- **Authentication**: Bearer Token authentication
- **HTTP Client**: Refit-based with custom handlers
- **Logging**: Microsoft.Extensions.Logging integration
- **Testing**: Microsoft Testing Platform with AwesomeAssertions
- **Packaging**: NuGet package with comprehensive metadata
- **CI/CD**: GitHub Actions with automated publishing
- **Code Quality**: Zero warnings policy, comprehensive test coverage

### API Structure
```csharp
// Core client initialization
var client = new ThousandEyesClient(new ThousandEyesClientOptions
{
    BearerToken = "your-bearer-token"
});

// Account Groups management
await client.AccountGroups.GetAllAsync(cancellationToken);
await client.AccountGroups.GetByIdAsync("1234", cancellationToken);
await client.AccountGroups.CreateAsync(request, cancellationToken);
await client.AccountGroups.UpdateAsync("1234", request, cancellationToken);
await client.AccountGroups.DeleteAsync("1234", cancellationToken);

// Users management
await client.Users.GetAllAsync(cancellationToken);
await client.Users.GetByIdAsync("123", cancellationToken);
await client.Users.CreateAsync(request, cancellationToken);
await client.Users.UpdateAsync("123", request, cancellationToken);
await client.Users.DeleteAsync("123", cancellationToken);
await client.Users.GetCurrentAsync(cancellationToken);

// Roles and Permissions
await client.Roles.GetAllAsync(cancellationToken);
await client.Roles.GetByIdAsync("23", cancellationToken);
await client.Roles.CreateAsync(request, cancellationToken);
await client.Roles.UpdateAsync("23", request, cancellationToken);
await client.Roles.DeleteAsync("23", cancellationToken);
await client.Permissions.GetAllAsync(cancellationToken);

// Audit and User Events
await client.UserEvents.GetAllAsync(window: "24h", cancellationToken: cancellationToken);
await client.UserEvents.GetAllAsync(startDate: startDate, endDate: endDate, cancellationToken: cancellationToken);
```

### Development Standards
- Modern C# patterns (primary constructors, collection expressions, required properties)
- File-scoped namespaces throughout
- Comprehensive error handling and logging
- 100% test success rate requirement
- Zero warnings build policy
- Extensive integration testing against live ThousandEyes sandbox

### API Coverage by OpenAPI Specification
Based on `Specification/administrative_api_7_0_63.yaml`:

#### Account Groups (`/account-groups`)
- ? `GET /account-groups` - List account groups
- ? `POST /account-groups` - Create account group (with optional expand parameter)
- ? `GET /account-groups/{id}` - Retrieve account group (with optional expand parameter)
- ? `PUT /account-groups/{id}` - Update account group (with optional expand parameter)
- ? `DELETE /account-groups/{id}` - Delete account group

#### Users (`/users`)
- ? `GET /users` - List users (with optional aid parameter)
- ? `POST /users` - Create user (with optional aid parameter)
- ? `GET /users/{id}` - Retrieve user (with optional aid parameter)
- ? `PUT /users/{id}` - Update user (with optional aid parameter)
- ? `DELETE /users/{id}` - Delete user (with optional aid parameter)
- ? `GET /users/current` - Retrieve current user

#### Roles (`/roles`)
- ? `GET /roles` - List roles (with optional aid parameter)
- ? `POST /roles` - Create role (with optional aid parameter)
- ? `GET /roles/{id}` - Retrieve role (with optional aid parameter)
- ? `PUT /roles/{id}` - Update role (with optional aid parameter)
- ? `DELETE /roles/{id}` - Delete role (with optional aid parameter)

#### Permissions (`/permissions`)
- ? `GET /permissions` - List assignable permissions (with optional aid parameter)

#### User Events (`/audit-user-events`)
- ? `GET /audit-user-events` - List activity log events with comprehensive filtering:
  - Time-based filtering (window, startDate, endDate)
  - Account group filtering (aid, useAllPermittedAids)
  - Cursor-based pagination support

### Documentation
- Complete README with usage examples and troubleshooting
- XML documentation for all public APIs
- Comprehensive contributor guidelines
- Implementation plan and coding standards
- Full OpenAPI specification coverage documentation

## Release Notes

This is the baseline release of ThousandEyes.Api, providing a complete, production-ready .NET library for interacting with the ThousandEyes Administrative API v7.0.63. The library provides full coverage of all administrative endpoints as defined in the official OpenAPI specification.

### API Completeness
All endpoints from the ThousandEyes Administrative API v7.0.63 specification are implemented with:
- Complete CRUD operations where applicable
- Full parameter support including optional query parameters
- Proper response model mapping
- Comprehensive error handling for all HTTP status codes (400, 401, 403, 404, 429, 500)
- Bearer token authentication
- Request/response logging and retry mechanisms

### Coming Soon
- Enhanced convenience methods and utilities
- Additional filtering and search capabilities
- Workflow automation helpers
- Integration with other ThousandEyes API modules (Tests, Reports, etc.)

---

**Note**: Future releases will document changes from this baseline. This initial release represents a comprehensive, fully-featured library ready for production use with complete coverage of the ThousandEyes Administrative API specification.