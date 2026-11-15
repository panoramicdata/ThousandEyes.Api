using System.Reflection;
using System.Runtime.CompilerServices;
using AwesomeAssertions;
using ThousandEyes.Api.Interfaces;

namespace ThousandEyes.Api.Test.UnitTests;

/// <summary>
/// Every public API wrapper must hand each call to the matching Refit method with the same arguments and return its result
/// </summary>
public class WrapperForwardingTests
{
	private static readonly Type[] CustomMappedWrappers = [typeof(UserEventsApi), typeof(AccountGroupsApi)];

	public static TheoryData<Type> Wrappers => [.. Discover().Select(w => w.Wrapper)];

	[Fact]
	public void Discovery_FindsEveryWrapper()
	{
		_ = Discover().Count().Should().Be(35);
	}

	[Theory]
	[MemberData(nameof(Wrappers))]
	public async Task EveryMethod_ForwardsArgumentsAndReturnsTheRefitResult(Type wrapperType)
	{
		var (_, apiType, refitType) = Discover().Single(w => w.Wrapper == wrapperType);
		var proxy = RecordingProxy.Create(refitType);
		var wrapper = Activator.CreateInstance(wrapperType, proxy)!;
		var checkedMethods = 0;

		foreach (var method in apiType.GetMethods().Where(m => !CustomMappedWrappers.Contains(wrapperType)))
		{
			var parameters = method.GetParameters();
			var args = parameters.Select(p => ValueFactory.Make(p.ParameterType, p.Name!)).ToArray();
			var refitMethod = refitType.GetMethod(method.Name, [.. parameters.Select(p => p.ParameterType)]);
			_ = refitMethod.Should().NotBeNull($"{wrapperType.Name}.{method.Name} should map to a Refit method with the same parameters");

			var task = (Task)method.Invoke(wrapper, args)!;
			await task;

			var call = ((RecordingProxy)(object)proxy).Calls.Last();
			_ = call.Name.Should().Be(method.Name);
			_ = call.Args.Should().Equal(args, $"{wrapperType.Name}.{method.Name} must pass its arguments through unchanged");
			if (call.Returned is not null)
			{
				var result = task.GetType().GetProperty("Result")!.GetValue(task);
				_ = result.Should().BeSameAs(call.Returned, $"{wrapperType.Name}.{method.Name} must return the Refit result");
			}

			checkedMethods++;
		}

		_ = checkedMethods.Should().Be(CustomMappedWrappers.Contains(wrapperType) ? 0 : apiType.GetMethods().Length);
	}

	private static IEnumerable<(Type Wrapper, Type Api, Type Refit)> Discover() =>
		typeof(ThousandEyesClient).Assembly.GetTypes()
			.Where(t => t is { IsClass: true, IsAbstract: false })
			.Select(t => (Wrapper: t, Parameters: t.GetConstructors() is [var ctor] ? ctor.GetParameters() : null))
			.Where(x => x.Parameters is [{ ParameterType: { IsInterface: true, Name: var name } }] && name.EndsWith("RefitApi", StringComparison.Ordinal))
			.Select(x => (x.Wrapper, Api: x.Wrapper.GetInterfaces().Single(i => i.IsPublic && i.Namespace!.StartsWith("ThousandEyes.Api.Interfaces", StringComparison.Ordinal)), Refit: x.Parameters![0].ParameterType));

	[Fact]
	public async Task UserEvents_WindowQuery_PassesWindowAndNoDates()
	{
		var proxy = RecordingProxy.Create(typeof(IUserEventsRefitApi));
		var sut = new UserEventsApi((IUserEventsRefitApi)(object)proxy);
		var token = new CancellationToken(true);

		_ = await sut.GetAllAsync("acc", true, "1d", "cur", token);

		var call = ((RecordingProxy)(object)proxy).Calls.Single();
		_ = call.Args.Should().Equal("acc", true, "1d", null, null, "cur", token);
	}

	[Fact]
	public async Task UserEvents_DateRangeQuery_PassesIsoDatesAndNoWindow()
	{
		var proxy = RecordingProxy.Create(typeof(IUserEventsRefitApi));
		var sut = new UserEventsApi((IUserEventsRefitApi)(object)proxy);
		var token = new CancellationToken(true);

		_ = await sut.GetAllAsync(new DateTime(2024, 1, 2, 3, 4, 5, DateTimeKind.Utc), new DateTime(2024, 2, 3, 4, 5, 6, DateTimeKind.Utc), "acc", false, "cur", token);

		var call = ((RecordingProxy)(object)proxy).Calls.Single();
		_ = call.Args.Should().Equal("acc", false, null, "2024-01-02T03:04:05Z", "2024-02-03T04:05:06Z", "cur", token);
	}
}

/// <summary>
/// Builds a distinguishable value for any parameter type without needing a usable constructor
/// </summary>
internal static class ValueFactory
{
	public static object? Make(Type type, string name)
	{
		var underlying = Nullable.GetUnderlyingType(type);
		if (underlying is not null)
		{
			return Make(underlying, name);
		}

		return type switch
		{
			_ when type == typeof(string) => name,
			_ when type == typeof(bool) => true,
			_ when type == typeof(int) => name.Length,
			_ when type == typeof(long) => name.Length,
			_ when type == typeof(DateTime) => new DateTime(2024, 1, 2, 3, 4, 5, DateTimeKind.Utc),
			_ when type == typeof(CancellationToken) => new CancellationToken(true),
			_ when type.IsInterface => null,
			_ when type.IsEnum => Enum.GetValues(type).GetValue(0),
			_ when type.IsArray => MakeArray(type.GetElementType()!, name),
			_ when type.IsValueType => Activator.CreateInstance(type),
			_ => RuntimeHelpers.GetUninitializedObject(type)
		};
	}

	private static Array MakeArray(Type element, string name)
	{
		var array = Array.CreateInstance(element, 1);
		array.SetValue(Make(element, name), 0);
		return array;
	}
}

/// <summary>
/// Stands in for a Refit interface, recording each call and answering with a fresh value of the declared type
/// </summary>
public class RecordingProxy : DispatchProxy
{
	public List<RecordedCall> Calls { get; } = [];

	public static object Create(Type refitInterface)
	{
		var create = typeof(DispatchProxy).GetMethods()
			.Single(m => m.Name == nameof(DispatchProxy.Create) && m.GetGenericArguments().Length == 2);
		return create.MakeGenericMethod(refitInterface, typeof(RecordingProxy)).Invoke(null, null)!;
	}

	protected override object? Invoke(MethodInfo? targetMethod, object?[]? args)
	{
		var returnType = targetMethod!.ReturnType;
		if (!returnType.IsGenericType)
		{
			Calls.Add(new RecordedCall(targetMethod.Name, args ?? [], null));
			return Task.CompletedTask;
		}

		var value = ValueFactory.Make(returnType.GetGenericArguments()[0], targetMethod.Name);
		Calls.Add(new RecordedCall(targetMethod.Name, args ?? [], value));
		return typeof(Task).GetMethod(nameof(Task.FromResult))!.MakeGenericMethod(returnType.GetGenericArguments()[0]).Invoke(null, [value]);
	}
}

public sealed record RecordedCall(string Name, object?[] Args, object? Returned);
