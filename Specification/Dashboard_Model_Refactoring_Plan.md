# ?? DASHBOARDS API - MODEL REFACTORING PLAN

## ?? **Objective**
Refactor all Dashboard API models to use proper inheritance, eliminate code duplication, and follow DRY principles with common base classes for shared properties.

---

## ??? **Base Class Hierarchy Design**

### **Level 1: Core Base Classes**

#### **`ApiResource` (Root Base Class)**
```csharp
/// <summary>
/// Base class for all API resources with HAL links
/// </summary>
public abstract class ApiResource
{
    /// <summary>
    /// Navigation links (HAL)
    /// </summary>
    [JsonPropertyName("_links")]
    public Links? Links { get; set; }
}
```

#### **`AccountGroupResource` : `ApiResource`**
```csharp
/// <summary>
/// Base class for resources that belong to an account group
/// </summary>
public abstract class AccountGroupResource : ApiResource
{
    /// <summary>
    /// Account group ID
    /// </summary>
    public string? Aid { get; set; }
}
```

#### **`AuditableResource` : `AccountGroupResource`**
```csharp
/// <summary>
/// Base class for resources that track creation and modification
/// </summary>
public abstract class AuditableResource : AccountGroupResource
{
    /// <summary>
    /// When the resource was created
    /// </summary>
    public DateTime? CreatedDate { get; set; }
    
    /// <summary>
    /// When the resource was last modified
    /// </summary>
    public DateTime? ModifiedDate { get; set; }
    
    /// <summary>
    /// User who created the resource
    /// </summary>
    public string? CreatedBy { get; set; }
    
    /// <summary>
    /// User who last modified the resource
    /// </summary>
    public string? ModifiedBy { get; set; }
}
```

### **Level 2: Dashboard-Specific Base Classes**

#### **`DashboardResourceBase` : `AuditableResource`**
```csharp
/// <summary>
/// Base class for dashboard-related resources
/// </summary>
public abstract class DashboardResourceBase : AuditableResource
{
    /// <summary>
    /// Unique identifier
    /// </summary>
    public required string Id { get; set; }
    
    /// <summary>
    /// Resource title/name
    /// </summary>
    public required string Title { get; set; }
    
    /// <summary>
    /// Resource description
    /// </summary>
    public string? Description { get; set; }
}
```

---

## ?? **Model Refactoring Plan**

### **Phase 1: Create Base Classes**

#### **Files to Create:**
1. ? `ApiResource.cs` - Root base with `_links`
2. ? `AccountGroupResource.cs` - Adds `Aid` property
3. ? `AuditableResource.cs` - Adds audit tracking
4. ? `DashboardResourceBase.cs` - Dashboard-specific base
5. ? `PaginatedResponse.cs<T>` - Generic pagination wrapper
6. ? `Links.cs` - Rename from `DashboardLinks` to be generic
7. ? `Link.cs` - Already exists, verify it matches spec

### **Phase 2: Refactor Dashboard Models**

#### **Dashboard.cs** : `DashboardResourceBase`
```csharp
public class Dashboard : DashboardResourceBase
{
    // Inherited: Id, Title, Description, Aid, CreatedDate, ModifiedDate, CreatedBy, ModifiedBy, Links
    
    // Dashboard-specific properties only:
    public bool IsBuiltIn { get; set; }
    public bool IsPrivate { get; set; }
    public bool IsDefaultForUser { get; set; }
    public bool IsDefaultForAccount { get; set; }
    public DashboardWidget[] Widgets { get; set; } = [];
    public DashboardTimeSpan? DefaultTimespan { get; set; }
    public bool IsGlobalOverride { get; set; }
    
    // Note: DashboardId maps to Id, DashboardCreatedBy maps to CreatedBy, etc.
}
```

**Property Mapping:**
- ? Remove: `DashboardId` ? Use `Id` from base
- ? Remove: `Aid` ? Inherited from `AccountGroupResource`
- ? Remove: `Links` ? Inherited from `ApiResource`
- ? Remove: `DashboardCreatedBy` ? Use `CreatedBy` from base
- ? Remove: `DashboardModifiedBy` ? Use `ModifiedBy` from base
- ? Remove: `DashboardModifiedDate` ? Use `ModifiedDate` from base
- ? Keep: `Title`, `Description` (defined in `DashboardResourceBase`)
- ? Keep: All dashboard-specific properties

