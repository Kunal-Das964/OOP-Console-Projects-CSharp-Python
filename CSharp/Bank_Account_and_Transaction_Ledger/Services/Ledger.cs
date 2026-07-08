using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class Ledger
    {
        private Dictionary<string, Account> accounts = new Dictionary<string, Account>();
        private List<Transaction> transactions = new List<Transaction>();
        private string filepath;

        public Ledger(string filepath)
        {
            this.filepath = filepath;
        }

        public bool AddAccount(Account account)
        {
            if (accounts.ContainsKey(account.AccountNumber))
            {
                Console.WriteLine($"Account {account.AccountNumber} already exists.");
                return false;
            }
            accounts[account.AccountNumber] = account;
            return true;
        }

        public void Deposit(string accountNumber, double amount)
        {
            if (!accounts.ContainsKey(accountNumber))
            {
                Console.WriteLine($"No account found with number {accountNumber}");
                return;
            }
            Account account = accounts[accountNumber];
            account.Deposit(amount);
            Transaction transaction = new Transaction(accountNumber, "deposit", amount);
            transactions.Add(transaction);
            TransactionStorage.AppendTransaction(transaction, filepath);
        }

        public void Withdraw(string accountNumber, double amount)
        {
            if (!accounts.ContainsKey(accountNumber))
            {
                Console.WriteLine($"No account found with number {accountNumber}");
                return;
            }
            Account account = accounts[accountNumber];
            account.Withdraw(amount);
            Transaction transaction = new Transaction(accountNumber, "withdraw", amount);
            transactions.Add(transaction);
            TransactionStorage.AppendTransaction(transaction, filepath);
        }

        public void ApplyMonthlyUpdateAll()
        {
            foreach(Account account in accounts.Values)
            {
                account.ApplyMonthlyUpdate();
            }
        }

        public List<Transaction> GetAllTransactions()
        {
            return transactions;
        }

        public Account GetAccount(string accountNumber)
        {
            return accounts.ContainsKey(accountNumber) ? accounts[accountNumber] : null;
        }
    }
}
