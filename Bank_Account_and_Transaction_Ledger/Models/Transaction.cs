using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Transaction
    {
        public string AccountNumber { get; private set; }
        public string TransactionType { get; private set; }
        public double Amount { get; private set; }
        public DateTime Timestamp { get; private set; }

        public Transaction(string accountNumber, string transactionType, double amount)
        {
            AccountNumber = accountNumber;
            TransactionType = transactionType;
            Amount = amount;
            Timestamp = DateTime.Now;
        }

        public void Display()
        {
            Console.WriteLine($"[{Timestamp}] {AccountNumber} | {TransactionType} | {Amount}");
        }
    }
}