#### **DashboardSnapshot.cs** : `AuditableResource`
```csharp
public class DashboardSnapshot : AuditableResource
{
    // Inherited: Aid, CreatedDate, ModifiedDate, CreatedBy, ModifiedBy, Links
    
    public required string SnapshotId { get; set; }
    public required string SnapshotName { get; set; }
    public bool IsShared { get; set; }
    public DateTime? SnapshotExpirationDate { get; set; }
    public bool IsScheduled { get; set; }
    public Dashboard? Dashboard { get; set; }
    public DashboardWidget[] Widgets { get; set; } = [];
    public SnapshotTimeSpan? TimeSpan { get; set; }
}
```

**Property Mapping:**
- ? Remove: `Aid` ? Inherited
- ? Remove: `Links` ? Inherited
- ? Remove: `SnapshotCreatedDate` ? Use `CreatedDate` from base
- ? Keep: All snapshot-specific properties

#### **DashboardFilterDetails.cs** : `AuditableResource`
```csharp
public class DashboardFilterDetails : AuditableResource
{
    // Inherited: Aid, CreatedDate, ModifiedDate, CreatedBy, ModifiedBy, Links
    
    public required string Id { get; set; }
    public required string Name { get; set; }
    public string? Description { get; set; }
    public DataSourceFilter[] Context { get; set; } = [];
}
```

**Property Mapping:**
- ? Remove: `Aid` ? Inherited
- ? Remove: `Links` ? Inherited
- ? Remove: `CreatedDate` ? Inherited
- ? Remove: `ModifiedDate` ? Inherited
- ? Remove: `CreatedBy` ? Already inherited, remove duplicate
- ? Remove: `ModifiedBy` ? Already inherited, remove duplicate
- ? Keep: Filter-specific properties

#### **FilterUserInfo.cs** - Rename to **`UserInfo.cs`** (Generic)
```csharp
/// <summary>
/// User information for audit tracking (generic, reusable)
/// </summary>
public class UserInfo
{
    public string? Uid { get; set; }
    public string? Name { get; set; }
}
```

**Change `AuditableResource` to use `UserInfo` type:**
```csharp
public abstract class AuditableResource : AccountGroupResource
{
    public DateTime? CreatedDate { get; set; }
    public DateTime? ModifiedDate { get; set; }
    public UserInfo? CreatedBy { get; set; }  // Changed from string
    public UserInfo? ModifiedBy { get; set; }  // Changed from string
}
```

### **Phase 3: Apply to All Modules**

#### **Scan All Existing Models:**
- ? **Account Management models** (AccountGroup, User, Role, etc.)
- ? **Tests models** (Test, HttpServerTest, etc.)
- ? **Agents models** (Agent, EnterpriseAgent, etc.)
- ? **Test Results models**
- ? **Alerts models** (Alert, AlertRule, etc.)
- ? **Dashboard models** (Dashboard, Snapshot, Filter)

#### **Common Patterns to Extract:**

**Pattern 1: Resources with `_links`**
```
??? AccountGroup ? ? Inherits from ApiResource
??? User ? ? Inherits from ApiResource  
??? Role ? ? Inherits from ApiResource
??? Alert ? ? Inherits from ApiResource
??? Dashboard ? ? Inherits from ApiResource
??? etc.
```

**Pattern 2: Resources with `aid`**
```
??? Test ? ? Inherits from AccountGroupResource
??? Agent ? ? Inherits from AccountGroupResource
??? AlertRule ? ? Inherits from AccountGroupResource
??? etc.
```

**Pattern 3: Resources with audit tracking**
```
??? User ? ? Inherits from AuditableResource
??? Dashboard ? ? Inherits from AuditableResource
??? DashboardFilter ? ? Inherits from AuditableResource
??? etc.
```

---

## ?? **Property Name Standardization**

### **Before (Inconsistent):**
```csharp
// Dashboard.cs
public string DashboardId { get; set; }
public string DashboardCreatedBy { get; set; }

// DashboardSnapshot.cs
public string SnapshotId { get; set; }
public DateTime SnapshotCreatedDate { get; set; }

// DashboardFilter.cs
public string Id { get; set; }
public DateTime CreatedDate { get; set; }
```

### **After (Consistent):**
```csharp
// All inherit from appropriate base class
public abstract class AuditableResource
{
    public string Id { get; set; }           // ? Consistent
    public DateTime? CreatedDate { get; set; } // ? Consistent
    public UserInfo? CreatedBy { get; set; }   // ? Consistent
}
```

---

## ?? **JSON Property Name Mapping Strategy**

### **Option 1: Add JsonPropertyName to Base Class Properties**
```csharp
public abstract class DashboardResourceBase : AuditableResource
{
    [JsonPropertyName("dashboardId")]  // Maps to API's dashboardId
    public required string Id { get; set; }
}
```

