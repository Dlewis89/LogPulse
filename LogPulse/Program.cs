using LogPulse.Models;
using LogPulse.Services;
using LogPulse.Output;
using System.Text;

class Program
{
    static void Main(string[] args)
    {
        if (args.Contains("-h") || args.Contains("--help"))
        {
            Console.WriteLine(HelpMenu());
            return;
        }

        if (args.Length == 0)
        {
            Console.WriteLine("Invalid number of arguments");
            Console.WriteLine(HelpMenu());
            return;
        }

        var filePath = args[0];

        if (filePath is null || !File.Exists(filePath))
        {
            Console.WriteLine($"File not found: {filePath}");
            return;
        }

        var logEntries = ReadLogEntries(filePath);

        var analyzer = new LogAnalyzer(logEntries);
        var summary = analyzer.GetOverallSummary();
        int topCount = 5; // default value

        // Check if either "-t" or "--top" is present
        int index = Array.IndexOf(args, "--top");
        if (index == -1)
        {
            index = Array.IndexOf(args, "-t");
        }

        // If a top argument was found, try to parse it
        if (index != -1)
        {
            if (index + 1 >= args.Length || !int.TryParse(args[index + 1], out topCount) || topCount <= 0)
            {
                Console.WriteLine("Invalid input for -t / --top");
                Console.WriteLine(HelpMenu());
                return;
            }
        }
        var formatter = new SummaryFormatter(topCount);
        var output = formatter.Format(summary);

        Console.WriteLine(output);
    }

    static string HelpMenu()
    {
        var menu = new StringBuilder();

        menu.AppendLine("LogPulse - CLI Log Analyzer");
        menu.AppendLine();

        menu.AppendLine("Usage:");
        menu.AppendLine("\tlogpulse <log-file> [options]");
        menu.AppendLine();

        menu.AppendLine("Options:");
        menu.AppendLine("\t -h, --help \t\t Show help information");
        menu.AppendLine("\t -t, --top <number> \t Number of top services to display (default: 5)");
        menu.AppendLine();

        menu.AppendLine("Example:");
        menu.AppendLine("\t logpulse app.log");
        menu.AppendLine("\t logpulse app.log --top 3");

        return menu.ToString();
    }

    static List<LogEntry> ReadLogEntries(string filePath)
    {
        var parser = new LogParser();
        return File.ReadLines(filePath)
                .Select(parser.Parse)
                .Where(e => e != null)
                .Cast<LogEntry>()
                .ToList();
    }
}
