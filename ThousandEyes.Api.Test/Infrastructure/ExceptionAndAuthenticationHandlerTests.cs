using System.Net;
using System.Reflection;
using AwesomeAssertions;
using ThousandEyes.Api.Exceptions;
using ThousandEyes.Api.Infrastructure;
using ThousandEyes.Api.Test.UnitTests;

namespace ThousandEyes.Api.Test.Infrastructure;

public class ExceptionAndAuthenticationHandlerTests
{
	public static TheoryData<Type, int> ExceptionConstructors
	{
		get
		{
			TheoryData<Type, int> data = [];
			foreach (var type in typeof(ThousandEyesApiException).Assembly.GetTypes().Where(t => t.IsAssignableTo(typeof(ThousandEyesApiException))))
			{
				for (var i = 0; i < type.GetConstructors().Length; i++)
				{
					data.Add(type, i);
				}
			}

			return data;
		}
	}

	[Theory]
	[MemberData(nameof(ExceptionConstructors))]
	public void EveryConstructor_ExposesItsArgumentsThroughTheMatchingProperties(Type type, int index)
	{
		var constructor = type.GetConstructors()[index];
		var parameters = constructor.GetParameters();
		var args = parameters.Select(p => ValueFactory.Make(p.ParameterType, p.Name!)).ToArray();

		var exception = (ThousandEyesApiException)constructor.Invoke(args);

		for (var i = 0; i < parameters.Length; i++)
		{
			var name = parameters[i].Name!;
			if (name == "message")
			{
				_ = exception.Message.Should().Be((string)args[i]!);
			}
			else if (name == "innerException")
			{
				_ = exception.InnerException.Should().BeSameAs(args[i]);
			}
			else
			{
				var property = type.GetProperty(name, BindingFlags.Instance | BindingFlags.Public | BindingFlags.IgnoreCase);
				_ = property.Should().NotBeNull($"{type.Name} should expose {name}");
				_ = property!.GetValue(exception).Should().Be(args[i]);
			}
		}
	}

	[Theory]
	[InlineData("secret-token", "Bearer secret-token")]
	[InlineData("", null)]
	public async Task Authentication_AddsABearerHeaderOnlyWhenATokenIsConfigured(string token, string? expected)
	{
		string? seen = null;
		var inner = new StubHandler((request, _) =>
		{
			seen = request.Headers.Authorization?.ToString();
			return StubHandler.Response(HttpStatusCode.OK);
		});
		using var handler = new AuthenticationHandler(new ThousandEyesClientOptions { BearerToken = token });

		using var response = await handler.GetAsync(inner);

		_ = response.StatusCode.Should().Be(HttpStatusCode.OK);
		_ = seen.Should().Be(expected);
	}
}
