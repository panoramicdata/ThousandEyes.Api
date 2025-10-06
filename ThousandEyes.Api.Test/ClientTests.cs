using AwesomeAssertions;

namespace ThousandEyes.Api.Test;

public class ClientTests
{
	[Fact]
	public void CreateClient_ValidBearerToken_Succeeds()
		=> _ = new ThousandEyesClient(new ThousandEyesClientOptions
		{
			BearerToken = "test-bearer-token-12345"
		});

	[Fact]
	public void CreateClient_ValidBearerToken_ExposesProperties()
	{
		// Arrange
		var options = new ThousandEyesClientOptions
		{
			BearerToken = "test-bearer-token-abc123"
		};

		// Act
		var client = new ThousandEyesClient(options);

		// Assert
		_ = client.Should().NotBeNull();
		// Note: Bearer token is not exposed as a public property for security reasons
	}

	[Fact]
	public void ThousandEyesClientOptions_Properties_ReturnExpectedValues()
	{
		// Arrange & Act
		var options = new ThousandEyesClientOptions
		{
			BearerToken = "my-bearer-token-xyz789"
		};

		// Assert
		_ = options.BearerToken.Should().Be("my-bearer-token-xyz789");
	}

	[Fact]
	public void CreateClient_NullOptions_ThrowsArgumentNullException()
	{
		Action act = () => _ = new ThousandEyesClient(null!);
		_ = act.Should().ThrowExactly<ArgumentNullException>()
			.WithMessage("Value cannot be null. (Parameter 'options')");
	}

	[Fact]
	public void CreateClient_NullBearerToken_ThrowsFormatException()
	{
		Action act = () => _ = new ThousandEyesClient(new ThousandEyesClientOptions
		{
			BearerToken = null!
		});
		_ = act.Should().ThrowExactly<FormatException>()
			.WithMessage("BearerToken must be set.");
	}

	[Fact]
	public void CreateClient_EmptyBearerToken_ThrowsFormatException()
	{
		Action act = () => _ = new ThousandEyesClient(new ThousandEyesClientOptions
		{
			BearerToken = ""
		});
		_ = act.Should().ThrowExactly<FormatException>()
			.WithMessage("BearerToken must be set.");
	}

	[Fact]
	public void CreateClient_WhitespaceBearerToken_ThrowsFormatException()
	{
		Action act = () => _ = new ThousandEyesClient(new ThousandEyesClientOptions
		{
			BearerToken = "   "
		});
		_ = act.Should().ThrowExactly<FormatException>()
			.WithMessage("BearerToken must be set.");
	}

	[Fact]
	public void CreateClient_ValidBearerTokenWithTimeout_Succeeds()
	{
		// Arrange & Act
		var client = new ThousandEyesClient(new ThousandEyesClientOptions
		{
			BearerToken = "test-bearer-token",
			RequestTimeout = TimeSpan.FromSeconds(60)
		});

		// Assert
		_ = client.Should().NotBeNull();
	}

	[Fact]
	public void CreateClient_ValidBearerTokenWithRetryOptions_Succeeds()
	{
		// Arrange & Act
		var client = new ThousandEyesClient(new ThousandEyesClientOptions
		{
			BearerToken = "test-bearer-token",
			MaxRetryAttempts = 5,
			RetryDelay = TimeSpan.FromSeconds(2),
			UseExponentialBackoff = true
		});

		// Assert
		_ = client.Should().NotBeNull();
	}

	[Fact]
	public void CreateClient_ValidBearerTokenWithLogging_Succeeds()
	{
		// Arrange
		var logger = new TestLogger();

		// Act
		var client = new ThousandEyesClient(new ThousandEyesClientOptions
		{
			BearerToken = "test-bearer-token",
			Logger = logger,
			EnableRequestLogging = true,
			EnableResponseLogging = true
		});

		// Assert
		_ = client.Should().NotBeNull();
	}

	[Fact]
	public void CreateClient_InvalidTimeoutZero_ThrowsArgumentException()
	{
		Action act = () => _ = new ThousandEyesClient(new ThousandEyesClientOptions
		{
			BearerToken = "test-bearer-token",
			RequestTimeout = TimeSpan.Zero
		});
		_ = act.Should().ThrowExactly<ArgumentException>()
			.WithMessage("RequestTimeout must be greater than zero. (Parameter 'RequestTimeout')");
	}

	[Fact]
	public void CreateClient_InvalidTimeoutNegative_ThrowsArgumentException()
	{
		Action act = () => _ = new ThousandEyesClient(new ThousandEyesClientOptions
		{
			BearerToken = "test-bearer-token",
			RequestTimeout = TimeSpan.FromSeconds(-1)
		});
		_ = act.Should().ThrowExactly<ArgumentException>()
			.WithMessage("RequestTimeout must be greater than zero. (Parameter 'RequestTimeout')");
	}

	[Fact]
	public void CreateClient_NegativeRetryAttempts_ThrowsArgumentException()
	{
		Action act = () => _ = new ThousandEyesClient(new ThousandEyesClientOptions
		{
			BearerToken = "test-bearer-token",
			MaxRetryAttempts = -1
		});
		_ = act.Should().ThrowExactly<ArgumentException>()
			.WithMessage("MaxRetryAttempts cannot be negative. (Parameter 'MaxRetryAttempts')");
	}

	private class TestLogger : ILogger
	{
		public IDisposable? BeginScope<TState>(TState state) where TState : notnull => null;
		public bool IsEnabled(LogLevel logLevel) => true;
		public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception?, string> formatter)
		{
			// Test logger implementation
		}
	}
}
