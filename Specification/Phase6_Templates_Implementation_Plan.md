# Phase 6.3 Implementation Plan: Templates API - FULL COMPLEXITY

## ?? **MEGA CHALLENGE ACCEPTED! ???**

Successfully implementing the **most complex API in the ThousandEyes specification** - the Templates API with complete Handlebars templating, multiple test types, deployment strategies, and comprehensive configuration support.

---

## ?? **Overview**

The Templates API is the **crown jewel** of ThousandEyes automation - it allows creating entire monitoring infrastructures from reusable templates with dynamic configuration using Handlebars notation.

### **Complexity Level**: ?? **EXTREME** ??
- **API Endpoints**: 8 operations
- **Test Type Models**: 9 different test configurations
- **Endpoint Test Models**: Complex endpoint test structures
- **Supporting Models**: Labels, Alert Rules, Dashboards, Dashboard Filters
- **Handlebars Templating**: Dynamic value substitution throughout
- **Deployment Strategies**: Create, Update, Ignore patterns
- **Estimated Files**: **80-120 files**
- **Estimated Implementation Time**: **4-6 hours**

---

## ?? **Implementation Phases**

We'll break this mega-implementation into **5 manageable sub-phases**:

### **Phase 6.3.1: Core Template Models** (Foundation)
**Estimated Files**: 15-20
**Estimated Time**: 45-60 minutes

### **Phase 6.3.2: Test Configuration Models** (The Mountain)
**Estimated Files**: 30-40
**Estimated Time**: 90-120 minutes

### **Phase 6.3.3: Supporting Asset Models** (Supporting Infrastructure)
**Estimated Files**: 20-25
**Estimated Time**: 60-75 minutes

### **Phase 6.3.4: API Implementation** (The Summit)
**Estimated Files**: 10-12
**Estimated Time**: 30-45 minutes

### **Phase 6.3.5: Testing & Validation** (The Victory Lap)
**Estimated Files**: 1 test file (6-8 tests)
**Estimated Time**: 30-45 minutes

---

## ?? **API Endpoints (8 Operations)**

```
GET    /templates                           # List templates (with filters)
POST   /templates                           # Create template
GET    /templates/{id}                      # Get template details
PUT    /templates/{id}                      # Update template
DELETE /templates/{id}                      # Delete template
POST   /templates/{id}/deploy               # Deploy template (creates assets)
GET    /templates/{id}/sharing-settings     # Get sharing settings
PUT    /templates/{id}/sharing-settings     # Update sharing settings
```

---

## ?? **Phase 6.3.1: Core Template Models**

### **Goal**: Establish foundation with core template structure

### **Files to Create (15-20)**

#### **Core Models** (5 files)
```
ThousandEyes.Api/Models/Templates/
??? Template.cs                      # Main template model
??? TemplateResponse.cs              # Template with ID and dates
??? TemplateResponseBase.cs          # Base with ID/dateCreated
??? Templates.cs                     # List wrapper (TemplatesResponse)
??? DeployTemplate.cs                # Deploy request model
```

#### **Enums** (4 files)
```
ThousandEyes.Api/Models/Templates/
??? CertificationLevel.cs            # user, thousandeyes, partner, certified
??? TemplateModule.cs                # default (only option for users)
??? DeploymentStrategy.cs            # create, update, ignore
??? ResourceInclusion.cs             # included, skipped
```

#### **User Input Models** (4 files)
```
ThousandEyes.Api/Models/Templates/
??? UserInput.cs                     # User input definition
??? UserInputType.cs                 # Enum: string, number, agents, tests, etc.
??? UserInputAllowedValue.cs         # Allowed value with name/value
??? UserInputValue.cs                # Can be string, number, array, object
```

#### **Grouping Models** (2 files)
```
ThousandEyes.Api/Models/Templates/
??? TemplateGrouping.cs              # Grouping for UI organization
??? TemplateGroupingType.cs          # Enum: test, user-input
```

#### **Sharing Settings Models** (3 files)
```
ThousandEyes.Api/Models/Templates/
??? SharingSettings.cs               # Sharing configuration
??? SharingSettingsResponse.cs       # Response with links
??? SharingScope.cs                  # Enum: default, organization
```

### **Key Design Decisions**

1. **UserInputValue Challenge**: This is `anyOf` multiple types - we'll use `System.Text.Json.JsonElement` or create a custom converter
2. **Template Complexity**: The main Template model will use `Dictionary<string, object>` for complex nested structures initially
3. **Handlebars Support**: We'll use string types with validation rather than custom Handlebars parsing

