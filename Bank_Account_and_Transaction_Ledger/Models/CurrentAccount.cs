using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class CurrentAccount : Account
    {
        public double OverdraftLimit { get; private set; }

        public CurrentAccount(string accountNumber, double initialBalance, double overdraftLimit):
            base(accountNumber, initialBalance)
        {
            OverdraftLimit = overdraftLimit;
        }

        public override void Withdraw(double amount)
        {
            if (amount <= 0)
            {
                throw new InvalidAmountException("Withdrawal amount must be positive.");
            }
            if(amount > Balance + OverdraftLimit)
            {
                throw new InsufficientFundsException("Withdrawal exceeds available balance and overdraft limit");
            }
            Balance -= amount;
        }

        public override void ApplyMonthlyUpdate()
        {
            Console.WriteLine($"Account {AccountNumber}: current account maintenace check complete.");
        }
    }
}
