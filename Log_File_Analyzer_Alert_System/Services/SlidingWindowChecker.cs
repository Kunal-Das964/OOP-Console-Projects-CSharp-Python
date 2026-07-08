using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public static class SlidingWindowChecker
    {
        public static List<(int Start, int ErrorCount)> CheckSlidingWindowAlerts(
        List<LogEntry> entries, int windowSize = 5, int errorThreshold = 3)
        {
            int n = entries.Count;
            List<(int, int)> alerts = new List<(int, int)>();

            if (n < windowSize)
            {
                return alerts; 
            }

            for (int start = 0; start <= n - windowSize; start++)
            {
                int errorCount = 0;
                for (int i = start; i < start + windowSize; i++)
                {
                    if (entries[i].Level == "ERROR")
                    {
                        errorCount++;
                    }
                }

                if (errorCount >= errorThreshold)
                {
                    alerts.Add((start, errorCount));
                }
            }

            return alerts;
        }

        public static void PrintSlidingWindowAlerts(List<(int Start, int ErrorCount)> alerts)
        {
            if (alerts.Count == 0)
            {
                Console.WriteLine("No sliding window alerts triggered.");
                return;
            }
            Console.WriteLine("--- Sliding Window Alerts ---");
            foreach (var alert in alerts)
            {
                Console.WriteLine($"ALERT: {alert.ErrorCount} ERROR entries found in window starting at line {alert.Start}.");
            }
        }
    }
}