### **Success Criteria**
- ? All core template CRUD models created
- ? User input system fully modeled
- ? Sharing settings complete
- ? Zero compilation errors

---

## ?? **Phase 6.3.2: Test Configuration Models (THE MOUNTAIN ???)**

### **Goal**: Implement all 9 test type configurations with full detail

This is the **largest and most complex phase** - creating models for all test types that can be defined in templates.

### **Files to Create (30-40)**

#### **Base Test Models** (3 files)
```
ThousandEyes.Api/Models/Templates/Tests/
??? TestConfigurationTemplate.cs     # Base/discriminator model
??? TestBaseTemplate.cs              # Common test properties
??? TestTypeTemplate.cs              # Enum: all test types
```

#### **Network Test Base** (1 file)
```
ThousandEyes.Api/Models/Templates/Tests/
??? NetworkTestBaseTemplate.cs       # Shared network test properties
```

#### **Agent to Server Test** (4 files)
```
ThousandEyes.Api/Models/Templates/Tests/
??? AgentToServerTestTemplate.cs     # Full config
??? AgentToServerTestTypeTemplate.cs
??? TargetServerTemplate.cs
??? AgentAssignmentTemplate.cs       # Agent assignment
```

#### **HTTP Server Test** (5 files)
```
ThousandEyes.Api/Models/Templates/Tests/
??? HttpServerTestTemplate.cs        # Full HTTP config
??? HttpServerTestBaseTemplate.cs    # HTTP-specific base
??? HttpVersionTemplate.cs
??? AuthTypeTestTemplate.cs
??? CustomHeadersTemplate.cs
```

#### **Page Load Test** (4 files)
```
ThousandEyes.Api/Models/Templates/Tests/
??? PageLoadTestTemplate.cs          # Full page load config
??? BrowserTestBaseTemplate.cs       # Browser test base
??? PageLoadTestBaseTemplate.cs
??? PageLoadStrategyTemplate.cs
```

#### **Agent to Agent Test** (3 files)
```
ThousandEyes.Api/Models/Templates/Tests/
??? AgentToAgentTestTemplate.cs
??? AgentToAgentTestTypeTemplate.cs
??? AgentToAgentTestDirectionTemplate.cs
```

#### **Voice Test** (3 files)
```
ThousandEyes.Api/Models/Templates/Tests/
??? VoiceTestTemplate.cs
??? VoiceTestTypeTemplate.cs
??? VoiceIntervalTemplate.cs
```

#### **Web Transaction Test** (3 files)
```
ThousandEyes.Api/Models/Templates/Tests/
??? TransactionTestTemplate.cs
??? TransactionTestBaseTemplate.cs
??? TransactionScriptTemplate.cs
```

#### **SIP Server Test** (4 files)
```
ThousandEyes.Api/Models/Templates/Tests/
??? SipServerTestTemplate.cs
??? SipServerTestBaseTemplate.cs
??? SipProtocolTemplate.cs
??? TargetSipCredentialsTemplate.cs
```

#### **DNS Server Test** (4 files)
```
ThousandEyes.Api/Models/Templates/Tests/
??? DnsServerTestTemplate.cs
??? DnsServersTemplate.cs
??? DnsServerEntryTemplate.cs
??? DnsTargetDomainTemplate.cs
```

#### **DNS Trace Test** (2 files)
```
ThousandEyes.Api/Models/Templates/Tests/
??? DnsTraceTestTemplate.cs
??? DnsTraceTestTypeTemplate.cs
```

#### **Shared Test Components** (5 files)
```
ThousandEyes.Api/Models/Templates/Tests/
??? LabelAssignmentTemplate.cs       # Label references
??? PortTemplate.cs                  # Port specification
??? NumPathTracesTemplate.cs
??? PathTraceModeTemplate.cs
??? ProbeModeTemplate.cs
```

### **Implementation Strategy**

1. **Start with TestBaseTemplate** - establishes common properties
2. **NetworkTestBaseTemplate** - shared network test properties
3. **Implement each test type** - one at a time, starting with simplest (Agent to Server)
4. **Use inheritance** - leverage base classes to reduce duplication
5. **Handlebars types** - Use `anyOf<T, HandlebarsExpression>` pattern

### **Key Challenges**

- **Polymorphic Test Types**: Use `[JsonDerivedType]` with discriminator on `type` field
- **Handlebars Templating**: Many fields can be either concrete values OR Handlebars expressions
- **Shared Properties**: Careful inheritance hierarchy to avoid duplication

