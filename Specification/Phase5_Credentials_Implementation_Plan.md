# Phase 5.2 Implementation Plan: Credentials API

## ?? Overview

Implement the Credentials API for managing secure credentials used in ThousandEyes transaction tests. This API provides CRUD operations for credentials that store encrypted values (passwords, tokens, API keys) for use in browser-based transaction tests.

---

## ?? API Endpoints (5 Operations)

### **Credentials Operations**
```
GET    /credentials              # List all credentials
POST   /credentials              # Create new credential
GET    /credentials/{id}         # Get credential details (with encrypted value)
PUT    /credentials/{id}         # Update credential
DELETE /credentials/{id}         # Delete credential
```

### **Key Characteristics**
- **Simple CRUD API**: No complex relationships or nested resources
- **Encrypted Values**: Credential values are encrypted by the API
- **Sensitive Data Permission**: Special permission required to view encrypted values
- **Account Group Context**: All operations support optional `aid` parameter
- **Response Variations**: 
  - List/Create/Update return `CredentialWithoutValue` (no encrypted value)
  - Get by ID returns `Credential` (includes encrypted value if permission granted)

---

## ?? Files to Create (16 Total)

### **Models** (3 files)
```
ThousandEyes.Api/Models/Credentials/
??? Credential.cs                     # Full credential with encrypted value
??? CredentialWithoutValue.cs         # Credential without value (inherits from ApiResource)
??? Credentials.cs                    # List response wrapper (inherits from ApiResource)
```

**Note**: No request-only model needed - will use `CredentialRequest` record for create/update

### **Interfaces** (3 files)
```
ThousandEyes.Api/Interfaces/Credentials/
??? ICredentials.cs                   # Public interface for client use

ThousandEyes.Api/Refit/Credentials/
??? ICredentialsRefitApi.cs           # Internal Refit interface
```

### **Implementations** (1 file)
```
ThousandEyes.Api/Implementations/Credentials/
??? CredentialsImpl.cs                # Implementation wrapping Refit calls
```

### **Module** (1 file)
```
ThousandEyes.Api/Modules/
??? CredentialsModule.cs              # Module registration
```

### **Tests** (1 file)
```
ThousandEyes.Api.Test/
??? CredentialsModuleTests.cs         # Integration tests (6-8 tests)
```

### **Client Updates** (2 files)
```
ThousandEyes.Api/
??? ThousandEyesClient.cs             # Add ICredentials property
??? Interfaces/IThousandEyesClient.cs # Add ICredentials property
```

### **Model Details**

#### `CredentialWithoutValue.cs` (inherits ApiResource)
```csharp
public class CredentialWithoutValue : ApiResource
{
    [JsonPropertyName("id")]
    public string? Id { get; set; }
    
    [JsonPropertyName("name")]
    public string? Name { get; set; }
}
```

#### `Credential.cs` (inherits CredentialWithoutValue)
```csharp
public class Credential : CredentialWithoutValue
{
    [JsonPropertyName("value")]
    public string? Value { get; set; }
}
```

#### `Credentials.cs` (inherits ApiResource)
```csharp
public class Credentials : ApiResource
{
    [JsonPropertyName("credentials")]
    public List<Credential> Items { get; set; } = [];
}
```

#### `CredentialRequest` (record for create/update)
```csharp
public record CredentialRequest(
    [property: JsonPropertyName("name")] string Name,
    [property: JsonPropertyName("value")] string Value
);
```

---

## ?? Interface Design

