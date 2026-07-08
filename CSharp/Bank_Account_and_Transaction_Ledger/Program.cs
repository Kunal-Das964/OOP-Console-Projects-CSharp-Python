using Models;
using Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank_Account_and_Transaction_Ledger
{
    public class Program
    {
        private static string GetBankingFilePath()
        {
            string dataFolder = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", "Data"));
            return Path.Combine(dataFolder, "transactions.txt");
        }

        private static void PrintMenu()
        {

            Console.WriteLine("\n--- Bank Menu ---");
            Console.WriteLine("1. Create an account");
            Console.WriteLine("2. Deposit");
            Console.WriteLine("3. Withdraw");
            Console.WriteLine("4. Show balance");
            Console.WriteLine("5. Apply monthly update to all accounts");
            Console.WriteLine("6. View statement (all or by account)");
            Console.WriteLine("7. Sort transactions by amount");
            Console.WriteLine("8. Search transactions above an amount");
            Console.WriteLine("9. Exit");
        }

        private static void CreateAccountFlow(Ledger ledger)
        {
            Console.Write("Type (1=Account, 2=Savings, 3=Current): ");
            string accType = Console.ReadLine().Trim();

            Console.Write("Account number: ");
            string accountNumber = Console.ReadLine().Trim();

            Console.Write("Initial balance: ");
            if (!double.TryParse(Console.ReadLine().Trim(), out double initialBalance))
            {
                Console.WriteLine("Invalid balance. Must be a number.");
                return;
            }

            Account account;

            if(accType == "1")
            {
                account = new Account(accountNumber, initialBalance);
            }
            else if(accType == "2")
            {
                Console.Write("Interest rate (e.g. 0.05 for 5%): ");
                if(!double.TryParse(Console.ReadLine().Trim(), out double interestRate))
                {
                    Console.WriteLine("Invalid interest rate. Must be a number.");
                    return;
                }
                account = new SavingAccount(accountNumber, initialBalance, interestRate);
            }
            else if (accType == "3")
            {
                Console.Write("Overdraft limit: ");
                if (!double.TryParse(Console.ReadLine().Trim(), out double overdraftLimit))
                {
                    Console.WriteLine("Invalid overdraft limit. Must be a number.");
                    return;
                }
                account = new CurrentAccount(accountNumber, initialBalance, overdraftLimit);
            }
            else
            {
                Console.WriteLine("Invalid type selected.");
                return;
            }

            ledger.AddAccount(account);
        }

        private static void DepositFlow(Ledger ledger)
        {
            Console.Write("Account number: ");
            string accountNumber = Console.ReadLine().Trim();

            Console.Write("Amount to deposit: ");
            if (!double.TryParse(Console.ReadLine().Trim(), out double amount))
            {
                Console.WriteLine("Invalid amount. Must be a number.");
                return;
            }

            try
            {
                ledger.Deposit(accountNumber, amount);
                Console.WriteLine("Deposit successful.");
            }
            catch (InvalidAmountException e)
            {
                Console.WriteLine($"Deposit failed: {e.Message}");
            }
        }

        private static void WithdrawFlow(Ledger ledger)
        {
            Console.Write("Account number: ");
            string accountNumber = Console.ReadLine().Trim();

            Console.Write("Amount to withdraw: ");
            if (!double.TryParse(Console.ReadLine().Trim(), out double amount))
            {
                Console.WriteLine("Invalid amount. Must be a number.");
                return;
            }

            try
            {
                ledger.Withdraw(accountNumber, amount);
                Console.WriteLine("Withdrawal successful.");
            }
            catch (InvalidAmountException e)
            {
                Console.WriteLine($"Withdrawal failed: {e.Message}");
            }
            catch (InsufficientFundsException e)
            {
                Console.WriteLine($"Withdrawal failed: {e.Message}");
            }
        }

        private static void ShowBalanceFlow(Ledger ledger)
        {
            Console.Write("Account number: ");
            string accountNumber = Console.ReadLine().Trim();

            Account account = ledger.GetAccount(accountNumber);
            if (account == null)
            {
                Console.WriteLine($"No account found with number {accountNumber}.");
                return;
            }
            account.ShowBalance();
        }

        private static void StatementFlow(string filepath)
        {
            Console.Write("View (1=All, 2=Single account): ");
            string choice = Console.ReadLine().Trim();
            var records = TransactionStorage.LoadTransactions(filepath);

            if (choice == "1")
            {
                TransactionStorage.PrintStatement(records);
            }
            else if (choice == "2")
            {
                Console.Write("Account number: ");
                string accountNumber = Console.ReadLine().Trim();
                TransactionStorage.PrintStatement(records, accountNumber);
            }
            else
            {
                Console.WriteLine("Invalid option.");
            }
        }

        private static void SortFlow(string filepath)
        {
            Console.Write("Descending? (y/n): ");
            bool desc = Console.ReadLine().Trim().ToLower() == "y";

            var records = TransactionStorage.LoadTransactions(filepath);
            var sortedRecords = SortHelper.SortTransactionByAmount(records, desc);
            TransactionStorage.PrintStatement(sortedRecords);
        }

        private static void SearchFlow(string filepath)
        {
            Console.Write("Show transactions above amount: ");
            if (!double.TryParse(Console.ReadLine().Trim(), out double threshold))
            {
                Console.WriteLine("Invalid amount. Must be a number.");
                return;
            }

            var records = TransactionStorage.LoadTransactions(filepath);
            var results = SortHelper.SearchTransactionAbove(records, threshold);
            TransactionStorage.PrintStatement(results);
        }
        public static void Main(string[] args)
        {
            string filePath = GetBankingFilePath();
            Ledger ledger = new Ledger(filePath);

            while (true)
            {
                PrintMenu();
                Console.Write("Choose an option: ");
                string choice = Console.ReadLine().Trim();

                switch (choice)
                {
                    case "1":
                        Console.Clear();
                        CreateAccountFlow(ledger);
                        break;
                    case "2":
                        Console.Clear();
                        DepositFlow(ledger);
                        break;
                    case "3":
                        Console.Clear();
                        WithdrawFlow(ledger);
                        break;
                    case "4":
                        Console.Clear();
                        ShowBalanceFlow(ledger);
                        break;
                    case "5":
                        Console.Clear();
                        ledger.ApplyMonthlyUpdateAll();
                        break;
                    case "6":
                        Console.Clear();
                        StatementFlow(filePath);
                        break;
                    case "7":
                        Console.Clear();
                        SortFlow(filePath);
                        break;
                    case "8":
                        Console.Clear();
                        SearchFlow(filePath);
                        break;
                    case "9":
                        Console.Clear();
                        Console.WriteLine("Goodbye!");
                        return;
                    default:
                        Console.Clear();
                        Console.WriteLine("Invalid option, try again.");
                        break;
                }
            }
        }
    }
}
