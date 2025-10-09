# ?? Phase 4 Completion: Internet Insights & Event Detection APIs - Implementation Plan

## ?? Overview

This document outlines the complete implementation plan for finishing **Phase 4: Specialized Monitoring APIs** by implementing:
1. **Phase 4.2**: Internet Insights API (Outages & Catalog Providers)
2. **Phase 4.3**: Event Detection API

Upon completion, Phase 4 will be 100% complete with comprehensive network infrastructure monitoring capabilities.

---

## ? Prerequisites (All Met)

- ? **Phase 1-3 Complete**: Administrative, Core Monitoring, Advanced Monitoring APIs
- ? **Phase 4.1 Complete**: BGP Monitors API
- ? **Base Class Refactoring Complete**: All models use base classes
- ? **100% Test Success Rate**: 53/53 tests passing
- ? **Zero Build Warnings**: Clean build maintained
- ? **One File Per Type Policy**: Strictly enforced across solution

---

## ?? Phase 4.2: Internet Insights API

### API Specification
- **Base URL**: `https://api.thousandeyes.com/v7/internet-insights`
- **Specification File**: `Specification/internet_insights_api_7_0_63.yaml`
- **Priority**: Medium - Global internet health monitoring
- **Complexity**: High (5 endpoints, complex filter models, multiple response types)

### Endpoints to Implement

```
POST /internet-insights/catalog/providers/filter    # Filter catalog providers
GET  /internet-insights/catalog/providers/{id}      # Get catalog provider details

POST /internet-insights/outages/filter              # Filter outages
GET  /internet-insights/outages/net/{outageId}      # Get network outage details
GET  /internet-insights/outages/app/{outageId}      # Get application outage details
```

### File Structure (One File Per Type)

#### **Models** (`ThousandEyes.Api/Models/InternetInsights/`)
Total: 23 files

**Catalog Provider Models** (9 files):
1. `CatalogProvider.cs` - Catalog provider summary
2. `CatalogProviderDetails.cs` - Detailed catalog provider info
3. `CatalogProviderFilter.cs` - Filter request for catalog providers
4. `CatalogProviderResponse.cs` - Response wrapper (inherits from ApiResource)
5. `ProviderLocation.cs` - Provider location information
6. `ProviderType.cs` - Enum for provider types (IAAS, SAAS, CDN, DNS, etc.)
7. `DataType.cs` - Enum for data types (Application, Network)
8. `Asn.cs` - Autonomous System Number info
9. `Region.cs` - Provider region enum/string

**Outage Models** (14 files):
10. `Outage.cs` - Outage summary
11. `OutageFilter.cs` - Filter request for outages
12. `OutagesResponse.cs` - Response wrapper (inherits from ApiResource)
13. `OutageScope.cs` - Enum (all, with-affected-test)
14. `NetworkOutageDetails.cs` - Detailed network outage info
15. `ApplicationOutageDetails.cs` - Detailed application outage info
16. `AffectedTest.cs` - Test impacted by outage
17. `AffectedAgent.cs` - Agent impacted by outage
18. `AffectedServer.cs` - Server impacted by application outage
19. `NetworkAffectedLocation.cs` - Network outage location impact
20. `ApplicationAffectedLocation.cs` - Application outage location impact
21. `OutageType.cs` - Enum (net, app)
22. `TimeWindow.cs` - Time window for filter (window vs startDate/endDate)
23. `OutageMetrics.cs` - Common outage metrics (duration, counts)

#### **Public Interfaces** (`ThousandEyes.Api/Interfaces/InternetInsights/`)
2 files:
1. `ICatalogProviders.cs` - Catalog provider operations
2. `IOutages.cs` - Outage operations

#### **Internal Refit Interfaces** (`ThousandEyes.Api/Refit/InternetInsights/`)
2 files:
1. `ICatalogProvidersRefitApi.cs` - Refit interface for catalog providers
2. `IOutagesRefitApi.cs` - Refit interface for outages

#### **Implementations** (`ThousandEyes.Api/Implementations/InternetInsights/`)
2 files:
1. `CatalogProvidersImpl.cs` - Catalog provider implementation
2. `OutagesImpl.cs` - Outage implementation

#### **Module** (`ThousandEyes.Api/Modules/`)
1 file:
1. `InternetInsightsModule.cs` - Module wrapper

#### **Integration Tests** (`ThousandEyes.Api.Test/`)
1 file:
1. `InternetInsightsModuleTests.cs` - 6 integration tests

**Total Files for Phase 4.2**: 31 files

---

## ?? Phase 4.3: Event Detection API

### API Specification
- **Base URL**: `https://api.thousandeyes.com/v7`
- **Specification File**: `Specification/event_detection_api_7_0_63.yaml`
- **Priority**: Medium - Automated anomaly detection
- **Complexity**: Medium (check if API exists in spec files)

