using Models;
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public static class LogLoader
    {
        public static List<LogEntry> LoadLogEntries(string filepath)
        {
            List<LogEntry> entries = new List<LogEntry>();

            if (!File.Exists(filepath))
            {
                Console.WriteLine($"Log file not found: {filepath}");
                return entries;
            }

            foreach (string rawLine in File.ReadAllLines(filepath))
            {
                string line = rawLine.Trim();

                if (string.IsNullOrEmpty(line))
                {
                    continue; 
                }

                string[] parts = line.Split('|');

                if (parts.Length != 3)
                {
                    Console.WriteLine($"Skipped malformed line: {line}");
                    continue;
                }

                string timestamp = parts[0].Trim();
                string level = parts[1].Trim();
                string message = parts[2].Trim();

                LogEntry entry = new LogEntry(timestamp, level, message);
                entries.Add(entry);
            }

            return entries;
        }
    }
}
