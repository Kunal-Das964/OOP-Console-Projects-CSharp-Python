using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public static class WordFrequencyAnalyzer
    {
        public static Dictionary<string, int> CountWordFrequency(List<LogEntry> errorEntries)
        {
            Dictionary<string, int> wordCounts = new Dictionary<string, int>();

            foreach (LogEntry entry in errorEntries)
            {
                string[] words = entry.Message.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                foreach (string word in words)
                {
                    string cleanedWord = word.ToLower();
                    if (!wordCounts.ContainsKey(cleanedWord))
                    {
                        wordCounts[cleanedWord] = 1;
                    }
                    else
                    {
                        wordCounts[cleanedWord] += 1;
                    }
                }
            }

            return wordCounts;
        }

        public static (string Word, int Count)? GetMostFrequentWord(Dictionary<string, int> wordCounts)
        {
            if (wordCounts.Count == 0)
            {
                return null;
            }

            string bestWord = null;
            int bestCount = 0;

            foreach (var entry in wordCounts)
            {
                if (entry.Value > bestCount)
                {
                    bestCount = entry.Value;
                    bestWord = entry.Key;
                }
            }

            return (bestWord, bestCount);
        }

        public static void PrintMostFrequentErrorWord(List<LogEntry> entries)
        {
            List<LogEntry> errorEntries = ErrorExtractor.ExtractErrorEntries(entries);

            if (errorEntries.Count == 0)
            {
                Console.WriteLine("No error messages to analyze.");
                return;
            }

            Dictionary<string, int> wordCounts = CountWordFrequency(errorEntries);
            var result = GetMostFrequentWord(wordCounts);

            if (result == null)
            {
                Console.WriteLine("No error messages to analyze.");
                return;
            }

            Console.WriteLine($"Most frequent word in ERROR messages: '{result.Value.Word}' ({result.Value.Count} occurrences)");
        }
    }
}
