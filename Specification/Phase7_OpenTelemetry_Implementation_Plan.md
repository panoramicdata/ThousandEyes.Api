# Phase 7: ThousandEyes for OpenTelemetry API - Implementation Plan

## ?? **Overview**

Phase 7 implements the **ThousandEyes for OpenTelemetry API**, which enables machine-to-machine integration for exporting ThousandEyes telemetry data in OpenTelemetry (OTel) format to observability platforms like Splunk, Grafana, Honeycomb, and any OTel-compatible client.

### **Business Value**
- **Unified Observability**: Export ThousandEyes data to existing observability platforms
- **Industry Standard**: Leverage OpenTelemetry format for wide compatibility
- **Flexible Integration**: Support for both metrics and traces
- **Multiple Backends**: GRPC and HTTP endpoint types with Splunk HEC support

### **API Complexity**: Medium
- **Endpoints**: 5 operations (CRUD + filtering)
- **Special Features**: OpenTelemetry signal types, data model versions, exporter configurations
- **Estimated Files**: 20-25 files
- **Estimated Tests**: 6-8 integration tests
- **Estimated Time**: 3-4 hours

---

## ?? **API Endpoints (5 Operations)**

### **Base URL**: `https://api.thousandeyes.com/v7/streams`

```
GET    /streams           # List all data streams
POST   /streams           # Create new data stream
GET    /streams/{id}      # Get data stream by ID
PUT    /streams/{id}      # Update data stream
DELETE /streams/{id}      # Delete data stream
```

### **Query Parameters**
- `aid`: Account group ID (optional, all endpoints)
- `type`: Filter by stream type (optional, GET endpoints)

---

## ?? **Data Models (15-18 files)**

### **Core Models** (5 files)
1. `Stream.cs` - Complete stream configuration
2. `StreamResponse.cs` - Stream with ID and links
3. `GetStreamResponse.cs` - Full stream response with audit and status
4. `CreateStreamResponse.cs` - Create response with audit
5. `StreamCollection.cs` - Collection of streams

### **Enums** (4 files)
6. `StreamType.cs` - opentelemetry, splunk-hec
7. `Signal.cs` - metric, trace
8. `EndpointType.cs` - grpc, http
9. `DataModelVersion.cs` - v1, v2

### **Configuration Models** (5 files)
10. `TagMatch.cs` - Tag filtering configuration
11. `TestMatch.cs` - Test filtering configuration
12. `Filters.cs` - Test type filters
13. `ExporterConfig.cs` - Exporter configuration
14. `ExporterConfigSplunkHec.cs` - Splunk HEC specific config

### **Supporting Models** (4 files)
15. `StreamStatus.cs` - Stream connection status
16. `StreamStatusType.cs` - connected, pending, failing
17. `TestMatchDomain.cs` - cea, endpoint
18. `TestType.cs` - Test type enum (reuse from existing if possible)

### **Request Models** (1 file)
19. `PutStream.cs` - Update stream request

---

## ??? **Implementation Structure**

### **Phase 7.1: Core Models and Enums** (45-60 minutes)
**Files**: 9 files
- Core enums (StreamType, Signal, EndpointType, DataModelVersion)
- Basic models (StreamType, StreamResponse, StreamCollection)
- Status models (StreamStatus, StreamStatusType)

**Success Criteria**:
- ? All enums with proper JSON serialization
- ? Base models with required properties
- ? ApiResource inheritance for collection
- ? Zero compilation errors

### **Phase 7.2: Configuration Models** (45-60 minutes)
**Files**: 9 files
- Filter models (TagMatch, TestMatch, Filters)
- Exporter configuration (ExporterConfig, ExporterConfigSplunkHec)
- Complete stream model with all properties
- Request/response models (PutStream, GetStreamResponse, CreateStreamResponse)

**Key Challenges**:
- Complex nested structures
- Multiple configuration options
- Conditional validation (Splunk HEC requires http endpoint)

**Success Criteria**:
- ? All configuration models created
- ? Proper property naming and JSON serialization
- ? Support for custom headers
- ? Zero compilation errors

### **Phase 7.3: API Implementation** (45-60 minutes)
**Files**: 4 files
- `IStreamsApi.cs` - Public interface
- `IStreamsRefitApi.cs` - Internal Refit interface
- `StreamsApi.cs` - Implementation
- `StreamsModule.cs` - Module wrapper

