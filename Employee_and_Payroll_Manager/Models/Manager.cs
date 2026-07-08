using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Manager : Employee
    {
        public int teamSize { get; private set; }
        public double bonusPercent { get; private set; }

        public Manager(string name, string id, double baseSalary, string department, int teamSize, double bonusPercent):
            base(name, id, baseSalary, department)
        {
            this.teamSize = teamSize;
            this.bonusPercent = bonusPercent;
        }

        public override double CalculateSalary()
        {
            return base.baseSalary + bonusPercent;
        }

        public override void DisplayInfo()
        {
            Console.WriteLine($"ID: {id}\nName: {name}\nDepartment: {department}\nTeam size: {teamSize}\nSalary (with bonus): {CalculateSalary()}");
        }

        public override string GeneratePayslipText()
        {
            string baseText = base.GeneratePayslipText();
            double bonusAmount = baseSalary * bonusPercent;

            var extraLines = new List<string>();
            extraLines.Add($"Team Size: {teamSize}");
            extraLines.Add($"Bonus Percent: {bonusPercent * 100:F1}%");
            extraLines.Add($"Bonus Amount: {bonusAmount}");

            return baseText + "\n" + string.Join("\n", extraLines);
        }
    }
}
