using System;
using LogPulse.Models;

namespace LogPulse.Services;

public class LogParser
{
    public LogEntry? Parse(string log)
    {
        string[] logData = log.Split("|");

        if (logData.Length != 4) return null; 

        string timestampStr = logData[0].Trim();
        string levelStr = logData[1].Trim();
        string service = logData[2].Trim();
        string message = logData[3].Trim();

        if (!DateTimeOffset.TryParseExact(timestampStr, "yyyy-MM-ddTHH:mm:ss", null, System.Globalization.DateTimeStyles.None, out var logDateTime)) return null;
        if (!Enum.TryParse<LogLevel>(levelStr, true, out var level)) return null;
            
        return new LogEntry(
            logDateTime,
            level,
            service,
            message
        );
    }
}