**Implementation**:
```csharp
public interface IStreamsApi
{
    Task<StreamCollection> GetAllAsync(string? aid, StreamType? type, CancellationToken cancellationToken);
    Task<CreateStreamResponse> CreateAsync(Stream request, string? aid, CancellationToken cancellationToken);
    Task<GetStreamResponse> GetByIdAsync(string id, string? aid, StreamType? type, CancellationToken cancellationToken);
    Task<GetStreamResponse> UpdateAsync(string id, PutStream request, string? aid, CancellationToken cancellationToken);
    Task DeleteAsync(string id, string? aid, CancellationToken cancellationToken);
}
```

**Success Criteria**:
- ? All 5 endpoints implemented
- ? Proper parameter handling
- ? Return type correctness
- ? Zero compilation errors

### **Phase 7.4: Client Integration** (15-30 minutes)
**Files**: 2 files (updated)
- `IThousandEyesClient.cs` - Add OpenTelemetry property
- `ThousandEyesClient.cs` - Initialize OpenTelemetry module

**Integration**:
```csharp
/// <summary>
/// Gets the OpenTelemetry module for data streaming configuration
/// </summary>
public OpenTelemetryModule OpenTelemetry { get; private set; }
```

**Success Criteria**:
- ? Module properly integrated
- ? Client initialization correct
- ? Build successful

### **Phase 7.5: Integration Tests** (45-60 minutes)
**Files**: 1 file
- `OpenTelemetryIntegrationTest.cs` - 6-8 comprehensive tests

**Test Coverage**:
1. `GetStreams_WithValidRequest_ReturnsStreams` - List all streams
2. `GetStreams_WithTypeFilter_ReturnsFilteredStreams` - Filter by type
3. `CreateStream_OpenTelemetryGrpc_CreatesStream` - Create OpenTelemetry stream
4. `CreateStream_SplunkHec_CreatesStream` - Create Splunk HEC stream
5. `GetStream_WithValidId_ReturnsStreamDetails` - Get specific stream
6. `UpdateStream_WithValidRequest_UpdatesStream` - Update stream configuration
7. `DeleteStream_WithValidId_DeletesStream` - Delete stream with cleanup
8. `GetStreamStatus_AfterCreation_ValidatesStatus` - Verify stream status

**Test Strategy**:
- Create streams with cleanup in finally blocks
- Test both OpenTelemetry and Splunk HEC types
- Validate all configuration options
- Test status transitions (pending ? connected/failing)

**Success Criteria**:
- ? 6-8 comprehensive tests
- ? All CRUD operations tested
- ? Both stream types validated
- ? Proper cleanup implementation

---

## ?? **Key Features**

### **1. OpenTelemetry Signal Support** ??
```csharp
var stream = new Stream
{
    Type = StreamType.Opentelemetry,
    Signal = Signal.Metric,  // or Signal.Trace
    EndpointType = EndpointType.Grpc,
    StreamEndpointUrl = "https://otel-collector.example.com",
    DataModelVersion = DataModelVersion.V2
};
```

**Constraints**:
- `Signal.Trace` requires `DataModelVersion.V2`
- `Signal.Metric` with `DataModelVersion.V1` cannot use trace

### **2. Splunk HEC Integration** ??
```csharp
var splunkStream = new Stream
{
    Type = StreamType.SplunkHec,
    Signal = Signal.Metric,
    EndpointType = EndpointType.Http,  // Required for Splunk HEC
    StreamEndpointUrl = "https://splunk.example.com:8088/services/collector",
    ExporterConfig = new ExporterConfig
    {
        SplunkHec = new ExporterConfigSplunkHec
        {
            Token = "your-splunk-hec-token",
            Source = "ThousandEyesOTel",
            SourceType = "ThousandEyesOTel",
            Index = "thousandeyes_metrics"
        }
    }
};
```

**Constraints**:
- Splunk HEC requires `EndpointType.Http`
- Token is required
- Source, SourceType, and Index are optional

### **3. Tag-Based Filtering** ???
```csharp
var stream = new Stream
{
    // ...stream configuration
    TagMatch = new TagMatch[]
    {
        new() { Key = "environment", Value = "production" },
        new() { Key = "region", Value = "us-east" }
    }
};
```

