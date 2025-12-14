namespace LogPulse.Models;

public record LogEntry (DateTimeOffset Timestamp, LogLevel Level, string Service, string Message);