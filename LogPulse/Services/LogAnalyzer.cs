using LogPulse.Models;

namespace LogPulse.Services;

public class LogAnalyzer
{
    private readonly List<LogEntry> _logEntries;
    public LogAnalyzer(List<LogEntry> logs)
    {
        _logEntries = logs;
    }

    public Dictionary<LogLevel, int> GetSummaryByLevel()
    {
        var levelCounts = new Dictionary<LogLevel, int>();

        foreach (var entry in _logEntries)
        {
            if (levelCounts.ContainsKey(entry.Level))
            {
                levelCounts[entry.Level]++;
            }
            else
            {
                levelCounts[entry.Level] = 1;
            }
        }

        return levelCounts;
    }

    public Dictionary<string, int> GetSummaryByService()
    {
        var serviceCounts = new Dictionary<string, int>( StringComparer.OrdinalIgnoreCase);

        foreach (var entry in _logEntries)
        {
            if (serviceCounts.ContainsKey(entry.Service))
            {
                serviceCounts[entry.Service]++;
            }
            else
            {
                serviceCounts[entry.Service] = 1;
            }
        }

        return serviceCounts;
    }

    public LogSummary GetOverallSummary()
    {
        DateTimeOffset? earliest = null;
        DateTimeOffset? latest = null;

        foreach (var entry in _logEntries)
        {
            if (earliest == null || entry.Timestamp < earliest)
            {
                earliest = entry.Timestamp;
            }

            if (latest == null || entry.Timestamp > latest)
            {
                latest = entry.Timestamp;
            }
        }
    
        return new LogSummary
        {
            TotalEntries = _logEntries.Count,
            EntriesByLevel = GetSummaryByLevel(),
            EntriesByService = GetSummaryByService(),
            EarliestTimestamp = earliest,
            LatestTimestamp = latest
        };
    }

}