### **Success Criteria**
- ? All 9 test types fully modeled
- ? Proper inheritance hierarchy
- ? Handlebars support throughout
- ? Zero compilation errors
- ? Clean separation of concerns

---

## ?? **Phase 6.3.3: Supporting Asset Models**

### **Goal**: Implement Labels, Alert Rules, Dashboards, and Dashboard Filters

### **Files to Create (20-25)**

#### **Label Configuration** (3 files)
```
ThousandEyes.Api/Models/Templates/Labels/
??? LabelConfigurationTemplate.cs    # Label definition
??? LabelTypeTemplate.cs             # tests, endpoint_tests
??? TestIdTemplate.cs                # Test reference
```

#### **Endpoint Test Configuration** (10-12 files)
```
ThousandEyes.Api/Models/Templates/EndpointTests/
??? EndpointTestConfigTemplate.cs    # Main endpoint test config
??? EndpointTestTypeTemplate.cs      # Http, Network
??? EndpointTestCategoryTemplate.cs  # SCHEDULED_TEST, DYNAMIC_APP_TEST
??? EndpointGenericConfigTemplate.cs
??? EndpointAgentSelectionConfigTemplate.cs
??? EndpointNetworkTestConfigTemplate.cs
??? EndpointHttpTestConfigTemplate.cs
??? EndpointAuthTypeTemplate.cs
??? EndpointServerConfigTemplate.cs
??? EndpointAlertRuleConfigTemplate.cs
??? EndpointMonitoringSettingsTypeTemplate.cs
??? EndpointTestIntervalTemplate.cs
```

#### **Alert Rule Configuration** (3 files)
```
ThousandEyes.Api/Models/Templates/AlertRules/
??? AlertRuleConfigurationTemplate.cs
??? AlertRuleTypeTemplate.cs         # All alert types
??? AlertRuleSeverityTemplate.cs     # INFO, MAJOR, MINOR, CRITICAL
```

#### **Dashboard Configuration** (5 files)
```
ThousandEyes.Api/Models/Templates/Dashboards/
??? DashboardConfigurationTemplate.cs
??? DashboardWidgetTemplate.cs
??? DashboardRefreshRateTemplate.cs
??? ApiDurationTemplate.cs
??? ApiWidgetMeasure.cs
```

#### **Dashboard Filter Configuration** (4 files)
```
ThousandEyes.Api/Models/Templates/DashboardFilters/
??? DashboardFilterConfigurationTemplate.cs
??? DashboardFilterContextTemplate.cs
??? ApiDataSourceFilterTemplate.cs
??? FilterContextDataSourceTemplate.cs
```

### **Implementation Strategy**

1. **Labels first** - simplest asset type
2. **Alert Rules** - moderate complexity
3. **Endpoint Tests** - complex but well-structured
4. **Dashboards** - very complex nested structures
5. **Dashboard Filters** - final piece

### **Key Challenges**

- **Dashboard Complexity**: Very deep nesting with many optional properties
- **Endpoint Tests**: Different from CEA tests, separate hierarchy
- **Handlebars Everywhere**: Almost all fields support templating

### **Success Criteria**
- ? All asset types fully modeled
- ? Handlebars support throughout
- ? Clean model hierarchy
- ? Zero compilation errors

---

## ?? **Phase 6.3.4: API Implementation**

### **Goal**: Implement interfaces, Refit APIs, implementations, and module

### **Files to Create (10-12)**

#### **Interfaces** (2 files)
```
ThousandEyes.Api/Interfaces/Templates/
??? ITemplates.cs                    # Public interface (8 operations)

ThousandEyes.Api/Refit/Templates/
??? ITemplatesRefitApi.cs            # Internal Refit interface
```

#### **Implementation** (1 file)
```
ThousandEyes.Api/Implementations/Templates/
??? TemplatesImpl.cs                 # Implementation wrapping Refit
```

#### **Module** (1 file)
```
ThousandEyes.Api/Modules/
??? TemplatesModule.cs               # Public module (8 operations)
```

#### **Client Integration** (2 files - update existing)
```
ThousandEyes.Api/
??? IThousandEyesClient.cs           # Add Templates property
??? ThousandEyesClient.cs            # Initialize Templates
```

#### **Helper/Utility Models** (4-6 files)
```
ThousandEyes.Api/Models/Templates/
??? HandlebarsExpression.cs          # Handlebars string type
??? StringTemplate.cs                # anyOf<string, Handlebars>
??? NumberTemplate.cs                # anyOf<number, Handlebars>
??? BooleanNumber.cs                 # 0 or 1 for boolean flags
??? WidgetType.cs                    # Dashboard widget types enum
??? MetricGroup.cs                   # Dashboard metric groups enum
```

