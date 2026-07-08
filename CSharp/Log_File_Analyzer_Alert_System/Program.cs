using Models;
using Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Log_File_Analyzer_Alert_System
{
    public class Program
    {
        private static string GetDataPath(string name)
        {
            string dataFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", "Data");
            return Path.Combine(dataFolder, name);
        }
        public static void Main(string[] args)
        {
            string appLogFile = GetDataPath("app.log");
            string errorsOnlyFile = GetDataPath("errors_only.log");

            List<LogEntry> entries = LogLoader.LoadLogEntries(appLogFile);
            Console.WriteLine($"Loaded {entries.Count} valid log entries.");

            Console.WriteLine();
            var counts = LevelCounter.CountLogLevels(entries);
            LevelCounter.PrintLevelCounts(counts);

            var errorEntries = ErrorExtractor.ExtractErrorEntries(entries);
            ErrorExtractor.WriteErrorsToFile(errorEntries, errorsOnlyFile);
            Console.WriteLine($"\nWrote {errorEntries.Count} ERROR entries to {errorsOnlyFile}.");

            Console.WriteLine();
            var alerts = SlidingWindowChecker.CheckSlidingWindowAlerts(entries);
            SlidingWindowChecker.PrintSlidingWindowAlerts(alerts);

            Console.WriteLine();
            WordFrequencyAnalyzer.PrintMostFrequentErrorWord(entries);
        }
    }
}
