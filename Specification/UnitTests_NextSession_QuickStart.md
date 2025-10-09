# ?? NEXT SESSION QUICK START

## Status: 66% Complete ? Target: 100% Complete

**Current**: ~99 errors in 7 files  
**Target**: 0 errors  
**Time**: ~70 minutes estimated

---

## ? IMMEDIATE ACTIONS

### Run This Command First:
```bash
run_build()
```
This confirms current error count and validates starting point.

---

## ?? FIX ORDER (DO IN THIS SEQUENCE)

### **BATCH 1: 15 mins** ?
Fix 3 files with simple property/required property issues:

**1. DashboardSnapshotsApiTests.cs**
```csharp
// Line 26: Change
Snapshots = [...] 
// TO:
DashboardSnapshots = [...]

// Add required properties:
new CreateDashboardSnapshotRequest { DashboardId = "123", DisplayName = "Test" }
new DashboardSnapshotResponse { SnapshotId = "123" }
new UpdateSnapshotExpirationRequest { SnapshotExpirationDate = DateTime.UtcNow.AddDays(30) }
```

**2. DashboardsApiTests.cs**
```csharp
// Lines 26, 48: Change
DashboardName = "Test"
// TO:
Title = "Test"

// Add required properties:
new Dashboard { DashboardId = "123", Title = "Test" }
new DashboardRequest { Title = "Test" }

// Line 35: Change
result.Dashboards.Should().Be(expectedResponse.Dashboards)
// TO:
result.Dashboards.Should().BeEquivalentTo(expectedResponse.Dashboards)
```

**3. TestResultsApiTests.cs**
```csharp
// Add these 5 properties to EVERY NetworkTestResult and HttpServerTestResult:
new NetworkTestResult
{
    TestId = "123",
    TestName = "Test",      // ADD
    AgentId = "agent-123",  // ADD
    AgentName = "Agent",    // ADD
    RoundId = "round-123",  // ADD
    Date = DateTime.UtcNow  // ADD
}
```

**After Batch 1**: Run `get_errors()` on these 3 files to validate

---

### **BATCH 2: 20 mins** ?
Fix HttpServerTestsApiTests.cs (biggest file):

**Step 1**: Fix collection property
```csharp
// Line 26: Change
TestsList = [...]
// TO:
Tests = [...]
```

**Step 2**: Add required properties to ALL HttpServerTest objects
```csharp
new HttpServerTest
{
    TestId = "123",        // ADD
    TestName = "Test",     // ADD
    Type = "http-server",  // ADD
    Interval = 300,        // ADD
    Url = "https://example.com"  // Already there
}
```

**Step 3**: Add required Agents property to ALL HttpServerTestRequest objects
```csharp
new HttpServerTestRequest
{
    Agents = []  // MUST ADD - always required
}
```

**Step 4**: Fix method signatures
```csharp
// GetByIdAsync - add expand parameter (4th position):
_refitApi.Setup(x => x.GetByIdAsync(testId, null, null, null, cancellationToken))
//                                                     ^^^^^ ADD THIS

// CreateAsync - add cancellationToken (last parameter):
_refitApi.Setup(x => x.CreateAsync(request, null, null, cancellationToken))
//                                                       ^^^^^^^^^^^^^^^^^^ ADD THIS

// UpdateAsync - add cancellationToken (last parameter):
_refitApi.Setup(x => x.UpdateAsync(testId, request, null, null, cancellationToken))
//                                                                ^^^^^^^^^^^^^^^^^^ ADD THIS
```

**After Batch 2**: Run `get_errors()` on this file to validate

---

### **BATCH 3: 15 mins** ??
Research and fix 2 files with unknown property names:

**1. UserAgentsApiTests.cs**
```bash
# Run this first:
get_file("ThousandEyes.Api\\Models\\Emulation\\UserAgent.cs")

# Look for actual property names, likely:
# - Id (not UserAgentId)
# - Value or UserAgent (not UserAgentValue)

# Then update test accordingly
```

**2. AccountGroupsApiTests.cs**
```bash
# Run this first:
code_search(["class AccountGroup"])

# If found, add using statement
# If not found, check if model has different name
```

**After Batch 3**: Run `get_errors()` on these 2 files to validate

---

### **BATCH 4: 20 mins** ???
Fix or rewrite EmulatedDevicesApiTests.cs:

```bash
# Run these first:
get_file("ThousandEyes.Api\\Interfaces\\IEmulatedDevicesApi.cs")
get_file("ThousandEyes.Api\\Interfaces\\IEmulatedDevicesRefitApi.cs")
get_file("ThousandEyes.Api\\Models\\Emulation\\EmulatedDevice.cs")

# Check:
# 1. Which methods actually exist in the API
# 2. Actual property names
# 3. Required properties

# Then rewrite test file to match actual API
```

**After Batch 4**: Run `get_errors()` on this file to validate

---

## ? FINAL VALIDATION (10 mins)

```bash
# Run full build
run_build()

# Should see:
# ? Build succeeded
# ? 0 errors
# ? 0 warnings

# If any errors remain:
# - Check error messages
# - Fix remaining issues
# - Re-run build
```

---

## ?? PROGRESS CHECKPOINTS

After each batch, update your status:

| Batch | Files | Est. Errors Fixed | Cumulative % |
|-------|-------|-------------------|--------------|
| Start | 7 | 0 | 66% |
| 1 | 3 | ~31 | 87% |
| 2 | 1 | ~35 | 94% |
| 3 | 2 | ~3 | 96% |
| 4 | 1 | ~30 | 100% |

---

## ?? SUCCESS CRITERIA

When you see this output from `run_build()`:
```
Build succeeded
  0 errors
  0 warnings
```

**YOU'RE DONE! ??**

Then document completion in:
- `Specification\UnitTests_Fix_Progress.md`
- Update status to "? 100% Complete"

---

## ?? PRO TIPS

1. **Don't skip validation steps** - Catch errors early
2. **Follow the order** - Easiest to hardest builds confidence
3. **Copy patterns** - Once you fix one required property, copy to all similar cases
4. **Check interfaces** - Method signatures must match exactly
5. **Use get_errors()** - Faster than full build for validation

---

## ?? IF YOU GET STUCK

**Problem**: Can't find property name
**Solution**: Use `get_file()` or `code_search()` to check actual model

**Problem**: Method signature doesn't match
**Solution**: Check interface with `get_file()` on interface file

**Problem**: Type not found
**Solution**: Use `code_search()` to find where class is defined

**Problem**: Required property - don't know what to set
**Solution**: Set any valid value (tests are unit tests, not integration tests)

---

**Start Time**: ?? _Record when you begin_  
**Expected End**: ?? _Start time + 70 minutes_

**GO! ??**
