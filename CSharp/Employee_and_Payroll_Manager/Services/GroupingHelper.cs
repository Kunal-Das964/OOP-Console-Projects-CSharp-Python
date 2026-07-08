using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public static class GroupingHelper
    {
        public static Dictionary<string, double> AverageSalaryByDepartment(List<Employee> employees)
        {
            Dictionary<string, (double Total, int Count)> departmentData = new Dictionary<string, (double Total, int Count)>();

            foreach(Employee employee in employees)
            {
                string department = employee.department;
                double salary = employee.CalculateSalary();

                if (!departmentData.ContainsKey(department))
                {
                    departmentData[department] = (salary, 1);
                }
                else
                {
                    var existing = departmentData[department];
                    departmentData[department] = (existing.Total + salary, existing.Count + 1);
                }
            }

            Dictionary<string, double> averages = new Dictionary<string, double>();
            foreach(var entry in departmentData)
            {
                averages[entry.Key] = entry.Value.Total / entry.Value.Count;
            }
            return averages;
        }

        public static void PrintDepartmentAverages(Dictionary<string, double> averages)
        {
            Console.WriteLine("--- Department-wise Average Salary ---");
            foreach(var entry in averages)
            {
                Console.WriteLine($"{entry.Key}: {entry.Value:F2}");
            }
        }
    }
}
