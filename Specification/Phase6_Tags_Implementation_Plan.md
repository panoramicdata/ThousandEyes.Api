# Phase 6.1 Implementation Plan: Tags API

## ?? Overview

Implement the Tags API for managing ThousandEyes asset tagging with key/value pairs. This API provides CRUD operations for tags and tag assignment management across tests, agents, dashboards, and endpoint tests.

---

## ?? API Endpoints (10 Operations)

### **Tag CRUD Operations** (6 operations)
```
GET    /tags                    # List all tags (with optional expand)
POST   /tags                    # Create single tag
POST   /tags/bulk               # Create multiple tags
GET    /tags/{id}               # Get tag details (with optional expand)
PUT    /tags/{id}               # Update tag
DELETE /tags/{id}               # Delete tag
```

### **Tag Assignment Operations** (4 operations)
```
POST   /tags/{id}/assign        # Assign tag to multiple objects
POST   /tags/{id}/unassign      # Remove tag from multiple objects
POST   /tags/assign             # Assign multiple tags to multiple objects (bulk)
POST   /tags/unassign           # Remove multiple tags from multiple objects (bulk)
```

### **Key Characteristics**
- **Tagging System**: Key/value pairs for asset metadata
- **Multiple Object Types**: test, dashboard, endpoint-test, v-agent
- **Assignment Management**: Cumulative tag assignment operations
- **Bulk Operations**: Efficient bulk create and assignment operations
- **Optional Expand**: Include assignments with `expand=assignments`
- **Account Group Context**: All operations support optional `aid` parameter
- **Color and Icons**: Visual customization support

---

## ?? Files to Create (15-17 Total)

### **Models** (5-6 files)
```
ThousandEyes.Api/Models/Tags/
??? TagInfo.cs                    # Base tag model (no _links)
??? Tag.cs                        # Full tag with _links (inherits TagInfo)
??? Tags.cs                       # List response wrapper (inherits ApiResource)
??? TagAssignment.cs              # Single tag assignment model
??? BulkTagAssignment.cs          # Bulk tag assignment model
??? BulkTagAssignments.cs         # Bulk assignments wrapper
??? BulkTagResponse.cs            # Bulk create response with errors
```

### **Enums** (3 files)
```
ThousandEyes.Api/Models/Tags/
??? ObjectType.cs                 # test, dashboard, endpoint-test, v-agent
??? AccessType.cs                 # all, partner, system
??? AssignmentType.cs             # test, v-agent, endpoint-test, dashboard
??? ExpandTagsOptions.cs          # assignments
```

### **Nested Models** (2 files)
```
ThousandEyes.Api/Models/Tags/
??? Assignment.cs                 # Single assignment (id, type)
??? TagBulkCreateError.cs         # Bulk create error details
```

### **Interfaces** (2 files)
```
ThousandEyes.Api/Interfaces/Tags/
??? ITags.cs                      # Public interface for client use

ThousandEyes.Api/Refit/Tags/
??? ITagsRefitApi.cs              # Internal Refit interface
```

### **Implementations** (1 file)
```
ThousandEyes.Api/Implementations/Tags/
??? TagsImpl.cs                   # Implementation wrapping Refit calls
```

### **Module** (1 file)
```
ThousandEyes.Api/Modules/
??? TagsModule.cs                 # Module with all operations
```

### **Tests** (1 file)
```
ThousandEyes.Api.Test/
??? TagsModuleTests.cs            # Integration tests (8-10 tests)
```

### **Client Updates** (2 files)
```
ThousandEyes.Api/
??? ThousandEyesClient.cs         # Add Tags property
??? Interfaces/IThousandEyesClient.cs # Add Tags property
```

---

## ??? Model Details

### **TagInfo.cs** (Base Model - No _links)
```csharp
public class TagInfo : ApiResource
{
    [JsonPropertyName("id")]
    public string? Id { get; set; }
    
    [JsonPropertyName("aid")]
    public long? Aid { get; set; }
    
    [JsonPropertyName("key")]
    public string? Key { get; set; }
    
    [JsonPropertyName("value")]
    public string? Value { get; set; }
    
    [JsonPropertyName("color")]
    public string? Color { get; set; }
    
    [JsonPropertyName("icon")]
    public string? Icon { get; set; }
    
    [JsonPropertyName("description")]
    public string? Description { get; set; }
    
    [JsonPropertyName("objectType")]
    public ObjectType? ObjectType { get; set; }
    
    [JsonPropertyName("accessType")]
    public AccessType? AccessType { get; set; }
    
    [JsonPropertyName("createDate")]
    public string? CreateDate { get; set; }
    
    [JsonPropertyName("legacyId")]
    public long? LegacyId { get; set; }
    
    [JsonPropertyName("assignments")]
    public List<Assignment> Assignments { get; set; } = [];
}
```

