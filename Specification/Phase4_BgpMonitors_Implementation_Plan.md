# ?? Phase 4: BGP Monitors API - Implementation Plan

## ?? Overview

This document outlines the implementation plan for **Phase 4: BGP Monitors API**, the next phase in the ThousandEyes API .NET library development. This phase focuses on specialized monitoring capabilities for BGP routing information, enabling comprehensive network infrastructure visibility.

---

## ? Prerequisites (All Met)

- ? **Phase 1 Complete**: Administrative API (Account Management, Users, Roles, Permissions, Audit Logs)
- ? **Phase 2 Complete**: Core Monitoring APIs (Tests, Agents, Test Results)
- ? **Phase 3 Complete**: Advanced Monitoring APIs (Alerts, Dashboards, Snapshots, Filters)
- ? **Base Class Refactoring Complete**: All models use base classes (ApiResource, AccountGroupResource, AuditableResource)
- ? **100% Test Success Rate**: 49/49 tests passing
- ? **Zero Build Warnings**: Clean build with no errors, warnings, or messages
- ? **Modern .NET 9 Patterns**: Primary constructors, collection expressions, file-scoped namespaces

---

## ?? Phase 4 Scope: BGP Monitors API

### API Specification
- **Base URL**: `https://api.thousandeyes.com/v7`
- **Specification File**: `Specification/bgp_monitors_api_7_0_63.yaml`
- **Priority**: Medium - Network infrastructure monitoring
- **Complexity**: Low (single GET endpoint)

### Endpoints to Implement

```
GET /monitors                       # List BGP monitors
  Query Parameters:
  - aid (string, optional): Account group ID
  
  Response: Monitors object containing:
  - monitors[]: Array of Monitor objects
  - _links: HAL navigation links
```

---

## ?? Implementation Components

### 1. Models (`ThousandEyes.Api/Models/BgpMonitors/`)

#### 1.1 Monitor.cs
```csharp
namespace ThousandEyes.Api.Models.BgpMonitors;

/// <summary>
/// BGP monitor information
/// </summary>
public class Monitor
{
    /// <summary>
    /// BGP monitor ID
    /// </summary>
    public required string MonitorId { get; set; }
    
    /// <summary>
    /// Display name of the BGP monitor
    /// </summary>
    public string? MonitorName { get; set; }
    
    /// <summary>
    /// IP address of the BGP monitor
    /// </summary>
    public string? IpAddress { get; set; }
    
    /// <summary>
    /// Name of the autonomous system in which the monitor is found
    /// </summary>
    public string? Network { get; set; }
    
    /// <summary>
    /// Country ID (ISO 3166-1 alpha-2)
    /// </summary>
    public string? CountryId { get; set; }
    
    /// <summary>
    /// Type of monitor (public or private)
    /// </summary>
    public MonitorType? MonitorType { get; set; }
}
```

#### 1.2 MonitorType.cs
```csharp
namespace ThousandEyes.Api.Models.BgpMonitors;

/// <summary>
/// Type of BGP monitor
/// </summary>
public enum MonitorType
{
    /// <summary>
    /// Public BGP monitor (available to all accounts)
    /// </summary>
    Public,
    
    /// <summary>
    /// Private BGP monitor (custom/internal)
    /// </summary>
    Private
}
```

#### 1.3 Monitors.cs (Response Wrapper)
```csharp
using System.Text.Json.Serialization;
using ThousandEyes.Api.Models;

namespace ThousandEyes.Api.Models.BgpMonitors;

/// <summary>
/// Response containing list of BGP monitors
/// </summary>
public class Monitors : ApiResource
{
    /// <summary>
    /// List of BGP monitors
    /// </summary>
    [JsonPropertyName("monitors")]
    public Monitor[] MonitorsList { get; set; } = [];
}
```

### 2. Public Interface (`ThousandEyes.Api/Interfaces/BgpMonitors/`)

#### 2.1 IBgpMonitors.cs
```csharp
using ThousandEyes.Api.Models.BgpMonitors;

namespace ThousandEyes.Api.Interfaces.BgpMonitors;

/// <summary>
/// Public interface for BGP Monitors operations
/// </summary>
public interface IBgpMonitors
{
    /// <summary>
    /// Retrieves a list of BGP monitors available to your account in ThousandEyes,
    /// including public and private feeds.
    /// </summary>
    /// <param name="aid">Optional account group ID for context</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>List of BGP monitors</returns>
    Task<Monitors> GetAllAsync(string? aid, CancellationToken cancellationToken);
}
```

