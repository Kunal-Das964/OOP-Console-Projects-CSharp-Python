using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Employee
    {
        public string name { get; private set; }
        public string id { get; private set; }
        public double baseSalary { get; private set; }
        public string department { get; private set; }

        public Employee(string name, string id, double baseSalary, string department)
        {
            this.name = name;
            this.id = id;
            this.baseSalary = baseSalary;
            this.department = department;
        }

        public virtual double CalculateSalary()
        {
            return baseSalary;
        }

        public virtual void DisplayInfo()
        {
            Console.WriteLine($"ID: {id} | Name: {name} | Salary: {baseSalary} | Department: {department}");
        }

        public virtual string GeneratePayslipText()
        {
            var lines = new List<string>();
            lines.Add($"Employee ID: {id}");
            lines.Add($"Name: {name}");
            lines.Add($"Department: {department}");
            lines.Add($"Base Salary: {baseSalary}");
            lines.Add($"Final Salary: {CalculateSalary()}");
            return string.Join("\n", lines);
        }

    }
}