### **Tag.cs** (Inherits TagInfo)
```csharp
public class Tag : TagInfo
{
    // Inherits all properties from TagInfo
    // _links inherited from ApiResource
}
```

### **Tags.cs** (List Wrapper)
```csharp
public class Tags : ApiResource
{
    [JsonPropertyName("tags")]
    public List<Tag> Items { get; set; } = [];
}
```

### **Assignment.cs** (Nested Model)
```csharp
public class Assignment
{
    [JsonPropertyName("id")]
    public string? Id { get; set; }
    
    [JsonPropertyName("type")]
    public AssignmentType? Type { get; set; }
}
```

### **TagAssignment.cs** (Assignment Request)
```csharp
public class TagAssignment
{
    [JsonPropertyName("assignments")]
    public List<Assignment> Assignments { get; set; } = [];
}
```

### **BulkTagAssignment.cs**
```csharp
public class BulkTagAssignment
{
    [JsonPropertyName("tagId")]
    public string? TagId { get; set; }
    
    [JsonPropertyName("assignments")]
    public List<Assignment> Assignments { get; set; } = [];
}
```

### **BulkTagAssignments.cs**
```csharp
public class BulkTagAssignments
{
    [JsonPropertyName("tags")]
    public List<BulkTagAssignment> Tags { get; set; } = [];
}
```

### **BulkTagResponse.cs**
```csharp
public class BulkTagResponse
{
    [JsonPropertyName("tags")]
    public List<Tag> Tags { get; set; } = [];
    
    [JsonPropertyName("errors")]
    public List<TagBulkCreateError> Errors { get; set; } = [];
}
```

### **TagBulkCreateError.cs**
```csharp
public class TagBulkCreateError
{
    [JsonPropertyName("tag")]
    public Dictionary<string, TagInfo>? Tag { get; set; }
    
    [JsonPropertyName("responseCode")]
    public int? ResponseCode { get; set; }
    
    [JsonPropertyName("message")]
    public string? Message { get; set; }
}
```

---

## ?? Interface Design

### **ITags.cs** (Public Interface)
```csharp
public interface ITags
{
    /// <summary>
    /// Retrieves a list of tags in the specified account group.
    /// </summary>
    /// <param name="aid">Optional account group ID</param>
    /// <param name="expand">Optional expand options (assignments)</param>
    /// <param name="cancellationToken">Cancellation token</param>
    Task<Tags> GetAllAsync(string? aid, string[]? expand, CancellationToken cancellationToken);
    
    /// <summary>
    /// Creates a new tag.
    /// </summary>
    /// <param name="request">Tag information</param>
    /// <param name="aid">Optional account group ID</param>
    /// <param name="cancellationToken">Cancellation token</param>
    Task<TagInfo> CreateAsync(TagInfo request, string? aid, CancellationToken cancellationToken);
    
    /// <summary>
    /// Creates multiple tags in bulk.
    /// </summary>
    /// <param name="request">Bulk tag response with tags to create</param>
    /// <param name="aid">Optional account group ID</param>
    /// <param name="cancellationToken">Cancellation token</param>
    Task<BulkTagResponse> CreateBulkAsync(BulkTagResponse request, string? aid, CancellationToken cancellationToken);
    
    /// <summary>
    /// Retrieves a tag using its ID.
    /// </summary>
    /// <param name="id">Tag ID</param>
    /// <param name="aid">Optional account group ID</param>
    /// <param name="expand">Optional expand options (assignments)</param>
    /// <param name="cancellationToken">Cancellation token</param>
    Task<Tag> GetByIdAsync(string id, string? aid, string[]? expand, CancellationToken cancellationToken);
    
    /// <summary>
    /// Updates a tag.
    /// </summary>
    /// <param name="id">Tag ID</param>
    /// <param name="request">Updated tag information</param>
    /// <param name="aid">Optional account group ID</param>
    /// <param name="cancellationToken">Cancellation token</param>
    Task<TagInfo> UpdateAsync(string id, TagInfo request, string? aid, CancellationToken cancellationToken);
    
    /// <summary>
    /// Deletes a tag.
    /// </summary>
    /// <param name="id">Tag ID</param>
    /// <param name="aid">Optional account group ID</param>
    /// <param name="cancellationToken">Cancellation token</param>
    Task DeleteAsync(string id, string? aid, CancellationToken cancellationToken);
    
    /// <summary>
    /// Assigns a tag to one or more objects. Cumulative behavior - previous assignments persist.
    /// </summary>
    /// <param name="id">Tag ID</param>
    /// <param name="request">Assignment details</param>
    /// <param name="aid">Optional account group ID</param>
    /// <param name="cancellationToken">Cancellation token</param>
    Task<BulkTagAssignment> AssignAsync(string id, TagAssignment request, string? aid, CancellationToken cancellationToken);
    
    /// <summary>
    /// Removes a tag from one or more objects.
    /// </summary>
    /// <param name="id">Tag ID</param>
    /// <param name="request">Assignment details</param>
    /// <param name="aid">Optional account group ID</param>
    /// <param name="cancellationToken">Cancellation token</param>
    Task UnassignAsync(string id, TagAssignment request, string? aid, CancellationToken cancellationToken);
    
    /// <summary>
    /// Assigns multiple tags to multiple objects in bulk. Cumulative behavior.
    /// </summary>
    /// <param name="request">Bulk assignments</param>
    /// <param name="aid">Optional account group ID</param>
    /// <param name="cancellationToken">Cancellation token</param>
    Task<BulkTagAssignments> AssignBulkAsync(BulkTagAssignments request, string? aid, CancellationToken cancellationToken);
    
    /// <summary>
    /// Removes multiple tags from multiple objects in bulk.
    /// </summary>
    /// <param name="request">Bulk assignments</param>
    /// <param name="aid">Optional account group ID</param>
    /// <param name="cancellationToken">Cancellation token</param>
    Task<BulkTagAssignments> UnassignBulkAsync(BulkTagAssignments request, string? aid, CancellationToken cancellationToken);
}
```

