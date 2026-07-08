using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public static class LevelCounter
    {
        public static Dictionary<string, int> CountLogLevels(List<LogEntry> entries)
        {
            Dictionary<string, int> counts = new Dictionary<string, int>();

            foreach (LogEntry entry in entries)
            {
                string level = entry.Level;

                if (!counts.ContainsKey(level))
                {
                    counts[level] = 1;
                }
                else
                {
                    counts[level] += 1;
                }
            }

            return counts;
        }

        public static void PrintLevelCounts(Dictionary<string, int> counts)
        {
            Console.WriteLine("--- Log Level Frequency ---");
            foreach (var entry in counts)
            {
                Console.WriteLine($"{entry.Key}: {entry.Value}");
            }
        }
    }
}