### 3. Internal Refit Interface (`ThousandEyes.Api/Refit/BgpMonitors/`)

#### 3.1 IBgpMonitorsRefitApi.cs
```csharp
using Refit;
using ThousandEyes.Api.Models.BgpMonitors;

namespace ThousandEyes.Api.Refit.BgpMonitors;

/// <summary>
/// Internal Refit interface for BGP Monitors API
/// </summary>
internal interface IBgpMonitorsRefitApi
{
    /// <summary>
    /// Retrieves a list of BGP monitors
    /// </summary>
    [Get("/monitors")]
    Task<Monitors> GetAllAsync(
        [Query] string? aid,
        CancellationToken cancellationToken);
}
```

### 4. Implementation Class (`ThousandEyes.Api/Implementations/BgpMonitors/`)

#### 4.1 BgpMonitorsImpl.cs
```csharp
using Refit;
using ThousandEyes.Api.Interfaces.BgpMonitors;
using ThousandEyes.Api.Models.BgpMonitors;
using ThousandEyes.Api.Refit.BgpMonitors;

namespace ThousandEyes.Api.Implementations.BgpMonitors;

/// <summary>
/// Implementation of BGP Monitors operations
/// </summary>
internal class BgpMonitorsImpl(IBgpMonitorsRefitApi refitApi) : IBgpMonitors
{
    private readonly IBgpMonitorsRefitApi _refitApi = refitApi;

    public async Task<Monitors> GetAllAsync(string? aid, CancellationToken cancellationToken)
        => await _refitApi.GetAllAsync(aid, cancellationToken);
}
```

### 5. Module Update (`ThousandEyes.Api/Modules/`)

#### 5.1 BgpMonitorsModule.cs (Replace existing placeholder)
```csharp
using Refit;
using ThousandEyes.Api.Implementations.BgpMonitors;
using ThousandEyes.Api.Interfaces.BgpMonitors;
using ThousandEyes.Api.Refit.BgpMonitors;

namespace ThousandEyes.Api.Modules;

/// <summary>
/// BGP Monitors API module for network infrastructure monitoring
/// </summary>
/// <remarks>
/// ? Phase 4 - IMPLEMENTED
/// Specialized monitoring for BGP routing information including:
/// - Public BGP monitors (global routing visibility)
/// - Private BGP monitors (custom/internal routing)
/// - Monitor location and network information
/// - AS (Autonomous System) information
/// </remarks>
public class BgpMonitorsModule
{
    /// <summary>
    /// Gets the BGP Monitors interface for monitor operations
    /// </summary>
    public IBgpMonitors BgpMonitors { get; }

    /// <summary>
    /// Initializes a new instance of the BgpMonitorsModule
    /// </summary>
    /// <param name="httpClient">The HTTP client to use for API calls</param>
    /// <param name="refitSettings">Refit settings for JSON serialization</param>
    public BgpMonitorsModule(HttpClient httpClient, RefitSettings refitSettings)
    {
        var refitApi = RestService.For<IBgpMonitorsRefitApi>(httpClient, refitSettings);
        BgpMonitors = new BgpMonitorsImpl(refitApi);
    }
}
```

### 6. Client Update (`ThousandEyes.Api/`)

#### 6.1 ThousandEyesClient.cs (Update constructor and property)
```csharp
// In constructor, replace the NotImplementedException line with:
BgpMonitors = new BgpMonitorsModule(_httpClient, _refitSettings);

// Replace the property with:
/// <summary>
/// Gets the BGP Monitors module for network infrastructure monitoring
/// </summary>
public BgpMonitorsModule BgpMonitors { get; private set; }
```

### 7. Integration Tests (`ThousandEyes.Api.Test/`)

