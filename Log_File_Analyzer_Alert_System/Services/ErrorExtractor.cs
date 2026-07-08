using Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public static class ErrorExtractor
    {
        public static List<LogEntry> ExtractErrorEntries(List<LogEntry> entries)
        {
            List<LogEntry> results = new List<LogEntry>();
            foreach (LogEntry entry in entries)
            {
                if (entry.Level == "ERROR")
                {
                    results.Add(entry);
                }
            }
            return results;
        }

        public static void WriteErrorsToFile(List<LogEntry> errorEntries, string filepath)
        {
            Directory.CreateDirectory(Path.GetDirectoryName(filepath));
            using (StreamWriter writer = new StreamWriter(filepath, false)) // false = overwrite
            {
                foreach (LogEntry entry in errorEntries)
                {
                    writer.WriteLine($"{entry.Timestamp} | {entry.Level} | {entry.Message}");
                }
            }
        }
    }
}
