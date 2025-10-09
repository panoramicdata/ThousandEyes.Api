# ?? Phase 5.1: Integrations API - Implementation Plan

## ?? Overview

Implementing the **Integrations API** for Phase 5.1, which enables webhook and third-party service integrations (Slack, PagerDuty, ServiceNow, etc.) for the ThousandEyes platform.

---

## ?? **API Specification**

- **Base URL**: `https://api.thousandeyes.com/v7`
- **Specification File**: `Specification/integrations_api_7_0_63.yaml`
- **Priority**: High - Critical for enterprise automation
- **Complexity**: Medium-High (polymorphic authentication models, complex relationships)

---

## ?? **API Endpoints** (10 Total)

### **1. Webhook Operations** (5 endpoints)
```
GET    /operations/webhooks                    # List webhook operations
POST   /operations/webhooks                    # Create webhook operation
GET    /operations/webhooks/{id}               # Get webhook operation
PUT    /operations/webhooks/{id}               # Update webhook operation
DELETE /operations/webhooks/{id}               # Delete webhook operation
```

### **2. Generic Connectors** (5 endpoints)
```
GET    /connectors/generic                     # List connectors
POST   /connectors/generic                     # Create connector
GET    /connectors/generic/{id}                # Get connector
PUT    /connectors/generic/{id}                # Update connector
DELETE /connectors/generic/{id}                # Delete connector
```

### **3. Operation Connectors** (4 endpoints)
```
GET    /operations/{type}/{id}/connectors      # Get operation connectors
PUT    /operations/{type}/{id}/connectors      # Set operation connectors
GET    /connectors/generic/{id}/operations     # List connector operations
PUT    /connectors/generic/{id}/operations     # Set connector operations
```

**Total**: 10 unique endpoints (4 overlap conceptually)

---

## ?? **File Structure** (One File Per Type)

### **Models** (`ThousandEyes.Api/Models/Integrations/`) - ~25 files

#### Core Models (5 files)
1. `WebhookOperation.cs` - Webhook operation configuration
2. `GenericConnector.cs` - Generic connector configuration
3. `Assignments.cs` - Response wrapper for assignments (inherits ApiResource)
4. `Header.cs` - HTTP header configuration
5. `WebhookOperations.cs` - Response wrapper (inherits ApiResource)
6. `GenericConnectors.cs` - Response wrapper (inherits ApiResource)

#### Enums (4 files)
7. `OperationCategory.cs` - Enum (Alerts, Recommendations, TrafficMonitoring)
8. `OperationStatus.cs` - Enum (Pending, Connected, Failing, Unverified)
9. `OperationType.cs` - Enum (Webhook)
10. `ConnectorType.cs` - Enum (Generic)
11. `AuthenticationType.cs` - Enum (Basic, BearerToken, OAuthAuthCode, OAuthClientCredentials, OtherToken)

#### Authentication Models (6 files) - Polymorphic Design
12. `ConnectorAuthentication.cs` - Base class for authentication
13. `BasicAuthentication.cs` - Username/password authentication
14. `BearerTokenAuthentication.cs` - Bearer token authentication
15. `OtherTokenAuthentication.cs` - Custom token authentication
16. `OAuthCodeAuthentication.cs` - OAuth authorization code flow
17. `OAuthClientCredentialsAuthentication.cs` - OAuth client credentials flow

### **Public Interfaces** (`ThousandEyes.Api/Interfaces/Integrations/`) - 3 files
18. `IWebhookOperations.cs` - Webhook operations interface
19. `IGenericConnectors.cs` - Generic connectors interface
20. `IOperationConnectors.cs` - Operation-connector mapping interface

### **Internal Refit Interfaces** (`ThousandEyes.Api/Refit/Integrations/`) - 3 files
21. `IWebhookOperationsRefitApi.cs` - Refit interface for webhooks
22. `IGenericConnectorsRefitApi.cs` - Refit interface for connectors
23. `IOperationConnectorsRefitApi.cs` - Refit interface for mappings

### **Implementations** (`ThousandEyes.Api/Implementations/Integrations/`) - 3 files
24. `WebhookOperationsImpl.cs` - Webhook operations implementation
25. `GenericConnectorsImpl.cs` - Generic connectors implementation
26. `OperationConnectorsImpl.cs` - Operation-connector mapping implementation

### **Module** (`ThousandEyes.Api/Modules/`) - 1 file
27. `IntegrationsModule.cs` - Module wrapper

### **Integration Tests** (`ThousandEyes.Api.Test/`) - 1 file
28. `IntegrationsModuleTests.cs` - 10 integration tests

**Total Files**: 28 files

---

## ?? **Key Design Challenges**

### **1. Polymorphic Authentication**
The API uses **discriminated unions** for authentication types. This requires careful JSON serialization handling:

```csharp
// Base class approach
public abstract class ConnectorAuthentication
{
    public required AuthenticationType Type { get; set; }
}

// Derived classes
public class BasicAuthentication : ConnectorAuthentication
{
    public required string Username { get; set; }
    public required string Password { get; set; }
}

public class OAuthCodeAuthentication : ConnectorAuthentication
{
    public required string Code { get; set; }
    public required string OAuthAuthUrl { get; set; }
    // ... more properties
}
```