### **ICredentials.cs** (Public Interface)
```csharp
public interface ICredentials
{
    /// <summary>
    /// Retrieves a list of credentials configured in ThousandEyes.
    /// </summary>
    /// <param name="aid">Optional account group ID</param>
    /// <param name="cancellationToken">Cancellation token</param>
    Task<Credentials> GetAllAsync(string? aid, CancellationToken cancellationToken);
    
    /// <summary>
    /// Creates a new credential for ThousandEyes transaction tests.
    /// </summary>
    /// <param name="request">Credential details (name and value)</param>
    /// <param name="aid">Optional account group ID</param>
    /// <param name="cancellationToken">Cancellation token</param>
    Task<CredentialWithoutValue> CreateAsync(CredentialRequest request, string? aid, CancellationToken cancellationToken);
    
    /// <summary>
    /// Retrieves detailed information about a credential, including the encrypted value.
    /// Requires "View sensitive data in web transaction scripts" permission to view the value.
    /// </summary>
    /// <param name="id">Credential ID</param>
    /// <param name="aid">Optional account group ID</param>
    /// <param name="cancellationToken">Cancellation token</param>
    Task<Credential> GetByIdAsync(string id, string? aid, CancellationToken cancellationToken);
    
    /// <summary>
    /// Updates the credential for ThousandEyes transaction tests.
    /// </summary>
    /// <param name="id">Credential ID</param>
    /// <param name="request">Updated credential details</param>
    /// <param name="aid">Optional account group ID</param>
    /// <param name="cancellationToken">Cancellation token</param>
    Task<CredentialWithoutValue> UpdateAsync(string id, CredentialRequest request, string? aid, CancellationToken cancellationToken);
    
    /// <summary>
    /// Deletes a ThousandEyes transaction test credential.
    /// </summary>
    /// <param name="id">Credential ID</param>
    /// <param name="aid">Optional account group ID</param>
    /// <param name="cancellationToken">Cancellation token</param>
    Task DeleteAsync(string id, string? aid, CancellationToken cancellationToken);
}
```

### **ICredentialsRefitApi.cs** (Internal Refit Interface)
```csharp
internal interface ICredentialsRefitApi
{
    [Get("/credentials")]
    Task<Credentials> GetAllAsync([Query] string? aid, CancellationToken cancellationToken);
    
    [Post("/credentials")]
    Task<CredentialWithoutValue> CreateAsync([Body] CredentialRequest request, [Query] string? aid, CancellationToken cancellationToken);
    
    [Get("/credentials/{id}")]
    Task<Credential> GetByIdAsync(string id, [Query] string? aid, CancellationToken cancellationToken);
    
    [Put("/credentials/{id}")]
    Task<CredentialWithoutValue> UpdateAsync(string id, [Body] CredentialRequest request, [Query] string? aid, CancellationToken cancellationToken);
    
    [Delete("/credentials/{id}")]
    Task DeleteAsync(string id, [Query] string? aid, CancellationToken cancellationToken);
}
```

---

## ?? Integration Tests (6-8 Tests)

### **Test Coverage Plan**
```csharp
[Collection("Integration Tests")]
public class CredentialsModuleTests
{
    // CRUD Operations (5 tests)
    1. GetCredentials_WithValidRequest_ReturnsCredentials
    2. CreateCredential_WithValidRequest_CreatesCredential
    3. GetCredential_WithValidId_ReturnsCredentialWithValue
    4. UpdateCredential_WithValidRequest_UpdatesCredential
    5. DeleteCredential_WithValidId_DeletesCredential
    
    // Security & Permissions (2-3 tests)
    6. CreateCredential_WithSensitiveValue_EncryptsValue
    7. GetAllCredentials_DoesNotReturnValues  // List returns CredentialWithoutValue
    8. GetCredential_WithoutPermission_ReturnsCredentialWithoutValue (optional)
}
```

### **Test Implementation Notes**
- **Create test credentials**: Use descriptive names like "Test Credential - {timestamp}"
- **Cleanup**: Delete all test credentials in cleanup phase
- **Value validation**: Verify encrypted values are different from plain text
- **Permission testing**: May require separate test account without sensitive data permission

---

## ?? Implementation Steps

### **Step 1: Models** (3 files)
1. Create `Credential.cs` - full model with value
2. Create `CredentialWithoutValue.cs` - base model (inherits ApiResource)
3. Create `Credentials.cs` - list wrapper (inherits ApiResource)

### **Step 2: Interfaces** (2 files)
4. Create `ICredentials.cs` - public interface
5. Create `ICredentialsRefitApi.cs` - internal Refit interface

### **Step 3: Implementation** (1 file)
6. Create `CredentialsImpl.cs` - implementation with primary constructor