**Requirements**:
- Tag keys must follow [OpenTelemetry attribute naming conventions](https://opentelemetry.io/docs/specs/semconv/general/naming/#recommendations-for-application-developers)
- Invalid characters will cause validation errors

### **4. Test-Based Filtering** ??
```csharp
var stream = new Stream
{
    // ...stream configuration
    TestMatch = new TestMatch[]
    {
        new() { Id = "12345", Domain = TestMatchDomain.Cea },      // Cloud/Enterprise Agent test
        new() { Id = "67890", Domain = TestMatchDomain.Endpoint }   // Endpoint Agent test
    },
    Filters = new Filters
    {
        TestTypes = new FiltersTestTypes
        {
            Values = new[] 
            { 
                TestType.HttpServer, 
                TestType.AgentToServer,
                TestType.Bgp 
            }
        }
    }
};
```

### **5. Stream Status Monitoring** ??
```csharp
var stream = await client.OpenTelemetry.Streams.GetByIdAsync(streamId, null, null, cancellationToken);

Console.WriteLine($"Status: {stream.StreamStatus.Status}");
Console.WriteLine($"Last Success: {stream.StreamStatus.LastSuccess}");
Console.WriteLine($"Last Failure: {stream.StreamStatus.LastFailure}");

// Status values:
// - connected: Data successfully reaching endpoint
// - pending: No data currently being sent
// - failing: Data being sent but not reaching endpoint
```

### **6. Custom Headers** ??
```csharp
var stream = new Stream
{
    // ...stream configuration
    CustomHeaders = new Dictionary<string, string>
    {
        ["Authorization"] = "Bearer your-token",
        ["X-Custom-Header"] = "custom-value"
    }
};
```

---

## ?? **Important Constraints**

### **URL Validation**
- Must be syntactically correct
- Must be reachable
- Must use HTTPS protocol
- GRPC endpoints cannot contain paths (e.g., `https://example.com` ?, `https://example.com/collector` ?)
- HTTP endpoints must include full path (e.g., `https://example.com/collector` ?)

### **Stream Limits**
- Maximum **10 data streams per account group**
- Exceeding limit returns 412 Precondition Failed

### **Data Model Compatibility**
| Signal | Data Model V1 | Data Model V2 |
|--------|---------------|---------------|
| Metric | ? Supported | ? Supported |
| Trace | ? Not Supported | ? Supported |

### **Endpoint Type Requirements**
| Stream Type | Allowed Endpoint Types |
|-------------|----------------------|
| opentelemetry | grpc, http |
| splunk-hec | http (required) |

---

## ?? **Testing Strategy**

### **Unit Testing Approach**
- Test model serialization/deserialization
- Validate enum conversions
- Test required property enforcement

### **Integration Testing Approach**
1. **Setup**: Create test streams with unique configurations
2. **Execution**: Test all CRUD operations
3. **Validation**: Verify responses and status
4. **Cleanup**: Delete created streams in finally blocks

### **Test Data**
```csharp
// OpenTelemetry GRPC stream
var otelGrpcStream = new Stream
{
    Type = StreamType.Opentelemetry,
    Signal = Signal.Metric,
    EndpointType = EndpointType.Grpc,
    StreamEndpointUrl = "https://otel-collector.test.com",
    DataModelVersion = DataModelVersion.V2,
    Enabled = true
};

// Splunk HEC stream
var splunkStream = new Stream
{
    Type = StreamType.SplunkHec,
    Signal = Signal.Metric,
    EndpointType = EndpointType.Http,
    StreamEndpointUrl = "https://splunk.test.com:8088/services/collector",
    DataModelVersion = DataModelVersion.V2,
    Enabled = true,
    ExporterConfig = new ExporterConfig
    {
        SplunkHec = new ExporterConfigSplunkHec
        {
            Token = "test-token-12345",
            Source = "ThousandEyesOTel",
            Index = "test_index"
        }
    }
};
```

---

## ?? **Estimated Metrics**

| Metric | Estimate |
|--------|----------|
| **Total Files** | 20-25 files |
| **Models** | 15-18 files |
| **Interfaces** | 2 files |
| **Implementation** | 2 files |
| **Tests** | 1 file (6-8 tests) |
| **Implementation Time** | 3-4 hours |
| **Lines of Code** | ~1,200 lines |

---

## ? **Success Criteria**

### **Code Quality**
- ? Zero build warnings
- ? Zero compilation errors
- ? All files follow "one file per type" pattern
- ? Modern .NET 9 patterns (primary constructors, collection expressions, required properties)
- ? Comprehensive XML documentation

### **Functionality**
- ? All 5 API endpoints implemented
- ? Support for both OpenTelemetry and Splunk HEC stream types
- ? Support for both metric and trace signals
- ? Tag-based and test-based filtering
- ? Stream status monitoring
- ? Custom headers support
- ? Data model version support (v1 and v2)

### **Testing**
- ? 6-8 comprehensive integration tests
- ? All CRUD operations tested
- ? Both stream types validated
- ? Status monitoring tested
- ? Proper cleanup implementation

### **Architecture**
- ? Clean separation of concerns
- ? Consistent with existing module patterns
- ? Proper error handling
- ? ApiResource inheritance where appropriate

---

## ?? **Implementation Checklist**

### **Phase 7.1: Core Models** ?? 45-60 min
- [ ] Create `StreamType.cs` enum
- [ ] Create `Signal.cs` enum
- [ ] Create `EndpointType.cs` enum
- [ ] Create `DataModelVersion.cs` enum
- [ ] Create `StreamStatusType.cs` enum
- [ ] Create `StreamStatus.cs` model
- [ ] Create `StreamResponse.cs` base model
- [ ] Create `StreamCollection.cs` collection
- [ ] Create `TestMatchDomain.cs` enum
- [ ] Build and validate

### **Phase 7.2: Configuration Models** ?? 45-60 min
- [ ] Create `TagMatch.cs` model
- [ ] Create `TestMatch.cs` model
- [ ] Create `Filters.cs` model
- [ ] Create `FiltersTestTypes.cs` model
- [ ] Create `ExporterConfig.cs` model
- [ ] Create `ExporterConfigSplunkHec.cs` model
- [ ] Create `PutStream.cs` request model
- [ ] Create `Stream.cs` complete model
- [ ] Create `GetStreamResponse.cs` response model
- [ ] Create `CreateStreamResponse.cs` response model
- [ ] Build and validate

### **Phase 7.3: API Implementation** ?? 45-60 min
- [ ] Create `IStreamsApi.cs` public interface
- [ ] Create `IStreamsRefitApi.cs` internal interface
- [ ] Create `StreamsApi.cs` implementation
- [ ] Create `OpenTelemetryModule.cs` module
- [ ] Build and validate

### **Phase 7.4: Client Integration** ?? 15-30 min
- [ ] Update `IThousandEyesClient.cs` with OpenTelemetry property
- [ ] Update `ThousandEyesClient.cs` to initialize OpenTelemetry module
- [ ] Build and validate

### **Phase 7.5: Integration Tests** ?? 45-60 min
- [ ] Create `OpenTelemetryIntegrationTest.cs`
- [ ] Implement list streams test
- [ ] Implement filter streams test
- [ ] Implement create OpenTelemetry stream test
- [ ] Implement create Splunk HEC stream test
- [ ] Implement get stream test
- [ ] Implement update stream test
- [ ] Implement delete stream test with cleanup
- [ ] Implement stream status validation test
- [ ] Run all tests and validate

### **Phase 7.6: Documentation** ?? 15-30 min
- [ ] Update implementation plan
- [ ] Create Phase 7 completion document
- [ ] Update README with OpenTelemetry examples
- [ ] Commit and push changes

---

## ?? **Getting Started**

To begin Phase 7 implementation:

1. **Review this plan** thoroughly
2. **Start with Phase 7.1** (Core Models)
3. **Follow the checklist** sequentially
4. **Build after each phase** to catch errors early
5. **Write tests** as you complete implementation
6. **Document** as you go

---

## ?? **Reference Links**

- [ThousandEyes for OpenTelemetry Documentation](https://docs.thousandeyes.com/product-documentation/integration-guides/opentelemetry)
- [OpenTelemetry Specification](https://opentelemetry.io/docs/specs/)
- [OpenTelemetry Attribute Naming Conventions](https://opentelemetry.io/docs/specs/semconv/general/naming/#recommendations-for-application-developers)
- [Splunk HEC Documentation](https://docs.splunk.com/Documentation/Splunk/latest/Data/UsetheHTTPEventCollector)

---

**Ready to implement Phase 7 and reach 100% project completion! ??**