### **ITagsRefitApi.cs** (Internal Refit Interface)
```csharp
internal interface ITagsRefitApi
{
    [Get("/tags")]
    Task<Tags> GetAllAsync([Query] string? aid, [Query(CollectionFormat.Multi)] string[]? expand, CancellationToken cancellationToken);
    
    [Post("/tags")]
    Task<TagInfo> CreateAsync([Body] TagInfo request, [Query] string? aid, CancellationToken cancellationToken);
    
    [Post("/tags/bulk")]
    Task<BulkTagResponse> CreateBulkAsync([Body] BulkTagResponse request, [Query] string? aid, CancellationToken cancellationToken);
    
    [Get("/tags/{id}")]
    Task<Tag> GetByIdAsync(string id, [Query] string? aid, [Query(CollectionFormat.Multi)] string[]? expand, CancellationToken cancellationToken);
    
    [Put("/tags/{id}")]
    Task<TagInfo> UpdateAsync(string id, [Body] TagInfo request, [Query] string? aid, CancellationToken cancellationToken);
    
    [Delete("/tags/{id}")]
    Task DeleteAsync(string id, [Query] string? aid, CancellationToken cancellationToken);
    
    [Post("/tags/{id}/assign")]
    Task<BulkTagAssignment> AssignAsync(string id, [Body] TagAssignment request, [Query] string? aid, CancellationToken cancellationToken);
    
    [Post("/tags/{id}/unassign")]
    Task UnassignAsync(string id, [Body] TagAssignment request, [Query] string? aid, CancellationToken cancellationToken);
    
    [Post("/tags/assign")]
    Task<BulkTagAssignments> AssignBulkAsync([Body] BulkTagAssignments request, [Query] string? aid, CancellationToken cancellationToken);
    
    [Post("/tags/unassign")]
    Task<BulkTagAssignments> UnassignBulkAsync([Body] BulkTagAssignments request, [Query] string? aid, CancellationToken cancellationToken);
}
```

---

## ?? Integration Tests (8-10 Tests)

### **Test Coverage Plan**
```csharp
[Collection("Integration Tests")]
public class TagsModuleTests
{
    // CRUD Operations (6 tests)
    1. GetTags_WithValidRequest_ReturnsTags
    2. CreateTag_WithValidRequest_CreatesTag
    3. CreateTags_Bulk_CreatesMultipleTags
    4. GetTag_WithValidId_ReturnsTag
    5. UpdateTag_WithValidRequest_UpdatesTag
    6. DeleteTag_WithValidId_DeletesTag
    
    // Assignment Operations (4 tests)
    7. AssignTag_ToTest_AssignsTag
    8. UnassignTag_FromTest_RemovesTag
    9. AssignTags_Bulk_AssignsMultipleTags
    10. UnassignTags_Bulk_RemovesMultipleTags
}
```

### **Test Implementation Notes**
- **Create test tags**: Use descriptive names like "Test Tag - {timestamp}"
- **Cleanup**: Delete all test tags in cleanup phase
- **Assignment testing**: May require test creation for assignment validation
- **Bulk operations**: Verify partial success handling
- **Expand parameter**: Test both with and without `expand=assignments`

---

## ?? Implementation Steps