#### 7.1 BgpMonitorsModuleTests.cs
```csharp
using AwesomeAssertions;
using ThousandEyes.Api.Models.BgpMonitors;

namespace ThousandEyes.Api.Test;

[Collection("Integration Tests")]
public class BgpMonitorsModuleTests(IntegrationTestFixture fixture)
{
    private readonly IntegrationTestFixture _fixture = fixture;

    [Fact]
    public async Task GetBgpMonitors_WithValidRequest_ReturnsMonitors()
    {
        // Arrange
        var client = _fixture.GetThousandEyesClient();
        var cancellationToken = CancellationToken.None;

        // Act
        var result = await client.BgpMonitors.BgpMonitors.GetAllAsync(
            aid: null,
            cancellationToken);

        // Assert
        result.Should().NotBeNull();
        result.MonitorsList.Should().NotBeEmpty();
        
        // Validate first monitor structure
        var firstMonitor = result.MonitorsList[0];
        firstMonitor.MonitorId.Should().NotBeNullOrWhiteSpace();
        firstMonitor.MonitorName.Should().NotBeNullOrWhiteSpace();
        firstMonitor.IpAddress.Should().NotBeNullOrWhiteSpace();
        firstMonitor.Network.Should().NotBeNullOrWhiteSpace();
        firstMonitor.CountryId.Should().NotBeNullOrWhiteSpace();
        firstMonitor.MonitorType.Should().NotBeNull();
    }

    [Fact]
    public async Task GetBgpMonitors_WithAccountGroupId_ReturnsFilteredMonitors()
    {
        // Arrange
        var client = _fixture.GetThousandEyesClient();
        var cancellationToken = CancellationToken.None;
        
        // Get account groups to get a valid AID
        var accountGroups = await client.AccountManagement.AccountGroups.GetAllAsync(cancellationToken);
        var testAid = accountGroups.AccountGroupList[0].Aid;

        // Act
        var result = await client.BgpMonitors.BgpMonitors.GetAllAsync(
            aid: testAid,
            cancellationToken);

        // Assert
        result.Should().NotBeNull();
        result.MonitorsList.Should().NotBeEmpty();
    }

    [Fact]
    public async Task GetBgpMonitors_ValidatesPublicMonitors()
    {
        // Arrange
        var client = _fixture.GetThousandEyesClient();
        var cancellationToken = CancellationToken.None;

        // Act
        var result = await client.BgpMonitors.BgpMonitors.GetAllAsync(
            aid: null,
            cancellationToken);

        // Assert
        result.MonitorsList.Should().Contain(m => m.MonitorType == MonitorType.Public);
    }

    [Fact]
    public async Task GetBgpMonitors_ResponseHasLinks()
    {
        // Arrange
        var client = _fixture.GetThousandEyesClient();
        var cancellationToken = CancellationToken.None;

        // Act
        var result = await client.BgpMonitors.BgpMonitors.GetAllAsync(
            aid: null,
            cancellationToken);

        // Assert
        result.Links.Should().NotBeNull();
        result.Links?.Self.Should().NotBeNull();
        result.Links?.Self?.Href.Should().NotBeNullOrWhiteSpace();
    }
}
```

---

## ?? Implementation Steps

### Step 1: Create Models (Estimated: 15 minutes)
1. Create `ThousandEyes.Api/Models/BgpMonitors/` directory
2. Create `Monitor.cs` - BGP monitor information model
3. Create `MonitorType.cs` - Enum for monitor types
4. Create `Monitors.cs` - Response wrapper inheriting from `ApiResource`

### Step 2: Create Interfaces (Estimated: 10 minutes)
1. Create `ThousandEyes.Api/Interfaces/BgpMonitors/` directory
2. Create `IBgpMonitors.cs` - Public consumer-facing interface
3. Create `ThousandEyes.Api/Refit/BgpMonitors/` directory
4. Create `IBgpMonitorsRefitApi.cs` - Internal Refit interface with proper decorators

### Step 3: Create Implementation (Estimated: 10 minutes)
1. Create `ThousandEyes.Api/Implementations/BgpMonitors/` directory
2. Create `BgpMonitorsImpl.cs` - Implementation bridging public and Refit interfaces

### Step 4: Update Module (Estimated: 10 minutes)
1. Replace `ThousandEyes.Api/Modules/BgpMonitorsModule.cs` placeholder
2. Initialize Refit API and implementation
3. Expose public interface through property

### Step 5: Update Client (Estimated: 5 minutes)
1. Update `ThousandEyesClient.cs` constructor to initialize `BgpMonitorsModule`
2. Replace `NotImplementedException` with actual module property
3. Update XML documentation

