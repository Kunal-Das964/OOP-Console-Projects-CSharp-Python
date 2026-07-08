using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Account
    {
        public string AccountNumber { get; private set; }
        public double Balance { get; protected set; }

        public Account(string accountNumber, double balance=0)
        {
            AccountNumber = accountNumber;
            Balance = balance;
        }

        public virtual void Deposit(double amount)
        {
            if(amount < 0)
            {
                throw new InvalidAmountException("Deposit amount must be positive");
            }
            Balance += amount;
        }

        public virtual void Withdraw(double amount)
        {
            if(amount < 0)
            {
                throw new InvalidAmountException("withdrawal amount must be positive");
            }
            if(amount > Balance)
            {
                throw new InsufficientFundsException("Insufficient funds for this withdraw");
            }
            Balance -= amount;
        }

        public void ShowBalance()
        {
            Console.WriteLine($"Account {AccountNumber} | Balance: {Balance}");
        }

        public virtual void ApplyMonthlyUpdate()
        {
            Console.WriteLine($"Account {AccountNumber}: no monthly update defined");
        }
    }
}