### **Option 2: Use Type-Specific Property Names (Recommended)**
Keep property names matching the API to avoid JsonPropertyName attributes:

```csharp
// Dashboard.cs - matches API exactly
public class Dashboard : AuditableResource
{
    public string DashboardId { get; set; }  // Matches API: dashboardId (via camelCase)
    // Inherited: Aid, Links, etc.
}
```

**Decision:** Use **Option 2** - property names should match API response names (using camelCase serialization)

---

## ?? **Implementation Checklist**

### **Step 1: Create Base Classes** ?
- [ ] Create `ApiResource.cs`
- [ ] Create `AccountGroupResource.cs`
- [ ] Create `AuditableResource.cs`
- [ ] Create `DashboardResourceBase.cs` (if needed)
- [ ] Rename `FilterUserInfo` ? `UserInfo` (make generic)
- [ ] Rename `DashboardLinks` ? `Links` (make generic)
- [ ] Create `PaginatedResponse<T>.cs` for paginated responses

### **Step 2: Refactor Dashboard Models** ?
- [ ] Update `Dashboard.cs` to inherit from base
- [ ] Update `DashboardSnapshot.cs` to inherit from base
- [ ] Update `DashboardFilterDetails.cs` to inherit from base
- [ ] Remove duplicate properties
- [ ] Update property names for consistency
- [ ] Add `[JsonPropertyName("_links")]` only where needed

### **Step 3: Update All Other Modules** ?
- [ ] Scan all models in `Models/AccountManagement/`
- [ ] Scan all models in `Models/Tests/`
- [ ] Scan all models in `Models/Agents/`
- [ ] Scan all models in `Models/TestResults/`
- [ ] Scan all models in `Models/Alerts/`
- [ ] Apply inheritance where applicable
- [ ] Standardize property names

### **Step 4: Update Tests** ?
- [ ] Update integration tests for property name changes
- [ ] Verify all tests pass with new model structure
- [ ] Add tests for base class inheritance

### **Step 5: Documentation** ?
- [ ] Update XML documentation for base classes
- [ ] Update usage examples in README
- [ ] Update implementation plan
- [ ] Create migration guide for property name changes

---

## ?? **Breaking Changes**

### **Property Renames:**
```csharp
// OLD ? NEW
DashboardId ? DashboardId (no change, but via inheritance)
DashboardName ? Title
DashboardType ? (needs investigation - might be missing)
DashboardsList ? Direct array return
DashboardCreatedBy ? CreatedBy (via inheritance)
DashboardModifiedDate ? ModifiedDate (via inheritance)
```

### **Array vs Object Responses:**
```csharp
// OLD
Task<Dashboards> GetAllAsync(...)  // Wrapped in object

// NEW  
Task<Dashboard[]> GetAllAsync(...)  // Direct array ?
```

---

## ?? **Benefits**

### **1. DRY Principle**
- ? No duplicate property definitions
- ? Common properties in base classes
- ? Easier to maintain and extend

### **2. Type Safety**
- ? Compiler catches missing base class properties
- ? Consistent property types across models
- ? Clearer inheritance hierarchy

### **3. Readability**
- ? Models show only unique properties
- ? Inheritance makes relationships clear
- ? Easier to understand model structure

### **4. Maintainability**
- ? Change base class once, affects all inheritors
- ? Consistent naming across all models
- ? Easier to add new common properties

---

## ?? **Impact Analysis**

### **Files to Create:** ~7
- Base class files
- Generic helper classes

### **Files to Modify:** ~60+
- All model files across all modules
- All test files using these models
- Interface/API implementation files

### **Estimated Effort:** 4-6 hours
- Creating base classes: 1 hour
- Refactoring Dashboard models: 1 hour
- Refactoring all other models: 2-3 hours
- Testing and verification: 1 hour

---

## ? **Success Criteria**

1. ? All models inherit from appropriate base classes
2. ? No duplicate property definitions across models
3. ? All tests pass (100% success rate)
4. ? Zero build warnings
5. ? Property names consistent across all models
6. ? JSON serialization/deserialization works correctly
7. ? Integration tests validate API responses

---

## ?? **Next Steps**

1. **Review and approve this plan**
2. **Create base classes**
3. **Refactor models incrementally (one module at a time)**
4. **Test after each module refactoring**
5. **Update documentation**
6. **Run full integration test suite**

This refactoring will significantly improve code quality and maintainability while eliminating code duplication across the entire solution.
