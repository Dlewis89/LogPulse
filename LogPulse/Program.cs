using LogPulse.Models;
using LogPulse.Services;
using LogPulse.Output;

class Program
{
    static void Main(string[] args)
    {
        if (args.Length == 0)
        {
            Console.WriteLine("Usage: logpulse <logfile>");
            return;
        }

        var filePath = args[0];

        if (!File.Exists(filePath))
        {
            Console.WriteLine($"File not found: {filePath}");
            return;
        }

        var parser = new LogParser();
        var logEntries = new List<LogEntry>();

        foreach (var line in File.ReadLines(filePath))
        {
            var entry = parser.Parse(line);
            if (entry != null)
            {
                logEntries.Add(entry);
            }
        }

        var analyzer = new LogAnalyzer(logEntries);
        var summary = analyzer.GetOverallSummary();

        var formatter = new SummaryFormatter();
        var output = formatter.Format(summary);

        Console.WriteLine(output);
    }
}
