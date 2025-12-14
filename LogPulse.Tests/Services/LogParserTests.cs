using Xunit;
using LogPulse.Services;
using LogPulse.Models;

namespace LogPulse.Tests.Services;

public class LogParserTests
{
    [Fact]
    public void Parse_ValidLogLine_ReturnsLogEntry()
    {
        // Arrange
        var parser = new LogParser();
        var logLine = "2025-12-13T14:20:00|Info|AuthService|User logged in";

        // Act
        var result = parser.Parse(logLine);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(LogLevel.Info, result.Level);
        Assert.Equal("AuthService", result.Service);
        Assert.Equal("User logged in", result.Message);
        Assert.Equal(DateTimeOffset.Parse("2025-12-13T14:20:00"), result.Timestamp);
        
    }

    [Fact]
    public void Parse_InvalidLogLine_ReturnsLogEntry()
    {
        // Arrange
        var parser = new LogParser();
        var logLine = "2025-12-13T14:20:00 Info AuthService User logged in";

        // Act
        var result = parser.Parse(logLine);

        // Assert
        Assert.Null(result);
        
    }
}