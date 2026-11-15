using AwesomeAssertions;
using ThousandEyes.Api.Test.UnitTests;

namespace ThousandEyes.Api.Test;

public class ModelAndClientTests
{
	public static TheoryData<Type> ModelTypes =>
		[.. typeof(ThousandEyesClient).Assembly.GetTypes()
			.Where(t => t is { IsClass: true, IsPublic: true, IsAbstract: false, IsGenericTypeDefinition: false }
				&& t.Namespace!.StartsWith("ThousandEyes.Api.Models", StringComparison.Ordinal))];

	[Theory]
	[MemberData(nameof(ModelTypes))]
	public void EveryWritableProperty_ReadsBackWhatWasSet(Type type)
	{
		var instance = type.GetConstructor(Type.EmptyTypes) is null
			? System.Runtime.CompilerServices.RuntimeHelpers.GetUninitializedObject(type)
			: Activator.CreateInstance(type)!;

		foreach (var property in type.GetProperties().Where(p => p is { CanRead: true, CanWrite: true } && p.GetIndexParameters().Length == 0))
		{
			var value = ValueFactory.Make(property.PropertyType, property.Name);
			property.SetValue(instance, value);

			_ = property.GetValue(instance).Should().Be(value, $"{type.Name}.{property.Name}");
		}
	}

	[Fact]
	public void Client_ExposesEveryModuleAndDisposes()
	{
		using var client = new ThousandEyesClient(new ThousandEyesClientOptions { BearerToken = "token", MaxRetryAttempts = 0 });

		var modules = typeof(ThousandEyesClient).GetProperties()
			.Where(p => p.Name != nameof(ThousandEyesClient.Snapshots) && p.GetIndexParameters().Length == 0 && p.GetMethod!.IsStatic == false)
			.Select(p => p.GetValue(client))
			.ToList();

		_ = modules.Should().OnlyContain(m => m != null).And.HaveCountGreaterThan(10);
		_ = ThousandEyesClient.BaseUrl.Should().Be("https://api.thousandeyes.com/v7");
		_ = ((Func<object>)(() => client.Snapshots)).Should().Throw<NotImplementedException>();
	}
}
