using LogPulse.Models;
using System.Text;

namespace LogPulse.Output;

public class SummaryFormatter
{
    private readonly int _topN;

    public SummaryFormatter(int topN = 5)
    {
        _topN = topN;
    }
    public string Format(LogSummary summary)
    {
        var sb = new StringBuilder();

        sb.AppendLine("LogPulse Summary");
        sb.AppendLine(new string('-', 40));

        sb.AppendLine($"Total Entries: {summary.TotalEntries}");
        sb.AppendLine($"Time Range:    {summary.EarliestTimestamp?.ToString("yyyy-MM-dd HH:mm")} → {summary.LatestTimestamp?.ToString("yyyy-MM-dd HH:mm")}");
        sb.AppendLine();

        sb.AppendLine("By Level");
        sb.AppendLine(new string('-', 20));
        foreach (var kvp in summary.EntriesByLevel)
        {
            sb.AppendLine($"{kvp.Key,-6}: {kvp.Value}");
        }
        sb.AppendLine();

        sb.AppendLine($"Top {_topN} Services");
        sb.AppendLine(new string('-', 20));

        var topServices = summary.EntriesByService
            .OrderByDescending(kvp => kvp.Value)
            .ThenBy(kvp => kvp.Key)
            .Take(_topN);

        foreach (var kvp in topServices)
        {
            sb.AppendLine($"{kvp.Key,-20} {kvp.Value}");
        }

        return sb.ToString();
    }
}
