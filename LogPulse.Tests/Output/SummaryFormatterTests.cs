using Xunit;
using LogPulse.Models;
using LogPulse.Output;
using System;
using System.Collections.Generic;

namespace LogPulse.Tests.Output;

public class SummaryFormatterTests
{
    private LogSummary GetSampleSummary()
    {
        return new LogSummary
        {
            TotalEntries = 6,
            EarliestTimestamp = DateTimeOffset.Parse("2025-12-13T08:00:00"),
            LatestTimestamp = DateTimeOffset.Parse("2025-12-13T08:20:00"),
            EntriesByLevel = new Dictionary<LogLevel, int>
            {
                { LogLevel.Info, 3 },
                { LogLevel.Warn, 2 },
                { LogLevel.Error, 1 }
            },
            EntriesByService = new Dictionary<string, int>
            {
                { "AuthService", 3 },
                { "OrderService", 2 },
                { "InventoryService", 1 }
            }
        };
    }

    [Fact]
    public void Format_ReturnsTopNServices_Correctly()
    {
        // Arrange
        var summary = GetSampleSummary();
        var formatter = new SummaryFormatter(topN: 2);

        // Act
        string output = formatter.Format(summary);

        // Assert
        Assert.Contains("Top 2 Services", output);          // header shows correct N
        Assert.Contains("AuthService", output);            // top service included
        Assert.Contains("OrderService", output);           // second service included
        Assert.DoesNotContain("InventoryService", output); // excluded because topN=2
    }

    [Fact]
    public void Format_IncludesAllLevels()
    {
        // Arrange
        var summary = GetSampleSummary();
        var formatter = new SummaryFormatter();

        // Act
        string output = formatter.Format(summary);

        // Assert
        Assert.Contains("Info  : 3", output);
        Assert.Contains("Warn  : 2", output);
        Assert.Contains("Error : 1", output);
    }

    [Fact]
    public void Format_IncludesTimestamps()
    {
        // Arrange
        var summary = GetSampleSummary();
        var formatter = new SummaryFormatter();

        // Act
        string output = formatter.Format(summary);

        // Assert
        Assert.Contains("2025-12-13 08:00", output); // earliest
        Assert.Contains("2025-12-13 08:20", output); // latest
    }
}