### **Interface Design**

```csharp
public interface ITemplates
{
    // CRUD Operations
    Task<TemplatesResponse> GetAllAsync(
        string? aid, 
        CertificationLevel? certificationLevel, 
        TemplateModule? module, 
        string? name, 
        CancellationToken cancellationToken);
    
    Task<TemplateResponse> CreateAsync(
        Template request, 
        string? aid, 
        CancellationToken cancellationToken);
    
    Task<TemplateResponse> GetByIdAsync(
        string id, 
        string? aid, 
        CancellationToken cancellationToken);
    
    Task<TemplateResponse> UpdateAsync(
        string id, 
        Template request, 
        string? aid, 
        CancellationToken cancellationToken);
    
    Task DeleteAsync(
        string id, 
        string? aid, 
        CancellationToken cancellationToken);
    
    // Deployment
    Task<TemplateResponse> DeployAsync(
        string id, 
        DeployTemplate request, 
        string? aid, 
        CancellationToken cancellationToken);
    
    // Sharing Settings
    Task<SharingSettingsResponse> GetSharingSettingsAsync(
        string id, 
        string? aid, 
        CancellationToken cancellationToken);
    
    Task<SharingSettingsResponse> UpdateSharingSettingsAsync(
        string id, 
        SharingSettings request, 
        string? aid, 
        CancellationToken cancellationToken);
}
```

### **Success Criteria**
- ? All 8 endpoints implemented
- ? Clean interface design
- ? Proper error handling
- ? Module follows established patterns
- ? Zero compilation errors

---

## ?? **Phase 6.3.5: Testing & Validation**

### **Goal**: Comprehensive integration tests for all operations

### **Files to Create (1)**

```
ThousandEyes.Api.Test/
??? TemplatesModuleTests.cs          # 6-8 integration tests
```

### **Test Coverage Plan**

```csharp
[Collection("Integration Tests")]
public class TemplatesModuleTests
{
    // Basic CRUD (5 tests)
    1. GetTemplates_WithValidRequest_ReturnsTemplates
    2. CreateTemplate_WithValidRequest_CreatesTemplate
    3. GetTemplate_WithValidId_ReturnsTemplate
    4. UpdateTemplate_WithValidRequest_UpdatesTemplate
    5. DeleteTemplate_WithValidId_DeletesTemplate
    
    // Advanced Operations (3 tests)
    6. DeployTemplate_WithUserInputs_DeploysSuccessfully
    7. GetSharingSettings_WithValidId_ReturnsSettings
    8. UpdateSharingSettings_WithValidRequest_UpdatesSettings
}
```

### **Test Strategy**

1. **Simple template creation** - basic template without complex assets
2. **Template with tests** - template containing test definitions
3. **Template deployment** - deploy template with user inputs
4. **Cleanup** - delete all created templates in cleanup phase

### **Test Challenges**

- **Complex JSON structures** - templates with nested assets
- **Handlebars validation** - ensure templating works correctly
- **Deployment testing** - may create real test assets
- **Cleanup complexity** - need to clean up deployed assets too

### **Success Criteria**
- ? 6-8 comprehensive integration tests
- ? All CRUD operations tested
- ? Deployment operation tested
- ? Sharing settings tested
- ? Clean test data management
- ? 100% test success rate

---

## ?? **Overall File Estimate**

| Phase | Component | Files | Status |
|-------|-----------|-------|--------|
| 6.3.1 | Core Template Models | 15-20 | ?? Planned |
| 6.3.2 | Test Configuration Models | 30-40 | ?? Planned |
| 6.3.3 | Supporting Asset Models | 20-25 | ?? Planned |
| 6.3.4 | API Implementation | 10-12 | ?? Planned |
| 6.3.5 | Testing | 1 | ?? Planned |
| **TOTAL** | **All Components** | **76-98** | **?? Target** |

**Conservative Estimate**: 80 files
**Realistic Estimate**: 90 files
**Maximum Estimate**: 100 files

---

## ?? **Key Design Decisions**

### **1. Handlebars Templating Approach**

Use a flexible type that supports both concrete values and Handlebars expressions:

```csharp
// Option A: anyOf with custom converter (more type-safe)
public class StringTemplate
{
    public string? Value { get; set; }
    public bool IsHandlebars { get; set; }
}

// Option B: Simple string with pattern validation (simpler)
// Just use string everywhere and let API validate Handlebars syntax
```

**Decision**: Use **Option B** (simple strings) for Phase 1, can enhance later if needed.

