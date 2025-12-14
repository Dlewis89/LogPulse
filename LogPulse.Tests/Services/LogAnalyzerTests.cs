using Xunit;
using LogPulse.Models;
using LogPulse.Services;
using System;
using System.Collections.Generic;

namespace LogPulse.Tests.Services;

public class LogAnalyzerTests
{
    private List<LogEntry> GetSampleLogs()
    {
        return new List<LogEntry>
        {
            new LogEntry(DateTimeOffset.Parse("2025-12-13T08:00:00"), LogLevel.Info, "AuthService", "User logged in"),
            new LogEntry(DateTimeOffset.Parse("2025-12-13T08:05:00"), LogLevel.Warn, "OrderService", "Order delayed"),
            new LogEntry(DateTimeOffset.Parse("2025-12-13T08:10:00"), LogLevel.Error, "AuthService", "Login failed"),
            new LogEntry(DateTimeOffset.Parse("2025-12-13T08:15:00"), LogLevel.Info, "InventoryService", "Stock updated")
        };
    }

    [Fact]
    public void GetSummaryByLevel_ReturnsCorrectCounts()
    {
        // Arrange
        var logs = GetSampleLogs();
        var analyzer = new LogAnalyzer(logs);

        // Act
        var summary = analyzer.GetSummaryByLevel();

        // Assert
        Assert.Equal(2, summary[LogLevel.Info]);
        Assert.Equal(1, summary[LogLevel.Warn]);
        Assert.Equal(1, summary[LogLevel.Error]);
    }

    [Fact]
    public void GetSummaryByService_ReturnsCorrectCounts()
    {
        // Arrange
        var logs = GetSampleLogs();
        var analyzer = new LogAnalyzer(logs);

        // Act
        var summary = analyzer.GetSummaryByService();

        // Assert
        Assert.Equal(2, summary["AuthService"]);
        Assert.Equal(1, summary["OrderService"]);
        Assert.Equal(1, summary["InventoryService"]);
    }

    [Fact]
    public void GetOverallSummary_ReturnsCorrectSummary()
    {
        // Arrange
        var logs = GetSampleLogs();
        var analyzer = new LogAnalyzer(logs);

        // Act
        var overall = analyzer.GetOverallSummary();

        // Assert
        Assert.Equal(4, overall.TotalEntries);
        Assert.Equal(DateTimeOffset.Parse("2025-12-13T08:00:00"), overall.EarliestTimestamp);
        Assert.Equal(DateTimeOffset.Parse("2025-12-13T08:15:00"), overall.LatestTimestamp);

        // Check a couple of level counts
        Assert.Equal(2, overall.EntriesByLevel[LogLevel.Info]);
        Assert.Equal(1, overall.EntriesByLevel[LogLevel.Error]);

        // Check a couple of service counts
        Assert.Equal(2, overall.EntriesByService["AuthService"]);
    }
}
