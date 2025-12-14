namespace LogPulse.Models;

public class LogSummary
{
    public int TotalEntries { get; init; }

    public Dictionary<LogLevel, int> EntriesByLevel { get; init; }= new();

    public Dictionary<string, int> EntriesByService { get; init; }= new();

    public DateTimeOffset? EarliestTimestamp { get; init; }
    public DateTimeOffset? LatestTimestamp { get; init; }
}