### **Step 1: Models** (10-12 files)
1. Create `TagInfo.cs` - Base model
2. Create `Tag.cs` - Full model with _links
3. Create `Tags.cs` - List wrapper
4. Create `Assignment.cs` - Assignment details
5. Create `TagAssignment.cs` - Single assignment request
6. Create `BulkTagAssignment.cs` - Bulk assignment item
7. Create `BulkTagAssignments.cs` - Bulk assignments wrapper
8. Create `BulkTagResponse.cs` - Bulk create response
9. Create `TagBulkCreateError.cs` - Bulk error details
10. Create `ObjectType.cs` - Enum
11. Create `AccessType.cs` - Enum
12. Create `AssignmentType.cs` - Enum
13. Create `ExpandTagsOptions.cs` - Enum

### **Step 2: Interfaces** (2 files)
14. Create `ITags.cs` - Public interface
15. Create `ITagsRefitApi.cs` - Internal Refit interface

### **Step 3: Implementation** (1 file)
16. Create `TagsImpl.cs` - Implementation with primary constructor

### **Step 4: Module** (1 file)
17. Create `TagsModule.cs` - Module class with all operations

### **Step 5: Client Integration** (2 files)
18. Update `IThousandEyesClient.cs` - Add ITags property
19. Update `ThousandEyesClient.cs` - Implement property

### **Step 6: Tests** (1 file)
20. Create `TagsModuleTests.cs` - 8-10 integration tests

### **Step 7: Validation**
21. Run `get_errors` on all new files
22. Fix any compilation errors
23. Run `run_build` to validate entire solution
24. Verify zero errors, warnings, messages

---

## ? Expected Outcomes

### **Code Quality**
- ? Zero compilation errors
- ? Zero warnings
- ? Zero messages
- ? All files follow "one file per type" pattern
- ? Modern .NET 9 patterns (primary constructors, collection expressions, file-scoped namespaces)
- ? Comprehensive XML documentation

### **Functionality**
- ? Full CRUD operations for tags
- ? Bulk create operations
- ? Tag assignment/unassignment operations
- ? Bulk assignment operations
- ? Account group context support
- ? Optional expand parameter support
- ? Clean separation of models

### **Testing**
- ? 8-10 integration tests
- ? 100% test success rate target
- ? Real tag creation and deletion
- ? Assignment validation
- ? Bulk operation validation
- ? Clean test data management

---

## ?? Key Design Decisions

### **1. Model Inheritance**
- `TagInfo` as base class (no _links)
- `Tag` inherits from `TagInfo` (adds _links from ApiResource)
- `Tags` inherits from `ApiResource` (list wrapper)

### **2. Assignment Models**
- Separate models for single and bulk assignments
- `Assignment` for individual assignment details
- `BulkTagAssignment` for bulk operations with tagId

### **3. Bulk Operations**
- `BulkTagResponse` for bulk create with error handling
- `BulkTagAssignments` for bulk assign/unassign operations
- Error tracking in bulk responses

### **4. Expand Parameter**
- Optional `string[]? expand` parameter
- Use `[Query(CollectionFormat.Multi)]` in Refit
- Supports `expand=assignments` to include assignment details

---

## ?? Learning Points

### **Bulk Operations Pattern**
Unlike simple CRUD APIs, Tags API includes bulk operations:
- Bulk create with error handling
- Bulk assign/unassign operations
- 207 Multi-Status responses

### **Assignment System**
- Cumulative assignment behavior
- Assignment to different object types
- Bulk assignment management

### **Optional Expand**
- Query parameter arrays in Refit
- CollectionFormat.Multi for proper serialization
- Optional expansion of related data

---

## ? Implementation Checklist

- [ ] Create 10-12 model files (TagInfo, Tag, Tags, assignments, bulk models, enums)
- [ ] Create 2 interface files (ITags, ITagsRefitApi)
- [ ] Create 1 implementation file (TagsImpl)
- [ ] Create 1 module file (TagsModule)
- [ ] Update 2 client files (ThousandEyesClient, IThousandEyesClient)
- [ ] Create 1 test file (TagsModuleTests with 8-10 tests)
- [ ] Validate all files with get_errors
- [ ] Run full build with run_build
- [ ] Verify zero errors/warnings/messages
- [ ] Mark Phase 6.1 complete

---

## ?? Success Criteria

1. ? **15-17 files created** following "one file per type" pattern
2. ? **10 API operations implemented** with full CRUD and assignments
3. ? **Zero build errors** across entire solution
4. ? **Zero build warnings** across entire solution
5. ? **8-10 integration tests** ready for validation
6. ? **Model inheritance** using ApiResource base class
7. ? **Modern .NET 9 patterns** maintained
8. ? **Comprehensive XML documentation**
9. ? **Zero technical debt**
10. ? **100% test success rate** target

---

**Ready to implement Phase 6.1! ??**

**Estimated Time**: 2-2.5 hours
**Complexity**: Medium (more complex than Credentials, simpler than Integrations)
**Dependencies**: None (self-contained module)
