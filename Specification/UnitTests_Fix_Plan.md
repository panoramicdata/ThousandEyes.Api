# Unit Tests Error Fixes - Progress Tracking

## Error Categories

### 1. Namespace vs Type Conflicts ?
- `UsersApiTests.cs` - 'Users' is a namespace
- `AgentsApiTests.cs` - 'Agents' is a namespace  
- `AlertsApiTests.cs` - 'Alerts' is a namespace
- `EndpointAgentsApiTests.cs` - 'EndpointAgents' is a namespace
- `TemplatesApiTests.cs` - 'Templates' is a namespace
- `TestsApiTests.cs` - 'Tests' is a namespace

**Solution**: Add fully qualified type name or use type alias

### 2. Missing/Renamed Properties
- `Role.RoleName` ? Check actual property name
- `AccountGroup` type not found
- `Dashboard.DashboardName` ? Should be `Title`
- `DashboardSnapshotsPage.Snapshots` ? Check actual property
- `DashboardFilters.FiltersList` ? Check actual property
- `UserAgent.UserAgentId/UserAgentValue` ? Check actual properties
- `EndpointAgent.AgentId/AgentName` ? Check actual properties
- `EmulatedDevice` response properties
- `DetectedEvent.EventId` ? Should be `Id`
- `Outage.OutageId` ? Should be `Id`
- `TemplateResponseBase.Name` ? Check inheritance
- `HttpServerTests.TestsList` ? Check actual property
- `TestVersionHistoryResponse.Versions` ? Check actual property
- `TestVersionHistory.Version` ? Check actual property

### 3. Required Properties Not Set
Multiple tests instantiate models without setting required properties

### 4. Method Signature Mismatches
- `EmulatedDevicesApi.GetAllAsync` parameter count
- `EmulatedDevicesApi.GetByIdAsync/UpdateAsync/DeleteAsync` methods missing
- `HttpServerTestsApi.GetByIdAsync` missing `expand` parameter
- `HttpServerTestsApi.CreateAsync/UpdateAsync` missing cancellation token

### 5. Assertion Method Issues
- `StringCollectionAssertions.Be` doesn't exist
- `GenericCollectionAssertions<Dashboard>.Be` doesn't exist

### 6. Ambiguous References
- `Monitor` ambiguous between `ThousandEyes.Api.Models.BgpMonitors.Monitor` and `System.Threading.Monitor`

## Fix Strategy

1. ? Fix OpenTelemetry Integration Test (CreateStreamResponse.Id)
2. Fix namespace conflicts with type aliases
3. Fix property name mismatches
4. Add required properties to test objects
5. Fix method signatures
6. Fix assertion methods
7. Fix ambiguous references

## Execution Plan

### Phase 1: Namespace Conflicts (6 files)
- [ ] UsersApiTests.cs
- [ ] AgentsApiTests.cs
- [ ] AlertsApiTests.cs
- [ ] EndpointAgentsApiTests.cs
- [ ] TemplatesApiTests.cs
- [ ] TestsApiTests.cs

### Phase 2: Property Issues (Multiple files)
- [ ] Check and fix model properties
- [ ] Update test objects

### Phase 3: Required Properties
- [ ] Add required properties to all test instantiations

### Phase 4: Method Signatures
- [ ] Fix EmulatedDevicesApi tests
- [ ] Fix HttpServerTestsApi tests

### Phase 5: Assertions
- [ ] Fix assertion method calls

### Phase 6: Ambiguous References
- [ ] BgpMonitorsImplTests.cs

## Progress

**Total Errors**: ~150+ errors
**Fixed**: 13 OpenTelemetry errors
**Remaining**: ~137+ errors

