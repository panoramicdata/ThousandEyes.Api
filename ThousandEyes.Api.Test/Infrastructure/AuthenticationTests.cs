using AwesomeAssertions;

namespace ThousandEyes.Api.Test.Infrastructure;

public class AuthenticationTests
{
	[Fact]
	public void ThousandEyesClientOptions_WithValidCredentials_ValidatesSuccessfully()
	{
		// Arrange
		var options = new ThousandEyesClientOptions
		{
			Account = "testaccount",
			ClientId = "550e8400-e29b-41d4-a716-446655440000",
			ClientSecret = "550e8400-e29b-41d4-a716-446655440000-123e4567-e89b-12d3-a456-426614174000"
		};

		// Act & Assert - Should not throw
		options.Validate();
	}

	[Fact]
	public void ThousandEyesClientOptions_WithInvalidClientId_ThrowsFormatException()
	{
		// Arrange
		var options = new ThousandEyesClientOptions
		{
			Account = "testaccount",
			ClientId = "invalid-guid",
			ClientSecret = "550e8400-e29b-41d4-a716-446655440000-123e4567-e89b-12d3-a456-426614174000"
		};

		// Act & Assert
		var act = options.Validate;
		_ = act.Should().Throw<FormatException>()
			.WithMessage("*ClientId must be a valid GUID format*");
	}

	[Fact]
	public void AuthenticationHandler_WithValidCredentials_ShouldValidateOptions()
	{
		// Arrange
		var logger = new TestLogger();
		var options = new ThousandEyesClientOptions
		{
			Account = "testaccount",
			ClientId = "550e8400-e29b-41d4-a716-446655440000",
			ClientSecret = "550e8400-e29b-41d4-a716-446655440000-123e4567-e89b-12d3-a456-426614174000",
			Logger = logger
		};

		// Act & Assert - Verify the options are valid
		options.Validate();
		_ = options.Should().NotBeNull();
		_ = options.Account.Should().Be("testaccount");
	}

	private class TestLogger : ILogger
	{
		public IDisposable? BeginScope<TState>(TState state) where TState : notnull => null;
		public bool IsEnabled(LogLevel logLevel) => true;
		public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception?, string> formatter)
		{
			// Test logger implementation - parameters are required by interface
			_ = logLevel;
			_ = eventId;
			_ = state;
			_ = exception;
			_ = formatter;
		}
	}
}