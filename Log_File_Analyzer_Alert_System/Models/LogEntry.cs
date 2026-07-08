using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class LogEntry
    {
        public string Timestamp { get; private set; }
        public string Level { get; private set; }
        public string Message { get; private set; }

        public LogEntry(string timestamp, string level, string message)
        {
            Timestamp = timestamp;
            Level = level;
            Message = message;
        }

        public void DisplayInfo()
        {
            Console.WriteLine($"[{Timestamp}] {Level}: {Message}");
        }
    }
}
