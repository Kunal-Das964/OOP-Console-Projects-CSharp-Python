using Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class TransactionRecord
    {
        public string AccountNumber { get; set; }
        public string TransactionType { get; set; }
        public string Amount { get; set; }
        public string Timestamp { get; set; }
    }
    public static class TransactionStorage
    {
        // Line format: account_number,transaction_type,amount,timestamp

        public static void AppendTransaction(Transaction transaction, string filepath)
        {
            Directory.CreateDirectory(Path.GetDirectoryName(filepath));
            using (StreamWriter writer = new StreamWriter(filepath, true))
            {
                string line = $"{transaction.AccountNumber}, {transaction.TransactionType}, {transaction.Amount}, {transaction.Timestamp}";
                writer.WriteLine(line);
            }
        }

        public static List<TransactionRecord> LoadTransactions(string filepath)
        {
            List<TransactionRecord> records = new List<TransactionRecord>();
            if (!File.Exists(filepath))
            {
                return records;
            }

            foreach(string rawLine in File.ReadAllLines(filepath))
            {
                string line = rawLine.Trim();
                if (string.IsNullOrEmpty(line))
                {
                    continue;
                }
                string[] parts = line.Split(',');
                if(parts.Length != 4)
                {
                    continue;
                }
                records.Add(new TransactionRecord
                {
                    AccountNumber = parts[0],
                    TransactionType = parts[1],
                    Amount = parts[2],
                    Timestamp = parts[3]
                });
            }
            return records;
        }

        public static void PrintStatement(List<TransactionRecord> records, string accountNumber = null)
        {
            Console.WriteLine("--- Statement ---");
            foreach (TransactionRecord r in records)
            {
                if(accountNumber != null && r.AccountNumber != accountNumber)
                {
                    continue;
                }
                Console.WriteLine($"[{r.Timestamp}] {r.AccountNumber} | {r.TransactionType} | {r.Amount}");
            }
        }
    }
}