### Note on Event Detection API
After reviewing the available specification files, **Event Detection API does not appear to have a separate OpenAPI specification**. This may be:
1. Part of another API module
2. Not yet available in v7.0.63
3. Covered by Alerts API (which handles event-based alerting)

**Decision**: Skip Phase 4.3 for now or investigate if it's covered by existing Alerts API.

---

## ?? Implementation Approach

### Strategy
Given the complexity of Internet Insights API (31 files), we'll implement it in stages:

**Stage 1**: Catalog Providers API (simpler, 2 endpoints)
- Create all catalog provider models (9 files)
- Create public interface, Refit interface, implementation
- Add 2 integration tests
- Validate build

**Stage 2**: Outages API (more complex, 3 endpoints)
- Create all outage models (14 files)
- Create public interface, Refit interface, implementation
- Add 4 integration tests
- Validate build

**Stage 3**: Module Integration
- Create InternetInsightsModule
- Update ThousandEyesClient
- Update IThousandEyesClient interface
- Run full test suite

### Estimated Timeline
- **Stage 1 (Catalog Providers)**: 2 hours
- **Stage 2 (Outages)**: 2.5 hours
- **Stage 3 (Integration)**: 30 minutes
- **Total**: ~5 hours for Phase 4.2

---

## ?? Implementation Steps - Stage 1: Catalog Providers

### Step 1.1: Create Catalog Provider Models (30 minutes)
Create 9 model files in `ThousandEyes.Api/Models/InternetInsights/`:
1. `Asn.cs`
2. `CatalogProvider.cs`
3. `CatalogProviderDetails.cs`
4. `CatalogProviderFilter.cs`
5. `CatalogProviderResponse.cs` (inherits from ApiResource)
6. `ProviderLocation.cs`
7. `ProviderType.cs` (enum)
8. `DataType.cs` (enum)
9. `Region.cs` (string, not enum - free-form)

### Step 1.2: Create Catalog Provider Interfaces (15 minutes)
1. `ICatalogProviders.cs` - Public interface with:
   - `FilterAsync(CatalogProviderFilter filter, string? aid, CancellationToken cancellationToken)`
   - `GetByIdAsync(string providerId, string? aid, CancellationToken cancellationToken)`

2. `ICatalogProvidersRefitApi.cs` - Refit interface with proper decorators

### Step 1.3: Create Catalog Provider Implementation (15 minutes)
1. `CatalogProvidersImpl.cs` - Bridge implementation

### Step 1.4: Validation (10 minutes)
- Run `get_errors` on all new files
- Fix any compilation errors
- Validate zero warnings

---

## ?? Implementation Steps - Stage 2: Outages

### Step 2.1: Create Outage Models (45 minutes)
Create 14 model files in `ThousandEyes.Api/Models/InternetInsights/`:
1. `Outage.cs`
2. `OutageFilter.cs`
3. `OutagesResponse.cs` (inherits from ApiResource)
4. `OutageScope.cs` (enum)
5. `NetworkOutageDetails.cs`
6. `ApplicationOutageDetails.cs`
7. `AffectedTest.cs`
8. `AffectedAgent.cs`
9. `AffectedServer.cs`
10. `NetworkAffectedLocation.cs`
11. `ApplicationAffectedLocation.cs`
12. `OutageType.cs` (enum)
13. `TimeWindow.cs` (helper for startDate/endDate/window)
14. `OutageMetrics.cs` (shared metrics)

### Step 2.2: Create Outage Interfaces (15 minutes)
1. `IOutages.cs` - Public interface with:
   - `FilterAsync(OutageFilter filter, string? aid, CancellationToken cancellationToken)`
   - `GetNetworkOutageAsync(string outageId, string? aid, CancellationToken cancellationToken)`
   - `GetApplicationOutageAsync(string outageId, string? aid, CancellationToken cancellationToken)`

2. `IOutagesRefitApi.cs` - Refit interface with proper decorators

### Step 2.3: Create Outage Implementation (15 minutes)
1. `OutagesImpl.cs` - Bridge implementation

### Step 2.4: Validation (10 minutes)
- Run `get_errors` on all new files
- Fix any compilation errors
- Validate zero warnings

---

## ?? Implementation Steps - Stage 3: Module Integration

### Step 3.1: Create Module (10 minutes)
1. `InternetInsightsModule.cs` - Module wrapper with:
   - `CatalogProviders` property (ICatalogProviders)
   - `Outages` property (IOutages)

### Step 3.2: Update Client (10 minutes)
1. Update `ThousandEyesClient.cs` constructor
2. Add `InternetInsights` property
3. Update XML documentation