**JSON Serialization Strategy**:
- Use `JsonDerivedType` attribute (System.Text.Json built-in polymorphism)
- Type discriminator: `"type": "basic"` ? `BasicAuthentication`

### **2. Operation-Connector Relationships**
Operations can be assigned to connectors (many-to-many relationship):
- One operation can have multiple connectors
- One connector can have multiple operations

**API Design**:
- `PUT /operations/{type}/{id}/connectors` - Assigns connectors to an operation
- `PUT /connectors/generic/{id}/operations` - Assigns operations to a connector

### **3. Handlebars Templates**
Webhook operations support **Handlebars templates** for:
- Payload customization: `"{\"property1\": {{numericVar}}}"`
- Query parameters: `"{\"queryParam1\":\"{{stringVar}}\"}"`

**Implementation**: Store as strings, no template validation (client-side only)

---

## ?? **Implementation Strategy**

### **Stage 1: Core Models & Enums** (30 minutes)
Create 17 model files:
- Enums (5 files)
- Core models (6 files)
- Authentication models (6 files)

### **Stage 2: Response Wrappers** (10 minutes)
Create 3 response wrapper models:
- `Assignments.cs` (inherits ApiResource)
- `WebhookOperations.cs` (inherits ApiResource)
- `GenericConnectors.cs` (inherits ApiResource)

### **Stage 3: Interfaces** (20 minutes)
Create 6 interface files:
- 3 public interfaces
- 3 Refit interfaces

### **Stage 4: Implementations** (15 minutes)
Create 3 implementation files

### **Stage 5: Module & Integration** (15 minutes)
- Create IntegrationsModule
- Update ThousandEyesClient
- Update IThousandEyesClient interface

### **Stage 6: Integration Tests** (30 minutes)
Create IntegrationsModuleTests with 10 tests:
1. `GetWebhookOperations_WithValidRequest_ReturnsOperations`
2. `CreateWebhookOperation_WithValidRequest_CreatesOperation`
3. `GetWebhookOperation_WithValidId_ReturnsOperation`
4. `UpdateWebhookOperation_WithValidRequest_UpdatesOperation`
5. `DeleteWebhookOperation_WithValidId_DeletesOperation`
6. `GetGenericConnectors_WithValidRequest_ReturnsConnectors`
7. `CreateGenericConnector_WithValidRequest_CreatesConnector`
8. `GetOperationConnectors_WithValidOperationId_ReturnsConnectors`
9. `SetOperationConnectors_WithValidRequest_AssignsConnectors`
10. `SetConnectorOperations_WithValidRequest_AssignsOperations`

### **Stage 7: Validation** (10 minutes)
- Run build
- Fix any errors/warnings
- Verify zero diagnostics

**Total Estimated Time**: ~2.5 hours

---

## ?? **Model Design Guidelines**

### **Authentication Polymorphism**
```csharp
[JsonPolymorphic(TypeDiscriminatorPropertyName = "type")]
[JsonDerivedType(typeof(BasicAuthentication), "basic")]
[JsonDerivedType(typeof(BearerTokenAuthentication), "bearer-token")]
[JsonDerivedType(typeof(OtherTokenAuthentication), "other-token")]
[JsonDerivedType(typeof(OAuthCodeAuthentication), "oauth-auth-code")]
[JsonDerivedType(typeof(OAuthClientCredentialsAuthentication), "oauth-client-credentials")]
public abstract class ConnectorAuthentication
{
    public required AuthenticationType Type { get; set; }
}
```

### **Response Wrappers**
```csharp
public class Assignments : ApiResource
{
    public string[] Items { get; set; } = [];
}

public class WebhookOperations : ApiResource
{
    [JsonPropertyName("items")]
    public WebhookOperation[] OperationsList { get; set; } = [];
}
```

---

## ? **Success Criteria**

1. ? **Zero build errors** across entire solution
2. ? **Zero build warnings** across entire solution
3. ? **All 10 endpoints implemented** with full CRUD where applicable
4. ? **28 new files** following "one file per type" pattern
5. ? **Polymorphic authentication** working correctly with JSON serialization
6. ? **10 integration tests** ready for validation
7. ? **Models use base classes** where appropriate
8. ? **Modern .NET 9 patterns** maintained
9. ? **Comprehensive XML documentation**
10. ? **Zero technical debt**

---

## ?? **Benefits Delivered**

### **Business Value**
- **Webhook Integrations**: Custom webhook endpoints for automation
- **Third-Party Connectors**: Slack, PagerDuty, ServiceNow integrations
- **Flexible Authentication**: Support for basic, bearer, OAuth flows
- **Operation Mapping**: Assign alerts/recommendations to multiple channels
- **Template Customization**: Handlebars templates for payload customization

### **Technical Value**
- **Polymorphic Design**: Advanced JSON serialization patterns
- **Relationship Management**: Many-to-many operation-connector mapping
- **Complete CRUD**: Full lifecycle management for webhooks and connectors
- **Production-Ready**: Comprehensive error handling and validation

---

**Ready to begin implementation of Phase 5.1: Integrations API!**
