# LogPulse

LogPulse is a lightweight, cross-platform **CLI log analyzer** written in C#. It parses structured log files, summarizes log entries by severity and service, and provides easy-to-read output for quick insights. Designed for real-world usage, it includes unit-tested core functionality and configurable top-N service reporting.

---

## Features

- Parses pipe-delimited log files with timestamp, log level, service, and message fields.
- Summarizes logs by:
  - Log Level (`Debug`, `Info`, `Warn`, `Error`, `Critical`)
  - Service
- Provides overall summary including:
  - Total entries
  - Earliest and latest log timestamps
  - Top-N services by log count
- Fully tested with **unit tests** for parser, analyzer, and output formatting.
- CLI-friendly output using a neat table-like display.

---

## Log Format

LogPulse expects logs in the following **pipe-delimited format**:


**Example:**

- 2025-12-13T08:00:00|INFO|AuthService|User logged in
- 2025-12-13T08:05:00|WARN|OrderService|Order delayed
- 2025-12-13T08:10:00|ERROR|AuthService|Login failed

---

## Getting Started

### Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0) installed
- Linux, Windows, or macOS

---

### Build & Run

1. **Clone the repository**

```bash
git clone https://github.com/dlewis89/LogPulse.git
cd LogPulse
dotnet run --project LogPulse -- <path-to-log-file>
```
## License

This project is licensed under the MIT License.