### Step 3.3: Update Interface (5 minutes)
1. Update `IThousandEyesClient.cs` with `InternetInsights` property

### Step 3.4: Create Integration Tests (20 minutes)
Create `InternetInsightsModuleTests.cs` with 6 tests:
1. `FilterCatalogProviders_WithValidFilter_ReturnsProviders`
2. `GetCatalogProvider_WithValidId_ReturnsProviderDetails`
3. `FilterOutages_WithTimeWindow_ReturnsOutages`
4. `FilterOutages_WithDateRange_ReturnsOutages`
5. `GetNetworkOutage_WithValidId_ReturnsOutageDetails`
6. `GetApplicationOutage_WithValidId_ReturnsOutageDetails`

### Step 3.5: Final Validation (10 minutes)
- Run full build
- Run all tests (expect 59 total: 53 existing + 6 new)
- Verify 100% test success rate
- Verify zero warnings

---

## ? Success Criteria

1. ? **Zero build errors** across entire solution
2. ? **Zero build warnings** across entire solution
3. ? **100% test success rate** (59/59 tests passing)
4. ? **All Internet Insights endpoints implemented** (5 endpoints)
5. ? **One file per type** strictly enforced (31 new files)
6. ? **Models use base classes** where appropriate
7. ? **Modern .NET 9 patterns** maintained
8. ? **Comprehensive XML documentation**
9. ? **Integration tests validate real API**

---

## ?? Model Design Guidelines

### Base Class Usage
```csharp
// Response wrappers inherit from ApiResource
public class CatalogProviderResponse : ApiResource
{
    [JsonPropertyName("providers")]
    public CatalogProvider[] ProvidersList { get; set; } = [];
}

// Individual models do NOT inherit (they have no _links property)
public class CatalogProvider
{
    public required string Id { get; set; }
    public string? ProviderName { get; set; }
    // ...
}
```

### Enum Design
```csharp
// Provider types from spec
public enum ProviderType
{
    Iaas,    // Infrastructure as a Service
    Saas,    // Software as a Service
    Cdn,     // Content Delivery Network
    Dns      // DNS Provider
}

// JSON serialization handles case conversion automatically
```

### Filter Request Models
```csharp
// Filter models for POST requests
public class CatalogProviderFilter
{
    public string? ProviderName { get; set; }
    public ProviderType? ProviderType { get; set; }
    public string? Region { get; set; }
    public string? Location { get; set; }
    public string? Asn { get; set; }
    public bool? Included { get; set; }
}
```

---

## ?? Phase 4 Completion Benefits

### Business Value
- **Global Internet Health Monitoring**: Track internet outages affecting business operations
- **Provider Catalog Management**: Discover and monitor critical infrastructure providers
- **Proactive Issue Detection**: Identify internet-level issues before they impact users
- **Impact Analysis**: Understand scope and duration of internet outages

### Technical Value
- **Complete Phase 4**: All specialized monitoring APIs implemented
- **88% Project Completion**: Nearly complete library
- **Production-Ready**: Comprehensive monitoring capabilities
- **Proven Architecture**: Validated patterns across 8 major modules

---

## ?? Project Status After Phase 4 Completion

- **Overall Project**: ~**88% complete** (Phase 4 of 7 fully implemented)
- **Phase 1 (Administrative)**: ? 100% complete
- **Phase 2 (Core Monitoring)**: ? 100% complete
- **Phase 3 (Advanced Monitoring)**: ? 100% complete
- **Phase 4 (Specialized Monitoring)**: ? 100% complete
- **Phase 5-7**: ?? Planned (Integration, Specialized Features, OpenTelemetry)

---

## ?? Next Steps After Phase 4

**Phase 5: Integration and Advanced APIs** (Lower Priority)
- Integrations API (webhooks, PagerDuty, Slack, etc.)
- Credentials API (API key management)
- Usage API (quota and usage tracking)

**Estimated Timeline**: 2-3 weeks for Phase 5

---

## ?? Implementation Notes

### API Complexity
Internet Insights is the most complex API implemented so far due to:
- Multiple filter request models with many optional parameters
- Two distinct domains (Catalog Providers vs Outages)
- Complex response hierarchies (affected tests, agents, locations, servers)
- Time-based filtering with multiple approaches (window vs date range)

### Testing Strategy
- Use mock/example data for initial tests (API may not have live outages)
- Focus on model serialization/deserialization
- Validate filter parameter handling
- Test both happy paths and error scenarios

### File Organization
Strict "one file per type" policy means:
- 31 new files for Phase 4.2
- Clear file naming (type name = file name)
- Easy navigation and maintenance
- Better source control (fewer merge conflicts)

---

**Ready to begin implementation of Phase 4.2: Internet Insights API!**