### **2. Complex Nested Structures**

For deeply nested structures like dashboards:

```csharp
// Option A: Fully typed (100+ files)
public class DashboardConfigurationTemplate
{
    public string? Title { get; set; }
    public List<DashboardWidgetTemplate> Widgets { get; set; } = [];
}

// Option B: Use JsonElement for flexibility (fewer files)
public class DashboardConfigurationTemplate
{
    public string? Title { get; set; }
    public JsonElement? Widgets { get; set; }
}
```

**Decision**: Use **Option A** (fully typed) - we're going for full complexity!

### **3. Test Type Polymorphism**

```csharp
[JsonPolymorphic(TypeDiscriminatorPropertyName = "type")]
[JsonDerivedType(typeof(AgentToServerTestTemplate), "agent-to-server")]
[JsonDerivedType(typeof(HttpServerTestTemplate), "http-server")]
[JsonDerivedType(typeof(PageLoadTestTemplate), "page-load")]
// ... all 9 types
public abstract class TestConfigurationTemplate
{
    [JsonPropertyName("type")]
    public required string Type { get; set; }
}
```

### **4. Dictionary vs Typed Models**

For template asset collections:

```csharp
// The Template model uses dictionaries for flexibility
public class Template
{
    [JsonPropertyName("tests")]
    public Dictionary<string, TestConfigurationTemplate> Tests { get; set; } = [];
    
    [JsonPropertyName("alertRules")]
    public Dictionary<string, AlertRuleConfigurationTemplate> AlertRules { get; set; } = [];
}
```

This allows dynamic key naming while maintaining type safety for values.

---

## ?? **Known Challenges**

### **1. Massive Scope**
- **Challenge**: 80-100 files is the largest single API implementation
- **Mitigation**: Break into 5 clear sub-phases, tackle one at a time

### **2. Handlebars Complexity**
- **Challenge**: Almost every field can be templated with Handlebars
- **Mitigation**: Use string types, let API handle validation

### **3. Test Configuration Variety**
- **Challenge**: 9 different test types with unique configurations
- **Mitigation**: Strong inheritance hierarchy, reuse base classes

### **4. Deep Nesting**
- **Challenge**: Dashboard widgets have 5+ levels of nesting
- **Mitigation**: Create well-named intermediate types

### **5. Deployment Complexity**
- **Challenge**: Deploy operation creates real assets from template
- **Mitigation**: Careful test design with cleanup

---

## ? **Success Criteria - FULL IMPLEMENTATION**

### **Code Quality**
- ? Zero compilation errors across all ~90 files
- ? Zero warnings
- ? Zero messages
- ? All files follow "one file per type" pattern
- ? Modern .NET 9 patterns throughout
- ? Comprehensive XML documentation

### **Functionality**
- ? All 8 API endpoints implemented
- ? Full support for all 9 test types
- ? Complete endpoint test configurations
- ? Labels, alert rules, dashboards, dashboard filters
- ? User inputs with Handlebars support
- ? Deployment strategies
- ? Sharing settings management

### **Testing**
- ? 6-8 integration tests
- ? 100% test success rate
- ? CRUD operations validated
- ? Deployment operation tested
- ? Clean test data management

### **Architecture**
- ? Clean inheritance hierarchies
- ? Proper polymorphism for test types
- ? Reusable base classes
- ? Clear separation of concerns
- ? Follows established patterns from previous phases

---

## ?? **Implementation Strategy**

### **Session 1: Foundation (Phase 6.3.1)**
- Core template models
- User inputs
- Enums and base types
- ~20 files
- **Validate with build**

### **Session 2: The Mountain (Phase 6.3.2)**
- All test type models
- Test base classes
- Network test configurations
- ~35 files
- **Validate with build**

### **Session 3: Supporting Cast (Phase 6.3.3)**
- Labels, alert rules
- Endpoint tests
- Dashboards and filters
- ~23 files
- **Validate with build**

### **Session 4: API & Testing (Phase 6.3.4 + 6.3.5)**
- Interfaces and implementations
- Module creation
- Client integration
- Integration tests
- ~12 files
- **Validate with build and tests**

---

## ?? **The Mountain Awaits! ???**

This is the **most ambitious single API implementation** in the ThousandEyes library. When complete, it will provide:

- ? **Complete template management**
- ? **All 9 test type configurations**
- ? **Full Handlebars templating support**
- ? **Deployment automation**
- ? **Infrastructure as code capabilities**
- ? **Reusable monitoring templates**

**Let's climb this mountain! ??**

---

**Ready to start Phase 6.3.1: Core Template Models!**
