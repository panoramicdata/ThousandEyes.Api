using AwesomeAssertions;

namespace ThousandEyes.Api.Test.Infrastructure;

public class AuthenticationTests
{
	[Fact]
	public void ThousandEyesClientOptions_WithValidBearerToken_ValidatesSuccessfully()
	{
		// Arrange
		var options = new ThousandEyesClientOptions
		{
			BearerToken = "test-bearer-token-12345"
		};

		// Act & Assert - Should not throw
		options.Validate();
	}

	[Fact]
	public void ThousandEyesClientOptions_WithNullBearerToken_ThrowsFormatException()
	{
		// Arrange
		var options = new ThousandEyesClientOptions
		{
			BearerToken = null!
		};

		// Act & Assert
		var act = options.Validate;
		_ = act.Should().Throw<FormatException>()
			.WithMessage("BearerToken must be set.");
	}

	[Fact]
	public void ThousandEyesClientOptions_WithEmptyBearerToken_ThrowsFormatException()
	{
		// Arrange
		var options = new ThousandEyesClientOptions
		{
			BearerToken = ""
		};

		// Act & Assert
		var act = options.Validate;
		_ = act.Should().Throw<FormatException>()
			.WithMessage("BearerToken must be set.");
	}

	[Fact]
	public void ThousandEyesClientOptions_WithWhitespaceBearerToken_ThrowsFormatException()
	{
		// Arrange
		var options = new ThousandEyesClientOptions
		{
			BearerToken = "   "
		};

		// Act & Assert
		var act = options.Validate;
		_ = act.Should().Throw<FormatException>()
			.WithMessage("BearerToken must be set.");
	}

	[Fact]
	public void ThousandEyesClientOptions_WithValidBearerToken_ShouldValidateOptions()
	{
		// Arrange
		var logger = new TestLogger();
		var options = new ThousandEyesClientOptions
		{
			BearerToken = "test-bearer-token-abc123",
			Logger = logger
		};

		// Act & Assert - Verify the options are valid
		options.Validate();
		_ = options.Should().NotBeNull();
		_ = options.BearerToken.Should().Be("test-bearer-token-abc123");
	}

	[Fact]
	public void ThousandEyesClientOptions_WithTimeout_ShouldRespectTimeout()
	{
		// Arrange
		var options = new ThousandEyesClientOptions
		{
			BearerToken = "test-bearer-token",
			RequestTimeout = TimeSpan.FromSeconds(60)
		};

		// Act & Assert
		options.Validate();
		_ = options.RequestTimeout.Should().Be(TimeSpan.FromSeconds(60));
	}

	[Fact]
	public void ThousandEyesClientOptions_WithInvalidTimeout_ThrowsArgumentException()
	{
		// Arrange
		var options = new ThousandEyesClientOptions
		{
			BearerToken = "test-bearer-token",
			RequestTimeout = TimeSpan.Zero
		};

		// Act & Assert
		var act = options.Validate;
		_ = act.Should().Throw<ArgumentException>()
			.WithMessage("RequestTimeout must be greater than zero. (Parameter 'RequestTimeout')");
	}

	[Fact]
	public void ThousandEyesClientOptions_WithNegativeRetryAttempts_ThrowsArgumentException()
	{
		// Arrange
		var options = new ThousandEyesClientOptions
		{
			BearerToken = "test-bearer-token",
			MaxRetryAttempts = -1
		};

		// Act & Assert
		var act = options.Validate;
		_ = act.Should().Throw<ArgumentException>()
			.WithMessage("MaxRetryAttempts cannot be negative. (Parameter 'MaxRetryAttempts')");
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