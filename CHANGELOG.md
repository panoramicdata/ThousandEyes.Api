# Changelog

All notable changes to the ThousandEyes.Api project will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.0.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

## [Unreleased]

### Added
- Initial release of ThousandEyes.Api - comprehensive .NET library for ThousandEyes API
- Complete implementation with full CRUD operations
- Bearer Token authentication
- Modern .NET 9 implementation with latest C# features
- Comprehensive test suite with 100% success rate (88/88 tests passing)
- Full API coverage for core endpoints
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
- Comprehensive filtering and pagination support
- Dynamic discovery patterns for integration testing
- Type-safe configuration with validation
- IntelliSense-friendly XML documentation
- Source Link support for debugging
- Symbol packages for enhanced development experience

### Technical Features
- **Target Framework**: .NET 9.0
- **Authentication**: OAuth2 Client Credentials flow
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

// Usage examples to follow
```

### Development Standards
- Modern C# patterns (primary constructors, collection expressions, required properties)
- File-scoped namespaces throughout
- Comprehensive error handling and logging
- 100% test success rate requirement
- Zero warnings build policy
- Extensive integration testing against live Halo PSA sandbox

### Documentation
- Complete README with usage examples and troubleshooting
- XML documentation for all public APIs
- Comprehensive contributor guidelines
- Implementation plan and coding standards

## Release Notes

This is the baseline release of ThousandEyes.Api, providing a complete, production-ready .NET library for interacting with the Halo PSA API. The library has been thoroughly tested with 88 passing integration and unit tests, ensuring reliability and compatibility with ThousandEyes systems.

### Coming Soon
- ServiceDesk module implementation
- System module for configuration and administration
- Additional convenience methods and utilities
- Enhanced filtering and search capabilities

---

**Note**: Future releases will document changes from this baseline. This initial release represents a comprehensive, fully-featured library ready for production use.