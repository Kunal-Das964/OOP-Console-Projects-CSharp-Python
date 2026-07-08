using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class SavingAccount : Account
    {
        public double InterestRate { get; private set; }

        public SavingAccount(string accountNumber, double initialBalance, double interestRate):
            base(accountNumber, initialBalance)
        {
            InterestRate = interestRate;
        }

        public void ApplyInterest()
        {
            double interest = Balance * InterestRate;
            Balance += interest;
            Console.WriteLine($"Account {AccountNumber}: interest of {interest} applied.");
        }

        public override void ApplyMonthlyUpdate()
        {
            ApplyInterest();
        }
    }
}