### **Step 4: Module** (1 file)
7. Create `CredentialsModule.cs` - register Refit client and implementation

### **Step 5: Client Integration** (2 files)
8. Update `IThousandEyesClient.cs` - add `ICredentials Credentials { get; }`
9. Update `ThousandEyesClient.cs` - implement property

### **Step 6: Tests** (1 file)
10. Create `CredentialsModuleTests.cs` - 6-8 integration tests

### **Step 7: Validation**
11. Run `get_errors` on all new files
12. Fix any compilation errors
13. Run `run_build` to validate entire solution
14. Verify zero errors, warnings, messages

---

## ?? Expected Outcomes

### **Code Quality**
- ? Zero compilation errors
- ? Zero warnings
- ? Zero messages
- ? All files follow "one file per type" pattern
- ? Modern .NET 9 patterns (primary constructors, collection expressions, file-scoped namespaces)
- ? Comprehensive XML documentation

### **Functionality**
- ? Full CRUD operations for credentials
- ? Account group context support
- ? Encrypted value handling
- ? Proper inheritance (ApiResource base class)
- ? Clean separation of models (with/without value)

### **Testing**
- ? 6-8 integration tests
- ? 100% test success rate target
- ? Real credential creation and deletion
- ? Value encryption validation
- ? Clean test data management

---

## ?? Key Design Decisions

### **1. Model Inheritance**
- `CredentialWithoutValue` inherits from `ApiResource` (has `_links`)
- `Credential` inherits from `CredentialWithoutValue` (adds `value` property)
- `Credentials` inherits from `ApiResource` (list wrapper)

### **2. Request Model**
- Use simple record `CredentialRequest(string Name, string Value)` for create/update
- Keeps request models clean and concise

### **3. Response Variations**
- List operations return `Credentials` containing `Credential[]` (with values if permission granted)
- Create/Update return `CredentialWithoutValue` (no value in response)
- Get by ID returns `Credential` (with encrypted value if permission granted)

### **4. Security Considerations**
- Values are encrypted by API, not client
- Encrypted values are different from plain text input
- Special permission required to view encrypted values
- Tests should validate encryption occurs

---

## ?? Learning Points

### **Simple CRUD API**
Unlike Integrations API with polymorphic models and complex relationships, Credentials API is straightforward:
- No polymorphism needed
- No nested resources
- Standard CRUD pattern
- Clean inheritance hierarchy

### **Sensitive Data Handling**
- Client submits plain text values
- API handles encryption
- Encrypted values returned in responses
- Permission-based value visibility

### **Model Variations**
- Single model with optional properties vs. multiple models
- Chose multiple models for clarity (CredentialWithoutValue vs. Credential)
- Better type safety and explicit intent

---

## ?? Implementation Checklist

- [ ] Create 3 model files (Credential, CredentialWithoutValue, Credentials)
- [ ] Create 2 interface files (ICredentials, ICredentialsRefitApi)
- [ ] Create 1 implementation file (CredentialsImpl)
- [ ] Create 1 module file (CredentialsModule)
- [ ] Update 2 client files (ThousandEyesClient, IThousandEyesClient)
- [ ] Create 1 test file (CredentialsModuleTests with 6-8 tests)
- [ ] Validate all files with get_errors
- [ ] Run full build with run_build
- [ ] Verify zero errors/warnings/messages
- [ ] Mark Phase 5.2 complete

---

## ?? Success Criteria

1. ? **16 files created** following "one file per type" pattern
2. ? **5 API operations implemented** with full CRUD
3. ? **Zero build errors** across entire solution
4. ? **Zero build warnings** across entire solution
5. ? **6-8 integration tests** ready for validation
6. ? **Model inheritance** using ApiResource base class
7. ? **Modern .NET 9 patterns** maintained
8. ? **Comprehensive XML documentation**
9. ? **Zero technical debt**
10. ? **100% test success rate** target

---

**Ready to implement Phase 5.2! ??**

**Estimated Time**: 1-1.5 hours
**Complexity**: Low (simple CRUD API)
**Dependencies**: None (self-contained module)