### Step 6: Create Integration Tests (Estimated: 20 minutes)
1. Create `ThousandEyes.Api.Test/BgpMonitorsModuleTests.cs`
2. Implement 4 test scenarios:
   - Get all BGP monitors (basic validation)
   - Get BGP monitors with account group ID (filtered)
   - Validate public monitors exist
   - Validate HAL links in response

### Step 7: Validation (Estimated: 10 minutes)
1. Run `get_errors` to check for compilation errors
2. Fix any issues found
3. Run `run_build` to verify zero warnings
4. Run integration tests to verify 100% success rate

### Step 8: Documentation (Estimated: 10 minutes)
1. Update `Specification/ImplementationPlan.md` to mark Phase 4.1 as complete
2. Create `Specification/Phase4_BgpMonitors_Complete.md` summary document
3. Update main `README.md` with Phase 4 progress

---

## ? Success Criteria

1. ? **Zero build errors** across entire solution
2. ? **Zero build warnings** across entire solution
3. ? **Zero build messages** across entire solution
4. ? **100% test success rate** (53/53 tests passing - 49 existing + 4 new)
5. ? **All BGP Monitors endpoints implemented** (1 GET endpoint)
6. ? **Models use base classes** where appropriate (Monitors uses ApiResource)
7. ? **Consistent file organization** (one file per type pattern)
8. ? **Modern .NET 9 patterns** maintained
9. ? **Comprehensive XML documentation** for all public APIs
10. ? **Integration tests validate real API** responses

---

## ?? Estimated Timeline

| Task | Estimated Time |
|------|----------------|
| Create Models | 15 minutes |
| Create Interfaces | 10 minutes |
| Create Implementation | 10 minutes |
| Update Module | 10 minutes |
| Update Client | 5 minutes |
| Create Integration Tests | 20 minutes |
| Validation & Fixes | 10 minutes |
| Documentation | 10 minutes |
| **Total** | **1.5 hours** |

---

## ?? Benefits of Phase 4 Implementation

### 1. Network Infrastructure Visibility
- Access to global BGP routing information
- Visibility into public and private BGP monitors
- Location and network topology insights

### 2. BGP Route Analysis Foundation
- Prepare for future BGP test result analysis
- Enable BGP-based alerting scenarios
- Support for multi-homed network monitoring

### 3. Consistent API Pattern
- Follows proven patterns from Phases 1-3
- Maintains base class inheritance
- Consistent error handling and authentication

### 4. Testing Coverage
- Comprehensive integration tests
- Real API validation
- 100% test success rate maintained

---

## ?? What's Next After Phase 4.1

### Phase 4.2: Internet Insights API (Medium Priority)
- Global internet health monitoring
- Outage detection and impact analysis
- Provider and location models
- Estimated: 2-3 days

### Phase 4.3: Event Detection API (Medium Priority)
- Automated anomaly detection
- Event correlation and grouping
- Event impact and metric models
- Estimated: 2-3 days

---

## ?? Notes

### API Simplicity
The BGP Monitors API is intentionally simple with a single GET endpoint. This makes it an excellent starting point for Phase 4, allowing us to:
- Establish the Phase 4 patterns quickly
- Validate the architecture with minimal complexity
- Build confidence before tackling more complex APIs

### Base Class Usage
The `Monitors` wrapper class inherits from `ApiResource` to get the `_links` property for HAL navigation, following the established pattern from base class refactoring.

### Monitor Type Enum
The `MonitorType` enum uses proper C# naming (Public/Private) with JSON serialization handling the lowercase conversion automatically.

### Future BGP Features
While this phase focuses on listing BGP monitors, future phases may include:
- BGP route information retrieval
- BGP path visualization
- BGP-specific test results
- BGP alerts and notifications

---

## ?? Phase 4.1 Completion Milestone

Upon successful implementation, the library will provide:
- ? **Complete BGP Monitors API** - Access to all BGP monitoring infrastructure
- ? **Foundation for BGP Analysis** - Prepare for advanced BGP features
- ? **85% Project Completion** - 4 of 7 phases fully implemented
- ? **Proven Architecture** - Validated patterns across 7 major modules
- ? **Enterprise-Ready** - Production-grade monitoring capabilities

**Next Target**: Phase 4.2 (Internet Insights API) for global internet health monitoring.
